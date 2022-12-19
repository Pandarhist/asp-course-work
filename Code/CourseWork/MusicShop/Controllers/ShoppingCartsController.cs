using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.DbContexts;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationContext _context;

        public ShoppingCartsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ShoppingCarts.Include(s => s.Order).Include(s => s.Product);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Order)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Count")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shoppingCart.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", shoppingCart.ProductId);
            return View(shoppingCart);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shoppingCart.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", shoppingCart.ProductId);
            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,Count")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shoppingCart.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", shoppingCart.ProductId);
            return View(shoppingCart);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Order)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoppingCarts == null)
            {
                return Problem("Entity set 'ApplicationContext.ShoppingCarts'  is null.");
            }
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
          return _context.ShoppingCarts.Any(e => e.OrderId == id);
        }
    }
}
