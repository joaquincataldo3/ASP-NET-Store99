using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using Store99.Interfaces.Responses;
using Store99.Interfaces.Services;
using Store99.Models;
using Store99.Repositories;
using System.Net;

namespace Store99.Service.ShoeService
{
    public class ShoeService : IShoeService
    {
        private ShoeRepository _shoeRepository;
        private IMapper _mapper;
        public ShoeService(ShoeRepository shoeRepository, IMapper mapper)
        {
            _shoeRepository = shoeRepository;
            _mapper = mapper;
        }

        public ICollectionShoeResponse ValidateGetOnDemandShoes()
        {
            ShoeCollectionResponse response;
            ICollection<Shoe> shoes = _shoeRepository.GetAllOnDemandShoes();
            if (shoes.Count == 0)
            {
                response = new()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Empty array",
                    Data = null
                };
            }
            try
            {
                ICollection<ShoeDto> mappedShoes = _mapper.Map<ICollection<ShoeDto>>(shoes);
                response = new()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Empty array",
                    Data = mappedShoes
                };
                return response;
            }
            catch (Exception)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Error mapping shoes",
                    Data = null
                };
                return response;
            }

        }

        public ICollectionShoeResponse ValidateGetInStockShoes()
        {
            ShoeCollectionResponse response; 
            ICollection<Shoe> shoes = _shoeRepository.GetAllInStockShoes();
            if(shoes.Count == 0)
            {
                response = new()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Empty array",
                    Data = null
                };
            }
            try
            {
                ICollection<ShoeDto> mappedShoes = _mapper.Map<ICollection<ShoeDto>>(shoes);
                response = new()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Empty array",
                    Data = mappedShoes
                };
                return response;
            }
            catch (Exception)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Error mapping shoes",
                    Data = null
                };
                return response;
            }
            
        }

        public IOneShoeResponse ValidateGetShoe(int shoeId)
        {
            Shoe shoe = _shoeRepository.GetShoeById(shoeId);
            OneShoeResponse response;
            if (shoe == null)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Shoe not found",
                    Data = null
                };
                return response;
            }
            try
            {
                ShoeDto shoeMapped = _mapper.Map<ShoeDto>(shoe);
                response = new()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Shoe found successfully",
                    Data = shoeMapped
                };
                return response;
            }
            catch (Exception)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Error mapping shoe",
                    Data = null
                };
                return response;
            }

        }

        public IOneShoeResponse ValidateShoeCreation(CreateShoeDto createShoeDto)
        // TODO - CREATE IMAGE UPLOAD IN IMAGE SERVICE AND CREATE IMAGE IN IMAGE SERVICE
        {
            OneShoeResponse response = new();
            string shoeName = createShoeDto.Name.ToLower();
            Shoe shoeAlreadyExists = _shoeRepository.GetShoeByName(shoeName);
            if (shoeAlreadyExists != null)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.Conflict,
                    Message = "Shoe already exists"
                };
                return response;
            }
            try
            {//
                bool createNewShoe = _shoeRepository.CreateShoe(createShoeDto);
                if (!createNewShoe)
                {
                    response = new()
                    {
                        Success = false,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Internal Server Error"
                    };
                    return response;
                }
                response = new()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Shoe created successfully"
                };
                return response;
            }
        }

        
    }
}
