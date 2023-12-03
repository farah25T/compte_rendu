using compte_rendu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace compte_rendu.Controllers
{
    public class CustomerController : Controller
    {   private readonly ApplicationdbContext _db;

        public CustomerController(ApplicationdbContext db)
        {
               _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var Customers = _db.Customers.Include(c=>c.MemberShipType);
            
            return View( await Customers.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["membershipTypeId"] = new SelectList(_db.Set<Membershiptypes>(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,membershipTypeId")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                _db.Add(customer);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewData["membershipTypeId"] = new SelectList(_db.Set<Membershiptypes>(), "Id", "Id", customer.MemberShipId);
            return View(customer);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Customers == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers
                .Include(c => c.MemberShipType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Customers == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["membershipTypeId"] = new SelectList(_db.Set<Membershiptypes>(), "Id", "Id", customer.MemberShipId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,membershipTypeId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    _db.Update(customer);
                    await _db.SaveChangesAsync();
                
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["membershipTypeId"] = new SelectList(_db.Set<Membershiptypes>(), "Id", "Id", customer.MemberShipId);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_db.Customers == null)
            {
                return Problem("NO Data Found");
            }
            var customer = await _db.Customers.FindAsync(id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
