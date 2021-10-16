using com.xmbill.json.api;
using com.xmbill.json.core;
using com.xmbill.json.core.reader;
using com.xmbill.sample.DescDemo;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.sample
{
    public class JsonPathDescExample
    {
        protected static string Str_RootIsArray = "[\n" +
            "  123456,\n" +
            "  {\n" +
            "    \"name\": \"cofax.tld\",\n" +
            "    \"desc\": \"/WEB-INF/tlds/co,,.x.tld\"\n" +
            "  },\n" +
            "  {\n" +
            "    \"name\": \"cofax.tld\",\n" +
            "    \"desc\": \"/WEB-INF/tds/cofaeee..tld\"\n" +
            "  },\n" +
            "  {\n" +
            "    \"name\": \"cofax.tld\",\n" +
            "    \"desc\": \"/WEB-INF/tlds.oooofax.tld\"\n" +
            "  },\n" +
            "  [\n" +
            "    1,\n" +
            "    2,\n" +
            "    3\n" +
            "  ]\n" +
            "]";
        public static object RootIsArrayToObject()
        {
            JsonPathDesc RootIsArrayDesc = JsonPathDesc.RootOfArray();
            object obj = Json.ToObject(Json.NewJsonParameter(Str_RootIsArray), RootIsArrayDesc);
            return obj;
        }
        public static object RootIsArrayToObjectOfCustomType()
        {
            JsonPathDesc RootIsArrayDesc = JsonPathDesc.RootOfArray();
            RootIsArrayDesc.AddArrayElement(2, ObjectNodeDesc.handler(
                (object parentObj,string key) => { return new Student(); }, 
                (object obj, JsonType jsonType, string key, object value) => {
                    switch (key)
                    {
                        case "name":
                            {
                                ((Student)obj).name = (string)value;
                                break;
                            }
                        case "desc":
                            {
                                ((Student)obj).desc = (string)value;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                
                    }));
            object obj1 = Json.ToObject(Json.NewJsonParameter(Str_RootIsArray), RootIsArrayDesc);
            return obj1;
        }

        public static object RootIsArrayToArrayTypeObject()
        {
            JsonPathDesc RootIsArrayDesc = JsonPathDesc.RootOfArray();
            RootIsArrayDesc.AddArrayElement(4, ArrayNodeDesc.handler(
                (object parentObj,string key) => { return new int[4]; },
                (object obj, JsonType jsonType, int key, object value) => {
                    ((int[])obj)[key] = (int)JsonReaderUtils.ConvertValue(jsonType,value,typeof(int));
                }));
            object obj1 = Json.ToObject(Json.NewJsonParameter(Str_RootIsArray), RootIsArrayDesc);
            return obj1;
        }

        public static object RootIsArrayToObjectOfCustomType1()
        {
            JsonPathDesc RootIsArrayDesc = JsonPathDesc.RootOfArray();
            RootIsArrayDesc.AddArrayElement(2, ObjectNodeDesc.handler(
                (object parentObj,string key) => { return new Student(); },
                (object obj, JsonType jsonType, string key, object value) => {
                    ((Student)obj).Set(key, value);
                }));
            object obj1 = Json.ToObject(Json.NewJsonParameter(Str_RootIsArray), RootIsArrayDesc);
            return obj1;
        } 

    }
}
