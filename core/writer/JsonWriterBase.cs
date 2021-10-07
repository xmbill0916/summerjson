using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void writeFast(StringBuilder stringBuilder, String str)
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
 * 需要转义的字符串
 *
 * @param stringBuilder
 * @param str
 */
        public static void write(StringBuilder stringBuilder, String str)
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
        public static void writeQuote(StringBuilder stringBuilder, String str)
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
        //    String hex = Integer.toHexString(ch);

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


        /**
    * null
    *
    * @return
    */
        public static void writeNull(StringBuilder stringBuilder)
        {
            stringBuilder.Append("null");
        }

        public static void backspace(StringBuilder stringBuilder)
        {
            stringBuilder.Length = stringBuilder.Length - 1;
        }
    }
}
