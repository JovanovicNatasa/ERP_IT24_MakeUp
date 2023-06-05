using AutoMapper;
using MakeupWebShop.Db;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrendController:Controller
    {
        private readonly IBrendRepository brendRepository;
        private readonly IMapper mapper;

        public BrendController(IBrendRepository brendRepository, IMapper mapper)
        {
            this.brendRepository = brendRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var brandsEntity = await brendRepository.GetAllAsync();

            var brandsDto = mapper.Map<List<Models.DTO.Brend>>(brandsEntity);

            return Ok(brandsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBrandByIdAsync(int id)
        {
            var brandEntity = await brendRepository.GetByIdAsync(id);

            if (brandEntity == null)
            {
                return NotFound("There is no brand with this id.");
            }

            var brandDto = mapper.Map<Models.DTO.Brend>(brandEntity);
            return Ok(brandDto);
        }
        [HttpPost] //, Authorize(Roles = "Admin")
        public async Task<IActionResult> AddBrandAsync(Models.DTO.AddBrendRequest addBrendRequest)
        {
            //Request(DTO) to entity model
            var brandEntity = new Db.TblBrend()
            {
                NazivBrenda = addBrendRequest.NazivBrenda,
            };
            //pass details to Repository
            brandEntity = await brendRepository.AddAsync(brandEntity);

            //Conwert back to DTO
            var brandDto = mapper.Map<Models.DTO.Brend>(brandEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(brandDto);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBrandAsync(int id)
        {
            //Get address from Db
            var brandEntity = await brendRepository.DeleteAsync(id);

            //If null NotFound
            if (brandEntity == null)
            {
                return NotFound("There is no brand with this id.");
            }

            //Convert response to DTO
            var brandDto = mapper.Map<Models.DTO.Brend>(brandEntity);
            //Return response
            return Ok(brandDto);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBrandAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateBrendRequest updateBrendRequest)
        {
            //Convert DTO to entity
            var brandEntity = new Db.TblBrend()
            {
                 NazivBrenda= updateBrendRequest.NazivBrenda,
            };

            //Update Address using repository

            brandEntity = await brendRepository.UpdateAsync(id, brandEntity);

            //if null NotFound
            if (brandEntity == null)
            {
                return NotFound("There is no brand with this id.");
            }

            //Convert back to DTO
            var brandDto = mapper.Map<Models.DTO.Brend>(brandEntity);
            //Return ok
            return Ok(brandDto);
        }
    }
}
