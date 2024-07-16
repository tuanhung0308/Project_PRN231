using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PRN231_API.Models;
using Project_PRN231_API.ViewModel.Crop;

namespace Project_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly FarmManagement_PRN231Context _context;
        private readonly IMapper _mapper;

        public CropsController(FarmManagement_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var o = _context.Crops.ToList();
            var result = _mapper.Map<List<CropVM>>(o);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var o = _context.Crops.FirstOrDefault(x => x.CropId == id);
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                var result = _mapper.Map<CropVM>(o);
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult Add(CropVM cropVM)
        {
            if (string.IsNullOrEmpty(cropVM.CropName))
            {
                return BadRequest();
            }

            var crop = _mapper.Map<Crop>(cropVM);
            _context.Crops.Add(crop);
            _context.SaveChanges();
            return Ok(_mapper.Map<CropVM>(crop));
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(CropVM newCrop, int id)
        {
            var oldCrop = _context.Crops.FirstOrDefault(x => x.CropId == id);
            if(string.IsNullOrEmpty(newCrop.CropName) == false)
            {
                oldCrop.CropName = newCrop.CropName;
                oldCrop.PlantingDate = newCrop.PlantingDate;
                oldCrop.ActualHarvestDate = newCrop.ActualHarvestDate;
                oldCrop.ExpectedHarvestDate = newCrop.ExpectedHarvestDate;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var crop = _context.Crops.FirstOrDefault(x => x.CropId == id);
            if (crop == null)
            {
                return NotFound();
            }

            _context.Crops.Remove(crop);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
