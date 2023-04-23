using API.Dtos;
using API.Helpers;
using API.Helpers.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /**
     * Si se quiere versionar algun metodo, se tiene que colocar
     * [MapToApiVersion("1.1")] en la parte superior de cada metodo
    **/
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class ProductController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<Product>>> GetAll([FromQuery] Params productParams)
        {
            var result = await _unitOfWork.Products.GetAllWithPaginationAsync(productParams.PageIndex, productParams.PageSize, productParams.Search);

            var products = _mapper.Map<List<Product>>(result.registers);

            return new Pager<Product>(products, result.totallyRegister, productParams.PageIndex, productParams.PageSize, productParams.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> Post(AddUpdateProductDto productoDto)
        {
            var product = _mapper.Map<Product>(productoDto);

            _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Post), product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Put(Guid id, [FromBody] AddUpdateProductDto productDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            var updateProduct = _mapper.Map<Product>(productDto);

            updateProduct.Id = id;

            _unitOfWork.Products.Detach(product);
            _unitOfWork.Products.Update(updateProduct);
            await _unitOfWork.SaveAsync();

            return updateProduct;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveAsync();

            return Ok(new
            {
                Id = id,
            });
        }
    }
}
