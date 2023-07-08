using AutoMapper;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RacunController : Controller
    {
        private readonly IRacunRepository racunRepository;
        private readonly IMapper mapper;
        public RacunController(IRacunRepository racunRepository, IMapper mapper)
        {
            this.racunRepository = racunRepository;
            this.mapper = mapper;
        }
        [HttpGet,Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBillsAsync()
        {
            var billsEntity = await racunRepository.GetAllAsync();

            var billsDto = mapper.Map<List<Models.DTO.Racun>>(billsEntity);

            return Ok(billsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBillByIdAsync(int id)
        {
            var billEntity = await racunRepository.GetByIdAsync(id);

            if (billEntity == null)
            {
                return NotFound("There is no bill with this id.");
            }

            var billDto = mapper.Map<Models.DTO.Racun>(billEntity);
            return Ok(billDto);
        }

        [HttpGet]
        [Route("payment-intent/{paymentIntentId}")]
        public async Task<IActionResult> GetBillByPaymentIntentIdAsync(string paymentIntentId)
        {
            var billEntity = await racunRepository.GetByPaymentIntentIdAsync(paymentIntentId);

            if (billEntity == null)
            {
                return NotFound("There is no bill with this PaymentIntentId.");
            }

            var billDto = mapper.Map<Models.DTO.Racun>(billEntity);
            return Ok(billDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddBillAsync(Models.DTO.AddRacunRequest addRacunRequest)
        {

            // Request(DTO) to entity model
            var billEntity = new Db.TblRacun()
            {
                RacunId = addRacunRequest.RacunId,
                DatumKupovine = DateTime.Today,
                VremeKupovine = DateTime.Now.TimeOfDay,
                IznosPopusta = 0,
                KorpaId = addRacunRequest.KorpaId,
            };

            // Pass details to Repository
            var addedBillEntity = await racunRepository.AddAsync(billEntity);


            var billDto = mapper.Map<Models.DTO.Racun>(addedBillEntity);

            return Ok(billDto);


        }
        [HttpDelete,Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBillAsync(int id)
        {
            //Get address from Db
            var billEntity = await racunRepository.DeleteAsync(id);

            //If null NotFound
            if (billEntity == null)
            {
                return NotFound("There is no bill with this id.");
            }

            //Convert response to DTO
            var billDto = mapper.Map<Models.DTO.Racun>(billEntity);
            //Return response
            return Ok(billDto);
        }
        [HttpPut,Authorize(Roles ="Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBillAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateRacunRequest updateRacunRequest)
        {
            //Convert DTO to entity
            var billEntity = new Db.TblRacun()
            {
                DatumKupovine = updateRacunRequest.DatumKupovine,
                VremeKupovine = updateRacunRequest.VremeKupovine,
                IznosPost = updateRacunRequest.IznosPost,
                IznosSaPost = updateRacunRequest.IznosSaPost,
                IznosPopusta = updateRacunRequest.IznosPopusta,
                IznosSaPopustom = updateRacunRequest.IznosSaPopustom,
                KorpaId = updateRacunRequest.KorpaId,
            };


            //Update Address using repository

            billEntity = await racunRepository.UpdateAsync(id, billEntity);

            //if null NotFound
            if (billEntity == null)
            {
                return NotFound("There is no bill with this id.");
            }

            //Convert back to DTO
            var billDto = mapper.Map<Models.DTO.Racun>(billEntity);
            //Return ok
            return Ok(billDto);
        }

        [HttpGet]
        [Route("max-racun-id")]
        public IActionResult GetMaxRacunId()
        {
            var maxRacunId = racunRepository.GetMaxRacunId();
            return Ok(maxRacunId);
        }




    }
}
