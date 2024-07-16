using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PRN231_API.Models;
using Project_PRN231_API.ViewModel.Storage;

namespace Project_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly FarmManagement_PRN231Context _context;
        private readonly IMapper _mapper;

        public StorageController(FarmManagement_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/storage
        [HttpGet]
        public IActionResult GetStorage()
        {
            var storage = _context.Storages.ToList();
            var storageViewModels = _mapper.Map<List<StorageViewModel>>(storage);
            return Ok(storageViewModels);
        }

        // GET: api/storage/5
        [HttpGet("{id}")]
        public IActionResult GetStorage(int id)
        {
            var storage = _context.Storages.Find(id);

            if (storage == null)
            {
                return NotFound();
            }

            var storageViewModel = _mapper.Map<StorageViewModel>(storage);
            return Ok(storageViewModel);
        }

        // POST: api/storage
        [HttpPost]
        public IActionResult PostStorage(StorageViewModel storageViewModel)
        {
            var storage = _mapper.Map<Storage>(storageViewModel);
            _context.Storages.Add(storage);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStorage), new { id = storage.StorageId }, storageViewModel);
        }

        // PUT: api/storage/5
        [HttpPut("{id}")]
        public IActionResult PutStorage(int id, StorageViewModel storageViewModel)
        {
            if (id != storageViewModel.StorageId)
            {
                return BadRequest();
            }

            var storage = _mapper.Map<Storage>(storageViewModel);
            _context.Entry(storage).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/storage/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStorage(int id)
        {
            var storage = _context.Storages.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            _context.Storages.Remove(storage);
            _context.SaveChanges();
            return NoContent();
        }

        private bool StorageExists(int id)
        {
            return _context.Storages.Any(e => e.StorageId == id);
        }
    }
}
