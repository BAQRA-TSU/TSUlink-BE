using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Repositories;

namespace TSUApplicationApi.Services
{
    public class CategoryService : ICategoryService
    {
        //private readonly ApplicationDbContext _context;

        //public CategoryService(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<List<CategoryDto>> GetCategoriesAsync()
        //{
        //    var categories = await _context.Categories
        //        .Include(c => c.Semesters)
        //            .ThenInclude(s => s.Subjects)
        //        .ToListAsync();

        //    var result = categories.Select(c => new CategoryDto
        //    {
        //        Course = c.Course,
        //        Semesters = c.Semesters.Select(s => new SemesterDto
        //        {
        //            Name = s.Name,
        //            Items = s.Subjects.Select(sub => new SubjectItemDto
        //            {
        //                Name = sub.Name,
        //                //ShortName = sub.ShortName
        //                Id = sub.Id,
        //            }).ToList()
        //        }).ToList()
        //    }).ToList();

        //    return result;
        //}

        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _repository.GetCategoriesAsync();

            var result = categories.Select(c => new CategoryDto
            {
                Course = c.Course,
                Semesters = c.Semesters.Select(s => new SemesterDto
                {
                    Name = s.Name,
                    Items = s.Subjects.Select(sub => new SubjectItemDto
                    {
                        Id = sub.Id,
                        Name = sub.Name
                    }).ToList()
                }).ToList()
            }).ToList();

            return result;
        }
    }
}