using com.xmbill.json.api;
using com.xmbill.json.core.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.core
{
    public class JsonUtils
    {
        /**
      * 解析入口
      *
      * @param jsonParameter
      * @param jsonValueInvoke
      * @return
      */
        public static Object parse(JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            JsonParserBase.parserStart(jsonParameter);
            int ijt = JsonParserBase.getInternalJsonType(jsonParameter);
            Object value = null;
            do
            {
                if (JsonParserBase.isObjectEnd(jsonParameter))
                    break;
                switch (ijt)
                {
                    case 1:
                        {
                            value = JsonParserObject.parseObject(jsonParameter, jsonValueInvoke);
                            break;
                        }
                    case 2:
                        {
                            value = JsonParserArray.parseArray(jsonParameter, jsonValueInvoke);
                            break;
                        }
                    case 3:
                        {
                            value = JsonParser.getStringOfString(jsonParameter);
                            break;
                        }
                    case 4:
                        {
                            value = JsonParser.getNumberOfString(jsonParameter);
                            break;
                        }
                    case 5:
                        {
                            JsonParser.getTrueValue(jsonParameter);
                            value = true;
                            break;
                        }
                    case 6:
                        {
                            JsonParser.getFalseValue(jsonParameter);
                            value = false;
                            break;
                        }
                    case 7:
                        {
                            JsonParser.getNullValue(jsonParameter);
                            value = null;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                JsonParserBase.skipObjectSpace(jsonParameter);
            } while (JsonParserBase.isNonEndAndNextChar(jsonParameter));
            return value;
        }

        public static void write(StringBuilder jsonWriter, Object value)
        {
            write(jsonWriter, value, null);
        }

        public static void write(StringBuilder jsonWriter, Object value, IJsonObjectWriter jsonObjectWriter)
        {
            if (value == null) jsonWriter.Append("null");
            else
            {
                Type clazz = value.GetType();
                //            if (Collection.class.isAssignableFrom(clazz))
                //                JsonWriterArray.writeCollection(jsonWriter, value, jsonObjectWriter);
                //            else if (Map.class.isAssignableFrom(clazz)) {
                //                JsonWriterObject.write(jsonWriter, value, jsonObjectWriter);
                //            } else if (clazz.isArray())
                //{
                //    JsonWriterArray.writeArray(jsonWriter, value, jsonObjectWriter);
                //}
                //else
                //{
                //    if (!JsonWriterBase.writeValue(jsonWriter, value, clazz))
                //    {
                //        if (IJsonObjectWriter.class.isAssignableFrom(clazz)) {
                //    ((IJsonObjectWriter)value).toJson(JsonWriter.newInstance(jsonWriter), value, clazz);
                //} else if (jsonObjectWriter != null)
                //    jsonObjectWriter.toJson(JsonWriter.newInstance(jsonWriter), value, clazz);
                //else
                //    throw new RuntimeException(String.format("%s Object Json serialization is not implemented," +
                //            "it can be serialized by implementing the IJsonObjectWriter interface ", clazz.getName()));
                //                }
                //            }
                //        }
                //    }
            }
        }
    }
}