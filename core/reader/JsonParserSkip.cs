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


namespace com.xmbill.json.core.reader
{
    public class JsonParserSkip:JsonParserBase
    {
        public static void skipValue(JsonParameter jsonParameter)
        {
            skipSpace(jsonParameter);
            int ijt = getInternalJsonType(jsonParameter);
            switch (ijt)
            {
                case 1:
                    {
                        skipObject(jsonParameter);
                        break;
                    }
                case 2:
                    {
                        skipArray(jsonParameter);
                        break;
                    }
                case 3:
                    {
                        skipStringOfString(jsonParameter);
                        break;
                    }
                case 4:
                    {
                        skipNumberOfString(jsonParameter);
                        break;
                    }
                case 5:
                    {
                        skipTrueValue(jsonParameter);
                        break;
                    }
                case 6:
                    {
                        skipFalseValue(jsonParameter);
                        break;
                    }
                case 7:
                    {
                        skipNullValue(jsonParameter);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public static void skipObject(JsonParameter jsonParameter)
        {
            JsonParserObject.parseObjectStart(jsonParameter);
            bool isEnd = isObjectEnd(jsonParameter);
            if (!isEnd)
            {
                int ijt;
                do
                {
                    skipObjectKeyAndSkipToValue(jsonParameter);
                    ijt = getInternalJsonType(jsonParameter);
                    switch (ijt)
                    {
                        case 1:
                            {
                                skipObject(jsonParameter);
                                break;
                            }
                        case 2:
                            {
                                skipArray(jsonParameter);
                                break;
                            }
                        case 3:
                            {
                                skipStringOfString(jsonParameter);
                                break;
                            }
                        case 4:
                            {
                                skipNumberOfString(jsonParameter);
                                break;
                            }
                        case 5:
                            {
                                skipTrueValue(jsonParameter);
                                break;
                            }
                        case 6:
                            {
                                skipFalseValue(jsonParameter);
                                break;
                            }
                        case 7:
                            {
                                skipNullValue(jsonParameter);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    if (hasNonObjectNextNode(jsonParameter))
                        break;
                } while (isNonEnd(jsonParameter));
            }
            JsonParserObject.parseObjectEnd(jsonParameter);
        }

        public static void skipArray(JsonParameter jsonParameter)
        {
            JsonParserArray.parseArrayStart(jsonParameter);
            bool isEnd = isArrayEnd(jsonParameter);
            if (!isEnd)
            {
                int ijt;
                do
                {
                    ijt = getInternalJsonType(jsonParameter);
                    switch (ijt)
                    {
                        case 1:
                            {
                                skipObject(jsonParameter);
                                break;
                            }
                        case 2:
                            {
                                skipArray(jsonParameter);
                                break;
                            }
                        case 3:
                            {
                                skipStringOfString(jsonParameter);
                                break;
                            }
                        case 4:
                            {
                                skipNumberOfString(jsonParameter);
                                break;
                            }
                        case 5:
                            {
                                skipTrueValue(jsonParameter);
                                break;
                            }
                        case 6:
                            {
                                skipFalseValue(jsonParameter);
                                break;
                            }
                        case 7:
                            {
                                skipNullValue(jsonParameter);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    if (hasNonArrayNextNode(jsonParameter))
                        break;
                } while (isNonEnd(jsonParameter));
            }
            JsonParserArray.parseArrayEnd(jsonParameter);
        }

        public static void skipObjectKeyAndSkipToValue(JsonParameter jsonParameter)
        {
            skipStringOfString(jsonParameter);
            skipObjectKeyToValueSpace(jsonParameter);
        }

        /**
         * 第一个双引号进入此方法
         *
         * @param jsonParameter
         * @return
         */
        public static void skipStringOfString(JsonParameter jsonParameter)
        {
            if (getInternalJsonType(jsonParameter) == 7)
            {
                skipNullValue(jsonParameter);
            }
            else
                skipStringOfStringNonNull(jsonParameter);
        }

        /**
         * 第一个双引号进入此方法
         *
         * @param jsonParameter
         * @return
         */
        public static void skipStringOfStringNonNull(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            if (jsonParameter.buffer[jsonParameter.index] == '"')
            {
                jsonParameter.index++;
                return;
            }
            jsonParameter.offset = -1;
            skipStringOfStringNotEscaped(jsonParameter);
            if (jsonParameter.offset != -1)
                skipStringOfStringEscaped(jsonParameter);
        }

        /**
         * 第一个字符开始，不是左双引号开始
         *
         * @param jsonParameter
         * @return
         */
        public static void skipStringOfStringNotEscaped(JsonParameter jsonParameter)
        {
            int offset = 0;
            char c;
            char[] buffer = jsonParameter.buffer;
            do
            {
                c = buffer[jsonParameter.index + offset];
                if (c == '\\')
                {
                    jsonParameter.offset = offset;
                    return;
                }
                if (c == '"')
                {
                    jsonParameter.index += offset + 1;
                    return;
                }
                offset++;
            } while (jsonParameter.index + offset < jsonParameter.length);
        }

        /**
         * 第一个字符开始，不是左双引号开始
         * 有带转义的字符串
         *
         * @param jsonParameter
         */
        public static void skipStringOfStringEscaped(JsonParameter jsonParameter)
        {
            char[] buffer = jsonParameter.buffer;
            if (jsonParameter.offset > 0)
            {
                jsonParameter.index += jsonParameter.offset;
                jsonParameter.offset = -1;
            }
            char c;
            do
            {
                c = buffer[jsonParameter.index];
                if (c != '\\')
                {
                    jsonParameter.index++;
                    if (c == '"')
                    {
                        break;
                    }
                    jsonParameter.sb.Append(c);
                }
                else
                {
                    jsonParameter.index++;
                    c = buffer[jsonParameter.index];

                    switch (c)
                    {
                        case '"':
                        case '\\':
                        case '/':
                        case 'b':
                        case 'f':
                        case 'n':
                        case 'r':
                        case 't':
                            {
                                jsonParameter.index++;
                                break;
                            }
                        case 'u':
                            {
                                ++jsonParameter.index;
                                
                                jsonParameter.index += 4;
                                break;
                            }
                        default:
                            {
                                //这里应该是不经过才对
                                jsonParameter.index++;
                                break;
                            }
                    }
                }
            } while (jsonParameter.index < jsonParameter.length);
        }

        /**
         * 第一个字符
         *
         * @param jsonParameter
         * @return
         */
        public static void skipNumberOfString(JsonParameter jsonParameter)
        {
            if (getInternalJsonType(jsonParameter) == 7)
            {
                skipNullValue(jsonParameter);
            }
            else
                skipNumberOfStringNonNull(jsonParameter);
        }

        /**
         * 不考虑null
         *
         * @param jsonParameter
         * @return
         */
        public static void skipNumberOfStringNonNull(JsonParameter jsonParameter)
        {
            char c;
            char[] buffer = jsonParameter.buffer;
            do
            {
                c = buffer[jsonParameter.index];
                if (isNumberEnd(c))
                    break;
                jsonParameter.index++;
            } while (jsonParameter.index < jsonParameter.length);

        }

        public static void skipFalseValue(JsonParameter jsonParameter)
        {
            jsonParameter.index += 5;
        }

        public static void skipTrueValue(JsonParameter jsonParameter)
        {
            jsonParameter.index += 4;
        }

        public static void skipNullValue(JsonParameter jsonParameter)
        {
            jsonParameter.index += 4;
        }

    }
}
