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

using com.xmbill.json.core;
using com.xmbill.json.core.reader;
using System;
using System.Text;

namespace com.xmbill.json.api
{
    public delegate object NewInstanceHandler(object parentObj,string key);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="jsonType"></param>
    /// <param name="key"></param>
    /// <param name="value"> jsonType jsNumber 时 value 还是数字字符串 须使用JsonReaderUtils.ConvertValue 指定转化的数值类型</param>
    public delegate void SetObjectValueHandler(object obj, JsonType jsonType, string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="jsonType"></param>
    /// <param name="key"></param>
    /// <param name="value"> jsonType jsNumber 时 value 还是数字字符串 须使用JsonReaderUtils.ConvertValue 指定转化的数值类型</param>
    public delegate void SetArrayValueHandler(object obj, JsonType jsonType, int key, object value);
    public class Json
    {
        public static JsonParameter NewJsonParameter(string jsonString)
        {
            return JsonParameter.newInstance(jsonString.ToCharArray());
        }

        public static JsonParameter NewJsonParameter(char[] jsonCharArray)
        {
            return JsonParameter.newInstance(jsonCharArray);
        }

        public static JsonParameter NewJsonParameter(char[] jsonCharArray, int length)
        {
            return JsonParameter.newInstance(jsonCharArray, length);
        }

        public static JsonParameter NewJsonParameter(char[] jsonCharArray, int start, int end)
        {
            return JsonParameter.newInstance(jsonCharArray, start, end);
        }

        public static object ToObject(JsonParameter jsonParameter)
        {
            return ToObject(jsonParameter, new DefaultInvokeParse());
        }


        public static object ToObject(JsonParameter jsonParameter, IJsonParseInvoke jsonParseInvoke)
        {
            return JsonUtils.parse(jsonParameter, jsonParseInvoke);
        }

        public static object ToObject(JsonParameter jsonParameter, JsonPathDesc jsonPathDesc)
        {
            return JsonUtils.parse(jsonParameter, DescInvokeParse.newInstance(jsonPathDesc));
        }
        public static void ToJson(object obj, StringBuilder jsonWriter)
        {
            ToJson(obj, jsonWriter, null);
        }

        public static void ToJson(object obj, StringBuilder jsonWriter, IJsonObjectWriter jsonObjectWriter)
        {
            JsonUtils.write(jsonWriter, obj, jsonObjectWriter);
        }
    }
}
