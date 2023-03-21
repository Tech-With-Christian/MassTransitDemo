using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using AutoMapper;
using Domain.Entitites.Products;
using DTO.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/Product/get
        [HttpGet("get")]
        public async Task<IActionResult> GetAllProductsAsync(CancellationToken ct)
        {
            // Get products from database
            List<Product> products = await _mediator.Send(new GetAllProductsQuery(), ct);

            // Map products to DTO
            List<ProductResponseDto> mappedProducts = _mapper.Map<List<ProductResponseDto>>(products);

            return Ok(mappedProducts);
        }

        // GET: api/Product/get/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id, CancellationToken ct)
        {
            // Get product from database by ID
            Product product = await _mediator.Send(new GetProductByIdQuery(id), ct);

            // Map product to DTO
            ProductResponseDto mappedProduct = _mapper.Map<ProductResponseDto>(product);

            // Return mapped product
            return Ok(mappedProduct);
        }

        // GET: api/Product/get/category/{id}
        [HttpGet("get/category/{id}")]
        public async Task<IActionResult> GetProductByCategoryAsync(Guid id, CancellationToken ct)
        {
            // Get all products within a specific category
            List<Product> products = await _mediator.Send(new GetProductsByCategoryQuery(id), ct);

            // Map the products into a list
            List<ProductResponseDto> mappedProductsInCategory = _mapper.Map<List<ProductResponseDto>>(products);

            // Return the mapped products
            return Ok(mappedProductsInCategory);
        }

        // POST: api/Product/add
        [HttpPost("add")]
        public async Task<IActionResult> AddProductAsync(ProductCreateRequestDto productToCreate, CancellationToken ct)
        {
            // Map DTO to domain model
            Product product = _mapper.Map<Product>(productToCreate);

            // Add/Create product in database
            product = await _mediator.Send(new AddProductCommand(product), ct);

            // Map Created product to DTO
            ProductResponseDto mappedProduct = _mapper.Map<ProductResponseDto>(product);

            // Return mapped created product
            return Ok(mappedProduct);
        }

        // PUT: api/Product/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductAsync(ProductUpdateRequestDto productToUpdate, CancellationToken ct)
        {
            // Map DTO to domain model
            Product product = _mapper.Map<Product>(productToUpdate);

            // Update product in database
            product = await _mediator.Send(new UpdateProductCommand(product), ct);

            // Map updated product to DTO
            ProductResponseDto mappedProduct = _mapper.Map<ProductResponseDto>(product);

            // Return mapped updated product
            return Ok(mappedProduct);
        }

        // DELETE: api/Product/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteProductCommand(id), ct);

            return Ok("Product has successfully been removed from the database.");
        }
    }
}
