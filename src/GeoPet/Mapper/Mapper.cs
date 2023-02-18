using AutoMapper;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;

namespace GeoPet.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<PetRequest, PetDto>();
        CreateMap<PetParentRequest, PetParentDto>();
        CreateMap<PetDto, PetResponse>();
        CreateMap<PetParentDto, PetParentResponse>();
    }
}