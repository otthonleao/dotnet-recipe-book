using AutoMapper;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        // CreateMap<RequestRegisterUserJson, User>()
        //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //     .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
        //     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
        //     .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(destination => destination.Password, options => options.Ignore());
    }
}