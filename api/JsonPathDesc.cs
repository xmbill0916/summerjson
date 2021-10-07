using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.api
{
    public class JsonPathDesc
    {
        private JsonPathDesc parent;
        private   Dictionary<String, JsonPathDesc> children = new Dictionary<string, JsonPathDesc>();
        private INodeDesc nodeDesc;

        /**
         * 对象根目录
         *
         * @return
         */
        public static JsonPathDesc rootOfObject()
        {
            return root(new IObjectNodeDesc() { 
            public void set(Object obj, JsonType jsonType, String key, Object value)
            {
                ((Dictionary)obj).put(key, value);
            }
             
                public Object newInstance()
            {
                return new HashMap<>();
            }
        });
    }

    /**
     * 数组根目录
     *
     * @return
     */
    public static JsonPathDesc rootOfArray()
    {
        return root(()=> { 
            public void set(Object obj, JsonType jsonType, int key, Object value)
        {
            ((List<Object>)obj).Add(value);
        } 
            public Object newInstance()
        {
            return new List<Object>();
        }
    });
    }

/**
 * 根目录
 *
 * @param nodeDesc
 * @return
 */
public static JsonPathDesc root(INodeDesc nodeDesc)
{
    JsonPathDesc jsonPathDesc = new JsonPathDesc();
    jsonPathDesc.nodeDesc = nodeDesc;
    return jsonPathDesc;
}

private JsonPathDesc newInstance(JsonPathDesc parentNode, String key, INodeDesc nodeDesc)
{
    JsonPathDesc jsonPathDesc = new JsonPathDesc();
    jsonPathDesc.nodeDesc = nodeDesc;
    jsonPathDesc.parent = parentNode;
    parentNode.children.put(key, jsonPathDesc);
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
public JsonPathDesc addChild(String key, INodeDesc nodeDesc)
{
    return newInstance(this, key, nodeDesc);
}

/**
 * 结点值对应的描述
 * 返回父结点
 *
 * @param key
 * @param nodeDesc
 * @return
 */
public JsonPathDesc addChildOfReturnParent(String key, INodeDesc nodeDesc)
{
    newInstance(this, key, nodeDesc);
    return this;
}

/**
 * 结点对应的值是数组
 *
 * @param key
 * @return
 */
public JsonPathDesc addChildOfArray(String key)
{
    return addChild(key, new IArrayNodeDesc() {
            @Override
            public void set(Object obj, JsonType jsonType, int key, Object value)
    {
        ((List)obj).add(value);
    }

    @Override
            public Object newInstance()
    {
        return new ArrayList<>();
    }
});
    }

    /**
     * 数组值对应的描述
     *
     * @param nodeDesc
     * @return
     */
    public JsonPathDesc addChildOfArrayValue(INodeDesc nodeDesc)
{
    return addChild("-", nodeDesc);
}

/**
 * 键是可变的字符串，
 * 值是指定的对象
 *
 * @param nodeDesc
 * @return
 */
public JsonPathDesc addChildOfMapValue(INodeDesc nodeDesc)
{
    return addChild("*", nodeDesc);
}

/**
 * 特定键
 * 键值描述
 * 没找到，则找addChildOfMapValue对应的键值描述
 *
 * @param key
 * @param nodeDesc
 * @return
 */
public JsonPathDesc addChildOfMapValue(String key, INodeDesc nodeDesc)
{
    return addChild(key, nodeDesc);
}

/**
 * 键得到其值对应的描述
 *
 * @param key
 * @return
 */
public JsonPathDesc getChild(String key)
{
    return children.get(key);
}

/**
 * 按路径得到描述
 *
 * @param path
 * @return
 */
public JsonPathDesc getChild(List<String> path)
{
    JsonPathDesc currentRoot = this;
    for (int i = 1, iLen = path.size(); i < iLen; i++)
    {
        currentRoot = currentRoot.children.get(path.get(i));
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
public JsonPathDesc getParent()
{
    return parent;
}

/**
 * 父结点的父结点
 * @return
 */
public JsonPathDesc getParentAndParent()
{
    return parent.parent;
}

/**
 * 得到当前结点描述
 * @return
 */
public INodeDesc getNodeDesc()
{
    return nodeDesc;
}

/**
 * 描述对应的实例
 * @return
 */
public Object newInstance()
{
    return nodeDesc.newInstance();
}

/**
 * 数组对应的赋值回调
 * @param obj
 * @param jsonType
 * @param key
 * @param value
 */
public void set(Object obj, JsonType jsonType, int key, Object value)
{
    ((IArrayNodeDesc)nodeDesc).set(obj, jsonType, key, value);
}

/**
 * 对象赋值回调
 * @param obj
 * @param jsonType
 * @param key
 * @param value
 */
public void set(Object obj, JsonType jsonType, String key, Object value)
{
    ((IObjectNodeDesc)nodeDesc).set(obj, jsonType, (String)key, value);
}

    }
}
