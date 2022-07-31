using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;
using ZakaZaka.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZakaZaka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IMapper _mapper;

        public ShopController(IShopService shopService, IMapper mapper) 
        {
            _shopService = shopService;
            _mapper = mapper;
        }
        // GET: api/<ShopController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var shops = await _shopService.GetAsync();
            return Ok(shops);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var shop = await _shopService.GetShopById(id);
            return Ok(shop);
        }

        // GET api/<ShopController>/5
        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync([FromBody]ShopFilter shopFilter)
        {
            var shops = await _shopService.SearchAsync(shopFilter);
            
            return Ok(shops);
        }

        // POST api/<ShopController>
        [HttpPost]
        public async Task<IActionResult> AddNewShopAsync([FromBody] ShopRequestModel requestModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var entity = _mapper.Map<Shop>(requestModel);
                var shopId = await _shopService.AddNewShopAsync(entity);
                return Ok(shopId);
            }
            catch (System.Exception)
            {

                throw;
            }
           
        }

        // PUT api/<ShopController>/5
        [HttpPut("{id}/details")]
        public async Task<IActionResult> UpdateShopDetails(int id, [FromBody] UpdateShopRequestModel requestModel)
        {
            try
            {
                var shop = _mapper.Map<Shop>(requestModel);
                await _shopService.UpdateShopAsnyc(id, shop);
                return Ok(shop);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // DELETE api/<ShopController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _shopService.DeleteShopAsync(id);
                return NoContent();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
