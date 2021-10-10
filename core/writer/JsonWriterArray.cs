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
using System.Text;

namespace com.xmbill.json.core.writer
{
    public class JsonWriterArray
    {
        public static void Write(StringBuilder jsonWriter, object value, IJsonObjectWriter jsonObjectWriter)
        {
            if (value == null) JsonWriterBase.writeNull(jsonWriter);
            if (value is DBNull) JsonWriterBase.writeNull(jsonWriter);
            else
            {
                if (value is byte[])
                    JsonWriterBase.write(jsonWriter,Convert.ToBase64String((byte[])value, 0, ((byte[])value).Length, Base64FormattingOptions.None));
                else if (value is Array) {
                    writeArray(jsonWriter, value, jsonObjectWriter);
                }
                else if (value is IList)
                    writeList(jsonWriter, (IEnumerable)value,jsonObjectWriter);

              

            }
        }

        
        private static void writeList(StringBuilder jsonWriter, Object value, IJsonObjectWriter jsonObjectWriter)
        { 
                JsonWriterBase.beginArray(jsonWriter);
                foreach (object ev in (IList)value)
                {
                    JsonUtils.write(jsonWriter, ev, jsonObjectWriter);
                    JsonWriterBase.commaChar(jsonWriter);
                }
                JsonWriterBase.endArray(jsonWriter); 
        }
        private static void writeArray(StringBuilder jsonWriter, Object value, IJsonObjectWriter jsonObjectWriter)
        {
            JsonWriterBase.beginArray(jsonWriter);
            foreach (object e in (Array)value)
            {
                JsonUtils.write(jsonWriter, e, jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endArray(jsonWriter);
        }

    }
}
