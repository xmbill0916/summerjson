using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.json.api
{
    public interface IJsonUnSerialization
    {
        void set(JsonType jsonType, String key, Object value);
    }
}
