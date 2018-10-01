﻿using System;
using System.Collections.Generic;

namespace MovieApp.Entities
{
    public partial class Wallet
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal? Balance { get; set; }
        public string NetworkName { get; set; }
        public string Address { get; set; }
        public int? CreatedAt { get; set; }
        public int? UpdatedAt { get; set; }
        public int? Version { get; set; }
    }
}
