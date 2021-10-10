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
    public class JsonParserObject:JsonParser
    {
        /**
   * 解析对象开始
   *
   * @param jsonParameter
   */
        public static void parseObjectStart(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpace(jsonParameter);
        }

        /**
         * 解析对象结束
         * 到下一个起启点
         *
         * @param jsonParameter
         */
        public static void parseObjectEnd(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpaceOfEnd(jsonParameter);
        }

        public static object parseObject(JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            return parseObject(null,"", new List<string>(), jsonParameter, jsonValueInvoke);
        }

        public static object parseObject(object parentObj, string parentKey, List<string> descPath, JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            parseObjectStart(jsonParameter);
            bool isEnd = isObjectEnd(jsonParameter);
            descPath.Add(parentKey);
            object obj = jsonValueInvoke.beforeParseObject(parentObj,parentKey, descPath);
            if (!isEnd)
            {
                string key;
                object value = null;
                int ijt;
                do
                {
                    key = getObjectKeyAndSkipToValue(jsonParameter);
                    ijt = getInternalJsonType(jsonParameter);
                    switch (ijt)
                    {
                        case 1:
                            {
                                value = parseObject(obj,key, descPath, jsonParameter, jsonValueInvoke);
                                break;
                            }
                        case 2:
                            {
                                value = JsonParserArray.parseArray(obj,key, descPath, jsonParameter, jsonValueInvoke);
                                break;
                            }
                        case 3:
                            {
                                value = getStringOfString(jsonParameter);
                                break;
                            }
                        case 4:
                            {
                                value = getNumberOfString(jsonParameter);
                                break;
                            }
                        case 5:
                            {
                                getTrueValue(jsonParameter);
                                value = true;
                                break;
                            }
                        case 6:
                            {
                                getFalseValue(jsonParameter);
                                value = false;
                                break;
                            }
                        case 7:
                            {
                                getNullValue(jsonParameter);
                                value = null;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    jsonValueInvoke.setObjectKeyValue(obj, jsonTypes[ijt], key, value);
                    if (hasNonObjectNextNode(jsonParameter))
                        break;
                } while (isNonEnd(jsonParameter));
            }
            parseObjectEnd(jsonParameter);
            jsonValueInvoke.afterParseObject(parentObj, parentKey, obj, descPath);
            descPath.RemoveAt(descPath.Count - 1);
            return obj;

        }
    }
}
