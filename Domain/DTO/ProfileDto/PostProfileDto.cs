﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.ProfileDto
{
    public class PostProfileDto
    {
        public string UserId { get; set; } = string.Empty;
        public int Age { get; set; }
        public string City { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
