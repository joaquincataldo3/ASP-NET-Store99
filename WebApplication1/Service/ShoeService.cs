using Microsoft.AspNetCore.Mvc;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using Store99.Interfaces;
using Store99.Models;
using Store99.Repositories;
using System.Net;

namespace Store99.Service
{
    public class ShoeService
    {
        private ShoeRepository _shoeRepository;
        public ShoeService(ShoeRepository shoeRepository)
        {
            _shoeRepository = shoeRepository;
        }

        public IShoeServiceResponse ValidateProductCreation(CreateShoeDto createShoeDto)
        // TODO - CREATE IMAGE UPLOAD IN IMAGE SERVICE AND CREATE IMAGE IN IMAGE SERVICE
        {
            ShoeServiceResponse response = new();
            string shoeName = createShoeDto.Name.ToLower();
            Shoe? shoeAlreadyExists = _shoeRepository.GetShoeByName(shoeName);
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
                if(!createNewShoe)
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
