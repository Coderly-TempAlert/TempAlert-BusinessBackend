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

        CreateMap<Store, AddStoreDto>()
            .ReverseMap();

        CreateMap<Store, UpdateStoreDto>()
            .ReverseMap();

        CreateMap<Alert, AddAlertDto>()
            .ReverseMap();

        CreateMap<StoreProducts, StoreProducts>()
           .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store))
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

    }

}
