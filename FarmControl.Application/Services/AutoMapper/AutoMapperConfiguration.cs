using AutoMapper;
using HashidsNet;
namespace FarmControl.Application.Services.AutoMapper;

public class AutoMapperConfiguration : Profile
{
    private readonly IHashids _hashids;
    public AutoMapperConfiguration(IHashids hashids)
    {
        _hashids = hashids;


        RequestForEntity();
        EntityForResponse();
    }

    private void RequestForEntity()
    {
        CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entities.User>()
                    .ForMember(destiny => destiny.Password, config => config.Ignore());
    }

    private void EntityForResponse()
    {

    }
}