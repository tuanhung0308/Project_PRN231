using System;
using System.Collections.Generic;

namespace Project_PRN231_API.Models
{
    public partial class Harvesting
    {
        public Harvesting()
        {
            Storages = new HashSet<Storage>();
        }

        public int HarvestId { get; set; }
        public int? CropId { get; set; }
        public DateTime HarvestDate { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;

        public virtual Crop? Crop { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
    }
}
