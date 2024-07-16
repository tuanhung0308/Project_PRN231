namespace Project_PRN231_API.ViewModel.CareProcess
{
    public class CareProcessVM
    {
        public int CareProcessId { get; set; }
        public int CropId { get; set; }
        public string? CropName { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? PerformedBy { get; set; }
    }
}
