﻿using com.xmbill.json.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.api
{
    public interface IArrayNodeDesc:INodeDesc
    {
        void set(Object obj, JsonType jsonType, int key, Object value);
    }
}
