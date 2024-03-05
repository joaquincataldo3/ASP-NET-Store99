using Microsoft.AspNetCore.Mvc;
using Store99.Models;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;
using Store99.Interfaces.Repositories;
using Store99.Interfaces.Services;
using Store99.Interfaces.Responses;
using Store99.Service.ShoeService;

namespace Store99.Controllers
{
    // el controller es simil al controller en nestjs o a un route en express
    // esto va a /shoe ya que se llama ShoeController
    [Route("api/shoes")]
    [ApiController]
    public class ShoeController : Controller
    {
        // utilizamos interfaz de shoe repository
        private readonly IShoeRepository shoeRepository;
        private readonly Cloudinary cloudinary;
        private readonly IShoeService shoeService;

        // instanciamos el constructor e inyectamos el repositorio
        public ShoeController(IShoeRepository shoeRepository, Cloudinary cloudinary, IShoeService shoeService)
        {
            this.shoeRepository = shoeRepository;
            this.cloudinary = cloudinary;
            this.shoeService = shoeService;
        }

        // rutas
        [HttpGet("on-demand")]
        [ProducesResponseType(200, Type = typeof(ICollectionShoeResponse))]
        [ProducesResponseType(500)]
        public IActionResult GetOnDemandShoes()
        {
            try
            {
                ICollectionShoeResponse shoesOnDemand = shoeService.ValidateGetOnDemandShoes();
                return Ok(shoesOnDemand);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("in-stock")]
        [ProducesResponseType(200, Type = typeof(ICollectionShoeResponse))]
        public IActionResult GetInStockShoes()
        {
            try
            {
                ICollectionShoeResponse shoesInStock = shoeService.ValidateGetInStockShoes();
                return Ok(shoesInStock);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("by-category/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<ShoeDto>))]
        [ProducesResponseType(500)]
        public IActionResult GetShoesByCategory(int categoryId)
        {
            try
            {
                var shoesByCategory = shoeRepository.GetShoesByBrand(categoryId);
                return Ok(shoesByCategory);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{shoeId}")]
        [ProducesResponseType(200, Type = typeof(OneShoeResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetShoe(string shoeId)
        {
            bool isInt = int.TryParse(shoeId, out int parsedIntId);
            OneShoeResponse response;
            if (!isInt)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid Shoe Id",
                    Data = null
                };
                return BadRequest(response);
            }
            IOneShoeResponse validationResponse = shoeService.ValidateGetShoe(parsedIntId);
            if(validationResponse.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(validationResponse);
            }
            if(validationResponse.StatusCode == HttpStatusCode.InternalServerError) 
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(validationResponse);
        }


        [HttpPost("new")]
        [ProducesResponseType(201, Type = typeof(OneShoeResponse))]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateShoe([FromBody] CreateShoeDto createShoeDto)
        {
            IOneShoeResponse response = shoeService.ValidateShoeCreation(createShoeDto);
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return BadRequest(response);
            }
            if(response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(500);
            }
            return Ok(response);
        }
    }

}
