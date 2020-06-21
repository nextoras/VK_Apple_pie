using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public class UserPhotosBindingModel
    {
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// Фронтальное фото
        /// </summary>
        public IFormFile PhotoFront { get; set; }

        public IFormFile PhotoSide { get; set; }

        public long Height { get; set; }

        
    }
}
