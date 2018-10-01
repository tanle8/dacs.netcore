using System;
using System.Collections.Generic;

namespace MovieApp.Entities
{
    public partial class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string SecondPassword { get; set; }
        public string IpWhiteList { get; set; }
        public string Status { get; set; }
    }
}
