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
using System.Globalization;
using System.Text;

namespace com.xmbill.json.core.writer
{
    public class JsonWriterBase
    {
        private static int[] nodeStrKey = new int[118];

        static JsonWriterBase()
        {
            //转义
            nodeStrKey['"'] = 1;
            nodeStrKey['\\'] = 2;
            nodeStrKey['/'] = 3;
            nodeStrKey['\b'] = 4;
            nodeStrKey['\f'] = 5;
            nodeStrKey['\n'] = 6;
            nodeStrKey['\r'] = 7;
            nodeStrKey['\t'] = 8;
            //nodeStrKey['u'] = 3;//汉字
        }

        /**
         * {
         *
         * @return
         */
        public static void beginObject(StringBuilder stringBuilder)
        {
            stringBuilder.Append('{');
        }

        /**
         * }
         *
         * @return
         */
        public static void endObject(StringBuilder stringBuilder)
        {
            endObject(stringBuilder, true);

        }
        public static void endObject(StringBuilder stringBuilder, bool checkCommaChar)
        {
            if (checkCommaChar)
            {
                int lastIndex = stringBuilder.Length - 1;
                if (stringBuilder[lastIndex] == '{')
                    stringBuilder.Append('}');
                else
                    stringBuilder[lastIndex] = '}';
            }
            else
                stringBuilder.Append('}');

        }
        /**
         * [
         *
         * @return
         */
        public static void beginArray(StringBuilder stringBuilder)
        {
            stringBuilder.Append('[');
        }

        /**
         * ]
         *
         * @return
         */

        public static void endArray(StringBuilder stringBuilder)
        {
            endArray(stringBuilder, true);
        }

        public static void endArray(StringBuilder stringBuilder, bool checkCommaChar)
        {
            if (checkCommaChar)
            {
                int lastIndex = stringBuilder.Length - 1;
                if (stringBuilder[lastIndex] == '[')
                    stringBuilder.Append(']');
                else
                {
                    stringBuilder[lastIndex] = ']';
                }
            }
            else
                stringBuilder.Append(']');

        }

        /**
         * :
         *
         * @return
         */
        public static void objColon(StringBuilder stringBuilder)
        {
            stringBuilder.Append(':');
        }

        /**
         * ,
         *
         * @return
         */
        public static void commaChar(StringBuilder stringBuilder)
        {
            stringBuilder.Append(',');
        }


        /**
         * "
         *
         * @return
         */
        public static void strQuote(StringBuilder stringBuilder)
        {
            stringBuilder.Append('"');
        }


        /**
 * 添加字符串
 *
 * @param str
 * @return
 */
        public static void writeFast(StringBuilder stringBuilder, string str)
        {
            if (str == null) stringBuilder.Append("null");
            else
            {
                stringBuilder.Append('"');
                stringBuilder.Append(str);
                stringBuilder.Append('"');
            }
        }
        /**
 * false true
 *
 * @param value
 * @return
 */
        public static void write(StringBuilder stringBuilder, bool value)
        {
            stringBuilder.Append(value ? "true" : "false");
        }

        /**
 * 需要转义的字符串
 *
 * @param stringBuilder
 * @param str
 */
        public static void write(StringBuilder stringBuilder, string str)
        {
            if (str == null) stringBuilder.Append("null");
            else
            {
                stringBuilder.Append('"');
                writeQuote(stringBuilder, str);
                stringBuilder.Append('"');
            }
        }

