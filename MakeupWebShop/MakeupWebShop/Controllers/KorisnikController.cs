using AutoMapper;
using BCrypt.Net;
using MakeupWebShop.Db;
using MakeupWebShop.Models.DTO;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MakeupWebShop.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IAdresaRepository adresaRepository;
        private readonly IMapper mapper;
        public KorisnikController(IKorisnikRepository korisnikRepository, IMapper mapper,
            IConfiguration configuration, IAdresaRepository adresaRepository)
        {
            this.configuration = configuration;
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
            this.adresaRepository = adresaRepository;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var usersEntity = await korisnikRepository.GetAllAsync();

            var usersDto = mapper.Map<List<Models.DTO.Korisnik>>(usersEntity);

            return Ok(usersDto);
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var userEntity = await korisnikRepository.GetByIdAsync(id);

            if (userEntity == null)
            {
                return NotFound("There is no user with this id.");
            }

            var userDto = mapper.Map<Models.DTO.Korisnik>(userEntity);
            return Ok(userDto);
        }
        //public static TblKorisnik userRegistred = new Db.TblKorisnik();



        [HttpGet]
        //[Authorize(Roles = "Admin, User")]
        [Route("username/{username}")]
        public async Task<IActionResult> GetUserByUsernameAsync(string username)
        {
            var userEntity = await korisnikRepository.GetByUsernameAsync(username);

            if (userEntity == null)
            {
                return NotFound("There is no user with this username.");
            }

            var userDto = mapper.Map<Models.DTO.Korisnik>(userEntity);
            return Ok(userDto);
        }


        [HttpPost("register")]
        public async Task<IActionResult> AddUserAsync(Models.DTO.AddKorisnikRequest addKorisnikRequest)
        {
            string hashLozinka = BCrypt.Net.BCrypt.HashPassword(addKorisnikRequest.Lozinka);
            // Check if the address already exists
            TblAdresa adresa = await adresaRepository.GetAdresaByDetailsAsync(addKorisnikRequest);

            Random random = new Random();
            int randomId = random.Next(1000000);


            //Request(DTO) to entity model
            var userEntity = new Db.TblKorisnik()
            {
                KorisnikId= randomId,
                Ime = addKorisnikRequest.Ime,
                Prezime = addKorisnikRequest.Prezime,
                Jmbg = "jmbg000000000",
                Email = addKorisnikRequest.Email,
                Kontakt = addKorisnikRequest.Kontakt,
                Username = addKorisnikRequest.Username,
                Lozinka = hashLozinka,
                BrojKupovina = 0,
                Adresa = adresa,
                UlogaId =1,
                //UlogaId = addKorisnikRequest.UlogaId,

            };
            if (userEntity.UlogaId == 2)
            {
                userEntity.BrojKupovina = 0;
            }
            //pass details to Repository
            userEntity = await korisnikRepository.AddAsync(userEntity);

            //Conwert back to DTO
            var userDto = mapper.Map<Models.DTO.Korisnik>(userEntity);
       
            return Ok(userDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(Models.DTO.LoginKorisnikRequest loginKorisnikRequest)
        {
            var userEntity = await korisnikRepository.GetByUsernameAsync(loginKorisnikRequest.Username);


            if (userEntity == null)
            {
                return NotFound("User not found");
            }

            // Verify if the password is correct
            if (!BCrypt.Net.BCrypt.Verify(loginKorisnikRequest.Lozinka, userEntity.Lozinka))
            {
                return Unauthorized("Invalid password");
            }

            string token = CreateToken(userEntity);
            return Ok(token);
        }
        [HttpDelete, Authorize(Roles = "Admin,User")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            //Get address from Db
            var userEntity = await korisnikRepository.DeleteAsync(id);

            //If null NotFound
            if (userEntity == null)
            {
                return NotFound("There is no user with this id.");
            }

            //Convert response to DTO
            var userDto = mapper.Map<Models.DTO.Korisnik>(userEntity);
            //Return response
            return Ok(userDto);
        }
        [HttpPut, Authorize(Roles = "Admin,User")]
        [Route("{id:int}/korisnik")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateKorisnikRequest updateKorisnikRequest)
        {
            string hashLozinka = BCrypt.Net.BCrypt.HashPassword(updateKorisnikRequest.Lozinka);
            //Convert DTO to entity
            var userEntity = new Db.TblKorisnik()
            {
                Ime = updateKorisnikRequest.Ime,
                Prezime = updateKorisnikRequest.Prezime,
                Jmbg = "jmbg000000000",
                Email = updateKorisnikRequest.Email,
                Kontakt = updateKorisnikRequest.Kontakt,
                Username = updateKorisnikRequest.Username,
                Lozinka = hashLozinka,
                BrojKupovina = updateKorisnikRequest.BrojKupovina,
                AdresaId = updateKorisnikRequest.AdresaId,
                //UlogaId = updateKorisnikRequest.UlogaId,
            };


            //Update Address using repository

            userEntity = await korisnikRepository.UpdateAsync(id, userEntity);

            //if null NotFound
            if (userEntity == null)
            {
                return NotFound("There is no user with this id.");
            }

            //Convert back to DTO
            var userDto = mapper.Map<Models.DTO.Korisnik>(userEntity);
            //Return ok
            return Ok(userDto);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{korisnikId:int}/uloga")]
        public async Task<IActionResult> UpdateUserRoleAsync([FromRoute] int korisnikId, [FromBody] Models.DTO.UpdateKorisnikUlogaRequest updateKorisnikUlogaRequest)
        {
            //Convert DTO to entity
            var userEntity = new Db.TblKorisnik()
            {
                UlogaId = updateKorisnikUlogaRequest.UlogaId,
            };


            //Update Address using repository

            userEntity = await korisnikRepository.UpdateUlogaAsync(korisnikId, userEntity);

            //if null NotFound
            if (userEntity == null)
            {
                return NotFound("There is no user with this id.");
            }

            //Convert back to DTO
            var userDto = mapper.Map<Models.DTO.Korisnik>(userEntity);
            //Return ok
            return Ok(userDto);
        }


        private string CreateToken(TblKorisnik korisnik)
        {
            List<Claim> claims = new List<Claim> ();
            if (korisnik.UlogaId==2)
            {
                claims.Add(new Claim(ClaimTypes.Name, korisnik.Username));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
               
            }

            else if (korisnik.UlogaId == 1)
            {
                claims.Add(new Claim(ClaimTypes.Name, korisnik.Username));
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
    
}
