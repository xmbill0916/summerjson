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
using System.Collections.Generic;
using System.Data;

namespace com.xmbill.json.api
{
    public class JsonPathDesc
    {
        private JsonPathDesc parent;
        private Dictionary<string, JsonPathDesc> children = new Dictionary<string, JsonPathDesc>();
        protected NodeDesc nodeDesc;
        /**
        * 根目录
        *
        * @param nodeDesc  ObjectNodeDesc or ArrayNodeDesc
        * @return
        */
        public static JsonPathDesc Root(NodeDesc nodeDesc)
        {
            JsonPathDesc jsonPathDesc = new JsonPathDesc();
            jsonPathDesc.nodeDesc = nodeDesc;
            return jsonPathDesc;
        }
        /**
         * 对象根目录
         *
         * @return
         */
        public static JsonPathDesc RootOfObject()
        {
            return Root(ObjectNodeDesc.DefaultObjectHandler);
        }

        /**
         * 数组根目录
         *
         * @return
         */
        public static JsonPathDesc RootOfArray()
        {
            return Root(ArrayNodeDesc.DefaultArrayHandler);
        }



        private JsonPathDesc newInstance(JsonPathDesc parentNode, string key, NodeDesc nodeDesc)
        {
            JsonPathDesc jsonPathDesc = new JsonPathDesc();
            jsonPathDesc.nodeDesc = nodeDesc;
            jsonPathDesc.parent = parentNode;
            parentNode.children.Add(key, jsonPathDesc);
            return jsonPathDesc;
        }

        /**
         * 结点值对应的描述
         * 返回子结点
         *
         * @param key
         * @param nodeDesc
         * @return
         */
        private JsonPathDesc AddChild(string key, NodeDesc nodeDesc)
        {
            return newInstance(this, key, nodeDesc);
        }
        /**
         * 对象结点对应的描述
         * 返回子结点
         *
         * @param childKey
         * @param childNodeDesc IObjectNodeDesc or IArrayNodeDesc
         * @return
         */
        private JsonPathDesc AddObjectChild(string childKey,NodeDesc childNodeDesc)
        {
            return newInstance(this, childKey, childNodeDesc);
        }
        /**
        * 对象结点对应的数组描述
        * 数组对象默认为ArrayList
        * @param childKey
        * @return
        */
        public JsonPathDesc AddObjectChildIsDefaultArray(string childKey)
        {
            return AddObjectChild(childKey, ArrayNodeDesc.DefaultArrayHandler);
        }
        /**
         * 对象默认结点描述
         * 注：在当结阶层未找到特点对象结点描述时，便使用此为默认结点技术
         * @param defaultNodeDesc ObjectNodeDesc or ArrayNodeDesc
         * @return
         */
        public JsonPathDesc AddObjectChildOfDefaultDesc(NodeDesc defaultNodeDesc)
        {
            if (defaultNodeDesc is ObjectNodeDesc)
                return AddObjectChild("*", defaultNodeDesc);
            else if (defaultNodeDesc is ArrayNodeDesc)
                return AddObjectChild("-", defaultNodeDesc);
            else
                return null;
        }
        /**
         * 对象结点对应的数组描述
         * 数组为自定义
         * @param childKey
         * @return
         */
        public JsonPathDesc AddObjectChildOfArray(string childKey, ArrayNodeDesc childNodeDesc)
        {
            return AddObjectChild(childKey, childNodeDesc);
        }


        /**
      * 对象结点对应的对象描述
      * @param childKey
      * @param childNodeDesc 
      * @return
      */
        public JsonPathDesc AddObjectChildOfObject(string childKey, ObjectNodeDesc childNodeDesc)
        {
            return newInstance(this, childKey, childNodeDesc);
        }

