using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xmbill.json.api
{
   public interface IJsonObjectWriter
    {
        void toJson(JsonWriter jsonWriter, Object value, Type clazz);
    }
}
