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

using com.xmbill.json.core;
using System;
using System.Collections.Generic;

namespace com.xmbill.json.api
{
    public interface IJsonParseInvoke
    {
        /**
         * 结点描述
         *
         * @param jsonPathDesc
         */
        void setJsonPathDesc(JsonPathDesc jsonPathDesc);

        /**
         * 开始解析对象前
         * 返回对象实例，默认为Map<String,Object>
         *
         * @param parentKey
         * @param descPath
         * @return
         */
        Object beforeParseObject(object parentObj, string parentKey, List<string> descPath);

        /**
         * 解析对象结点键值回调
         *
         * @param object
         * @param jsonType
         * @param key
         * @param value
         */
        void setObjectKeyValue(object obj, JsonType jsonType, string key, object value);

        /**
         * 解析对象完成之后
         *
         * @param parentKey
         * @param object
         * @param descPath
         */
        void afterParseObject(object parentObj, string parentKey, object obj, List<string> descPath);

        /**
         * 开始解析数组前
         *
         * @param parentKey
         * @param descPath
         * @return
         */
        Object beforeParseArray(object parentObj, string parentKey, List<string> descPath);

        /**
         * 赋数组结点
         *
         * @param list
         * @param jsonType
         * @param index
         * @param value
         */
        void setArrayValue(object list, JsonType jsonType, int index, object value);

        /**
         * 解析数组完成之后
         *
         * @param parentKey
         * @param ary
         * @param descPath
         */
        void afterParseArray(object parentObj, string parentKey, object ary, List<string> descPath);
    }
}
