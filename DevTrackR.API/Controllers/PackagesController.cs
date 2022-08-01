using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistence;
using DevTrackR.API.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTrackR.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _repository;
        public PackagesController(IPackageRepository repository)
        {
            _repository = repository;
        }

        // GET "api/packages"
        [HttpGet]
        public IActionResult GetAll() {

            /*
            // Exemplo que está retornando algumas informações..
            // para testar API sem utilizar um BD. 

            var packages = new List<Package> {
                new Package("Pacote 1", 1.3M),
                new Package("Pacote 2", 0.2M)
            };

            return Ok(packages);
            */

            var packages = _repository.GetAll();

            return Ok(packages);
        }

         // GET "api/packages/ (code)"
         // EX: api/packages/1234-f123-e3214-r0123/
        [HttpGet("{code}")]
        public IActionResult GetByCode(string code) {

            /*
            // Exemplo que está retornando algumas informações.. 
            // para testar API sem utilizar um BD.

            var package = new Package("Pacote 2", 0.2M);

            return Ok(package);
            */

            var package = _repository.GetByCode(code);
                
                // _context
                // .Packages
                // .Include(p => p.Updates)
                // .SingleOrDefault(p => p.Code == code);

            if (package == null) {
                return NotFound();
            }

            return Ok(package);
        }

        // POST "api/packages"
        [HttpPost]
        public IActionResult Post(AddPackageInputModel model) {

            if (model.Title.Length < 10) {
                return BadRequest("Title length must be at least 10 characters long.");
            }

            var package = new Package(model.Title, model.Weight);

            _repository.Add(package);
            

            // _context.Packages.Add(package);
            // _context.SaveChanges();
            
            return CreatedAtAction(
                   "GetByCode", 
                   new { code = package.Code },
                   package);
        }

        // POST para Update "api/packages"
        // EX: api/packages/1234-f123-e3214-r0123/updates/
        [HttpPost("{code}/updates")]

        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model) {
            
            // Exemplo que está retornando algumas informações.. 
            // para testar API sem utilizar um BD.

            //var package = new Package("Pacote 01", 1.2M);

            var package = _repository.GetByCode(code);
            
                // _context
                // .Packages
                // .SingleOrDefault(p => p.Code == code);

            if (package == null) {
                return NotFound();
            }

            package.AddUpdate(model.Status, model.Delivered);

            _repository.Update(package);
            //_context.SaveChanges();

            return NoContent();
        }

        /*
        // PUT "api/packages"
        // PUT é a atualização de algum recurso;
        // EX: api/packages/1234-f123-e3214-r0123/
        [HttpPut("{code}")]

        public IActionResult Put(string code) {

            return Ok();
        }
        */
    }
}