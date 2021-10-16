using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.xmbill.json.api
{
   public class ObjectDescUtils
    {

        public static JsonPathDesc DataSetInstanceHandler(DataSet dataset)
        {
            return DataSetInstanceHandler(dataset, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset">存在完整的数据结构实例</param>
        /// <param name="dataRowSetValueHandler">obj为DataRow实例 可通过 ((DataRow)obj).Table.TableName来区别是那个数据表 </param>
        /// <returns></returns>
        public static JsonPathDesc DataSetInstanceHandler(DataSet dataset, SetObjectValueHandler dataRowSetValueHandler)
        {
              return DataSetInstanceHandler(null, null, dataset, dataRowSetValueHandler);
        }
        public static JsonPathDesc DataSetInstanceHandler(JsonPathDesc parentJsonPathDesc, string key, DataSet dataset)
        {
            return DataSetInstanceHandler(parentJsonPathDesc, key, dataset, null);
        }
        public static JsonPathDesc DataSetInstanceHandler(JsonPathDesc parentJsonPathDesc,string key,DataSet dataset, SetObjectValueHandler dataRowSetValueHandler)
        {
            //dataset
            ObjectNodeDesc datasetObjectNodeDesc = new ObjectNodeDesc();
            datasetObjectNodeDesc.NewInstance = (object parentObj, string k) => { return dataset; };
            datasetObjectNodeDesc.ObjectSetValueHandler = (object obj, JsonType jsonType, string k, object value) => { };
            JsonPathDesc jsonPathDesc,root;
            if (parentJsonPathDesc == null)
            {
                jsonPathDesc = JsonPathDesc.Root(datasetObjectNodeDesc);
                root = jsonPathDesc;
            }
            else
            {
                jsonPathDesc = parentJsonPathDesc.AddObjectChildOfObject(key, datasetObjectNodeDesc);
                root = parentJsonPathDesc;
            }

            JsonPathDesc tmpRoot;

            foreach (DataTable table in dataset.Tables)
            {
                //datatable 
                ArrayNodeDesc arrayNodeDesc = new ArrayNodeDesc();
                arrayNodeDesc.ArraySetValueHandler = (object obj, JsonType jsonType, int k, object value) => { ((DataTable)obj).Rows.Add((DataRow)value); };
                arrayNodeDesc.NewInstance = (object parentObj, string k) => { return ((DataSet)parentObj).Tables[k]; };
                tmpRoot = jsonPathDesc.AddObjectChildOfArray(table.TableName, arrayNodeDesc);

                //datarow
                ObjectNodeDesc objectNodeDesc = new ObjectNodeDesc();
                if (dataRowSetValueHandler == null)
                    dataRowSetValueHandler = ObjectNodeDesc.DataRowSetValueDefaultHandler;
                objectNodeDesc.ObjectSetValueHandler = dataRowSetValueHandler;
                objectNodeDesc.NewInstance = (object parentObj, string k) => { return ((DataTable)parentObj).NewRow(); };

                tmpRoot.AddArrayElement(objectNodeDesc);
            }
            return root;
        }

    }
}
