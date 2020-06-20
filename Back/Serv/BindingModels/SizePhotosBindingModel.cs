using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public class SizePhotosBindingModel
    {

        /// <summary>
        /// Фронтальное фото
        /// </summary>
        public IFormFile PhotoFront { get; set; }

        /// <summary>
        /// Боковое фото
        /// </summary>

        public IFormFile PhotoSide { get; set; }


    }
}
