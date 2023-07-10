using Api.Data.Entities;
using Api.Data.Entities.Identity;
using Api.Dto.Address;
using Api.Dto.Bank;
using Api.Dto.BloodGroupe;
using Api.Dto.Idintity;
using Api.Dto.User;
using AutoMapper;

namespace Api.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {



            CreateMap<ApplicationUser, UserDetailDto>()
                .IncludeMembers(s => s.Address)
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));
            CreateMap<Address, UserDetailDto>();
            CreateMap<UserUpdateDto, ApplicationUser > ()
                .ForMember(src => src.Picture, opt => opt.Ignore());
            CreateMap<UserUpdateDto, Address > ();
            CreateMap<Governorate, CityDto>();
            CreateMap<City, CityDto>();
            CreateMap<ApplicationUser, GetDonorsDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));
            CreateMap<ApplicationUser, GetDonorById>()
                .IncludeMembers(s => s.Address)
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));
            CreateMap<Address, GetDonorById>();
            CreateMap<UpdateBankInAdminDto, Bank>();
            CreateMap<UpdateBankInAdminDto, Address>();
            CreateMap<AddBankInAdminDto, Bank>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));
            CreateMap<AddBankInAdminDto, Address>();

        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;
            return age;
        }
    }

        
}
