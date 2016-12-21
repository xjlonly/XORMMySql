using System.Collections.Generic;
using System.Linq;

namespace XORM.Base
{
    public class QueryResult
    {
        public Dictionary<string, object> RowResultList = new Dictionary<string, object>();

        public List<string> Tables
        {
            get
            {
                return Enumerable.ToList<string>((IEnumerable<string>)this.RowResultList.Keys);
            }
        }

        public T Get<T>()
        {
            T obj1 = default(T);
            foreach (object obj2 in this.RowResultList.Values)
            {
                if (obj2 is T)
                {
                    obj1 = (T)obj2;
                    break;
                }
            }
            return obj1;
        }

        public T Get<T>(string TabName)
        {
            T obj = default(T);
            foreach (string index in this.RowResultList.Keys)
            {
                string str = index.Substring(index.IndexOf('.') + 1);
                if (TabName == str && this.RowResultList[index] is T)
                    obj = (T)this.RowResultList[index];
            }
            return obj;
        }
    }
}
