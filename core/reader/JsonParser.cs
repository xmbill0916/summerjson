﻿using com.xmbill.json.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.core.reader
{
    public class JsonParser :JsonParserBase
    {
        /**
  * 第一个字符是左双引号
  * 取对象结点键值
  *
  * @param jsonParameter
  * @return
  */
        public static String getObjectKey(JsonParameter jsonParameter)
        {
            return getStringOfString(jsonParameter);
        }

        /**
         * 第一个字符是左双引号进入此方法
         * 得到键值，并跳过键与值的分隔符
         *
         * @param jsonParameter
         * @return
         */
        public static String getObjectKeyAndSkipToValue(JsonParameter jsonParameter)
        {
            String result = getStringOfString(jsonParameter);
            skipObjectKeyToValueSpace(jsonParameter);
            return result;
        }
        /**
         * 第一个字符是左双引号
         * 没有特殊转义字符字符串
         *
         * @param jsonParameter
         * @return
         */
        public static String getFastString(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            int offset = 0;
            char c;
            char[] buffer = jsonParameter.buffer;
            do
            {
                c = buffer[jsonParameter.index + offset];
                if (c == '"')
                {
                    String result = new String(buffer, jsonParameter.index, offset);
                    jsonParameter.index += offset + 1;
                    return result;
                }
                offset++;
            } while (jsonParameter.index + offset < jsonParameter.length);
            return null;
        }

        /**
         * 第一个双引号进入此方法
         *
         * @param jsonParameter
         * @return
         */
        public static String getStringOfString(JsonParameter jsonParameter)
        {
            if (getInternalJsonType(jsonParameter) == 7)
            {
                getNullValue(jsonParameter);
                return null;
            }
            else
                return getStringOfStringNonNull(jsonParameter);
        }

        /**
         * 第一个双引号进入此方法
         *
         * @param jsonParameter
         * @return
         */
        public static String getStringOfStringNonNull(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            if (jsonParameter.buffer[jsonParameter.index] == '"')
            {
                jsonParameter.index++;
                return "";
            }

            jsonParameter.offset = -1;
            String result = getStringOfStringNotEscaped(jsonParameter);
            if (jsonParameter.offset == -1)
                return result;
            else
            {
                return getStringOfStringEscaped(jsonParameter);
            }
        }

        /**
         * 第一个字符不是左双引号
         *
         * @param jsonParameter
         * @return
         */
        public static String getStringOfStringNotEscaped(JsonParameter jsonParameter)
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
                    return null;
                }
                if (c == '"')
                {
                    String result = new String(buffer, jsonParameter.index, offset);
                    jsonParameter.index += offset + 1;
                    return result;
                }
                offset++;
            } while (jsonParameter.index + offset < jsonParameter.length);
            return null;
        }



        /**
         * 第一个字符不是左双引号
         * 有带转义的字符串
         *
         * @param jsonParameter
         * @return
         */
        public static String getStringOfStringEscaped(JsonParameter jsonParameter)
        {
            jsonParameter.sb.Clear();
            char[] buffer = jsonParameter.buffer;
            StringBuilder sb = jsonParameter.sb;
            if (jsonParameter.offset > 0)
            {
                sb.Append(buffer, jsonParameter.index, jsonParameter.offset);
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
                                sb.Append((char)convertChar[c]);
                                jsonParameter.index++;
                                break;
                            }
                        case 'u':
                            {
                                ++jsonParameter.index;
                                uint result = 0;
                                for (int i = jsonParameter.index, end = i + 4; i < end; i++)
                                {
                                    c = buffer[i];
                                    result <<= 4;
                                    if (c >= '0' && c <= '9')
                                    {
                                        result += (uint)(c - '0');
                                    }
                                    else if (c >= 'a' && c <= 'f')
                                    {
                                        result += (uint)(c - 'a' + 10);
                                    }
                                    else if (c >= 'A' && c <= 'F')
                                    {
                                        result += (uint)(c - 'A' + 10);
                                    }
                                    else
                                    {
                                        throw new Exception("\\u" + new String(buffer, jsonParameter.index, 4));
                                    }
                                }
                                jsonParameter.index += 4;
                                sb.Append((char)result);
                                break;
                            }
                        default:
                            {
                                //这里应该是不经过才对
                                sb.Append('\\');
                                sb.Append(c);
                                jsonParameter.index++;
                                break;
                            }
                    }
                }
            } while (jsonParameter.index < jsonParameter.length);
            return jsonParameter.sb.ToString();
        }

        /**
         * 第一个字符是数字开始
         *
         * @param jsonParameter
         * @return
         */
        public static String getNumberOfString(JsonParameter jsonParameter)
        {
            if (getInternalJsonType(jsonParameter) == 7)
            {
                getNullValue(jsonParameter);
                return null;
            }
            else
                return getNumberOfStringNonNull(jsonParameter);
        }

        /**
         * 不考虑null
         *
         * @param jsonParameter
         * @return
         */
        public static String getNumberOfStringNonNull(JsonParameter jsonParameter)
        {
            char c;
            char[] buffer = jsonParameter.buffer;
            int ib = jsonParameter.index;
            do
            {
                c = buffer[jsonParameter.index];
                if (isNumberEnd(c))
                    break;
                jsonParameter.index++;
            } while (jsonParameter.index < jsonParameter.length);

            return new String(buffer, ib, jsonParameter.index - ib);
        }

        /**
         * false
         * @param jsonParameter
         * @return
         */
        public static bool getFalseValue(JsonParameter jsonParameter)
        {
            jsonParameter.index += 5;
            return false;
        }

        /**
         * true
         * @param jsonParameter
         * @return
         */
        public static bool getTrueValue(JsonParameter jsonParameter)
        {
            jsonParameter.index += 4;
            return true;
        }

        /**
         * null
         * @param jsonParameter
         */
        public static void getNullValue(JsonParameter jsonParameter)
        {
            jsonParameter.index += 4;
        }
    }
}
