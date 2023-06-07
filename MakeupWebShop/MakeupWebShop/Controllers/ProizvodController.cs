using AutoMapper;
using MakeupWebShop.Db;
using MakeupWebShop.Models.DTO;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProizvodController : Controller
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly IMapper mapper;
        public ProizvodController(IProizvodRepository proizvodRepository, IMapper mapper)
        {
            this.proizvodRepository = proizvodRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var productsEntity = await proizvodRepository.GetAllAsync();

            var productsDto = mapper.Map<List<Models.DTO.Proizvod>>(productsEntity);

            return Ok(productsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var productEntity = await proizvodRepository.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound("There is no product with this id.");
            }

            var productDto = mapper.Map<Models.DTO.Proizvod>(productEntity);
            return Ok(productDto);
        }
        [HttpPost] //, Authorize(Roles = "Admin")
        public async Task<IActionResult> AddProductAsync(Models.DTO.AddProizvodRequest addProizvodRequest)
        {
            //Request(DTO) to entity model
            var productEntity = new Db.TblProizvod()
            {
                Model = addProizvodRequest.Model,
                Sastav = addProizvodRequest.Sastav,
                NacinUpotrebe = addProizvodRequest.NacinUpotrebe,
                CenaPoKom = addProizvodRequest.CenaPoKom,
                KolicinaNaStanju = addProizvodRequest.KolicinaNaStanju,
                BrendId = addProizvodRequest.BrendId,
                TipId = addProizvodRequest.TipId,
                NamenaId = addProizvodRequest.NamenaId,
                KolekcijaId = addProizvodRequest.KolekcijaId,

            };
            //pass details to Repository
            productEntity = await proizvodRepository.AddAsync(productEntity);
            //Conwert back to DTO
            var productDto = mapper.Map<Models.DTO.Proizvod>(productEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(productDto);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            //Get address from Db
            var productEntity = await proizvodRepository.DeleteAsync(id);

            //If null NotFound
            if (productEntity == null)
            {
                return NotFound("There is no product with this id.");
            }

            //Convert response to DTO
            var productDto = mapper.Map<Models.DTO.Proizvod>(productEntity);
            //Return response
            return Ok(productDto);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateProizvodRequest updateProizvodRequest)
        {
            //Convert DTO to entity
            var productEntity = new Db.TblProizvod()
            {
                Model = updateProizvodRequest.Model,
                Sastav = updateProizvodRequest.Sastav,
                NacinUpotrebe = updateProizvodRequest.NacinUpotrebe,
                CenaPoKom = updateProizvodRequest.CenaPoKom,
                KolicinaNaStanju = updateProizvodRequest.KolicinaNaStanju,
                BrendId = updateProizvodRequest.BrendId,
                TipId = updateProizvodRequest.TipId,
                NamenaId = updateProizvodRequest.NamenaId,
                KolekcijaId = updateProizvodRequest.KolekcijaId,
            };


            //Update Address using repository

            productEntity = await proizvodRepository.UpdateAsync(id, productEntity);

            //if null NotFound
            if (productEntity == null)
            {
                return NotFound("There is no product with this id.");
            }

            //Convert back to DTO
            var productDto = mapper.Map<Models.DTO.Proizvod>(productEntity);
            //Return ok
            return Ok(productDto);
        }
        
    }
}
