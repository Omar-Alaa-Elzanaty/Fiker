using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.Dtos
{
    public class MediaFormFileDto
    {
        public string FileName { get; set; }
        public IFormFile Data { get; set; }
    }
}
