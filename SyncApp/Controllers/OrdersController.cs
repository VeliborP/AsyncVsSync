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
    public class OrdersController : ControllerBase
    {
        private readonly CustomerSupplier _context;

        public OrdersController(CustomerSupplier context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrder()
        {
            return _context.Order.Include(oi => oi.OrderItem).ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Order.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(int id)
        {
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            _context.SaveChanges();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
