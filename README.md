# summerjson
JSON parser/generator for C#
大部分的Json转化器，是直接转为指定的KV或List
而实际应用场景中可能需要转化为数据表、数据集、或特定结点可指定的类型；

summerjson 将解决如上问题，在转化过程可对json串结点指定的对象，这样带来使用的方便；
我们将json串转化为对象，分三步走
1，json按Json 规则解析各结点为字符串（字符与数值）、true、false、null；
   数值在解析阶段并不作转化，数值在转化时并未真正的数值类型，而是在对路径描述给对象赋值时进行精准转化
2，json路径类型描述（一个类型，一个类型赋值），Json串对象默认为Dictionary 数值默认为List
3，解析按路径描述进行转化

这样解析过程与赋值，以及对象类型分离，便于转化指定类型
降低Json串解析与指定对象以及赋值的偶合性，提高了转化器的灵活度



1,json 对象转为Dictionary 数组转化为List:
  JsonParameter jsonParameter= Json.NewJsonParameter(jsonStr);
  object obj=Json.ToObject(jsonParameter);
  
2,json 依据json 各结点描述转化
  Json.ToObject(JsonParameter jsonParameter, IJsonParseInvoke jsonParseInvoke)
  Json.ToObject(JsonParameter jsonParameter, JsonPathDesc jsonPathDesc)
  基本类型以外的结点才需要结点描述；
  参照例子
  2.1,json 数组或对象 指定类型 
            object obj = JsonPathDescExample.RootIsArrayToObject();
            obj = JsonPathDescExample.RootIsArrayToObjectOfCustomType();
            obj = JsonPathDescExample.RootIsArrayToArrayTypeObject();
            obj = JsonPathDescExample.RootIsArrayToObjectOfCustomType1();
  2.2,json 转 数据表 数据集
            object obj = JsonPathDescOfObjectExample.RootIsObjectToObject();
            obj = JsonPathDescOfObjectExample.RootIsObjectToDataTable();
            obj = JsonPathDescOfDataset.ToDataSetObject();
3,Json串路径描述类JsonPathDesc
  可参照例子来理解应用  
  
想法与实现一定存在不足，欢迎您宝贵意见！          
     397216371@
