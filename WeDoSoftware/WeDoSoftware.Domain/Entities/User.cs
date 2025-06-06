﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDoSoftware.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }               
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Training> Trainings { get; set; } = new List<Training>();
    }
}
