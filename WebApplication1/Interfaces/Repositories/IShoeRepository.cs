﻿using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using Store99.Models;

namespace Store99.Interfaces.Repositories
{
    public interface IShoeRepository
    {
        ICollection<ShoeDto> GetAllOnDemandShoes();
        ICollection<ShoeDto> GetAllInStockShoes();
        ICollection<ShoeDto> GetShoesByBrand(int brandId);
        bool CreateShoe(CreateShoeDto createShoeDto);
        Shoe? GetShoeById(int shoeId);
        Shoe? GetShoeByName(string name);
        bool SaveRecentChanges();
    }
}
