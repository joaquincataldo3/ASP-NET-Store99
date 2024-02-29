using Microsoft.AspNetCore.Mvc;
using Store99.Models;
using Store99.Interfaces;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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

        // instanciamos el constructor e inyectamos el repositorio
        public ShoeController(IShoeRepository shoeRepository, Cloudinary cloudinary)
        {
            this.shoeRepository = shoeRepository;
            this.cloudinary = cloudinary;
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
        [ProducesResponseType(200, Type=typeof(ICollection<ShoeDto>))]
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
        public IActionResult GetShoe(int shoeId)
        {
            try
            {
                var shoe = shoeRepository.GetShoeById(shoeId);
                if (shoe == null)
                {
                    return NotFound();
                }
                return Ok(shoe);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error"); ;
            }
            
        }


        [HttpPost("new")]
        [ProducesResponseType(201, Type = typeof(ShoeDto))]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateShoe([FromBody] CreateShoeDto createShoeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string shoeName = createShoeDto.Name.ToLower();
            Shoe? shoeAlreadyExists = shoeRepository.GetShoeByName(shoeName);
            if (shoeAlreadyExists != null)
            {
                return Conflict("Shoe already exists");
            }
            try
            {//
                ShoeDto createNewShoe = shoeRepository.CreateShoe(createShoeDto);
                ShoeDto shoeCreated = createNewShoe;
                return Ok(shoeCreated);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server Error");
            }
            
            
        }
    }

}
