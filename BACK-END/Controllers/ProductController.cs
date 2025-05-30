using AutoMapper;
using BACK_END.Data;
using BACK_END.Service;
using LIBRARY.Shared.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LIBRARY.Shared.DTO.ProductDTO;

namespace BACK_END.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CloudinaryService _cloudinary;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext context, CloudinaryService cloudinary, IMapper mapper)
        {
            _context = context;
            _cloudinary = cloudinary;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllProducts()
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.ProductImage)
                    .ToListAsync();

                var response = _mapper.Map<List<ProductResponseDto>>(products);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al listar los productos: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImage)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            var response = _mapper.Map<ProductResponseDto>(product);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createProduct([FromForm] ProductCreateDto dto)
        {
            try
            {
                var uploadResult = await _cloudinary.UploadImageAsync(dto.ImageFile);

                var product = _mapper.Map<Product>(dto);

                product.ProductImage = new ProductImage
                {
                    Name = dto.ImageFile.FileName,
                    ImageUrl = uploadResult.SecureUrl.ToString(),
                    ImageId = uploadResult.PublicId
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<ProductResponseDto>(product);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear el producto: " + ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> updateProduct(int id, [FromForm] ProductCreateDto dto)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductImage)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return NotFound("Producto no encontrado");

                // Actualiza campos del producto
                _mapper.Map(dto, product);

                // Si viene una nueva imagen, reemplaza
                if (dto.ImageFile != null)
                {
                    if (product.ProductImage != null)
                    {
                        await _cloudinary.DeleteImageAsync(product.ProductImage.ImageId);
                        _context.ProductImages.Remove(product.ProductImage);
                    }

                    var uploadResult = await _cloudinary.UploadImageAsync(dto.ImageFile);

                    product.ProductImage = new ProductImage
                    {
                        Name = dto.ImageFile.FileName,
                        ImageUrl = uploadResult.SecureUrl.ToString(),
                        ImageId = uploadResult.PublicId
                    };
                }

                await _context.SaveChangesAsync();

                var response = _mapper.Map<ProductResponseDto>(product);
                return Ok(response);
            }
            catch (DbUpdateException dbEx)
            {
                return BadRequest(dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al intentar actualizar el producto: " + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteProductById(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductImage)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return NotFound();

                if (product.ProductImage != null)
                {
                    await _cloudinary.DeleteImageAsync(product.ProductImage.ImageId);
                    _context.ProductImages.Remove(product.ProductImage);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el producto: " + ex.Message);
            }
        }
    }
}
