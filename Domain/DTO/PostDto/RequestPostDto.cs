﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.PostDto
{
    public class RequestPostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
