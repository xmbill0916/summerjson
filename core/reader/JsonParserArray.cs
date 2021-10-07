using com.xmbill.json.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.core.reader
{
    public class JsonParserArray:JsonParser
    {
        public static void parseArrayStart(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpace(jsonParameter);
        }

        public static void parseArrayEnd(JsonParameter jsonParameter)
        {
            jsonParameter.index++;
            skipSpaceOfEnd(jsonParameter);
        }

        public static Object parseArray(JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            return parseArray("", new List<String>(), jsonParameter, jsonValueInvoke);
        }

        public static Object parseArray(String parentKey, List<String> descPath, JsonParameter jsonParameter, IJsonParseInvoke jsonValueInvoke)
        {
            parseArrayStart(jsonParameter);
            bool isEnd = isArrayEnd(jsonParameter);
            descPath.Add(parentKey);
            Object array = jsonValueInvoke.beforeParseArray(parentKey, descPath);
            if (!isEnd)
            {
                Object value = null;
                int ijt;
                int index = 0;
                do
                {
                    ijt = getInternalJsonType(jsonParameter);
                    switch (ijt)
                    {
                        case 1:
                            {
                                value = JsonParserObject.parseObject("-", descPath, jsonParameter, jsonValueInvoke);
                                break;
                            }
                        case 2:
                            {
                                value = parseArray("-", descPath, jsonParameter, jsonValueInvoke);
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
                    jsonValueInvoke.setArrayValue(array, jsonTypes[ijt], index++, value);
                    if (hasNonArrayNextNode(jsonParameter))
                        break;
                } while (isNonEnd(jsonParameter));
            }
            parseArrayEnd(jsonParameter);
            jsonValueInvoke.afterParseArray(parentKey, array, descPath);
            descPath.RemoveAt(descPath.Count() - 1);
            return array;
        }

    }
}
