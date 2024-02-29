using Microsoft.AspNetCore.Mvc;
using Store99.Models;
using Store99.Interfaces;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

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
        [ProducesResponseType(200, Type = typeof(ICollection<Shoe>))]
        [ProducesResponseType(500)]
        public IActionResult GetOnDemandShoes()
        {
            try
            {
                var shoesOnDemand = shoeRepository.GetAllOnDemandShoes();
                return Ok(shoesOnDemand);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("in-stock")]
        [ProducesResponseType(200, Type = typeof(ICollection<ShoeDto>))]
        public IActionResult GetInStockShoes()
        {
            try
            {
                var shoesInStock = shoeRepository.GetAllInStockShoes();
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
        [ProducesResponseType(200, Type = typeof(Shoe))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetShoe(string shoeId)
        {
            bool isInt = int.TryParse(shoeId, out int parsedIntId);
            IShoeServiceResponse response;
            if (!isInt)
            {
                response = new()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid Shoe Id"
                };
                return BadRequest(response);
            }
            response = shoeService.ValidateGetShoe(parsedIntId);
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            if(response.StatusCode == HttpStatusCode.InternalServerError) 
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(response);
        }


        [HttpPost("new")]
        [ProducesResponseType(201, Type = typeof(ShoeDto))]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateShoe([FromBody] CreateShoeDto createShoeDto)
        {
            IShoeServiceResponse response = shoeService.ValidateShoeCreation(createShoeDto);
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
