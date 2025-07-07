using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.ApplicationItem;
using MicroElectronic.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Implementations
{
    public class ApplicationItemService : IApplicationItemService
    {
        private readonly IBaseRepository<ApplicationItem> _applicationItemRepository;
        private readonly IBaseRepository<Equipment> _equipmentRepository;

        public ApplicationItemService(IBaseRepository<ApplicationItem> applicationItemRepository, IBaseRepository<Equipment> equipmentRepository)
        {
            _applicationItemRepository = applicationItemRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<IBaseResponse<ApplicationItem>> Add(int equipId, int userId)
        {
            try
            {
                var applicationItem = await _applicationItemRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.EquipmentId == equipId);
                if (applicationItem == null)
                {
                    applicationItem = new ApplicationItem()
                    {
                        UserId = userId,
                        Quantity = 1,
                        EquipmentId = equipId,
                        Equipment = await _equipmentRepository.GetAll().FirstOrDefaultAsync(e => e.Id == equipId)
                    };

                    await _applicationItemRepository.Create(applicationItem);
                }
                else
                {
                    applicationItem.Quantity++;
                    await _applicationItemRepository.Update(applicationItem);
                }
                return new BaseResponse<ApplicationItem>()
                {
                    Data = applicationItem,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ApplicationItem>()
                {
                    Description = $"[AddToApplication]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ApplicationItemViewModel>>> GetApplicationItems(int userId)
        {
            try
            {
                var items = await _applicationItemRepository.GetAll()
                    .Join(_equipmentRepository.GetAll(), a => a.EquipmentId, e => e.Id, (a, e) => new ApplicationItemViewModel
                    {
                        ItemId = a.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Price = e.Price,
                        Quantity = a.Quantity,
                        ImageUrl = e.ImageUrl
                    }).ToListAsync();

                return new BaseResponse<IEnumerable<ApplicationItemViewModel>>()
                {
                    Data = items,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ApplicationItemViewModel>>()
                {
                    Description = $"[GetApplicationItems]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Remove(int id)
        {
            try
            {
                var item = await _applicationItemRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if(item == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Элемент заявки не найден",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                await _applicationItemRepository.Delete(item);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteApplicationItem]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> Clear(int userId)
        {
            try
            {
                var items = await _applicationItemRepository.GetAll().Where(x => x.UserId == userId).ToListAsync();
                if(items == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Элементов заявки с таким пользователем не найдено",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                foreach (var item in items)
                {
                    await _applicationItemRepository.Delete(item);
                }

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ClearApplicationItems]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> ChangeQuantity(int id, string operation)
        {
            try
            {
                var item = await _applicationItemRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if(item == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Элемент заявки не найден",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                if(operation == "increment")
                {
                    item.Quantity++;
                    await _applicationItemRepository.Update(item);
                }
                else if(operation == "decrement")
                {
                    item.Quantity--;
                    await _applicationItemRepository.Update(item);
                }

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = $"[ChangeQuantity]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
