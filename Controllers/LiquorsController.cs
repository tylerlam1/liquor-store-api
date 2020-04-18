﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CPSC471_Proj.Models;
using MySql.Data.MySqlClient;

namespace CPSC471_Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiquorsController : ControllerBase
    {
        private readonly Context _context;

        public LiquorsController(Context context)
        {
            _context = context;
        }

        // GET: api/Liquors
        [HttpGet("all")]
        public IEnumerable<Liquor> GetLiquor()
        {
            return _context.Liquor.FromSql("spLiquorGetAll").ToList();
        }

        // GET: api/Liquors/{liquor_id}/name
        [HttpGet("{input:int}/name")]
        public async Task<IActionResult> GetLiquorNameById([FromRoute] int input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorName.FromSql("CALL spLiquorGetNameById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);
        }

        // GET: api/Liquors/{liquor_id}/description
        [HttpGet("{input:int}/description")]
        public async Task<IActionResult> GetLiquorDescriptionById([FromRoute] int input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorDescription.FromSql("CALL spLiquorGetDescriptionById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);
        }

        // GET: api/Liquors/{liquor_id}/image
        [HttpGet("{input:int}/image")]
        public async Task<IActionResult> GetLiquorImageById([FromRoute] int input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorImage.FromSql("CALL spLiquorGetImageById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);
        }


        // ADDED:
        // GET: api/Liquors/{liquor_id}/price
        [HttpGet("{input:int}/price")]
        public async Task<IActionResult> GetLiquorPriceById([FromRoute] int input){
        
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorPrice.FromSql("CALL spLiquorGetPriceById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);
        
        
        
        
        }

        // ADDED:
        // GET: api/Liquors/{liquor_id}/sale_percentage
        [HttpGet("{input:int}/sale_percentage")]
        public async Task<IActionResult> GetLiquorSalePercentageById([FromRoute] int input)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorSalePercentage.FromSql("CALL spLiquorGetSalePercentageById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);




        }

        // GET: api/Liquors/{liquor_id}/sale_length
        [HttpGet("{input:int}/sale_length")]
        public async Task<IActionResult> GetLiquorSaleLengthById([FromRoute] int input)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorSaleLength.FromSql("CALL spLiquorGetSaleLengthById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);




        }

        // Get: api/Liquors/{liquor_id}/quantity
        [HttpGet("{input:int}/quantity")]
        public async Task<IActionResult> GetLiquorQuantityById([FromRoute] int input)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorQuantity.FromSql("CALL spLiquorGetQuantityById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);




        }


        // Get: api/Liquors/{liquor_id}/volume
        [HttpGet("{input:int}/volume")]
        public async Task<IActionResult> GetLiquorVolumeById([FromRoute] int input)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.LiquorVolume.FromSql("CALL spLiquorGetVolumeById (@id)", new MySqlParameter("@id", input)).ToListAsync();

            if (liquor == null)
            {
                return NotFound();
            }

            return Ok(liquor);




        }








        // PUT: api/Liquors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiquor([FromRoute] int id, [FromBody] Liquor liquor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != liquor.liquor_id)
            {
                return BadRequest();
            }

            _context.Entry(liquor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiquorExists(id))
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

        // POST: api/Liquors
        [HttpPost]
        public async Task<IActionResult> PostLiquor([FromBody] Liquor liquor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Liquor.Add(liquor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLiquor", new { id = liquor.liquor_id }, liquor);
        }

        // DELETE: api/Liquors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLiquor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liquor = await _context.Liquor.FindAsync(id);
            if (liquor == null)
            {
                return NotFound();
            }

            _context.Liquor.Remove(liquor);
            await _context.SaveChangesAsync();

            return Ok(liquor);
        }

        private bool LiquorExists(int id)
        {
            return _context.Liquor.Any(e => e.liquor_id == id);
        }

       
    }
}