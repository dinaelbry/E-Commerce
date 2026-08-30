using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTO_s.Product;
using E_Commerce.Application.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{

    public class ProductController(IProductServices productServices) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQueryParams,CancellationToken ct)
        {
            var Product = await productServices.GetAllProductAsync(productQueryParams);
            var Result = ToActionResult(Product);

            return Result;
        }


        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var Brands = await productServices.GetAllProductBrandsAsync();
            var Result = ToActionResult(Brands);

            return Result;
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var Types = await productServices.GetAllProductTypesAsync();
            var Result = ToActionResult(Types);

            return Result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id ,CancellationToken ct)
        {
            var product = await productServices.GetByIdAsync(id);
            var Result = ToActionResult(product);

            return Result;
        }
    }
}
