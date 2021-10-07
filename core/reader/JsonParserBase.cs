using com.xmbill.json.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.core.reader
{
    public class JsonParserBase
    {
        protected static JsonType[] jsonTypes = new JsonType[] { JsonType.jtNone, JsonType.jtObject, JsonType.jtArray, JsonType.jtString, JsonType.jtNumber, JsonType.jtTrue, JsonType.jtFalse, JsonType.jtNull };
        /**
       * 跳过空格与回车 换行 tab
       */
        protected static int[] spaceTag = new int[33];
        /**
         * 数组间空格
         */
        protected static int[] arrayVVSpaceTag = new int[45];
        /**
         * 对象键与值空格
         */
        protected static int[] objectKVSpaceTag = new int[59];
        /**
         * 对象与对象单空格
         */
        protected static int[] objectVVSpaceTag = new int[45];

        /**
         * json对象类型
         */
        protected static int[] JsonTypeTag = new int[128];
        /**
         * 转义字符
         */
        protected static int[] convertChar = new int[118];

        protected static int[] takenTag = new int[128];
        protected static int[] nodeStrKey = new int[118];
        protected static int[] nodeStrValue = new int[126];

        protected static int[] objectKeyHeader = new int[126];
        protected static int[] NumValue = new int[126];
        protected static int[] NumKeyByStrValue = new int[102];

        protected static int[] SkipCharAfterObjectKey = new int[126];

        static JsonParserBase()
        {
            initSpaceTag();
            initArraySpaceTag();
            initObjectSpaceTag();
            initJsonTypeTag();
            initConvertChar();
            initTakenTag();
        }

        private static void initSpaceTag()
        {
            spaceTag[' '] = 1;
            spaceTag['\t'] = 1;
            spaceTag['\r'] = 1;
            spaceTag['\n'] = 1;
        }

        private static void initArraySpaceTag()
        {
            arrayVVSpaceTag[' '] = 1;
            arrayVVSpaceTag['\t'] = 1;
            arrayVVSpaceTag['\r'] = 1;
            arrayVVSpaceTag['\n'] = 1;
            arrayVVSpaceTag[','] = 1;
        }

        private static void initObjectSpaceTag()
        {
            objectVVSpaceTag[' '] = 1;
            objectVVSpaceTag['\t'] = 1;
            objectVVSpaceTag['\r'] = 1;
            objectVVSpaceTag['\n'] = 1;
            objectVVSpaceTag[','] = 1;


            objectKVSpaceTag[' '] = 1;
            objectKVSpaceTag['\t'] = 1;
            objectKVSpaceTag['\r'] = 1;
            objectKVSpaceTag['\n'] = 1;
            objectKVSpaceTag[':'] = 1;
        }

        private static void initJsonTypeTag()
        {
            JsonTypeTag['{'] = 1;
            JsonTypeTag['['] = 2;
            JsonTypeTag['"'] = 3;
            JsonTypeTag['0'] = 4;
            JsonTypeTag['1'] = 4;
            JsonTypeTag['2'] = 4;
            JsonTypeTag['3'] = 4;
            JsonTypeTag['4'] = 4;
            JsonTypeTag['5'] = 4;
            JsonTypeTag['6'] = 4;
            JsonTypeTag['7'] = 4;
            JsonTypeTag['8'] = 4;
            JsonTypeTag['9'] = 4;
            JsonTypeTag['-'] = 4;
            JsonTypeTag['+'] = 4;
            JsonTypeTag['.'] = 4;
            JsonTypeTag['t'] = 5;
            JsonTypeTag['f'] = 6;
            JsonTypeTag['n'] = 7;
        }

        private static void initConvertChar()
        {
            convertChar['"'] = '"';
            convertChar['\\'] = '\\';
            convertChar['/'] = '/';
            convertChar['b'] = '\b';
            convertChar['f'] = '\f';
            convertChar['n'] = '\n';
            convertChar['r'] = '\r';
            convertChar['t'] = '\t';
        }

        private static void initTakenTag()
        {
            takenTag['{'] = 1;
            takenTag['}'] = 2;
            takenTag['['] = 3;
            takenTag[']'] = 4;
            takenTag[','] = 6;
            takenTag['"'] = 7;

            takenTag['0'] = 8;
            takenTag['1'] = 8;
            takenTag['2'] = 8;
            takenTag['3'] = 8;
            takenTag['4'] = 8;
            takenTag['5'] = 8;
            takenTag['6'] = 8;
            takenTag['7'] = 8;
            takenTag['8'] = 8;
            takenTag['9'] = 8;
            takenTag['-'] = 8;
            takenTag['+'] = 8;
            takenTag['.'] = 8;
            //        takenTag['e'] = 8;
            //        takenTag['E'] = 8;

            //takenTag[':'] = 5;

            takenTag['f'] = 11;
            takenTag['t'] = 10;

            takenTag['n'] = 12;
        }

        public static void parserStart(JsonParameter jsonParameter)
        {
            skipSpace(jsonParameter);
        }

        /**
         * 是Json类型，就跳过当前字符
         *
         * @param jsonParameter
         * @return
         */
        public static JsonType getJsonType(JsonParameter jsonParameter)
        {
            int iTag = JsonTypeTag[jsonParameter.buffer[jsonParameter.index]];
            return jsonTypes[iTag];
        }

        /**
         * 是Json类型，就跳过当前字符
         *
         * @param jsonParameter
         * @return
         */
        public static int getInternalJsonType(JsonParameter jsonParameter)
        {
            return JsonTypeTag[jsonParameter.buffer[jsonParameter.index]];
        }

        /**
         * 跳过空格、回车、换行
         *
         * @param jsonParameter
         */
        public static void skipSpace(JsonParameter jsonParameter)
        {
            char c;
            do
            {
                c = jsonParameter.buffer[jsonParameter.index];
                if (c > 33) break;
                if (spaceTag[c] == 0) break;
                jsonParameter.index++;
            } while (jsonParameter.index < jsonParameter.length);
        }

        public static void skipSpaceOfEnd(JsonParameter jsonParameter)
        {
            char c;
            while (jsonParameter.index < jsonParameter.length)
            {
                c = jsonParameter.buffer[jsonParameter.index];
                if (c > 33) break;
                if (spaceTag[c] == 0) break;
                jsonParameter.index++;
            }
        }

        /**
         * 下一个字符
         *
         * @param jsonParameter
         */
        public static void nextChar(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
        }

        /**
         * 回上一个字符
         *
         * @param jsonParameter
         */
        public static void previousChar(JsonParameter jsonParameter)
        {
            jsonParameter.index--;
        }

        /**
         * 跳过对象中键与值间的空格与冒号
         *
         * @param jsonParameter
         */
        public static void skipObjectKeyToValueSpace(JsonParameter jsonParameter)
        {
            char c;
            do
            {
                c = jsonParameter.buffer[jsonParameter.index];
                if (c > 58) break;
                if (objectKVSpaceTag[c] == 0) break;
                jsonParameter.index++;
            } while (jsonParameter.index < jsonParameter.length);
        }

        /**
         * 跳过对象单的空格与逗号
         *
         * @param jsonParameter
         */
        public static void skipObjectSpace(JsonParameter jsonParameter)
        {
            char c;
            while (jsonParameter.index < jsonParameter.length)
            {
                c = jsonParameter.buffer[jsonParameter.index];
                if (c > 44) break;
                if (objectVVSpaceTag[c] == 0) break;
                jsonParameter.index++;
            }
        }

        /**
         * 下一个对象结点
         * 自动对象间的空格
         *
         * @param jsonParameter
         * @return
         */
        public static bool hasNonObjectNextNode(JsonParameter jsonParameter)
        {
            skipObjectSpace(jsonParameter);
            return jsonParameter.buffer[jsonParameter.index] == '}';
        }

        /**
         * 对象是否结束
         *
         * @param jsonParameter
         * @return
         */
        public static bool isObjectEnd(JsonParameter jsonParameter)
        {
            return jsonParameter.buffer[jsonParameter.index] == '}';
        }

        /**
         * 对象是否结束
         *
         * @param c
         * @return
         */
        public static bool isObjectEnd(char c)
        {
            return c == '}';
        }

        /**
         * 下一个数组值
         * 自动数组元素间的空格
         *
         * @param jsonParameter
         * @return
         */
        public static bool hasNonArrayNextNode(JsonParameter jsonParameter)
        {
            skipArraySpace(jsonParameter);
            return jsonParameter.buffer[jsonParameter.index] == ']';
        }

        /**
         * 数组是否结束
         *
         * @param jsonParameter
         * @return
         */
        public static bool isArrayEnd(JsonParameter jsonParameter)
        {
            return jsonParameter.buffer[jsonParameter.index] == ']';
        }

        /**
         * 跳过数组间空格与分隔符,
         *
         * @param jsonParameter
         */
        public static void skipArraySpace(JsonParameter jsonParameter)
        {
            char c;
            while (jsonParameter.index < jsonParameter.length)
            {
                c = jsonParameter.buffer[jsonParameter.index];
                if (c > 44) break;
                if (arrayVVSpaceTag[c] == 0) break;
                jsonParameter.index++;
            }
        }


        /**
         * 数字字符串是否结束
         *
         * @param jsonParameter
         * @return
         */
        public static bool isNumberEnd(JsonParameter jsonParameter)
        {
            char c = jsonParameter.buffer[jsonParameter.index];
            return c == ',' || c == '}' || c == ']' || c == ' ' || c == '\t' || c == '\n' || c == '\r';
        }

        /**
         * 数字字符串是否结束
         *
         * @param c
         * @return
         */
        public static bool isNumberEnd(char c)
        {
            return c == ',' || c == '}' || c == ']' || c == ' ' || c == '\t' || c == '\n' || c == '\r';
        }

        /**
         * 未结束
         *
         * @param jsonParameter
         * @return
         */
        public static bool isNonEnd(JsonParameter jsonParameter)
        {
            return jsonParameter.index < jsonParameter.length;
        }

        /**
         * 未结束并指下一个字符
         *
         * @param jsonParameter
         * @return
         */
        public static bool isNonEndAndNextChar(JsonParameter jsonParameter)
        {
            if (jsonParameter.index < jsonParameter.length)
            {
                jsonParameter.index++;
                return true;
            }
            else
                return false;
        } 
    }
}
