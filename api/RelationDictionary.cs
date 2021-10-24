using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.json.api
{
    public class RelationDictionary<K,V>:IParent
    {
        protected Object parent;
        protected Dictionary<K, V> child = new Dictionary<K, V>();
        public object GetParent()
        {
            return this.parent;
        }

        public void SetParent(object parent)
        {
            this.parent = parent;
        }

        public void Set(K key, V value)
        {
            this.child.Add(key, value);
        }

        public V Get(K key)
        {
            V value;
            this.child.TryGetValue(key, out value);
                return value;
           
        }

        public Dictionary<K, V> GetChild()
        {
            return child;
        }
    }
}
