using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.DbContexts;
using MusicShop.Models;
using System.Text;

namespace MusicShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.PaymentType).Include(o => o.Status);
            return View(await applicationContext.ToListAsync());
        }

        [HttpGet]
        public async Task SalesReport()
        {
            var headers = new StringBuilder("Артикул товара\tКол-во\tЦена\tВыручка\tНаименование\n");
            var data = _context.Products
                .GroupJoin(_context.ShoppingCarts,
                p => p.Id,
                sc => sc.ProductId,
                (p, sc) => new
                {
                    ProductNumber = p.Id,
                    Name = p.Name,
                    SalesCount = sc
                        .Where(item => item.ProductId == p.Id)
                        .Sum(s => s.Count),
                    Price = p.Price,
                    Gain = p.Price * sc
                        .Where(item => item.ProductId == p.Id)
                        .Sum(s => s.Count),
                });
            var salesStatistics = data.ToList();

            var report = new StringBuilder("");
            report.AppendLine(headers.ToString());

            foreach (var item in salesStatistics)
            {
                report.Append($"\t{item.ProductNumber}\t");
                report.Append($"  {item.SalesCount}\t");
                report.Append($"{item.Price}\t");
                report.Append($"{item.Gain}\t");
                report.Append($"{item.Name}\t");
                report.AppendLine();
            }

            await Response.WriteAsync(report.ToString(), Encoding.Unicode);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.PaymentType)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Surname");
            ViewData["EmployeeId"] = new SelectList(_context.Staff, "Id", "Surname");
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,EmployeeId,StatusId,PaymentTypeId,TotalCost,OrderDate,PaymentDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Surname", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Staff, "Id", "Surname", order.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.StatusId);
            return View(order);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Surname", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Staff, "Id", "Surname", order.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.StatusId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,EmployeeId,StatusId,PaymentTypeId,TotalCost,OrderDate,PaymentDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Surname", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Staff, "Id", "Surname", order.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.StatusId);
            return View(order);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.PaymentType)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
