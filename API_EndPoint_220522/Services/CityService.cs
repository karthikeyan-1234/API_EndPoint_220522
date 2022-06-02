using API_EndPoint_220522.Caching;
using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using API_EndPoint_220522.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Services
{
    public class CityService : ICityService
    {
        IGenericRepo<City> repo;
        ILogger<ICityService> logger;
        ICacheManager cache;
        IMapper mapper;

        public CityService(IGenericRepo<City> repo, ILogger<ICityService> logger,ICacheManager cache,IMapper mapper)
        {
            this.repo = repo; this.logger = logger; this.cache = cache; this.mapper = mapper;
        }

        public async Task<CityDTO> AddCityAsync(CityDTO newCity)
        {
            try
            {
                var citi = mapper.Map<City>(newCity);
                var res = await repo.AddAsync(citi);
                await repo.SaveChangesAsync();
                return mapper.Map<CityDTO>(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, newCity);
                return null;
            }
        }

        public async Task<CityDTO> DeleteCityAsync(CityDTO city)
        {
            try
            {
                var res = repo.Delete(mapper.Map<City>(city));
                await repo.SaveChangesAsync();
                return mapper.Map<CityDTO>(res);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message, city);
                return null;
            }
        }

        public async Task<CityDTO> UpdateCityAsync(CityDTO city)
        {
            try
            {
                var res = repo.Update(mapper.Map<City>(city));
                await repo.SaveChangesAsync();
                return mapper.Map<CityDTO>(res);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message, city);
                return null;
            }
        }

        public async Task<IEnumerable<CityDTO>> GetAllCitiesAsync()
        {
            try
            {
                var key = this.GetType().ToString() + "-" + MethodBase.GetCurrentMethod().Name;
                var res = await cache.TryGetAsync<IEnumerable<City>>(key);

                if (res == null)
                {
                    logger.LogInformation("Cache is empty, obtaining city list from DB");
                    res = await repo.GetAllAsync();
                    await cache.TrySetAsync(key, res);
                }
                else
                    logger.LogInformation("Obtained city list from Cache");

                return mapper.Map<IEnumerable<CityDTO>>(res);
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
