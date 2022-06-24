﻿using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Role RoleNavigation { get; set; }
    }
}
