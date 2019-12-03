﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Administrator
{
    public class AdministratorVM
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string IconString { get; set; }
        public byte[] Icon { get; set; }
        public Guid? IdAdmin { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
        public string User { get; set; }
    }
}
