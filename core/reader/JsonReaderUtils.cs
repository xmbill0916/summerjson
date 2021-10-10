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

using System; 
using System.Text; 

namespace com.xmbill.json.core.reader
{
    public class JsonReaderUtils
    {
    /**
    * 解析后的值与赋值对象类型的转化器
    *
    * @param jsonType
    * @param value
    * @param destType
    * @return
    */
        public static object ConvertValue(JsonType jsonType, object value, Type destType)
        {
            if (value == null) return null;
            switch (jsonType)
            {
                case JsonType.jtString:
                    {
                        return ConvertValueFromString((string)value, destType);
                    }
                case JsonType.jtNumber:
                    {
                        return ConvertValueFromNumberString((string)value, destType);
                    }
                case JsonType.jtFalse:
                    {
                        if ((destType == typeof(bool)) || (destType == typeof(Boolean)))
                        {
                            return false;
                        }
                        else if (destType == typeof(long))
                            return 0L;
                        else
                            return 0;
                    }
                case JsonType.jtTrue:
                    {
                        if ((destType == typeof(bool)) || (destType == typeof(Boolean)))
                        {
                            return true;
                        }
                        else if (destType == typeof(long))
                            return 1L;
                        else
                            return 1;
                    }
                case JsonType.jtNull:
                    {
                        return null;
                    }
                default:
                    return value;
            }
        }


        private static object ConvertValueFromString(string value, Type type)
        {
            if (type == typeof(string)||type==typeof(String))
            {
                return value;
            }
            else if (type == typeof(int))
            {
                return int.Parse(value);
            }
            else if (type == typeof(long))
            {
                return long.Parse(value);
            }
            else if (type == typeof(decimal))
            {
                return decimal.Parse(value);
            }
            else if (type == typeof(Boolean) || type == typeof(bool))
            {
                return bool.Parse(value);
            }
            else if (type == typeof(char))
            {
                return char.Parse(value);
            }
            else if (type == typeof(Byte) || type == typeof(byte) || type == typeof(sbyte))
            {
                return byte.Parse(value);
            }
            else if (type == typeof(short))
            {
                return short.Parse(value);
            }
            else if (type == typeof(float)||(type ==typeof(Single)))
            {
                return float.Parse(value);
            }
            else if (type == typeof(Double) || type == typeof(double))
            {
                return double.Parse(value);
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }
            else if (type == typeof(Byte[]) || type == typeof(byte[]))
            {
                return Convert.FromBase64String((string)value);
            }
            else if (type == typeof(Guid))
            {
                return new Guid(value);
            }
            else if (type == typeof(StringBuilder))
            {
                return new StringBuilder(value);
            }
            //else if (type == typeof(BigInteger))
            //{
            //    return BigInteger.Parse(value);
            //}
            else return null;
        }

        public static object ConvertValueFromNumberString(string value, Type type)
        {
            if (type == typeof(string) || type == typeof(String))
            {
                return value;
            }
            else if (type == typeof(int))
            {
                return int.Parse(value);
            }
            else if (type == typeof(long))
            {
                return long.Parse(value);
            }
            else if (type == typeof(decimal))
            {
                return decimal.Parse(value);
            }
            else if (type == typeof(Boolean) || type == typeof(bool))
            {
                return "1".Equals(value);
            }
            else if (type == typeof(char))
            {
                return char.Parse(value);
            }
            else if (type == typeof(Byte) || type == typeof(byte) || type == typeof(sbyte))
            {
                return Byte.Parse(value);
            }
            else if (type == typeof(short))
            {
                return short.Parse(value);
            }
            else if (type == typeof(float)|| type == typeof(Single))
            {
                return float.Parse(value);
            }
            else if (type == typeof(double) || type == typeof(Double))
            {
                return double.Parse(value);
            }
            //else if (type==typeof(BigInteger))
            //{
            //    return BigInteger.Parse(value);
            //}
            else if (type.IsEnum)
            {
                return enumValueOfInt(value, type);
            }
            else return null;
        }

        private static object enumValueOfName(string value, Type type)
        {
            string[] enumValues = Enum.GetNames(type);
            foreach (string enumValue in enumValues)
            {
                if (enumValue.Equals(value))
                    return enumValue;
            }
            return null;
        }

        private static object enumValueOfInt(string value, Type type)
        {

            string[] enumValues = Enum.GetNames(type);
            int index = int.Parse(value);
            int iLen = enumValues.Length;
            if (index > 0 && index < iLen)
                return enumValues[index];
            else
                return null;
        }
    }
}
