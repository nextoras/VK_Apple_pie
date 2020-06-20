using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public class GetVkUserBindingModel
    {

        public long user_id { get; set; }

        public string access_token { get; set; }


    }
}
