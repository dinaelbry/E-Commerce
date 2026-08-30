using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Commerce.Application.DTO_s.Product
{
    public class ProductDto
    {
     
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public string PictureUrl { get; set; } = null!;
            public decimal Price { get; set; }

            public string BrandName { get; set; } = null!;
            public string TypeName { get; set; } = null!;

        }
    }

