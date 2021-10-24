using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.json.api
{
    public class RelationList<T> :IParent
    {
        private object parent;
        private List<T> list= new List<T>();

        public object GetParent()
        {
           return this.parent;
        }

        public void SetParent(object parent)
        {
            this.parent = parent;
        }

        public void Add(T value)
        {
            list.Add(value);
        }

        public List<T> GetList()
        {
            return list;
        }
    }
}
