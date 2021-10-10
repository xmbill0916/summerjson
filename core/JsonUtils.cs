/*
 * Copyright(C) 2021, 2031 xmbill0916 
 *  
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using com.xmbill.json.api;
using com.xmbill.json.core.reader;
using com.xmbill.json.core.writer;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Text;

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
        public static object parse(JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            JsonParserBase.parserStart(jsonParameter);
            int ijt = JsonParserBase.getInternalJsonType(jsonParameter);
            object value = null;
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

        public static void write(StringBuilder jsonWriter, object value)
        {
            write(jsonWriter, value, null);
        }

        public static void write(StringBuilder jsonWriter, object value, IJsonObjectWriter jsonObjectWriter)
        {
            if (value == null || value is DBNull) jsonWriter.Append("null");
            else
            {
                if (value is DataSet)
                {
                    JsonWriterDBData.Write(jsonWriter, (DataSet)value, jsonObjectWriter);
                }
                else if (value is DataTable)
                {
                    JsonWriterDBData.Write(jsonWriter, (DataTable)value, jsonObjectWriter);
                }
                //else if (value is DataRow)
                //{
                //    JsonWriterDBData.Write(jsonWriter, (DataRow)value, jsonObjectWriter);
                //}
                else if ((value is IDictionary) || (value is StringDictionary) || (value is NameValueCollection))
                    JsonWriterObject.write(jsonWriter, value, jsonObjectWriter);
                else if (value is IList){
                    JsonWriterArray.Write(jsonWriter, value, jsonObjectWriter);
                }
                else if (value is Array)
                    JsonWriterArray.Write(jsonWriter, value, jsonObjectWriter);
                else
                {
                    if (!JsonWriterBase.writeValue(jsonWriter, value))
                    {
                        if (value is IJsonObjectWriter)
                            ((IJsonObjectWriter)value).ToJson(JsonWriter.newInstance(jsonWriter), value, value.GetType());
                        else if (jsonObjectWriter != null)
                            jsonObjectWriter.ToJson(JsonWriter.newInstance(jsonWriter), value, value.GetType());
                        else
                            throw new Exception(string.Format("{0} object Json serialization is not implemented," +
                                    "it can be serialized by implementing the IJsonObjectWriter interface ", value.GetType().ToString()));
                    }
                }
            }
        }
    }
}