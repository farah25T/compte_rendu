using compte_rendu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace compte_rendu.Controllers
{
    public class MembershipTypesController : Controller
    {
        private readonly ApplicationdbContext _db;

        public MembershipTypesController(ApplicationdbContext context)
        {
            _db = context;
        }

        // GET: MembershipTypes
        public async Task<IActionResult> Index()
        {
            return _db.Memberships!= null ?
                        View(await _db.Memberships.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.MembershipType'  is null.");
        }

        // GET: MembershipTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Memberships == null)
            {
                return NotFound();
            }

            var membershipType = await _db.Memberships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipType == null)
            {
                return NotFound();
            }

            return View(membershipType);
        }

        // GET: MembershipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SignUpFee,DurationInMonth,DiscountRate,Name")] MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(membershipType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipType);
        }

        // GET: MembershipTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Memberships == null)
            {
                return NotFound();
            }

            var membershipType = await _db.Memberships.FindAsync(id);
            if (membershipType == null)
            {
                return NotFound();
            }
            return View(membershipType);
        }

        // POST: MembershipTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SignUpFee,DurationInMonth,DiscountRate,Name")] MembershipType membershipType)
        {
            if (id != membershipType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(membershipType);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipTypeExists(membershipType.Id))
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
            return View(membershipType);
        }

        // GET: MembershipTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Memberships== null)
            {
                return NotFound();
            }

            var membershipType = await _db.Memberships
                .FirstOrDefaultAsync(m => m.Id==id );
            if (membershipType == null)
            {
                return NotFound();
            }

            return View(membershipType);
        }

        // POST: MembershipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Memberships == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MembershipType'  is null.");
            }
            var membershipType = await _db.Memberships.FindAsync(id);
            if (membershipType != null)
            {
                _db.Memberships.Remove(membershipType);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipTypeExists(int id)
        {
            return (_db.Memberships?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
