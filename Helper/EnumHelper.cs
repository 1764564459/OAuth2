using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Helper
{
    /// <summary>
    /// 获取枚举键、值
    /// </summary>
    public class EnumHelper
    {

        public IEnumerable<EnumReault> Get<T>(T enum_item)
        {
            List<EnumReault> enum_list = new List<EnumReault>();
            var values = Enum.GetValues(typeof(T));
            foreach (var item in values)
            {
                enum_list.Add(new EnumReault { text = Enum.GetName(typeof(T), item), value =item });
            }
            return enum_list;
        }
    }
    public class EnumReault
    {
        public string text { get; set; }

        public object value { get; set; }
    }
}
