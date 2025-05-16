using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Services;

namespace TSUApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly ILecturerService _service;

        public LecturersController(ILecturerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecturer(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
