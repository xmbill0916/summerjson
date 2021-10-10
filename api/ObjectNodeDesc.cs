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
using System.Collections.Generic;
using System.Data;

namespace com.xmbill.json.api
{
    public class ObjectNodeDesc:NodeDesc
    {
        public SetObjectValueHandler ObjectSetValueHandler { get; set; }
        public static ObjectNodeDesc handler(NewInstanceHandler instanceHandler, SetObjectValueHandler objectValueHandler)
        {
            ObjectNodeDesc objectNodeDesc = new ObjectNodeDesc();
            objectNodeDesc.ObjectSetValueHandler = objectValueHandler;
            objectNodeDesc.NewInstance = instanceHandler;
            return objectNodeDesc;
        }

        public static ObjectNodeDesc DefaultObjectHandler = ObjectNodeDesc.handler(
                (object parentObj) => { return new Dictionary<string, object>(); },
                (object obj, JsonType jsonType, string key, object value) => { ((Dictionary<string, object>)obj).Add(key, value); }
                );

        public static ObjectNodeDesc DataSetHandler = ObjectNodeDesc.handler(
            (object parentObj) => { return new DataSet(); },
            (object obj, JsonType jsonType, string k, object v) =>
            {
                ((DataTable)v).TableName = k;
                ((DataSet)obj).Tables.Add((DataTable)v);
            });

        public static ObjectNodeDesc DataRowHandler(SetObjectValueHandler dataRowSetValueHandler)
        {
            ObjectNodeDesc objectNodeDesc = new ObjectNodeDesc();
            objectNodeDesc.ObjectSetValueHandler = dataRowSetValueHandler;
            objectNodeDesc.NewInstance = (object parentObj)=> { return ((DataTable)parentObj).NewRow(); };
            return objectNodeDesc;
        }
    }
}
