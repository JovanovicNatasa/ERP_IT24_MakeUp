using AutoMapper;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NamenaController : Controller
    {
        private readonly INamenaRepository namenaRepository;
        public readonly IMapper mapper;



        public NamenaController(INamenaRepository namenaRepository, IMapper mapper)

        {
            this.namenaRepository = namenaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurposesAsync()
        {
            var purposeDomain = await namenaRepository.GetAllAsync();


            var purposeDTO = mapper.Map<List<Models.DTO.Namena>>(purposeDomain);


            return Ok(purposeDTO);
        }


        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetNamenaAsync")]
        public async Task<IActionResult> GetPurposeAsync(int id)
        {
            var purpose = await namenaRepository.GetByIdAsync(id);

            if (purpose == null)
            {

                return NotFound("There is no purpose with this id.");
            }


            var purposeDTO = mapper.Map<Models.DTO.Namena>(purpose);

            return Ok(purposeDTO);
        }

        [HttpPost] //, Authorize(Roles = "Admin")
        public async Task<IActionResult> AddPurposeDTOAsync(Models.DTO.AddNamenaRequest addNamenaRequest)
        {
            var purpose = new Db.TblNamena()
            {
                NazivNamene = addNamenaRequest.NazivNamene
            };

            purpose = await namenaRepository.AddAsync(purpose);

            var purposeDTO = mapper.Map<Models.DTO.Namena>(purpose);

            return Ok(purposeDTO);
        }


        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePurposeAsync(int id)
        {

            //get doc from database

            var purpose = await namenaRepository.DeleteAsync(id);

            if (purpose == null)
            {
                return NotFound("There is no purpose with this id.");
            }

            var purposeDTO = mapper.Map<Models.DTO.Namena>(purpose);


            return Ok(purposeDTO);

        }

        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePurposeAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateNamenaRequest updateNamenaRequest)
        {
            var purpose = new Db.TblNamena()
            {

                NazivNamene = updateNamenaRequest.NazivNamene
            };

            purpose = await namenaRepository.UpdateAsync(id, purpose);

            if (purpose == null)
            {
                return NotFound("There is no purpose with this id.");
            }
            var purposeDTO = mapper.Map<Models.DTO.Namena>(purpose);




            return Ok(purposeDTO);

        }
    }
}
