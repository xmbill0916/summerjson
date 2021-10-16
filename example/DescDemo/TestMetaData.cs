using com.xmbill.json.api;
using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.sample.DescDemo
{
    public class TestMetaData   : IJsonObjectWriter
    {
        public int myInt { get; set; }
        public float myFloat { get; set; }
        public double myDouble { get; set; }
        public short myShort { get; set; }
        public bool myBoolean { get; set; }
        public string mystring { get; set; }
        public string string2 { get; set; }
        public byte myByte { get; set; }
        public DateTime mydate { get; set; }

        public void ToJson(JsonWriter jsonWriter, object value, Type clazz)
        {
            jsonWriter.begin(JsonType.jtObject).addKeyValue("myInt", myInt).addKeyValue("myFloat", myFloat).
                addKeyValue("myDouble", myDouble).addKeyValue("myShort", myShort).
                addKeyValue("myByte", myByte).addKeyValue("mystring", mystring).addKeyValue("date", mydate).end();
        }
    }
}
