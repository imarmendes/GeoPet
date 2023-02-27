using AutoMapper;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;

namespace GeoPet.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<PetRequest, Pet>();
        CreateMap<OwnerRequest, Owner>();
        CreateMap<Pet, PetResponse>();
        CreateMap<Owner, UserResponse>();
    }
}