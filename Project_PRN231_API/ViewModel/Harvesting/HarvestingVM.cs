namespace Project_PRN231_API.ViewModel.Harvesting
{
    public class HarvestingVM
    {
        public int HarvestId { get; set; }
        public string CropName { get; set; }
        public DateTime HarvestDate { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;
    }
}
