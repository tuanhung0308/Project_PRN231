using System;
using System.Collections.Generic;

namespace Project_PRN231_API.Models
{
    public partial class Crop
    {
        public Crop()
        {
            CareProcesses = new HashSet<CareProcess>();
            Harvestings = new HashSet<Harvesting>();
        }

        public int CropId { get; set; }
        public string CropName { get; set; } = null!;
        public DateTime PlantingDate { get; set; }
        public DateTime ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }

        public virtual ICollection<CareProcess> CareProcesses { get; set; }
        public virtual ICollection<Harvesting> Harvestings { get; set; }
    }
}
