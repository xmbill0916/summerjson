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
 * 
 * 397216371@QQ.com
 * https://github.com/xmbill0916/summerjson
 */
using com.xmbill.json.core;
using com.xmbill.json.core.writer;
using System.Text;
namespace com.xmbill.json.api
{
    public class JsonWriter
    {
        private StringBuilder jsonWriter;
        private JsonType jsonType;

        public static JsonWriter newInstance(StringBuilder stringBuilder)
        {
            JsonWriter jsonWriter = new JsonWriter();
            jsonWriter.jsonWriter = stringBuilder;
            return jsonWriter;
        }

        private JsonWriter()
        {
        }

        public JsonWriter begin(JsonType jsonType)
        {
            this.jsonType = jsonType;
            switch (this.jsonType)
            {
                case JsonType.jtObject:
                    {
                        JsonWriterBase.beginObject(jsonWriter);
                        break;
                    }
                case JsonType.jtArray:
                    {
                        JsonWriterBase.beginArray(jsonWriter);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return this;
        }

        public JsonWriter addKeyValue(string key, object value)
        {
            JsonWriterBase.writeFast(jsonWriter, key);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addKeyValue(string key1, object value1, string key2, object value2)
        {
            JsonWriterBase.writeFast(jsonWriter, key1);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value1);
            JsonWriterBase.commaChar(jsonWriter);
            JsonWriterBase.writeFast(jsonWriter, key2);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value2);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addKeyValue(string key1, object value1, string key2, object value2, string key3, object value3)
        {
            JsonWriterBase.writeFast(jsonWriter, key1);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value1);
            JsonWriterBase.commaChar(jsonWriter);
            JsonWriterBase.writeFast(jsonWriter, key2);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value2);
            JsonWriterBase.commaChar(jsonWriter);
            JsonWriterBase.writeFast(jsonWriter, key3);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value3);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addArrayValue(object value)
        {
            JsonUtils.write(jsonWriter, value);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addArrayValue(params object[] value)
        {
            JsonUtils.write(jsonWriter, value);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addString(string value)
        {
            JsonWriterBase.write(jsonWriter, value);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addBoolean(bool b)
        {
            JsonWriterBase.write(jsonWriter, b);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        //public JsonWriter addNumber(Number number)
        //{
            
        //    JsonUtils.write(jsonWriter, number);
        //    JsonWriterBase.commaChar(jsonWriter);
        //    return this;
        //}

        public JsonWriter addNull()
        {
            JsonWriterBase.writeNull(jsonWriter);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public void end()
        {
            switch (this.jsonType)
            {
                case JsonType.jtObject:
                    {
                        JsonWriterBase.endObject(jsonWriter);
                        break;
                    }
                case JsonType.jtArray:
                    {
                        JsonWriterBase.endArray(jsonWriter);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void backspace()
        {
            JsonWriterBase.backspace(jsonWriter);
        }


    }
}
