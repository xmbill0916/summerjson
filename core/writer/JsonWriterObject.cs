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


using com.xmbill.json.api;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace com.xmbill.json.core.writer
{
    public class JsonWriterObject
    {
        public static void write(StringBuilder jsonWriter, object value, IJsonObjectWriter jsonObjectWriter)
        {
            if (value == null) JsonWriterBase.writeNull(jsonWriter);
            else if (value is DBNull) JsonWriterBase.writeNull(jsonWriter);
            else
            {
                if (value is IDictionary)
                {
                    WriteDictionary(jsonWriter, value, jsonObjectWriter);
                }
                else if (value is StringDictionary)
                {
                    WriteStrDict(jsonWriter, value, jsonObjectWriter);
                }
                else if (value is NameValueCollection)
                {
                    WriteNameValueCollection(jsonWriter, value, jsonObjectWriter);
                }
            }

        }
        private static void WriteDictionary(StringBuilder jsonWriter, object value, IJsonObjectWriter jsonObjectWriter)
        {
            JsonWriterBase.beginObject(jsonWriter);
            foreach (DictionaryEntry entry in (IDictionary)value)
            {
                JsonWriterBase.writeObjectKey(jsonWriter, entry.Key);
                JsonWriterBase.objColon(jsonWriter);
                JsonUtils.write(jsonWriter, entry.Value, jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endObject(jsonWriter);
        }
        private static void WriteStrDict(StringBuilder jsonWriter, object value, IJsonObjectWriter jsonObjectWriter)
        {
            JsonWriterBase.beginObject(jsonWriter);             
            foreach (DictionaryEntry entry in (StringDictionary)value)
            {
                JsonWriterBase.writeObjectKey(jsonWriter, entry.Key);
                JsonWriterBase.objColon(jsonWriter);
                JsonUtils.write(jsonWriter, entry.Value, jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endObject(jsonWriter);
        }
        private static void WriteNameValueCollection(StringBuilder jsonWriter, object value, IJsonObjectWriter jsonObjectWriter)
        {
            NameValueCollection nameValueCollection = (NameValueCollection)value;
            JsonWriterBase.beginObject(jsonWriter);
            foreach (string key in nameValueCollection)
            {
                JsonWriterBase.writeObjectKey(jsonWriter, key);
                JsonWriterBase.objColon(jsonWriter);
                JsonUtils.write(jsonWriter, nameValueCollection[key], jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endObject(jsonWriter);
        }
    }
}