        /**
 * 字符串带转义
 *
 * @param stringBuilder
 * @param str
 */
        public static void writeQuote(StringBuilder stringBuilder, string str)
        {
            try
            {
                char[] strChar = str.ToCharArray();
                int tag = 0;
                int beginIndex = 0;
                int iC = strChar.Length;
                char chr;
                for (int i = 0; i < iC; i++)
                {
                    chr = strChar[i];
                    if (chr < 118)
                    {
                        tag = nodeStrKey[chr];
                    }
                    if (tag > 0)
                    {
                        switch (tag)
                        {
                            case 1:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, '"');
                                    break;
                                }
                            case 2:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, '\\');
                                    break;
                                }
                            case 3:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, '/');
                                    break;
                                }
                            case 4:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, 'b');
                                    break;
                                }
                            case 5:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, 'f');
                                    break;
                                }
                            case 6:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, 'n');
                                    break;
                                }
                            case 7:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, 'r');
                                    break;
                                }
                            case 8:
                                {
                                    escapeQuoteChar(stringBuilder, strChar, beginIndex, i, 't');
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        beginIndex = i + 1;
                        tag = 0;
                    }
                }
                if (iC > beginIndex)
                    stringBuilder.Append(strChar, beginIndex, iC - beginIndex);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //private static void unicodeEscape(StringBuilder stringBuilder, char ch)
        //{
        //    stringBuilder.Append("\\u");
        //    string hex = Integer.toHexString(ch);

        //    for (int i = hex.length(); i < 4; ++i)
        //    {
        //        stringBuilder.Append('0');
        //    }

        //    stringBuilder.Append(hex);
        //}

        private static void escapeQuoteChar(StringBuilder stringBuilder, char[] strChar, int beginIndex, int currentIndex, char chr)
        {
            if (currentIndex > beginIndex)
                stringBuilder.Append(strChar, beginIndex, currentIndex - beginIndex);
            stringBuilder.Append('\\').Append(chr);
        }

        public static void write(StringBuilder stringBuilder, Guid str)
        {
            stringBuilder.Append('"').Append(str.ToString()).Append('"');
        }

        /**
         * char
         *
         * @param str
         * @return
         */
        public static void write(StringBuilder stringBuilder, char str)
        {
            stringBuilder.Append('"').Append(str).Append('"');
        }



        /**
         * null
         *
         * @return
         */
        public static void writeNull(StringBuilder stringBuilder)
        {
            stringBuilder.Append("null");
        }
        public static void writeDBNull(StringBuilder stringBuilder)
        {
            stringBuilder.Append("null");
        }

        /**
         * BigDecimal
         *
         * @param value
         * @return
         */
        public static void write(StringBuilder stringBuilder, decimal value)
        {
            stringBuilder.Append(value.ToString());
        }

        /**
         * Double
         *
         * @param value
         * @return
         */
        public static void write(StringBuilder stringBuilder, double value)
        {
            stringBuilder.Append(value);
        }

        public static void write(StringBuilder stringBuilder, float value)
        {
            stringBuilder.Append(value);
        }

        /**
         * long
         *
         * @param value
         * @return
         */
        public static void write(StringBuilder stringBuilder, long value)
        {
            stringBuilder.Append(value);
        }
        public static void write(StringBuilder stringBuilder, ulong value)
        {
            stringBuilder.Append(value);
        }
        /**
         * int
         *
         * @param value
         * @return
         */
        public static void write(StringBuilder stringBuilder, int value)
        {
            stringBuilder.Append(value);
        }
        public static void write(StringBuilder stringBuilder, uint value)
        {
            stringBuilder.Append(value);
        }


        public static void write(StringBuilder stringBuilder, short value)
        {
            stringBuilder.Append(value);
        }
        public static void write(StringBuilder stringBuilder, ushort value)
        {
            stringBuilder.Append(value);
        }
        public static void write(StringBuilder stringBuilder, byte value)
        {
            stringBuilder.Append(value);
        }
        public static void write(StringBuilder stringBuilder, sbyte value)
        {
            stringBuilder.Append(value);
        }


        /**
         * Date
         * 2019-01-01
         *
         * @param value
         * @return
         */

        //public static void write(StringBuilder stringBuilder, Date value)
        //{
        //    if (value == null) stringBuilder.append("null");
        //    else
        //    {
        //        string str = new SimpleDateFormat("\"yyyy-MM-dd\"").format(value);
        //        stringBuilder.append(str);
        //    }
        //}



        /**
         * Time
         * 12:01:01
         *
         * @param value
         * @return
         */
        public static void write(StringBuilder stringBuilder, TimeSpan value)
        {

            string s = String.Format("{0:t}", value);
            stringBuilder.Append('"');
            stringBuilder.Append(s);
            stringBuilder.Append('"');
        }

        /**
         * 2019-01-01 12:00:01
         *
         * @param value
         * @return
         */
        public static void write(StringBuilder stringBuilder, DateTimeOffset value)
        {
            write(stringBuilder, value.DateTime);
        }

        public static void write(StringBuilder stringBuilder, DateTime value)
        {
            stringBuilder.Append('"');
            stringBuilder.Append(value.Year.ToString("0000", NumberFormatInfo.InvariantInfo));
            stringBuilder.Append('-').Append(value.Month.ToString("00", NumberFormatInfo.InvariantInfo));
            stringBuilder.Append('-').Append(value.Day.ToString("00", NumberFormatInfo.InvariantInfo));
            stringBuilder.Append(' ').Append(value.Hour.ToString("00", NumberFormatInfo.InvariantInfo));
            stringBuilder.Append(':').Append(value.Minute.ToString("00", NumberFormatInfo.InvariantInfo));
            stringBuilder.Append(':').Append(value.Second.ToString("00", NumberFormatInfo.InvariantInfo));
            stringBuilder.Append('"');
        }


        /**
         * 枚举
         *
         * @param stringBuilder
         * @param value
         */
        public static void write(StringBuilder stringBuilder, Enum value)
        {
            stringBuilder.Append('"');
            stringBuilder.Append(value.ToString());
            stringBuilder.Append('"');
        }

        /**
 * 对象的Key一定是字符串
 *
 * @param jsonWriter
 * @param value
 */
        public static void writeObjectKey(StringBuilder jsonWriter, Object value)
        {
            if (value == null) writeNull(jsonWriter);
            else if (value is DBNull) writeNull(jsonWriter);
            else
                writeObjectKey(jsonWriter, value, value.GetType());
        }
        public static void writeObjectKey(StringBuilder jsonWriter, Object value, Type clazz)
        {
            if (value == null) writeNull(jsonWriter);
            if (value is DBNull) writeNull(jsonWriter);
            else
            {
                write(jsonWriter, value.ToString());
            }
        }
        public static bool writeValue(StringBuilder jsonWriter, object value)
        {
            return writeValue(jsonWriter, value, value.GetType());
        }

        public static bool writeValue(StringBuilder jsonWriter, Object value, Type clazz)
        {
            bool result = true;
            if (value == null) writeNull(jsonWriter);
            if (value is DBNull) writeNull(jsonWriter);
            else
            {
                if ((value is string) || (value is String))
                {
                    write(jsonWriter, (string)value);
                }
               else if (value is decimal)
                {
                    write(jsonWriter, (decimal)value);
                }
                else if (value is char)
                {
                    write(jsonWriter, (char)value);
                }
                else if ((value is Boolean) || (value is bool))
                {
                    write(jsonWriter, (bool)value);
                }
                else if (value is TimeSpan)
                {
                    write(jsonWriter, (TimeSpan)value);
                }
                else if (value is DateTime)
                {
                    write(jsonWriter, (DateTime)value);
                }
                else if (value is DateTimeOffset)
                {
                    write(jsonWriter, (DateTimeOffset)value);
                }
                else if (value is Guid)
                {
                    write(jsonWriter, (Guid)value);
                }
                else if (value is Enum)
                {
                    write(jsonWriter, (Enum)value);
                }
                else if (value is int)
                {
                    write(jsonWriter, (int)value);
                }
                else if (value is long)
                {
                    write(jsonWriter, (long)value);
                }
                else if (value is short)
                {
                    write(jsonWriter, (short)value);
                }
                else if (value is uint)
                {
                    write(jsonWriter, (uint)value);
                }
                else if (value is ulong)
                {
                    write(jsonWriter, (ulong)value);
                }
                else if (value is ushort)
                {
                    write(jsonWriter, (ushort)value);
                }
                else if ((value is double)||(value is Double))
                {
                    write(jsonWriter, (double)value);
                }
                else if ((value is float) || (value is Single))
                {
                    write(jsonWriter, (float)value);
                }
                else if (value is sbyte)
                {
                    write(jsonWriter, (sbyte)value);
                }
                else if (value is byte)
                {
                    write(jsonWriter, (byte)value);
                }
                else if (value is Byte)
                {
                    write(jsonWriter, (Byte)value);
                }
                else if (value is StringBuilder)
                {
                    write(jsonWriter, value.ToString());
                }
                else if (value is Uri)
                {
                    write(jsonWriter, ((Uri)value).ToString());
                }
                else
                    result = false;
            }
            return result;
        }


        public static void backspace(StringBuilder stringBuilder)
        {
            stringBuilder.Length = stringBuilder.Length - 1;
        }
    }
}
