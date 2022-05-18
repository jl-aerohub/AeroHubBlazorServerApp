using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AeroHubBlazorServer.DbContexts;
using AeroHubBlazorServer.Models;

namespace AeroHubBlazorServer.Controllers
{
    public class MetaDatasController : Controller
    {
        private readonly MFINContext _context;

        public MetaDatasController(MFINContext context)
        {
            _context = context;
        }

        // GET: MetaDatas
        public async Task<IActionResult> Index()
        {
            var mFINContext = _context.MetaData.Include(m => m.QP);
            return View(await mFINContext.ToListAsync());
        }

        // GET: MetaDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MetaData == null)
            {
                return NotFound();
            }

            var metaData = await _context.MetaData
                .Include(m => m.QP)
                .FirstOrDefaultAsync(m => m.id == id);
            if (metaData == null)
            {
                return NotFound();
            }

            return View(metaData);
        }

        // GET: MetaDatas/Create
        public IActionResult Create()
        {
            ViewData["QPID"] = new SelectList(_context.QIFDocument, "QPID", "MBD");
            return View();
        }

        // POST: MetaDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileName,id,DocumentType,FeatureItemids,PartNumber,FileLink,User,DateTime,Signatures,SupplierName,ProcessNumber,QPID")] MetaData metaData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metaData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QPID"] = new SelectList(_context.QIFDocument, "QPID", "MBD", metaData.QPID);
            return View(metaData);
        }

        // GET: MetaDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MetaData == null)
            {
                return NotFound();
            }

            var metaData = await _context.MetaData.FindAsync(id);
            if (metaData == null)
            {
                return NotFound();
            }
            ViewData["QPID"] = new SelectList(_context.QIFDocument, "QPID", "MBD", metaData.QPID);
            return View(metaData);
        }

        // POST: MetaDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileName,id,DocumentType,FeatureItemids,PartNumber,FileLink,User,DateTime,Signatures,SupplierName,ProcessNumber,QPID")] MetaData metaData)
        {
            if (id != metaData.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metaData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetaDataExists(metaData.id))
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
            ViewData["QPID"] = new SelectList(_context.QIFDocument, "QPID", "MBD", metaData.QPID);
            return View(metaData);
        }

        // GET: MetaDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MetaData == null)
            {
                return NotFound();
            }

            var metaData = await _context.MetaData
                .Include(m => m.QP)
                .FirstOrDefaultAsync(m => m.id == id);
            if (metaData == null)
            {
                return NotFound();
            }

            return View(metaData);
        }

        // POST: MetaDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MetaData == null)
            {
                return Problem("Entity set 'MFINContext.MetaData'  is null.");
            }
            var metaData = await _context.MetaData.FindAsync(id);
            if (metaData != null)
            {
                _context.MetaData.Remove(metaData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetaDataExists(int id)
        {
          return (_context.MetaData?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
