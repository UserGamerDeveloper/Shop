using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Category;

namespace MicroElectronic.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IBaseResponse<Category>> Create(CategoryViewModel model);

        Task<IBaseResponse<IEnumerable<CategoryViewModel>>> GetCategories();

        Task<IBaseResponse<CategoryViewModel>> GetCategory(int id);

        Task<IBaseResponse<bool>> DeleteCategory(int id);

        Task<IBaseResponse<Category>> Update(int id, CategoryViewModel model);
    }
}
