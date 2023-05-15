using AutoMapper;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorpaController : Controller
    {
        private readonly IKorpaRepository korpaRepository;
       /* private readonly IProizvodUKorpiRepository proizvodUKorpiRepository;
        private readonly IProizvodRepository proizvodRepository;*/
        private readonly IMapper mapper;
        public KorpaController(IKorpaRepository korpaRepository, /*IProizvodUKorpiRepository proizvodUKorpiRepository,
            IProizvodRepository proizvodRepository,*/ IMapper mapper)
        {
            this.korpaRepository = korpaRepository;
           /* this.proizvodUKorpiRepository= proizvodUKorpiRepository;
            this.proizvodRepository=proizvodRepository;*/
            this.mapper = mapper;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllShoppingCartsAsync()
        {
            var shoppingCartsEntity = await korpaRepository.GetAllAsync();

            var shoppingCartsDto = mapper.Map<List<Models.DTO.Korpa>>(shoppingCartsEntity);

            return Ok(shoppingCartsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetShoppingCartByIdAsync(int id)
        {
            var shoppingCartEntity = await korpaRepository.GetByIdAsync(id);

            if (shoppingCartEntity == null)
            {
                return NotFound("There is no shoppingCart with this id.");
            }

            var shoppingCartDto = mapper.Map<Models.DTO.Korpa>(shoppingCartEntity);
            return Ok(shoppingCartDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddShoppingCartAsync(Models.DTO.AddKorpaRequest addKorpaRequest)
        {
            //Request(DTO) to entity model
            var shoppingCartEntity = new Db.TblKorpa()
            {
                KorpaId = addKorpaRequest.KorpaId,
                UkupanIznos = 0,
                BrProizvoda = 0,
                Popust = false,
                ProcenatPop = 0,
                KorisnikId = addKorpaRequest.KorisnikId,

            };
            //pass details to Repository
            shoppingCartEntity = await korpaRepository.AddAsync(shoppingCartEntity);

            //Conwert back to DTO
            var shoppingCartDto = mapper.Map<Models.DTO.Korpa>(shoppingCartEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(shoppingCartDto);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteShoppingCartAsync(int id)
        {
            //Get address from Db
            var shoppingCartEntity = await korpaRepository.DeleteAsync(id);

            //If null NotFound
            if (shoppingCartEntity == null)
            {
                return NotFound("There is no shoppingCart with this id.");
            }

            //Convert response to DTO
            var shoppingCartDto = mapper.Map<Models.DTO.Korpa>(shoppingCartEntity);
            //Return response
            return Ok(shoppingCartDto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateShoppingCartAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateKorpaRequest updateKorpaRequest)
        {
            //Convert DTO to entity
            var shoppingCartEntity = new Db.TblKorpa()
            {
                UkupanIznos = updateKorpaRequest.UkupanIznos,
                BrProizvoda = updateKorpaRequest.BrProizvoda,
                Popust = updateKorpaRequest.Popust,
                ProcenatPop = updateKorpaRequest.ProcenatPop,
                KorisnikId = updateKorpaRequest.KorisnikId,
            };


            //Update Address using repository

            shoppingCartEntity = await korpaRepository.UpdateAsync(id, shoppingCartEntity);

            //if null NotFound
            if (shoppingCartEntity == null)
            {
                return NotFound("There is no shoppingCart with this id.");
            }

            //Convert back to DTO
            var shoppingCartDto = mapper.Map<Models.DTO.Korpa>(shoppingCartEntity);
            //Return ok
            return Ok(shoppingCartDto);
        }

    }
}
