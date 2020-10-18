using System;
using System.Collections.Generic;

namespace BloodBankMgmt.Models
{
    public partial class Receiver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNo { get; set; }
        public string BloodGroup { get; set; }
    }
}
