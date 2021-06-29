﻿using System;
using System.Collections.Generic;
using System.Text;

namespace C2.Globalization
{
    public class AuthorInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public AuthorInfo()
        {
        }

        public AuthorInfo(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
