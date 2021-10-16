using com.xmbill.json.api;
using com.xmbill.json.core;
using com.xmbill.json.core.reader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.xmbill.sample.DescDemo
{

    public class JsonPathDescOfDataset
    {
        private static string snOfRoot = "{\"t1\":[{\"billno\":20211016,\"customer\":\"ms corp.微软公司\"}],\"t2\":[{\"sid\":1,\"pid\":\"p0001\",\"pname\":\"windown操作系统\",\"price\":50000,\"quantity\":10,\"amount\":500000,\"memo\":\"已收款\"},{\"sid\":2,\"pid\":\"p0001\",\"pname\":\"office\",\"price\":2000,\"quantity\":10,\"amount\":20000,\"memo\":\"已收款\"}],\"t3\":[{\"sid\":1,\"psid\":1,\"sn\":1234567899},{\"sid\":2,\"psid\":2,\"sn\":6655445566},{\"sid\":3,\"psid\":1,\"sn\":22335566},{\"sid\":4,\"psid\":2,\"sn\":556666444545}]}";
        public static object ToDataSetObject()
        {
            JsonParameter jsonParameter= Json.NewJsonParameter(snOfRoot);
            DataSet dataSet = new DataSet();

            DataTable t1 = new DataTable("t1");
            t1.Columns.Add("billno", typeof(decimal));
            t1.Columns.Add("customer", typeof(string));
            dataSet.Tables.Add(t1);

            DataTable t2 = new DataTable("t2");
            t2.Columns.Add("sid", typeof(decimal));
            t2.Columns.Add("pid", typeof(string));
            t2.Columns.Add("pname", typeof(string));
            t2.Columns.Add("price", typeof(decimal));
            t2.Columns.Add("quantity", typeof(decimal));
            t2.Columns.Add("amount", typeof(decimal));
            t2.Columns.Add("memo", typeof(string));
            dataSet.Tables.Add(t2);

            DataTable t3 = new DataTable("t3");
            dataSet.Tables.Add(t3);
            t3.Columns.Add("sid", typeof(decimal));
            t3.Columns.Add("psid", typeof(decimal));
            t3.Columns.Add("sn1", typeof(decimal));

            //json 与结构 关系描述
            JsonPathDesc root;
            //root = ObjectDescUtils.DataSetInstanceHandler(dataSet, (object obj, JsonType jsonType, string key, object value) =>
            //{
            //    if (jsonType == JsonType.jtNumber)
            //        value = JsonReaderUtils.ConvertValue(jsonType, value, typeof(decimal));
            //    ((DataRow)obj)[key] = value;
            //});
            root = ObjectDescUtils.DataSetInstanceHandler(dataSet);


            Json.ToObject(jsonParameter, root);

            return dataSet;

        }
    }
}
