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
using System.Collections.Generic;


namespace com.xmbill.json.core.reader
{
    public class JsonParserArray:JsonParser
    {
        public static void parseArrayStart(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpace(jsonParameter);
        }

        public static void parseArrayEnd(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpaceOfEnd(jsonParameter);
        }

        public static object parseArray(JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            return parseArray(null,"", new List<string>(), jsonParameter, jsonValueInvoke);
        }

        public static object parseArray(object parentObj, string parentKey, List<string> descPath, JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            parseArrayStart(jsonParameter);
            bool isEnd = isArrayEnd(jsonParameter);
            descPath.Add(parentKey);
            object array = jsonValueInvoke.beforeParseArray(parentObj,parentKey, descPath);
            if (!isEnd)
            {
                object value = null;
                int ijt;
                int index = 0;
                do
                {
                    ijt = getInternalJsonType(jsonParameter);
                    switch (ijt)
                    {
                        case 1:
                            {
                                value = JsonParserObject.parseObject(array, index.ToString(), descPath, jsonParameter, jsonValueInvoke);
                                break;
                            }
                        case 2:
                            {
                                value = parseArray(array,index.ToString(), descPath, jsonParameter, jsonValueInvoke);
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
                    jsonValueInvoke.setArrayValue(array, jsonTypes[ijt], index++, value);
                    if (hasNonArrayNextNode(jsonParameter))
                        break;
                } while (isNonEnd(jsonParameter));
            }
            parseArrayEnd(jsonParameter);
            jsonValueInvoke.afterParseArray(parentObj,parentKey, array, descPath);
            descPath.RemoveAt(descPath.Count - 1);
            return array;
        }

    }
}
