using AutoMapper;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdresaController:Controller
    {
        private readonly IAdresaRepository adresaRepository;
        private readonly IMapper mapper;

        public AdresaController(IAdresaRepository adresaRepository, IMapper mapper)
        {
            this.adresaRepository = adresaRepository;
            this.mapper = mapper;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAddressesAsync()
        {
            var addressesEntity = await adresaRepository.GetAllAsync();

            var addressesDto = mapper.Map<List<Models.DTO.Adresa>>(addressesEntity);

            return Ok(addressesDto);
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAddressByIdAsync(int id)
        {
            var addressEntity = await adresaRepository.GetByIdAsync(id);

            if (addressEntity == null)
            {
                return NotFound("There is no address with this id.");
            }

            var addressDto = mapper.Map<Models.DTO.Adresa>(addressEntity);
            return Ok(addressDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddAddressAsync(Models.DTO.AddAdresaRequest addAddressRequest)
        {
            //Request(DTO) to entity model
            var addressEntity = new Db.TblAdresa()
            {
                Grad = addAddressRequest.Grad,
                Ulica = addAddressRequest.Ulica,
                Broj = addAddressRequest.Broj,
                PostanskiBroj = addAddressRequest.PostanskiBroj,

            };
            //pass details to Repository
            addressEntity = await adresaRepository.AddAsync(addressEntity);

            //Conwert back to DTO
            var addressDto = mapper.Map<Models.DTO.Adresa>(addressEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(addressDto);
        }
        [HttpDelete, Authorize(Roles = "Admin, User")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAddressAsync(int id)
        {
            //Get address from Db
            var addressEntity = await adresaRepository.DeleteAsync(id);

            //If null NotFound
            if (addressEntity == null)
            {
                return NotFound("There is no address with this id.");
            }

            //Convert response to DTO
            var addressDto = mapper.Map<Models.DTO.Adresa>(addressEntity);
            //Return response
            return Ok(addressDto);
        }
        [HttpPut, Authorize(Roles = "Admin, User")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAddressAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateAdresaRequest updateAddressRequest)
        {
            //Convert DTO to entity
            var addressEntity = new Db.TblAdresa()
            {
                Grad = updateAddressRequest.Grad,
                Ulica = updateAddressRequest.Ulica,
                Broj = updateAddressRequest.Broj,
                PostanskiBroj = updateAddressRequest.PostanskiBroj,
            };


            //Update Address using repository

            addressEntity = await adresaRepository.UpdateAsync(id, addressEntity);

            //if null NotFound
            if (addressEntity == null)
            {
                return NotFound("There is no address with this id.");
            }

            //Convert back to DTO
            var addressDto = mapper.Map<Models.DTO.Adresa>(addressEntity);
            //Return ok
            return Ok(addressDto);
        }

    }
}
