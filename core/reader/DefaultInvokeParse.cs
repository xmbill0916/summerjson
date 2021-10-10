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
using System.Collections.Generic;

namespace com.xmbill.json.core.reader
{
    public class DefaultInvokeParse: IJsonParseInvoke
    {
        public void setJsonPathDesc(JsonPathDesc jsonPathDesc)
        {

        }

        public object beforeParseObject(object parentObj, string parentKey, List<string> descPath)
        { 
            return new Dictionary<string, object>();
        }

        public void setObjectKeyValue(object obj, JsonType jsonType, string key, object value)
        {
              if (jsonType == JsonType.jtNumber)
                 value = JsonReaderUtils.ConvertValue(jsonType, value, typeof(decimal));
            ((Dictionary<string, object>)obj).Add(key, value);
        }

        public void afterParseObject(object parentObj, string parentKey, object obj, List<string> descPath)
        { 
        }

        public object beforeParseArray(object parentObj, string parentKey, List<string> descPath)
        { 
            return new List<object>();
        }

        public void setArrayValue(object list, JsonType jsonType, int index, object value)
        {
             if (jsonType == JsonType.jtNumber)
                 value = JsonReaderUtils.ConvertValue(jsonType, value, typeof(decimal));
            ((List<object>)list).Add(value);
        }

        public void afterParseArray(object parentObj, string parentKey, object ary, List<string> descPath)
        { 

        }
    }
}
