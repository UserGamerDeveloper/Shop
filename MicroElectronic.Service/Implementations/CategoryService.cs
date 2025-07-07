using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Category;
using MicroElectronic.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroElectronic.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public CategoryService(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IBaseResponse<Category>> Create(CategoryViewModel model)
        {
            try
            {
                var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if(category != null)
                {
                    return new BaseResponse<Category>
                    {
                        Description = "Категория с таким названием уже существует",
                        StatusCode = System.Net.HttpStatusCode.PreconditionFailed
                    };
                }

                var imageUrl = model.ImageUrl;
                if(model.ImageUrl == null)
                {
                    imageUrl = "/images/default.png";
                }

                category = new Category()
                {
                    Name = model.Name,
                    ImageUrl = imageUrl
                };

                await _categoryRepository.Create(category);

                return new BaseResponse<Category>
                {
                    Data = category,
                    Description = "Категория успешно добавлена",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Category>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[CreateCategory]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                {
                    return new BaseResponse<bool> ()
                    {
                        Data = false,
                        Description = "Категория не найдена",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                await _categoryRepository.Delete(category);

                return new BaseResponse<bool> ()
                {
                    Data = true,
                    Description = "Категория удалена",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCategory]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAll()
                    .Select(x => new CategoryViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ImageUrl = x.ImageUrl
                    }).ToListAsync();

                return new BaseResponse<IEnumerable<CategoryViewModel>>()
                {
                    Data = categories,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CategoryViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetCategories]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<CategoryViewModel>> GetCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(category == null)
                {
                    return new BaseResponse<CategoryViewModel>()
                    {
                        Description = "Категория не найдена",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                var data = new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                };

                return new BaseResponse<CategoryViewModel>()
                {
                    Data = data,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryViewModel>()
                {
                    Description = $"[GetCategory]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Category>> Update(int id, CategoryViewModel model)
        {
            try
            {
                var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if(category == null)
                {
                    return new BaseResponse<Category>()
                    {
                        Description = "Категория не найдена",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                category.Name = model.Name;
                category.ImageUrl = model.ImageUrl;

                await _categoryRepository.Update(category);

                return new BaseResponse<Category>()
                {
                    Data = category,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Category>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
