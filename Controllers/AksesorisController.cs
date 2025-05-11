using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Dealer_Mobil_Acc.Models;
using System;
using API_Dealer_Mobil_Acc.Context;

namespace API_Dealer_Mobil_Acc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AksesorisController : ControllerBase
    {
        private readonly AksesorisContext _context;

        public AksesorisController(IConfiguration config)
        {
            _context = new AksesorisContext(config.GetConnectionString("WebApiDatabase"));
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
                var aksesoris = _context.GetById(id);
                if (aksesoris == null)
                {
                    return NotFound(new { message = "Aksesoris tidak ditemukan." });
                }
                return Ok(aksesoris);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Terjadi kesalahan saat mengambil data.", error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add([FromBody] Aksesoris a)
        {
            try
            {
                _context.Add(a);
                return Ok(new { message = "Aksesoris berhasil ditambahkan." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gagal menambahkan aksesoris.", error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Aksesoris a)
        {
            try
            {
                _context.Update(id, a);
                return Ok(new { message = "Aksesoris berhasil diupdate." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gagal mengupdate aksesoris.", error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _context.Delete(id);
                return Ok(new { message = "Aksesoris berhasil dihapus." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gagal menghapus aksesoris.", error = ex.Message });
            }
        }
    }
}
