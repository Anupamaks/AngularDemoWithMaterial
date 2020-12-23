using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{

    [ApiController]
    [Route("adverts/v1")]
    public class Advert : ControllerBase
    {


        private readonly IAdvertStorageServicecs _AdvertStorageServicecs;
        public Advert(IAdvertStorageServicecs AdvertStorageServicecs)
        {
            _AdvertStorageServicecs = AdvertStorageServicecs;
        }
        [Route("Create")]
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201,Type=typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordid;
            try
            {

              recordid = await _AdvertStorageServicecs.Add(model);
            }

            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
            return StatusCode(201, new CreateAdvertResponse { Id= recordid });

        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            string recordid;
            try
            {

                 await _AdvertStorageServicecs.Confirm(model);
            }

            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
            return new OkResult();

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }

}


           
    