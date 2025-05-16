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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return Ok(result);
        }
    }
}
