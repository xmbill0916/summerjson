using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.api
{
    public interface IObjectNodeDesc:INodeDesc
    {
        void set(Object obj, JsonType jsonType, String key, Object value);
    }
}
