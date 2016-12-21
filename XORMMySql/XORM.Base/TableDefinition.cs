
using System.Collections.Generic;

namespace XORM.Base
{
    public class TableDefinition
    {
        public string ORMTableName { get; set; }

        public List<string> ORMColList { get; set; }

        public string ORMTypeName { get; set; }

        public string ORMAssemblyName { get; set; }
    }
}
