using Project_PRN231_Client.Models;
namespace Project_PRN231_Client.Models
{
    public partial class CareProcess
    {
        public int CareProcessId { get; set; }
        public int? CropId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? PerformedBy { get; set; }

        public virtual Crop? Crop { get; set; }
        public virtual User? PerformedByNavigation { get; set; }
    }
}
