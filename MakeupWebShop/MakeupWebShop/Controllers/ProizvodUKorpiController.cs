using AutoMapper;
using MakeupWebShop.Db;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakeupWebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class ProizvodUKorpiController : Controller
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly IProizvodUKorpiRepository proizvodUKorpiRepository;
        private readonly IKorpaRepository korpaRepository;
        private readonly IMapper mapper; private readonly ILogger<ProizvodUKorpiController> logger; // Add ILogger field here

        public ProizvodUKorpiController(IProizvodUKorpiRepository proizvodUKorpiRepository,
            IProizvodRepository proizvodRepository, IKorpaRepository korpaRepository,IMapper mapper,
            ILogger<ProizvodUKorpiController> logger) // Add ILogger parameter here
        {
            this.proizvodUKorpiRepository = proizvodUKorpiRepository;
            this.proizvodRepository = proizvodRepository;
            this.korpaRepository = korpaRepository;
            this.mapper = mapper;
            this.logger = logger; // Store ILogger instance here
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProductInShoppingCartsAsync()
        {
            var productInShoppingCartsEntity = await proizvodUKorpiRepository.GetAllAsync();

            var productInShoppingCartsDto = mapper.Map<List<Models.DTO.ProizvodUKorpi>>(productInShoppingCartsEntity);

            return Ok(productInShoppingCartsDto);
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<IActionResult> GetProductInShoppingCartByIdAsync(int id)
        {
            var productInShoppingCartEntity = await proizvodUKorpiRepository.GetByIdAsync(id);

            if (productInShoppingCartEntity == null)
            {
                return NotFound("There is no product In Shopping Cart with this id.");
            }

            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            return Ok(productInShoppingCartDto);
        }
        [HttpGet]
        [Route("korpa/{korpaId:int}")]
        public async Task<IActionResult> GetProductInShoppingCartByKorpaIdAsync(int korpaId)
        {
            var productInShoppingCartEntity = await proizvodUKorpiRepository.GetByKorpaIdAsync(korpaId);

            if (productInShoppingCartEntity == null)
            {
                return NotFound("There is no product In Shopping Cart with this korpaId.");
            }

            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            return Ok(productInShoppingCartDto);
        }
        [HttpPost, Authorize(Roles = "User")]
        public async Task<IActionResult> AddProductInShoppingCartAsync(Models.DTO.AddProizvodUKorpiRequest addProizvodUKorpiRequest)
        {
            //Request(DTO) to entity model
            var productInShoppingCartEntity = new Db.TblProizvodUkorpi()
            {
                BrojKomada = addProizvodUKorpiRequest.BrojKomada,
                ProizvodId = addProizvodUKorpiRequest.ProizvodId,
                KorpaId = addProizvodUKorpiRequest.KorpaId,

            };
           
            //pass details to Repository
            productInShoppingCartEntity = await proizvodUKorpiRepository.AddAsync(productInShoppingCartEntity);

            await proizvodRepository.AddProductToCart(productInShoppingCartEntity.ProizvodId, productInShoppingCartEntity.BrojKomada, productInShoppingCartEntity.ProizUkorpiId, productInShoppingCartEntity.KorpaId);
            //await korpaRepository.UpdateTotalPriceAsync(productInShoppingCartEntity.ProizUkorpiId, productInShoppingCartEntity.KorpaId, productInShoppingCartEntity.ProizvodId, true);

            //Conwert back to DTO
            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            /*
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { id = addressDto.AdresaId }, addressDto);
           */
            return Ok(productInShoppingCartDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProductInShoppingCartAsync(int id)
        {
            var existingProductInShoppingCartEntity = (TblProizvodUkorpi)await proizvodUKorpiRepository.GetByIdAsync(id);
            //await korpaRepository.UpdateTotalPriceAsync(existingProductInShoppingCartEntity.ProizUkorpiId, existingProductInShoppingCartEntity.KorpaId, existingProductInShoppingCartEntity.ProizvodId, false);
            //Get address from Db
            var productInShoppingCartEntity = await proizvodUKorpiRepository.DeleteAsync(id);

           // await korpaRepository.ReduceTotalPriceAsync(existingProductInShoppingCartEntity.KorpaId, existingProductInShoppingCartEntity.ProizvodId, existingProductInShoppingCartEntity.BrojKomada);
            await proizvodRepository.RemoveProductFromCart(productInShoppingCartEntity.ProizvodId, productInShoppingCartEntity.BrojKomada, existingProductInShoppingCartEntity.KorpaId);
            

            //If null NotFound
            if (productInShoppingCartEntity == null)
            {
                return NotFound("There is no product In Shopping Cart with this id.");
            }

            //Convert response to DTO
            var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(productInShoppingCartEntity);
            //Return response
            return Ok(productInShoppingCartDto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateProizvodUKorpiRequest updateProizvodUKorpiRequest)
        {
            var existingProductInShoppingCartEntity = (TblProizvodUkorpi)await proizvodUKorpiRepository.GetByIdAsync(id);

            //Debugging statement
            //System.Diagnostics.Debug.WriteLine("existingProductInShoppingCartEntity.ProizvodId: " + existingProductInShoppingCartEntity.ProizvodId.ToString());

            //Convert DTO to entity
            var productInShoppingCartEntity = new Db.TblProizvodUkorpi()
            {
                BrojKomada = updateProizvodUKorpiRequest.BrojKomada,
                //ProizvodId = updateProizvodUKorpiRequest.ProizvodId,
                //KorpaId = updateProizvodUKorpiRequest.KorpaId,
            };

            //Debugging statement
            //System.Diagnostics.Debug.WriteLine("updatedProductInShoppingCartEntity.ProizvodId: " + productInShoppingCartEntity.ProizvodId.ToString());

            // Check if the quantity is changed, and update the product quantity accordingly.
            //if (existingProductInShoppingCartEntity.ProizvodId == productInShoppingCartEntity.ProizvodId)
            //{
                //Debugging statement
                System.Diagnostics.Debug.WriteLine("Inside if statement");

                if (existingProductInShoppingCartEntity.BrojKomada < productInShoppingCartEntity.BrojKomada)
                {

                    int difference = productInShoppingCartEntity.BrojKomada - existingProductInShoppingCartEntity.BrojKomada;
                    await proizvodRepository.AddProductToCart(existingProductInShoppingCartEntity.ProizvodId, difference, productInShoppingCartEntity.ProizUkorpiId, existingProductInShoppingCartEntity.KorpaId);

                }
                else if (existingProductInShoppingCartEntity.BrojKomada > productInShoppingCartEntity.BrojKomada)
                {
                    int difference = existingProductInShoppingCartEntity.BrojKomada - productInShoppingCartEntity.BrojKomada;
                    await proizvodRepository.RemoveProductFromCart(existingProductInShoppingCartEntity.ProizvodId, difference, existingProductInShoppingCartEntity.KorpaId);

                }
            //}
           /* else
            {
                //Debugging statement
                System.Diagnostics.Debug.WriteLine("Inside else statement");

                await proizvodRepository.RemoveProductFromCart(existingProductInShoppingCartEntity.ProizvodId, existingProductInShoppingCartEntity.BrojKomada, existingProductInShoppingCartEntity.KorpaId);

                await proizvodRepository.AddProductToCart(productInShoppingCartEntity.ProizvodId, productInShoppingCartEntity.BrojKomada, productInShoppingCartEntity.ProizUkorpiId, productInShoppingCartEntity.KorpaId);


            }*/
                //Update entity in the database
                var updatedProductInShoppingCartEntity = await proizvodUKorpiRepository.UpdateAsync(id, productInShoppingCartEntity);
                //if null NotFound
                if (updatedProductInShoppingCartEntity == null)
                {
                    return NotFound("There is no product In Shopping Cart with this id.");
                }

                //Convert back to DTO
                var productInShoppingCartDto = mapper.Map<Models.DTO.ProizvodUKorpi>(updatedProductInShoppingCartEntity);

                //Logging statement
                logger.LogInformation("Product with id " + id.ToString() + " has been updated.");

                //Return ok
                return Ok(productInShoppingCartDto);
            
            
        }

        }
    }
