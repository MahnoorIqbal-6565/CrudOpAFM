using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOpAFM
{
    public class ADOHandler : ICrudHandlers
    {
        public void create()
        {
     
            Console.WriteLine("Enter Your First Name: ");
            string FirstName = Console.ReadLine();

            Console.WriteLine("Enter Your Last Name: ");
            string LastName = Console.ReadLine();

            Console.WriteLine("Enter Your Class (numeric value): ");
            string classInput = Console.ReadLine();
            int Class;

            // Validate if the class input is numeric
            if (!int.TryParse(classInput, out Class))
            {
                Console.WriteLine("Invalid input! Please enter a valid numeric value for Class.");
                return;
            }

            Console.WriteLine("Enter Your School Name");
            string SchoolName = Console.ReadLine();

            Console.WriteLine("Enter Your Home Address");
            string HomeAddress = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(SchoolName) ||
                string.IsNullOrWhiteSpace(HomeAddress))
            {
                Console.WriteLine("One or more fields are empty. Record will not be inserted.");
                return;
            }

            string connectionString = "Server=DELL\\SQLEXPRESS; Database=StudentDetails; Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (FirstName, LastName, Class, SchoolName, HomeAddress) VALUES (@FirstName, @LastName, @Class, @SchoolName, @HomeAddress)", conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Class", Class);
                    cmd.Parameters.AddWithValue("@SchoolName", SchoolName);
                    cmd.Parameters.AddWithValue("@HomeAddress", HomeAddress);

                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        Console.WriteLine("Inserted Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert the record.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while connecting to the database:");
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred:");
                    Console.WriteLine(ex.Message);
                }
            }
        }


        public void update()
        {
            try
            {
                Console.WriteLine("Enter Student Id to Update: ");
                int Id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter New First Name (leave blank to skip): ");
                string firstName = Console.ReadLine();
                if (string.IsNullOrEmpty(firstName)) firstName = null;

                Console.WriteLine("Enter New Last Name (leave blank to skip): ");
                string lastName = Console.ReadLine();
                if (string.IsNullOrEmpty(lastName)) lastName = null;

                Console.WriteLine("Enter New Class (leave blank to skip): ");
                string className = Console.ReadLine();
                if (string.IsNullOrEmpty(className)) className = null;

                Console.WriteLine("Enter New School Name (leave blank to skip): ");
                string schoolName = Console.ReadLine();
                if (string.IsNullOrEmpty(schoolName)) schoolName = null;

                Console.WriteLine("Enter New Home Address (leave blank to skip): ");
                string homeAddress = Console.ReadLine();
                if (string.IsNullOrEmpty(homeAddress)) homeAddress = null;

                string connectionString = "Server=DELL\\SQLEXPRESS; Database=StudentDetails; Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE Student SET ";

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    if (firstName != null)
                    {

                        updateQuery += "FirstName = @FirstName, ";
                        parameters.Add(new SqlParameter("@FirstName", firstName));
                    }
                    if (lastName != null)
                    {
                        updateQuery += "LastName = @LastName, ";
                        parameters.Add(new SqlParameter("@LastName", lastName));
                    }
                    if (className != null)
                    {
                        updateQuery += "Class = @Class, ";
                        parameters.Add(new SqlParameter("@Class", className));
                    }
                    if (schoolName != null)
                    {
                        updateQuery += "SchoolName = @SchoolName, ";
                        parameters.Add(new SqlParameter("@SchoolName", schoolName));
                    }
                    if (homeAddress != null)
                    {
                        updateQuery += "HomeAddress = @HomeAddress, ";
                        parameters.Add(new SqlParameter("@HomeAddress", homeAddress));
                    }

                    if (updateQuery.EndsWith(", "))
                    {
                        updateQuery = updateQuery.Substring(0, updateQuery.Length - 2);
                    }

                    updateQuery += " WHERE Id = @Id";
                    parameters.Add(new SqlParameter("@Id", Id));

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        Console.WriteLine("Update Successful");
                    }
                    else
                    {
                        Console.WriteLine("No record found with the provided Id.");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter valid data.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while connecting to the database:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
        public void delete()
        {
            string connectionString = "Server=DELL\\SQLEXPRESS; Database=StudentDetails; Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;";
            try
            {
                Console.WriteLine("Enter Your Id: ");
                int id = int.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete from Student where Id=@id", conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        Console.WriteLine("Deleted Successfully");
                    }
                    else
                    {
                        Console.WriteLine("No record found with the provided Id.");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid numeric Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while connecting to the database:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
        }

        public void read()
        {
            try
            {
                string connectionString = "Server=DELL\\SQLEXPRESS; Database=StudentDetails; Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;";
                Console.WriteLine("Enter Your Id to Retrieve: ");
                int id = int.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Id = @id", conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("-------------------------------------------------------------------");
                                Console.WriteLine("Id: {0}", reader["Id"]);
                                Console.WriteLine("First Name: {0}", reader["FirstName"]);
                                Console.WriteLine("Last Name: {0}", reader["LastName"]);
                                Console.WriteLine("Class: {0}", reader["Class"]);
                                Console.WriteLine("School Name: {0}", reader["SchoolName"]);
                                Console.WriteLine("Home Address: {0}", reader["HomeAddress"]);
                                Console.WriteLine("-------------------------------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No record found with the provided Id.");
                        }
                    }
                    conn.Close();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid numeric Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while connecting to the database:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }

        }
    }

    public class ORMHandler : ICrudHandlers
    {
        public void create()
        {

            try
            {
                Console.Write("Enter first name: ");
                string FirstName = Console.ReadLine();

                Console.Write("Enter last name: ");
                string LastName = Console.ReadLine();

                Console.Write("Enter class: ");
                int Class = int.Parse(Console.ReadLine());

                Console.Write("Enter School Name: ");
                string SchoolName = Console.ReadLine();

                Console.Write("Enter Home Address : ");
                string HomeAddress = Console.ReadLine();

                if (FirstName.Length > 50 || LastName.Length > 50 || SchoolName.Length > 50 || HomeAddress.Length > 50)
                {
                    throw new Exception("Input exceeds the maximum allowed length of 50 characters.");
                }
                if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(SchoolName) ||
                string.IsNullOrWhiteSpace(HomeAddress))
                {
                    Console.WriteLine("One or more fields are empty. Record will not be inserted.");
                    return;
                }
                var student = new Student { FirstName = FirstName, LastName = LastName, Class = Class, SchoolName = SchoolName, HomeAddress = HomeAddress };

                using (var context = new StudentDetailsEntities())
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    Console.WriteLine("Student details have been inserted successfully.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter valid data.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while connecting to the database:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
    
        public void update()
        {
            using (var context = new StudentDetailsEntities())
            {
                try
                {
                    Console.Write("Enter the Student ID to update: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid Student ID! Please enter a valid numeric ID.");
                        return;
                    }

                    var student = context.Students.FirstOrDefault(p => p.Id == id);

                    if (student != null)
                    {
                        Console.WriteLine($"Current First Name: {student.FirstName}");
                        Console.Write("Enter New First Name (leave blank to keep existing): ");
                        string firstName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(firstName))
                        {
                            if (firstName.Length > 50)
                            {
                                Console.WriteLine("First Name exceeds the maximum allowed length of 50 characters.");
                                return;
                            }
                            student.FirstName = firstName;
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine($"Current Last Name: {student.LastName}");
                        Console.Write("Enter New Last Name (leave blank to keep existing): ");
                        string lastName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(lastName))
                        {
                            if (lastName.Length > 50)
                            {
                                Console.WriteLine("Last Name exceeds the maximum allowed length of 50 characters.");
                                return;
                            }
                            student.LastName = lastName;
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine($"Current Class: {student.Class}");
                        Console.Write("Enter New Class (leave blank to keep existing): ");
                        string classInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(classInput))
                        {
                            if (int.TryParse(classInput, out int newClass))
                            {
                                student.Class = newClass;
                            }
                            else
                            {
                                Console.WriteLine("Invalid class value! It must be a numeric value.");
                                return;
                            }
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine($"Current Address: {student.HomeAddress}");
                        Console.Write("Enter New Address (leave blank to keep existing): ");
                        string homeAddress = Console.ReadLine();
                        if (!string.IsNullOrEmpty(homeAddress))
                        {
                            if (homeAddress.Length > 50)
                            {
                                Console.WriteLine("Address exceeds the maximum allowed length of 50 characters.");
                                return;
                            }
                            student.HomeAddress = homeAddress;
                        }

                        context.SaveChanges();
                        Console.WriteLine("Student details have been updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while connecting to the database:");
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        
        public void delete()
        {
            using (var context = new StudentDetailsEntities())
            {
                Console.Write("Enter the Customer ID to delete: ");
                try
                {
                    int id = int.Parse(Console.ReadLine());

                    var student = context.Students.FirstOrDefault(c => c.Id == id);

                    if (student != null)
                    {
                    
                        context.Students.Remove(student);
                        context.SaveChanges();
                        Console.WriteLine("Student details have been deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student details have not found.");
                    } 
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input! Please enter a valid numeric Id.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while connecting to the database:");
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void read()
        {
            using (var context = new StudentDetailsEntities())
            {
                try
                {
                    List<Student> students = context.Students.ToList();

                    Console.Write("Enter the Student ID to Retrieve: ");

                    int id = int.Parse(Console.ReadLine());

                    var customer = context.Students.FirstOrDefault(c => c.Id == id);

                   
                    if (customer != null)
                    {
                        Console.WriteLine("\nStudent details that have been stored in the database:");
                        Console.WriteLine($"\nID: {customer.Id}\nFirstName: {customer.FirstName}\nLastName: {customer.LastName}\nCity: {customer.Class}\nSchoolName: {customer.SchoolName}\nAddress: {customer.HomeAddress}\n\n-----------------------------------");

                    }
                    else
                    {
                        Console.WriteLine("No record found with the provided Id.");
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input! Please enter a valid numeric Id.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while connecting to the database:");
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred:");
                    Console.WriteLine(ex.Message);

                }


            }

        }
    }
}
