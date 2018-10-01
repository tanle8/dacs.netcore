using System;
using System.Collections.Generic;

namespace MovieApp.Entities
{
    public partial class Ethereumwithdrawtransaction
    {
        public string Id { get; set; }
        public string Hash { get; set; }
        public string BlockNumber { get; set; }
        public string NetworkName { get; set; }
        public decimal Amount { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Fee { get; set; }
        public string Status { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public int InProcess { get; set; }
        public int Version { get; set; }
    }
}
