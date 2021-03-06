using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using API_EndPoint_220522.Models.SignalR;
using API_EndPoint_220522.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ILogger<CityController> logger;
        ICityService service;
        IStringLocalizer<CityController> localizer;
        IHubContext<BroadCastHub, IHubClient> hubContext;

        public CityController(ILogger<CityController> logger,ICityService service, IStringLocalizer<CityController> localizer, IHubContext<BroadCastHub, IHubClient> hubContext)
        {
            this.logger = logger;
            this.service = service;
            this.localizer = localizer;
            this.hubContext = hubContext;
        }

        [HttpPost("AddCity",Name = "AddCity")]
        public async Task<IActionResult> AddCity(CityDTO newCity)
        {
            newCity.name = localizer.GetString(newCity.name).Value;

            var res = await service.AddCityAsync(newCity);

            if (res != null)
            {
                logger.LogInformation($"City {localizer.GetString(newCity.name).Value} has been added to DB with ID {res.id}");
                await hubContext.Clients.All.BroadcastMessage();
                return Ok(res);
            }

            return BadRequest("Unable to add new city");
        }

        [HttpGet("GetAllCities",Name = "GetAllCities")]
        public async Task<IActionResult> GetAllCities()
        {
            var res = await service.GetAllCitiesAsync();

            if (res != null) 
            {
                logger.LogInformation($"Obtained {res.Count()} cities from DB..!!");
                return Ok(res); 
            }

            var errMsg = "Unable to obtain city list";
            logger.LogError(errMsg);
            return BadRequest(errMsg);
        }
    }
}
