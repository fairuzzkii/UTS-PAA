using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Dealer_Mobil_Acc.Models;
using API_Dealer_Mobil_Acc.Context;

namespace API_Dealer_Mobil_Acc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobilController : ControllerBase
    {
        private readonly MobilContext _context;

        public MobilController(IConfiguration config)
        {
            _context = new MobilContext(config.GetConnectionString("WebApiDatabase"));
        }

       
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _context.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Terjadi kesalahan saat mengambil data.", error = ex.Message });
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var mobil = _context.GetById(id);
                if (mobil == null)
                {
                    return NotFound(new { message = "Aksesoris tidak ditemukan." });
                }
                return Ok(mobil);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Terjadi kesalahan saat mengambil data.", error = ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add([FromBody] Mobil m)
        {
            bool success = _context.Add(m);
            if (!success)
                return StatusCode(500, new { message = "Gagal menambahkan mobil." });

            return Ok(new { message = "Mobil ditambahkan." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Mobil m)
        {
            bool success = _context.Update(id, m);
            if (!success)
                return StatusCode(500, new { message = "Gagal mengupdate mobil." });

            return Ok(new { message = "Mobil diupdate." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _context.Delete(id);
            if (!success)
                return StatusCode(500, new { message = "Gagal menghapus mobil." });

            return Ok(new { message = "Mobil dihapus." });
        }
    }

}
