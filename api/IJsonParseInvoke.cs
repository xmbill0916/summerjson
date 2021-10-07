using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.api
{
    public interface IJsonParseInvoke
    {
        /**
    * 结点描述
    *
    * @param jsonPathDesc
    */
          void setJsonPathDesc(JsonPathDesc jsonPathDesc);

        /**
         * 开始解析对象前
         * 返回对象实例，默认为Map<String,Object>
         *
         * @param parentKey
         * @param descPath
         * @return
         */
        Object beforeParseObject(String parentKey, List<String> descPath);

        /**
         * 解析对象结点键值回调
         *
         * @param object
         * @param jsonType
         * @param key
         * @param value
         */
        void setObjectKeyValue(Object obj, JsonType jsonType, String key, Object value);

        /**
         * 解析对象完成之后
         *
         * @param parentKey
         * @param object
         * @param descPath
         */
        void afterParseObject(String parentKey, Object obj, List<String> descPath);

        /**
         * 开始解析数组前
         *
         * @param parentKey
         * @param descPath
         * @return
         */
        Object beforeParseArray(String parentKey, List<String> descPath);

        /**
         * 赋数组结点
         *
         * @param list
         * @param jsonType
         * @param index
         * @param value
         */
        void setArrayValue(Object list, JsonType jsonType, int index, Object value);

        /**
         * 解析数组完成之后
         *
         * @param parentKey
         * @param ary
         * @param descPath
         */
        void afterParseArray(String parentKey, Object ary, List<String> descPath);
    }
}
