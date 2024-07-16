namespace Project_PRN231_API.ViewModel.Crop
{
    public class CropVM
    {
        public int CropId { get; set; }
        public string CropName { get; set; } = null!;
        public DateTime PlantingDate { get; set; }
        public DateTime ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
    }
}
