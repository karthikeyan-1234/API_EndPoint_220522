using API_EndPoint_220522.Caching;
using API_EndPoint_220522.Mapper;
using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using API_EndPoint_220522.Repositories;
using API_EndPoint_220522.Services;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace API_UnitTests_220522
{
    public class UnitTest
    {

        List<City> Cities = new List<City>()
            {
                new City { id = 1, name = "Chennai", pincode = "600042" },
                new City { id = 2, name = "Coimbatore", pincode = "600100"}
            };

        CityDTO newCityDTO = new CityDTO { id = 0, name = "Salem", pincode = "600043" };

        Mock<IGenericRepo<City>> mockCityRepo = new Mock<IGenericRepo<City>>();
        MapperConfiguration mapper_config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Mapping());
        });
        ICityService CityService;
        ICacheManager CacheManager;
        IMapper mapper;

        public UnitTest()
        {
            mockCityRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Cities);
            mockCityRepo.Setup(x => x.AddAsync(It.IsAny<City>()))
                .ReturnsAsync((City C) => {
                    C.id = Cities.Count + 1;
                    Cities.Add(C); return C;
                });
            mockCityRepo.Setup(x => x.Delete(It.IsAny<City>()))
                .Returns((City C) =>
                {
                    Cities.Remove(C);
                    return C;
                });
            mockCityRepo.Setup(x => x.Update(It.IsAny<City>()))
                .Returns((City C) =>
                {
                    Cities[C.id - 1] = C;
                    return C;
                }
                );
            
            mapper = mapper_config.CreateMapper();

            Mock<IDistributedCache> Cache = new Mock<IDistributedCache>();
            CacheManager = new CacheManager(Cache.Object);

            CityService = new CityService(mockCityRepo.Object, new NullLogger<ICityService>(), CacheManager,mapper);
        }

        [Fact]
        public void GeneralTest()
        {
            float x = 0;
            x.Should().BeOfType(typeof(float));
        }

        [Fact]
        public void Should_Get_All_Cities() => 
            CityService.GetAllCitiesAsync().Result.Should().BeOfType<List<CityDTO>>();

        [Fact]
        public void Should_Add_New_City() =>
            CityService.AddCityAsync(newCityDTO).Result.Should().BeOfType<CityDTO>();
        
    }
}
