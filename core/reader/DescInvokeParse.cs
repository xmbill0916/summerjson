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
using System.Collections.Generic;

namespace com.xmbill.json.core.reader
{
    public class DescInvokeParse : IJsonParseInvoke
    {
        private JsonPathDesc root;
        private JsonPathDesc currentDesc;
        private List<JsonPathDesc> stackNodeDesc = new List<JsonPathDesc>(32);

        public static IJsonParseInvoke newInstance(JsonPathDesc root)
        {
            DescInvokeParse descInvokeValue = new DescInvokeParse();
            descInvokeValue.setJsonPathDesc(root);
            return descInvokeValue;
        }

        public void setJsonPathDesc(JsonPathDesc root)
        {
            this.root = root;
        }

        public object beforeParseObject(object parentObj, string parentKey, List<string> descPath)
        { 
            if ("".Equals(parentKey))
            {
                if (descPath.Count == 1)
                {
                    if (root.GetNodeDesc() != null)
                    {
                        currentDesc = root;
                    }
                    else
                    {
                        currentDesc = null;
                    }
                }
                else
                    currentDesc = null;
            }
            else
            {
                if (currentDesc != null)
                {
                    JsonPathDesc currentDescTmp = currentDesc.GetChild(parentKey);
                    if ((!"*".Equals(parentKey)) && (currentDescTmp == null))
                        currentDescTmp = currentDesc.GetChild("*");
                    currentDesc = currentDescTmp;
                }
            }
            stackNodeDesc.Add(currentDesc);
            if (currentDesc == null)
                return new Dictionary<string, object>();
            else
                return currentDesc.NewInstance(parentObj);
        }

        public void setObjectKeyValue(object obj, JsonType jsonType, string key, object value)
        {
            if (currentDesc != null)
                currentDesc.Set(obj, jsonType, key, value);
            else
                ((Dictionary<string, object>)obj).Add(key, value);
        }

        public void afterParseObject(object parentObj, string parentKey, object obj, List<string> descPath)
        {
            int index = stackNodeDesc.Count - 1;
            stackNodeDesc.RemoveAt(index);
            if (index > 0)
                currentDesc = stackNodeDesc[index - 1];
            else
                currentDesc = null;
        }

        public object beforeParseArray(object parentObj, string parentKey, List<string> descPath)
        {
            if ("".Equals(parentKey))
            {
                if (descPath.Count == 1)
                {
                    if (root.GetNodeDesc() != null)
                    {
                        currentDesc = root;
                    }
                    else
                    {
                        currentDesc = null;
                    }
                }
                else
                    currentDesc = null;
            }
            else
            {
                if (currentDesc != null)
                {
                    currentDesc = currentDesc.GetChild(parentKey);
                }
            }
            stackNodeDesc.Add(currentDesc);
            if (currentDesc == null)
                return new List<object>();
            else
                return currentDesc.NewInstance(parentObj);
        }

        public void setArrayValue(object list, JsonType jsonType, int index, object value)
        {
            if (currentDesc != null)
                currentDesc.Set(list, jsonType, index, value);
            else
                ((List<object>)list).Add(value);
        }

        public void afterParseArray(object parentObj, string parentKey, object ary, List<string> descPath)
        {
            int index = stackNodeDesc.Count - 1;
            stackNodeDesc.RemoveAt(index);
            if (index > 0)
                currentDesc = stackNodeDesc[index - 1];
            else
                currentDesc = null;
        }

    }
}
