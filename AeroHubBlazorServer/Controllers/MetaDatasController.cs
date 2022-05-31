﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AeroHubBlazorServer.DbContexts;
using AeroHubBlazorServer.Models;

namespace AeroHubBlazorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDatasController : ControllerBase
    {
        private readonly MFINContext _context;

        static HttpClient client;

        public MetaDatasController(MFINContext context)
        {
            _context = context;
            client = new HttpClient();
        }

        // GET: api/MetaDatas
        [HttpGet]
        public async Task<List<MetaData>> GetMetaData()
        {
            if (_context.QIFDocument == null)
            {
                throw new InvalidOperationException("The database context is null");
            }

            return await _context.MetaData.ToListAsync();
        }

        // PUT: api/MetaDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetaData(int id, MetaData metaData)
        {
            if (id != metaData.id)
            {
                return BadRequest();
            }

            _context.Entry(metaData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaDataExists(id))
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

        // POST: api/MetaDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetaData>> PostMetaData(MetaData metaData)
        {
          if (_context.MetaData == null)
          {
              return Problem("Entity set 'MFINContext.MetaData'  is null.");
          }
            _context.MetaData.Add(metaData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MetaDataExists(metaData.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMetaData", new { id = metaData.id }, metaData);
        }

        // DELETE: api/MetaDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetaData(int id)
        {
            if (_context.MetaData == null)
            {
                return NotFound();
            }
            var metaData = await _context.MetaData.FindAsync(id);
            if (metaData == null)
            {
                return NotFound();
            }

            _context.MetaData.Remove(metaData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetaDataExists(int id)
        {
            return (_context.MetaData?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<List<QIFDocument>> GetQIFFiles()
        {
            if (_context.QIFDocument == null)
            {
                throw new InvalidOperationException("The database context is null");
            }

            return await _context.QIFDocument.ToListAsync();
        }

        // GET api/files/sample.png
        [HttpGet("{qpid}")]
        public async Task GetQIF(string qpid)
        {
            Uri uri = new($" https://generateqif.azurewebsites.us/api/generateqif?qpid={qpid}");
            var response = await client.GetAsync(uri);
            using (var fs = new FileStream(@$"C:\Users\shainc\Downloads\{qpid}.xml",
                FileMode.CreateNew))
            {
                await response.Content.CopyToAsync(fs);
            }

        }
    }
}
