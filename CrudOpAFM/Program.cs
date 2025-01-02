using System;

namespace CrudOpAFM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CRUD Operations on the 'Student Table'");

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("\nSelect the type of operation:");
                Console.WriteLine("1. ORM Operations");
                Console.WriteLine("2. ADO Operations");
                Console.WriteLine("3. Exit");
                Console.Write("Choice: ");

                string mainChoice = Console.ReadLine()?.Trim();

                if (mainChoice == "3" || mainChoice.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    exitProgram = true;
                    Console.WriteLine("Exiting the program. Goodbye!");
                    continue;
                }

                string operationType = mainChoice == "1" ? "orm" : mainChoice == "2" ? "ado" : null;

                if (operationType == null)
                {
                    Console.WriteLine("Invalid choice! Please select a valid option (1, 2, or 3).");
                    continue;
                }

                bool exitSubMenu = false;

                while (!exitSubMenu)
                {
                    Console.WriteLine($"\n{operationType.ToUpper()} CRUD Operations:");
                    Console.WriteLine("1. Insert");
                    Console.WriteLine("2. Read");
                    Console.WriteLine("3. Update");
                    Console.WriteLine("4. Delete");
                    Console.WriteLine("5. Go Back");
                    Console.Write("Choice: ");

                    string crudChoice = Console.ReadLine()?.ToLower();

                    if (crudChoice == "5" || crudChoice.Equals("go back", StringComparison.OrdinalIgnoreCase))
                    {
                        exitSubMenu = true;
                        Console.WriteLine($"Returning to the main menu from {operationType.ToUpper()} operations...");
                        continue;
                    }

                    ICrudHandlers handler = GetHandler(operationType, crudChoice);

                    if (handler == null)
                    {
                        Console.WriteLine("Invalid choice! Please select a valid CRUD option.");
                    }
                    else
                    {
                        switch (crudChoice)
                        {
                            case "1": handler.create(); break;
                            case "2": handler.read(); break;
                            case "3": handler.update(); break;
                            case "4": handler.delete(); break;
                            default: Console.WriteLine("Invalid CRUD operation."); break;
                        }
                    }
                }
            }
        }

        private static ICrudHandlers GetHandler(string operationType, string crudChoice)
        {
            IDataAccessFactory factory;
            if (operationType == "orm")
            {
                factory = new ORMHandlers();
            }
            else
            {
                factory = new ADOHandlers();
            }

            return factory.CreateCrudHandler(operationType, crudChoice);
        }
    }
}



