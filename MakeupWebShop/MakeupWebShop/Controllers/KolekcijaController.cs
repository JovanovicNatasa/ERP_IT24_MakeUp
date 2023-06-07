using AutoMapper;
using MakeupWebShop.Db;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KolekcijaController : Controller
    {
        private readonly IKolekcijaRepository kolekcijaRepository;
        private readonly IMapper mapper;

        public KolekcijaController(IKolekcijaRepository kolekcijaRepository, IMapper mapper)
        {
            this.kolekcijaRepository = kolekcijaRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCollectionsAsync()
        {
            var collectionsEntity = await kolekcijaRepository.GetAllAsync();

            var collectionsDto = mapper.Map<List<Models.DTO.Kolekcija>>(collectionsEntity);

            return Ok(collectionsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCollectionByIdAsync(int id)
        {
            var collectionEntity = await kolekcijaRepository.GetByIdAsync(id);

            if (collectionEntity == null)
            {
                return NotFound("There is no collection with this id.");
            }

            var collectionDto = mapper.Map<Models.DTO.Kolekcija>(collectionEntity);
            return Ok(collectionDto);
        }
        [HttpPost] //, Authorize(Roles = "Admin")
        public async Task<IActionResult> AddCollectionAsync(Models.DTO.AddKolekcijaRequest addKolekcijaRequest)
        {
            //Request(DTO) to entity model
            var collectionEntity = new Db.TblKolekcija()
            {
                NazivKolekcije = addKolekcijaRequest.NazivKolekcije,
            };
            //pass details to Repository
            collectionEntity = await kolekcijaRepository.AddAsync(collectionEntity);

            //Conwert back to DTO
            var collectionDto = mapper.Map<Models.DTO.Kolekcija>(collectionEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(collectionDto);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCollectionAsync(int id)
        {
            //Get address from Db
            var collectionEntity = await kolekcijaRepository.DeleteAsync(id);

            //If null NotFound
            if (collectionEntity == null)
            {
                return NotFound("There is no collection with this id.");
            }

            //Convert response to DTO
            var collectionDto = mapper.Map<Models.DTO.Kolekcija>(collectionEntity);
            //Return response
            return Ok(collectionDto);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCollectionAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateKolekcijaRequest updateKolekcijaRequest)
        {
            //Convert DTO to entity
            var collectionEntity = new Db.TblKolekcija()
            {
                NazivKolekcije = updateKolekcijaRequest.NazivKolekcije,
            };

            //Update Address using repository

            collectionEntity = await kolekcijaRepository.UpdateAsync(id, collectionEntity);

            //if null NotFound
            if (collectionEntity == null)
            {
                return NotFound("There is no collection with this id.");
            }

            //Convert back to DTO
            var collectionDto = mapper.Map<Models.DTO.Kolekcija>(collectionEntity);
            //Return ok
            return Ok(collectionDto);
        }
    }
}

