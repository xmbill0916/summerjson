using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.api
{
    public class JsonParameter
    {
        /**
  * 操作字符位置
  */
        public int index = 0;
        /**
         * 字符数组长度
         */
        public int length = 0;

        /**
         * Json字符
         */
        public char[] buffer;

        /**
         * 数组与对象的嵌套
         */
        // public char[] tokenStack;
        // public int stackIndex = -1;

        /**
         * 转义使用截取使用 位置变量
         * 内部操作
         */
        public int ePos = -1;
        /**
         * 字符时，没有转义前遍历过的偏移
         */
        public int offset = -1;
        /**
         * 字符临时对象
         */
        public StringBuilder sb = new StringBuilder(1024);

        protected JsonParameter()
        {

        }

        public void internalInit()
        {
            this.index = 0;
            this.ePos = -1;
            this.sb.Clear();
        }

        public static JsonParameter newInstance(char[] buffer)
        {
            return newInstance(buffer, buffer.Length);
        }

        public static JsonParameter newInstance(char[] buffer, int length)
        {
            JsonParameter jsonParameter = new JsonParameter();
            jsonParameter.internalInit();
            jsonParameter.buffer = buffer;
            jsonParameter.length = Math.Min(length, buffer.Length);
            return jsonParameter;
        }

        public static JsonParameter newInstance(char[] buffer, int start, int end)
        {
            JsonParameter jsonParameter = new JsonParameter();
            jsonParameter.internalInit();
            jsonParameter.index = start;
            jsonParameter.buffer = buffer;
            jsonParameter.length = Math.Min(buffer.Length, end);
            return jsonParameter;
        }

    }
}
