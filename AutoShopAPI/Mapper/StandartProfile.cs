using AutoMapper;
using AutoShop.API.Requests.Cars;
using AutoShop.API.Requests.Users;
using AutoShop.API.Responses.Cars;
using AutoShop.API.Responses.Users;
using AutoShop.Core.Models;


namespace AutoShop.API.Mapper
{
    public class StandartProfile : Profile
    {
        public StandartProfile()
        {
            // Users
            CreateMap<UserCreateRequest, User>();
            CreateMap<UserUpdateRequest, User>();

            CreateMap<User, UserCreateResponse>();
            CreateMap<User, UserUpdateResponse>();

            // Cars
            CreateMap<CarCreateRequest, Car>();
            CreateMap<CarUpdateRequest, Car>();

            CreateMap<Car, CarCreateResponse>();
            CreateMap<Car, CarUpdateResponse>();
        }
    }
}
