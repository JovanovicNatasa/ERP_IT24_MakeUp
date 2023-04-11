using AutoMapper;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class ProizvodUKorpiController : Controller
    {
        private readonly IProizvodUKorpiRepository proizvodUKorpiRepository;
        private readonly IMapper mapper;
        public ProizvodUKorpiController(IProizvodUKorpiRepository proizvodUKorpiRepository, IMapper mapper)
        {
            this.proizvodUKorpiRepository = proizvodUKorpiRepository;
            this.mapper = mapper;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProductInShoppingCartsAsync()
        {
            var productInShoppingCartsEntity = await proizvodUKorpiRepository.GetAllAsync();

            var productInShoppingCartsDto = mapper.Map<List<Models.DTO.ProizvodUKorpi>>(productInShoppingCartsEntity);

            return Ok(productInShoppingCartsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProductInShoppingCartByIdAsync(int id)
        {
            var productInShoppingCartEntity = await proizvodUKorpiRepository.GetByIdAsync(id);

            if (productInShoppingCartEntity == null)
            {
                return NotFound("There is no product In Shopping Cart with this id.");
            }

            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            return Ok(productInShoppingCartDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductInShoppingCartAsync(Models.DTO.AddProizvodUKorpiRequest addProizvodUKorpiRequest)
        {
            //Request(DTO) to entity model
            var productInShoppingCartEntity = new Db.TblProizvodUkorpi()
            {
                BrojKomada = addProizvodUKorpiRequest.BrojKomada,
                ProizvodId = addProizvodUKorpiRequest.ProizvodId,
                KorpaId = addProizvodUKorpiRequest.KorpaId,

            };
            //pass details to Repository
            productInShoppingCartEntity = await proizvodUKorpiRepository.AddAsync(productInShoppingCartEntity);

            //Conwert back to DTO
            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(productInShoppingCartDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProductInShoppingCartAsync(int id)
        {
            //Get address from Db
            var productInShoppingCartEntity = await proizvodUKorpiRepository.DeleteAsync(id);

            //If null NotFound
            if (productInShoppingCartEntity == null)
            {
                return NotFound("There is no product In Shopping Cart with this id.");
            }

            //Convert response to DTO
            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            //Return response
            return Ok(productInShoppingCartDto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateProizvodUKorpiRequest updateProizvodUKorpiRequest)
        {
            //Convert DTO to entity
            var productInShoppingCartEntity = new Db.TblProizvodUkorpi()
            {
                BrojKomada = updateProizvodUKorpiRequest.BrojKomada,
                ProizvodId = updateProizvodUKorpiRequest.ProizvodId,
                KorpaId = updateProizvodUKorpiRequest.KorpaId,
            };


            //Update Address using repository

            productInShoppingCartEntity = await proizvodUKorpiRepository.UpdateAsync(id, productInShoppingCartEntity);

            //if null NotFound
            if (productInShoppingCartEntity == null)
            {
                return NotFound("There is no product In Shopping Cart with this id.");
            }

            //Convert back to DTO
            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            //Return ok
            return Ok(productInShoppingCartDto);
        }
    }
}
