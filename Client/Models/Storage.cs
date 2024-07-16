using System;
using System.Collections.Generic;

namespace Project_PRN231_Client.Models
{
    public partial class Storage
    {
        public int StorageId { get; set; }
        public int? HarvestId { get; set; }
        public string StorageLocation { get; set; } = null!;
        public DateTime StorageDate { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public string Condition { get; set; } = null!;

        public virtual Harvesting? Harvest { get; set; }
    }
}
