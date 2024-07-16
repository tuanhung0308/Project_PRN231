using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_PRN231_API.Models;
using Project_PRN231_API.ViewModel.CareProcess;
using Project_PRN231_API.ViewModel.Crop;

namespace Project_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareProcessController : ControllerBase
    {
        private readonly FarmManagement_PRN231Context _context;
        private readonly IMapper _mapper;

        public CareProcessController(FarmManagement_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var o = _context.CareProcesses.Include(x => x.Crop).ToList().AsQueryable();
            var result = _mapper.Map<List<CareProcessVM>>(o);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult GetById(int id)
        {
            var o = _context.CareProcesses.Include(x => x.Crop).FirstOrDefault(x => x.CareProcessId == id);
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                var result = _mapper.Map<CareProcessVM>(o);
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult Add(CareProcessVM careProcessVM)
        {
            if (careProcessVM == null)
            {
                return BadRequest("CareProcessVM is null");
            }
            var crop = _context.Crops.FirstOrDefault(c => c.CropId == careProcessVM.CropId);
            if (crop == null)
            {
                return BadRequest("Invalid CropId");
            }
            var careProcess = _mapper.Map<CareProcess>(careProcessVM);
            careProcess.Crop = crop;

            _context.CareProcesses.Add(careProcess);
            _context.SaveChanges();
            return Ok(_mapper.Map<CareProcessVM>(careProcess));
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(CareProcessVM newCareProcess, int id)
        {
            var oldCareProcess = _context.CareProcesses.FirstOrDefault(x => x.CareProcessId == id);
            if (oldCareProcess == null)
            {
                return NotFound();
            }

            oldCareProcess.Description = newCareProcess.Description;
            oldCareProcess.StartDate = newCareProcess.StartDate;
            oldCareProcess.EndDate = newCareProcess.EndDate;
            oldCareProcess.PerformedBy = newCareProcess.PerformedBy;

            // Updating CropName (if applicable)
            var crop = _context.Crops.FirstOrDefault(c => c.CropId == oldCareProcess.CropId);
            if (crop != null && !string.IsNullOrEmpty(newCareProcess.CropName))
            {
                crop.CropName = newCareProcess.CropName;
            }

            _context.SaveChanges();
            return Ok(_mapper.Map<CareProcessVM>(oldCareProcess));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var careProcess = _context.CareProcesses.FirstOrDefault(x => x.CareProcessId == id);
            if (careProcess == null)
            {
                return NotFound();
            }
            _context.CareProcesses.Remove(careProcess);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
