using System;
using System.Collections.Generic;

namespace BloodBankMgmt.Models
{
    public partial class TotalBlood
    {
        public int? DonorId { get; set; }
        public int? ReceiverId { get; set; }
        public int? Unit { get; set; }

        public virtual Donor Donor { get; set; }
        public virtual Receiver Receiver { get; set; }
    }
}
