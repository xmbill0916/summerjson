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
using System.Data;
using System.Text;

namespace com.xmbill.json.core.writer
{
    public class JsonWriterDBData
    {
        public static void Write(StringBuilder jsonWriter, DataSet value, IJsonObjectWriter jsonObjectWriter)
        {
            JsonWriterBase.beginObject(jsonWriter);
            foreach(DataTable table in ((DataSet)value).Tables)
            {
                Write(jsonWriter, table,jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endObject(jsonWriter);
        }

        public static void Write(StringBuilder jsonWriter,DataTable value, IJsonObjectWriter jsonObjectWriter)
        {
            JsonWriterBase.beginArray(jsonWriter);
            foreach(DataRow row in ((DataTable)value).Rows)
            {
                Write(jsonWriter, row,jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endArray(jsonWriter);
        }

        public static void Write(StringBuilder jsonWriter,DataRow value, IJsonObjectWriter jsonObjectWriter)
        {
            JsonWriterBase.beginObject(jsonWriter);
            DataRow rowValue = (DataRow)value;
            DataColumnCollection dataColumns = rowValue.Table.Columns;
            foreach (DataColumn c in dataColumns)
            {
                JsonWriterBase.write(jsonWriter, c.ColumnName);
                JsonWriterBase.objColon(jsonWriter);
                JsonUtils.write(jsonWriter, rowValue[c],jsonObjectWriter);
                JsonWriterBase.commaChar(jsonWriter);
            }
            JsonWriterBase.endObject(jsonWriter);
        }
    }
}
