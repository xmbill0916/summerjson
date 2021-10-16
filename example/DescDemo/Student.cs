using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.sample.DescDemo
{
    public class Student
    {
        public string name { get; set; }
        public string desc { get; set; }

        public void Set(string propertyName,object value)
        {
            switch (propertyName)
            {
                case "name":
                    {
                        name = (string)value;
                        break;
                    }
                case "desc":
                    {
                        desc = (string)value;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
