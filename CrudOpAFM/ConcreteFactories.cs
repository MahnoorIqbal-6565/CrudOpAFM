using System;

namespace CrudOpAFM
{
    
    public class ADOHandlers : IDataAccessFactory
    {
        public ICrudHandlers CreateCrudHandler(string operationType, string choiceLower)
        {
            return new ADOHandler();
        }
    }

    
    public class ORMHandlers : IDataAccessFactory
    {
        public ICrudHandlers CreateCrudHandler(string operationType, string choiceLower)
        {
            return new ORMHandler();
        }
    }
}
