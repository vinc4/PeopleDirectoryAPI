using AutoMapper;
using PeopleDirectory.Application.DTO.Requests;
using PeopleDirectory.Intergration.Entities;

namespace PeopleDirectoryAPI.MappingProfiles
{
    public class ControllerReqToServiceReqProfile: Profile
    {
        public ControllerReqToServiceReqProfile() {
            CreateMap<ClientDto, Clients>();
        }
    }
}
