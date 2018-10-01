using System;
using System.Collections.Generic;

namespace MovieApp.Entities
{
    public partial class Bitcoinaddress
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public int? CreatedAt { get; set; }
        public int? UpdatedAt { get; set; }
        public string WalletId { get; set; }
    }
}
