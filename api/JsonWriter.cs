using com.xmbill.json.core;
using com.xmbill.json.core.writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public JsonWriter addKeyValue(String key, Object value)
        {
            JsonWriterBase.writeFast(jsonWriter, key);
            JsonWriterBase.objColon(jsonWriter);
            JsonUtils.write(jsonWriter, value);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

        public JsonWriter addKeyValue(String key1, Object value1, String key2, Object value2)
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

        public JsonWriter addKeyValue(String key1, Object value1, String key2, Object value2, String key3, Object value3)
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

        public JsonWriter addArrayValue(Object value)
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

        public JsonWriter addString(String value)
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

        public JsonWriter addNumber(Number number)
        {
            
            JsonUtils.write(jsonWriter, number);
            JsonWriterBase.commaChar(jsonWriter);
            return this;
        }

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
