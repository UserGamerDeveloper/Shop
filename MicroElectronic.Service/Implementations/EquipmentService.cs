using MicroElectronic.DAL.Interfaces;
using MicroElectronic.DAL.Repositories;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Equipment;
using MicroElectronic.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Implementations
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IBaseRepository<Equipment> _equipmentRepository;

        public EquipmentService(IBaseRepository<Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
    
        public async Task<IBaseResponse<Equipment>> Create(EquipmentViewModel item)
        {
            try
            {
                var Item = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Name == item.Name);
                if (Item != null)
                {
                    return new BaseResponse<Equipment>
                    {
                        Description = "Item с таким названием уже существует",
                        StatusCode = System.Net.HttpStatusCode.PreconditionFailed
                    };
                }

                var imageUrl = item.ImageUrl;
                if (item.ImageUrl == null)
                {
                    imageUrl = "/images/default.png";
                }

                Item = new Equipment()
                {
                    Name = item.Name,
                    ImageUrl = imageUrl,
                    CategoryId = item.CategoryId,
                    Description = item.Description,
                    Price = item.Price,
                    Size = item.Size,
                    BodyMaterial = item.BodyMaterial,
                    WorkingArea = item.WorkingArea,
                    Power = item.Power,
                    GuaranteePeriod = item.GuaranteePeriod,
                    FullDescription = item.FullDescription,
                    One = item.One,
                    Two = item.Two,
                    Three = item.Three,
                    ParamNameOne = item.ParamNameOne,
                    ParamValueOne = item.ParamValueOne,
                    ParamNameTwo = item.ParamNameTwo,
                    ParamValueTwo = item.ParamValueTwo,
                    ParamNameThree = item.ParamNameThree,
                    ParamValueThree = item.ParamValueThree,
                    ParamNameFour = item.ParamNameFour,
                    ParamValueFour = item.ParamValueFour,
                    ParamNameFive = item.ParamNameFive,
                    ParamValueFive = item.ParamValueFive,
                    ParamNameSix = item.ParamNameSix,
                    ParamValueSix = item.ParamValueSix,
                    ParamNameSeven = item.ParamNameSeven,
                    ParamValueSeven = item.ParamValueSeven,
                    ParamNameEight = item.ParamNameEight,
                    ParamValueEight = item.ParamValueEight,
                    ParamNameNine = item.ParamNameNine,
                    ParamValueNine = item.ParamValueNine,
                    ParamNameTen = item.ParamNameTen,
                    ParamValueTen = item.ParamValueTen,
                    ParamNameEleven = item.ParamNameEleven,
                    ParamValueEleven = item.ParamValueEleven,
                    ParamNameTwelve = item.ParamNameTwelve,
                    ParamValueTwelve = item.ParamValueTwelve,
                };

                await _equipmentRepository.Create(Item);

                return new BaseResponse<Equipment>
                {
                    Data = Item,
                    Description = "Категория успешно добавлена",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Equipment>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[CreateCategory]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            try
            {
                var category = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Категория не найдена",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                await _equipmentRepository.Delete(category);

                return new BaseResponse<bool>()
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

        public async Task<IBaseResponse<IEnumerable<Equipment>>> GetEquipments(int categoryId)
        {
            try
            {
                var equipments = await _equipmentRepository.GetAll()
                    .Where(x => x.CategoryId == categoryId).ToListAsync();

                return new BaseResponse<IEnumerable<Equipment>>()
                {
                    Data = equipments,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Equipment>>()
                {
                    Description = $"[GetEquipments]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EquipmentViewModel>> GetEquipment(int id)
        {
            try
            {
                var equipment = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if(equipment == null)
                {
                    return new BaseResponse<EquipmentViewModel>()
                    {
                        Description = "Оборудование не найдено",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                var data = new EquipmentViewModel()
                {
                    Id = equipment.Id,
                    CategoryId = equipment.CategoryId,
                    Name = equipment.Name,
                    Description = equipment.Description,
                    Price = equipment.Price,
                    Size = equipment.Size,
                    BodyMaterial = equipment.BodyMaterial,
                    WorkingArea = equipment.WorkingArea,
                    Power = equipment.Power,
                    GuaranteePeriod = equipment.GuaranteePeriod,
                    FullDescription = equipment.FullDescription,
                    ImageUrl = equipment.ImageUrl,
                    One = equipment.One,
                    Two = equipment.Two,
                    Three = equipment.Three,
                    ParamNameOne = equipment.ParamNameOne,
                    ParamValueOne = equipment.ParamValueOne,
                    ParamNameTwo = equipment.ParamNameTwo,
                    ParamValueTwo = equipment.ParamValueTwo,
                    ParamNameThree = equipment.ParamNameThree,
                    ParamValueThree = equipment.ParamValueThree,
                    ParamNameFour = equipment.ParamNameFour,
                    ParamValueFour = equipment.ParamValueFour,
                    ParamNameFive = equipment.ParamNameFive,
                    ParamValueFive = equipment.ParamValueFive,
                    ParamNameSix = equipment.ParamNameSix,
                    ParamValueSix = equipment.ParamValueSix,
                    ParamNameSeven = equipment.ParamNameSeven,
                    ParamValueSeven = equipment.ParamValueSeven,
                    ParamNameEight = equipment.ParamNameEight,
                    ParamValueEight = equipment.ParamValueEight,
                    ParamNameNine = equipment.ParamNameNine,
                    ParamValueNine = equipment.ParamValueNine,
                    ParamNameTen = equipment.ParamNameTen,
                    ParamValueTen = equipment.ParamValueTen,
                    ParamNameEleven = equipment.ParamNameEleven,
                    ParamValueEleven = equipment.ParamValueEleven,
                    ParamNameTwelve = equipment.ParamNameTwelve,
                    ParamValueTwelve = equipment.ParamValueTwelve,
                };

                return new BaseResponse<EquipmentViewModel>()
                {
                    Data = data,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentViewModel>
                {
                    Description = $"[GetEquipment]: {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Equipment>> Update(int id, EquipmentViewModel equipment)
        {
            try
            {
                var item = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (item == null)
                {
                    return new BaseResponse<Equipment>()
                    {
                        Description = "Категория не найдена",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                item.Name = equipment.Name;
                item.ImageUrl = equipment.ImageUrl;
                item.Description = equipment.Description;
                item.Price = equipment.Price;
                item.Size = equipment.Size;
                item.BodyMaterial = equipment.BodyMaterial;
                item.WorkingArea = equipment.WorkingArea;
                    item.Power = equipment.Power;
                    item.GuaranteePeriod = equipment.GuaranteePeriod;
                    item.FullDescription = equipment.FullDescription;
                item.One = equipment.One;
                item.Two = equipment.Two;
                item.Three = equipment.Three;
                item.ParamNameOne = equipment.ParamNameOne;
                item.ParamValueOne = equipment.ParamValueOne;
                item.ParamNameTwo = equipment.ParamNameTwo;
                item.ParamValueTwo = equipment.ParamValueTwo;
                item.ParamNameThree = equipment.ParamNameThree;
                item.ParamValueThree = equipment.ParamValueThree;
                item.ParamNameFour = equipment.ParamNameFour;
                item.ParamValueFour = equipment.ParamValueFour;
                item.ParamNameFive = equipment.ParamNameFive;
                item.ParamValueFive = equipment.ParamValueFive;
                item.ParamNameSix = equipment.ParamNameSix;
                item.ParamValueSix = equipment.ParamValueSix;
                item.ParamNameSeven = equipment.ParamNameSeven;
                item.ParamValueSeven = equipment.ParamValueSeven;
                item.ParamNameEight = equipment.ParamNameEight;
                item.ParamValueEight = equipment.ParamValueEight;
                item.ParamNameNine = equipment.ParamNameNine;
                item.ParamValueNine = equipment.ParamValueNine;
                item.ParamNameTen = equipment.ParamNameTen;
                item.ParamValueTen = equipment.ParamValueTen;
                item.ParamNameEleven = equipment.ParamNameEleven;
                item.ParamValueEleven = equipment.ParamValueEleven;
                item.ParamNameTwelve = equipment.ParamNameTwelve;
                item.ParamValueTwelve = equipment.ParamValueTwelve;

                await _equipmentRepository.Update(item);

                return new BaseResponse<Equipment>()
                {
                    Data = item,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Equipment>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
