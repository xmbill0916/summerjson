using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.core
{
    public enum JsonType
    {
        /**
    * 不是对象
    */
        jtNone,
        /**
         * 对象类型
         */
        jtObject,
        /**
         * 数组类型
         */
        jtArray,
        /**
         * 字符类型
         */
        jtString,
        /**
         * 数组类型
         */
        jtNumber,
        /**
         * 逻辑 true类型
         */
        jtTrue,
        /**
         * 逻辑 false类型
         */
        jtFalse,
        /**
         * null类型
         */
        jtNull

    }

}
