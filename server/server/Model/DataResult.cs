using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class DataResult
    {
        //数据返回结果：0正确，1，错误
        public int result = 0;
        public string resean = "未知错误";
        public object data;
    }
}
