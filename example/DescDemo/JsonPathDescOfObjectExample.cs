using com.xmbill.json.api;
using com.xmbill.json.core;
using com.xmbill.json.core.reader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.xmbill.sample.DescDemo
{
    public class JsonPathDescOfObjectExample
    {
        private static string s = "{\n" +
              "\n" +
              "    \"myInt\" : 1,\n" +
              "    \"myFloat\" : 1.1,\n" +
              "    \"myDouble\" : 1.2,\n" +
              "    \"myShort\" : 2,\n" +
              "    \"myBoolean\" : true,\n" +
              "    \"mystring\" : \"test\",\n" +
              "    \"string2\" : \"test \\\"I love bacon \\\", do you?\",\n" +
              "    \"myByte\" : 3 ,\n" +
              "    \"date1\" : \"2013-12-14T01:55:33.412Z\",\n" +
              "\n" +
              "    \"allType\" : {\n" +
              "        \"myInt\" : 1,\n" +
              "        \"myFloat\" : 1.1,\n" +
              "        \"myDouble\" : 1.2,\n" +
              "        \"myShort\" : 2,\n" +
              "        \"myBoolean\" : true,\n" +
              "        \"mystring\" : \"test\",\n" +
              "        \"myByte\" : 3,\n" +
              "    \"mydate\" : \"2013-12-14T01:55:33.412Z\",\n" +
              "    },\n" +
              "\n" +
              "\n" +
              "    \"allTypeList\" : [\n" +
              "        {\n" +
              "            \"myInt\" : 1,\n" +
              "            \"myFloat\" : 1.1,\n" +
              "            \"myDouble\" : 1.2,\n" +
              "            \"myShort\" : 2,\n" +
              "            \"myBoolean\" : true,\n" +
              "            \"mystring\" : \"test\",\n" +
              "            \"myByte\" : 3\n" +
              "        },\n" +
              "        {\n" +
              "            \"myInt\" : 1,\n" +
              "            \"myFloat\" : 1.1,\n" +
              "            \"myDouble\" : 1.2,\n" +
              "            \"myShort\" : 2,\n" +
              "            \"myBoolean\" : true,\n" +
              "            \"mystring\" : \"test\",\n" +
              "            \"myByte\" : 3\n" +
              "        },\n" +
              "        {\n" +
              "            \"myInt\" : 1,\n" +
              "            \"myFloat\" : 1.1,\n" +
              "            \"myDouble\" : 1.2,\n" +
              "            \"myShort\" : 2,\n" +
              "            \"myBoolean\" : true,\n" +
              "            \"mystring\" : \"test\",\n" +
              "            \"myByte\" : 3\n" +
              "        }\n" +
              "    ]\n" +
              "\n" +
              "}";
        public static object RootIsObjectToObject()
        {
            return Json.ToObject(Json.NewJsonParameter(s));
        }

        public static object RootIsObjectToDataTable()
        {
            JsonPathDesc root = JsonPathDesc.RootOfObject();
            root.AddObjectChildOfObject("allType", ObjectNodeDesc.handler(
                (object parentObj,string key) => { return new TestMetaData(); },
                (object obj, JsonType jsonType, string key, object value) => {
                    TestMetaData test = (TestMetaData)obj;
                    switch (key)
                    {
                        case "myInt":
                            {
                                test.myInt = (int)JsonReaderUtils.ConvertValue(jsonType, value, typeof(int));
                                break;
                            }
                        case "myFloat":
                            {
                                test.myFloat = (float)JsonReaderUtils.ConvertValue(jsonType, value, typeof(float));
                                break;
                            }
                        case "myDouble":
                            {
                                test.myDouble = (double)JsonReaderUtils.ConvertValue(jsonType, value, typeof(double));
                                break;
                            }
                        case "myShort":
                            {
                                test.myShort = (short)JsonReaderUtils.ConvertValue(jsonType, value, typeof(short));
                                break;
                            }
                        case "myBoolean":
                            {
                                test.myBoolean = (bool)JsonReaderUtils.ConvertValue(jsonType, value, typeof(bool));
                                break;
                            }
                        case "mystring":
                            {
                                test.mystring = (string)value;//(string)JsonReaderUtils.JsonReaderUtils.ConvertValue(jsonType,value, typeof(string));
                                break;
                            }
                        case "myByte":
                            {
                                test.myByte = (byte)JsonReaderUtils.ConvertValue(jsonType, value, typeof(byte));
                                break;
                            }
                        case "mydate":
                            {
                                test.mydate = (DateTime)JsonReaderUtils.ConvertValue(jsonType, value, typeof(DateTime));
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                })
                );
            root.AddObjectChildDataTable("allTypeList",
                (object parentObj,string key) => {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("myInt", typeof(int));
                    dataTable.Columns.Add("myFloat", typeof(float));
                    dataTable.Columns.Add("myDouble", typeof(double));
                    dataTable.Columns.Add("myShort", typeof(short));
                    dataTable.Columns.Add("myBoolean", typeof(bool));
                    dataTable.Columns.Add("mystring", typeof(string));
                    dataTable.Columns.Add("myByte", typeof(byte));
                    return dataTable;
                },
                (object obj, JsonType jsonType, string key, object value) =>
                {
                    ((DataRow)obj)[key]=value;
                }
                );
            return Json.ToObject(Json.NewJsonParameter(s), root);
        }
    }
}
