using com.xmbill.json.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.core.reader
{
    public class JsonParserObject:JsonParser
    {
        /**
   * 解析对象开始
   *
   * @param jsonParameter
   */
        public static void parseObjectStart(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpace(jsonParameter);
        }

        /**
         * 解析对象结束
         * 到下一个起启点
         *
         * @param jsonParameter
         */
        public static void parseObjectEnd(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpaceOfEnd(jsonParameter);
        }

        public static Object parseObject(JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            return parseObject("", new List<string>(), jsonParameter, jsonValueInvoke);
        }

        public static Object parseObject(String parentKey, List<string> descPath, JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            parseObjectStart(jsonParameter);
            bool isEnd = isObjectEnd(jsonParameter);
            descPath.Add(parentKey);
            Object obj = jsonValueInvoke.beforeParseObject(parentKey, descPath);
            if (!isEnd)
            {
                String key;
                Object value = null;
                int ijt;
                do
                {
                    key = getObjectKeyAndSkipToValue(jsonParameter);
                    ijt = getInternalJsonType(jsonParameter);
                    switch (ijt)
                    {
                        case 1:
                            {
                                value = parseObject(key, descPath, jsonParameter, jsonValueInvoke);
                                break;
                            }
                        case 2:
                            {
                                value = JsonParserArray.parseArray(key, descPath, jsonParameter, jsonValueInvoke);
                                break;
                            }
                        case 3:
                            {
                                value = getStringOfString(jsonParameter);
                                break;
                            }
                        case 4:
                            {
                                value = getNumberOfString(jsonParameter);
                                break;
                            }
                        case 5:
                            {
                                getTrueValue(jsonParameter);
                                value = true;
                                break;
                            }
                        case 6:
                            {
                                getFalseValue(jsonParameter);
                                value = false;
                                break;
                            }
                        case 7:
                            {
                                getNullValue(jsonParameter);
                                value = null;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    jsonValueInvoke.setObjectKeyValue(obj, jsonTypes[ijt], key, value);
                    if (hasNonObjectNextNode(jsonParameter))
                        break;
                } while (isNonEnd(jsonParameter));
            }
            parseObjectEnd(jsonParameter);
            jsonValueInvoke.afterParseObject(parentKey, obj, descPath);
            descPath.RemoveAt(descPath.Count() - 1);
            return obj;

        }
    }
}
