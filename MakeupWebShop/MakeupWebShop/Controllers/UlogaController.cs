using AutoMapper;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UlogaController : Controller
    {
        private readonly IUlogaRepository ulogaRepository;
        public readonly IMapper mapper;



        public UlogaController(IUlogaRepository ulogaRepository, IMapper mapper)

        {
            this.ulogaRepository = ulogaRepository;
            this.mapper = mapper;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var role = await ulogaRepository.GetAllAsync();


            var roleDTO = mapper.Map<List<Models.DTO.Uloga>>(role);


            return Ok(roleDTO);
        }


        [HttpGet, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        [ActionName("GetUlogaAsync")]
        public async Task<IActionResult> GetRoleAsync(int UlogaId)
        {
            var role = await ulogaRepository.GetByIdAsync(UlogaId);

            if (role == null)
            {
                return NotFound("There is no role with this id.");
            }


            var roleDTO = mapper.Map<Models.DTO.Uloga>(role);

            return Ok(roleDTO);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleDTOAsync(Models.DTO.AddUlogaRequest addUlogaRequest)
        {
            var role = new Db.TblUloga()
            {
                NazivUloge = addUlogaRequest.NazivUloge,
                KratakOpis = addUlogaRequest.KratakOpis,
                Sifra = addUlogaRequest.Sifra
            };

            role = await ulogaRepository.AddAsync(role);

            var roleDTO = mapper.Map<Models.DTO.Uloga>(role);

            return Ok(roleDTO);
        }


        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteRoleAsync(int UlogaId)
        {
            var role = await ulogaRepository.DeleteAsync(UlogaId);

            if (role == null)
            {
                return NotFound("There is no role with this id.");
            }

            var roleDTO = mapper.Map<Models.DTO.Uloga>(role);


            return Ok(roleDTO);

        }

        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute] int UlogaId, [FromBody] Models.DTO.UpdateUlogaRequest updateUlogaRequest)
        {
            var role = new Db.TblUloga()
            {

                NazivUloge = updateUlogaRequest.NazivUloge,
                KratakOpis = updateUlogaRequest.KratakOpis,
                Sifra = updateUlogaRequest.Sifra
            };

            role = await ulogaRepository.UpdateAsync(UlogaId, role);

            if (role == null)
            {
                return NotFound("There is no role with this id.");
            }
            var roleDTO = mapper.Map<Models.DTO.Uloga>(role);




            return Ok(roleDTO);

        }
    }
}
