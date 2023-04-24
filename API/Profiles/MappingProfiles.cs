using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, AddUpdateProductDto>()
            .ReverseMap();

        CreateMap<Store, AddUpdateStoreDto>()
            .ReverseMap();

    }
}
