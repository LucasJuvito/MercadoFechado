using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Responses
{
    class BaseResponse
    {
        public bool Success = false;
        public string Message = "Unknown error";

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