        /**
         * 数组值对应的描述
         *
         * @param nodeDesc
         * @return
         */
        public JsonPathDesc AddArrayElement(NodeDesc nodeDesc)
        {
            if (nodeDesc is ArrayNodeDesc)
                return AddChild("-", nodeDesc);
            else if (nodeDesc is ObjectNodeDesc)
                return AddChild("*", nodeDesc);
            else
                return null;
        }
        public JsonPathDesc AddArrayElement(int index,NodeDesc nodeDesc)
        {
            return AddChild(index.ToString(), nodeDesc);
        }
        public JsonPathDesc AddObjectChildDataTable(string key, DataTable dataTable)
        {
            return AddObjectChildDataTable(key, dataTable, ObjectNodeDesc.DataRowSetValueDefaultHandler);
        }
        public JsonPathDesc AddObjectChildDataTable(string key, DataTable dataTable, SetObjectValueHandler setDataRowValueHandler)
        {
            JsonPathDesc dataTableDesc = AddObjectChildOfArray(key, ArrayNodeDesc.DataTableHandler((object parentObj,string k)=> { return dataTable; }));
            dataTableDesc.AddArrayElement(ObjectNodeDesc.DataRowHandler(setDataRowValueHandler));
            return dataTableDesc;
        }


        public JsonPathDesc AddObjectChildDataTable(string key, NewInstanceHandler dataTableInstance, SetObjectValueHandler setDataRowValueHandler)
        {
            JsonPathDesc dataTableDesc = AddObjectChildOfArray(key, ArrayNodeDesc.DataTableHandler(dataTableInstance));
            dataTableDesc.AddArrayElement(ObjectNodeDesc.DataRowHandler(setDataRowValueHandler));
            return dataTableDesc;
        }

       
        public JsonPathDesc AddObjectChildOfDataSet(string key)
        { 
            return AddChild(key, ObjectNodeDesc.DataSetHandler);
        }
        public JsonPathDesc AddObjectChildOfDataSet(string key,DataSet dataSet)
        {
            return ObjectDescUtils.DataSetInstanceHandler(this, key, dataSet);
        }
        public JsonPathDesc AddObjectChildOfDataSet(string key, DataSet dataSet, SetObjectValueHandler dataRowSetValueHandler)
        {
            return ObjectDescUtils.DataSetInstanceHandler(this, key, dataSet, dataRowSetValueHandler);
        }
        /**
         * 键得到其值对应的描述
         *
         * @param key
         * @return
         */
        public JsonPathDesc GetChild(string key)
        {
            JsonPathDesc jsonPathDesc;
            children.TryGetValue(key,out jsonPathDesc);
            return jsonPathDesc;
        }

        /**
         * 按路径得到描述
         *
         * @param path
         * @return
         */
        public JsonPathDesc GetChild(List<string> path)
        {
            JsonPathDesc currentRoot = this; 
            for (int i = 1, iLen = path.Count; i < iLen; i++)
            {
                currentRoot.children.TryGetValue(path[i],out currentRoot);
                if (currentRoot == null)
                    return null;
            }
            if (currentRoot != this)
                return currentRoot;
            else
                return null;
        }

        /**
         * 父结点
         * @return
         */
        public JsonPathDesc GetParent()
        {
            return parent;
        }

        /**
         * 父结点的父结点
         * @return
         */
        public JsonPathDesc GetParentAndParent()
        {
            return parent.parent;
        }

        /**
         * 得到当前结点描述
         * @return
         */
        public NodeDesc GetNodeDesc()
        {
            return nodeDesc;
        }
         
        /**
         * 描述对应的实例
         * @return
         */
        public object NewInstance(object parentObj,string key)
        {
            return nodeDesc.NewInstance(parentObj,key);
        }

        /**
         * 数组对应的赋值回调
         * @param obj
         * @param jsonType
         * @param key
         * @param value
         */
        public void Set(object obj, JsonType jsonType, int key, object value)
        {
            ((ArrayNodeDesc)nodeDesc).ArraySetValueHandler(obj, jsonType, key, value);
        }

        /**
         * 对象赋值回调
         * @param obj
         * @param jsonType
         * @param key
         * @param value
         */
        public void Set(object obj, JsonType jsonType, string key, object value)
        {
            ((ObjectNodeDesc)nodeDesc).ObjectSetValueHandler(obj, jsonType, (string)key, value);
        }

    }
}
