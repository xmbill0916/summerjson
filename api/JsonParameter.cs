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
            this.sb.Length=0;
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
