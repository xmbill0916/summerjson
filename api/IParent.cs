using System;
using System.Collections.Generic;
using System.Text;

namespace com.xmbill.json.api
{
    public interface IParent
    {
        void SetParent(object parent);

        object GetParent();
    }

}
