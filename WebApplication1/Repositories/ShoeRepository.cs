﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Store99.AppContext;
using Store99.Controllers;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using Store99.Interfaces.Repositories;
using Store99.Models;

// repository sirve para obtener los datos. una especie de servicio en nestjs o controlador en express
namespace Store99.Repositories
{
    public class ShoeRepository : IShoeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShoeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // retornamos un icollection de shoes
        public ICollection<Shoe> GetAllOnDemandShoes()
        {
            ICollection<Shoe> shoes = _context.Shoes
                .Where(s => s.IsInStock == false)
                .Include(s => s.Brand)
                .Include(f => f.ShoeFile)
                .OrderBy(s => s.Id)
                .ToList();
            return shoes;
        }

        public ICollection<Shoe> GetAllInStockShoes()
        {
            ICollection<Shoe> shoesInStock = _context.Shoes
                .Where (s => s.IsInStock == true)
                .Include(b => b.Brand)
                .Include(f => f.ShoeFile)
                .OrderBy(s => s.IsInStock == true)
                .ToList();
            return shoesInStock;
        }

        public ICollection<ShoeDto> GetShoesByBrand(int brandId)
        {
            var shoesByCategory = _context.Shoes
                .Include(b => b.Brand)
                //.Include(f => f.ShoeFile)
                .Where(s => s.BrandId == brandId)
                .ToList();
            // mapeamos para que quede correcto con el dto que devolvemos
            var shoesByCategoryMapped = _mapper.Map<ICollection<ShoeDto>>(shoesByCategory);
            return shoesByCategoryMapped;

        }

        public Shoe? GetShoeById(int shoeId)
        {
            // usamos el where con uno
            // first or default devuelve la primera que encuentre o null
            return _context.Shoes.Where(s => s.Id == shoeId).FirstOrDefault();
        }
        public Shoe? GetShoeByName(string name)
        {
            // usamos el where con uno
            // first or default devuelve la primera que encuentre o null
            return _context.Shoes.Where(s => s.Name == name).FirstOrDefault();
        }

        public bool CreateShoe(CreateShoeDto createShoeDto)
        {
            try
            {
                var mapDto = _mapper.Map<Shoe>(createShoeDto);
                // esto devuelve la entidad, no el objeto en sí
                var newShoe = _context.Add(mapDto);
                SaveRecentChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool SaveRecentChanges() 
        {
            try
            {
                // siempre debemos salvar cambios  
                var isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
