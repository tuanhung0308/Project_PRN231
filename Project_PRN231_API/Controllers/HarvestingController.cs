using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Project_PRN231_API.Models;
using Project_PRN231_API.ViewModel.Harvesting;

namespace Project_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestingController : ControllerBase
    {
        private readonly FarmManagement_PRN231Context _context;
        private readonly IMapper _mapper;

        public HarvestingController(FarmManagement_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var harvestings = _context.Harvestings.Include(x=>x.Crop).ToList().AsQueryable();
            var result = _mapper.Map<List<HarvestingVM>>(harvestings);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var harvesting = _context.Harvestings.Include(x=>x.Crop).FirstOrDefault(x => x.HarvestId == id);
            if (harvesting == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<HarvestingVM>(harvesting);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(HarvestingVM harvestingVM)
        {
            if (harvestingVM == null)
            {
                return BadRequest("HarvestingVM is null");
            }

            var harvesting = _mapper.Map<Harvesting>(harvestingVM);
            _context.Harvestings.Add(harvesting);
            _context.SaveChanges();

            var result = _mapper.Map<HarvestingVM>(_context.Harvestings.FirstOrDefault(h => h.HarvestId == harvesting.HarvestId));
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(HarvestingVM newHarvesting, int id)
        {
            var oldHarvesting = _context.Harvestings.FirstOrDefault(x => x.HarvestId == id);
            if (oldHarvesting == null)
            {
                return NotFound();
            }

            oldHarvesting.HarvestDate = newHarvesting.HarvestDate;
            oldHarvesting.Quantity = newHarvesting.Quantity;
            oldHarvesting.Unit = newHarvesting.Unit;

            var result = _mapper.Map<HarvestingVM>(_context.Harvestings.FirstOrDefault(h => h.HarvestId == oldHarvesting.HarvestId));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var harvesting = _context.Harvestings.FirstOrDefault(x => x.HarvestId == id);
            if (harvesting == null)
            {
                return NotFound();
            }

            _context.Harvestings.Remove(harvesting);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
