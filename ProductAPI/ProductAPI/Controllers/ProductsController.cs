using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Context;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os produtos
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                if (_context.Product == null)
                {
                    return NotFound();
                }
                return await _context.Product.ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar sua solicitação.");
            }
        }

        /// <summary>
        /// Obtem um Porduto pelo seu Id
        /// </summary>
        /// <param name="id">Codigo do Produto</param>
        /// <returns>Objeto categoria</returns>
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                if (_context.Product == null)
                {
                    return NotFound();
                }
                var product = await _context.Product.FindAsync(id);

                if (product == null)
                {
                    return NotFound($"Produto com id '{id}' não encontrado...");
                }

                return product;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar sua solicitação.");
            }
        }

        /// <summary>
        /// Altera um Produto cadastrado
        /// </summary>
        /// <param name="id">Codigo do Produto na URL</param>
        /// <param name="product">Objeto do tipo Produto a ser alterado</param>
        /// <returns>Objeto Produto Alterado</returns>
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Id passado na Url diferente do Body");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound($"Produto com id '{id}' não encontrado...");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Cadastra um novo Produto
        /// </summary>
        /// <param name="product">Objeto Produto</param>
        /// <returns></returns>
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Product == null)
          {
              return Problem("Entity set 'ProductContext.Product'  is null.");
          }
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Exclui um Produto cadastrado
        /// </summary>
        /// <param name="id">Codigo do Produto</param>
        /// <returns></returns>
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Produto com id '{id}' não encontrado...");
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
