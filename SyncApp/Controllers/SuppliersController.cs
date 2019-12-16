using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyncApp.Data;

namespace SyncApp.Controllers
{
    [Route("api/sync/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly CustomerSupplier _context;

        public SuppliersController(CustomerSupplier context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetSupplier()
        {
            return _context.Supplier.ToList();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplier(int id)
        {
            var supplier = _context.Supplier.Find(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public IActionResult PutSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/Suppliers
        [HttpPost]
        public ActionResult<Supplier> PostSupplier(Supplier supplier)
        {
            _context.Supplier.Add(supplier);
            _context.SaveChanges();

            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public ActionResult<Supplier> DeleteSupplier(int id)
        {
            var supplier = _context.Supplier.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Supplier.Remove(supplier);
            _context.SaveChanges();

            return supplier;
        }

        private bool SupplierExists(int id)
        {
            return _context.Supplier.Any(e => e.Id == id);
        }
    }
}
