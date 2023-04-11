using AutoMapper;
using MakeupWebShop.Db;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipController : Controller
    {
        private readonly ITipRepository tipRepository;
        private readonly IMapper mapper;

        public TipController(ITipRepository tipRepository, IMapper mapper)
        {
            this.tipRepository = tipRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTypesAsync()
        {
            var typesEntity = await tipRepository.GetAllAsync();

            var typesDto = mapper.Map<List<Models.DTO.Tip>>(typesEntity);

            return Ok(typesDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTypeByIdAsync(int id)
        {
            var typeEntity = await tipRepository.GetByIdAsync(id);

            if (typeEntity == null)
            {
                return NotFound("There is no type with this id.");
            }

            var typeDto = mapper.Map<Models.DTO.Tip>(typeEntity);
            return Ok(typeDto);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTypeAsync(Models.DTO.AddTipRequest addTipRequest)
        {
            //Request(DTO) to entity model
            var typeEntity = new Db.TblTip()
            {
                NazivTipa = addTipRequest.NazivTipa,
            };
            //pass details to Repository
            typeEntity = await tipRepository.AddAsync(typeEntity);

            //Conwert back to DTO
            var typeDto = mapper.Map<Models.DTO.Tip>(typeEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(typeDto);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteTypeAsync(int id)
        {
            //Get address from Db
            var typeEntity = await tipRepository.DeleteAsync(id);

            //If null NotFound
            if (typeEntity == null)
            {
                return NotFound("There is no type with this id.");
            }

            //Convert response to DTO
            var typeDto = mapper.Map<Models.DTO.Tip>(typeEntity);
            //Return response
            return Ok(typeDto);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTypeAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateTipRequest updateTipRequest)
        {
            //Convert DTO to entity
            var typeEntity = new Db.TblTip()
            {
                NazivTipa = updateTipRequest.NazivTipa,
            };

            //Update Address using repository

            typeEntity = await tipRepository.UpdateAsync(id, typeEntity);

            //if null NotFound
            if (typeEntity == null)
            {
                return NotFound("There is no type with this id.");
            }

            //Convert back to DTO
            var typeDto = mapper.Map<Models.DTO.Tip>(typeEntity);
            //Return ok
            return Ok(typeDto);
        }
    }
}

