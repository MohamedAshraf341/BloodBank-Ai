using AutoMapper;
using BL.Dtos;
using DAL.Models;

namespace BL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ICollection<Address>, BankDetailDto>();
            CreateMap<Address, GetDonorById>()
                .IncludeMembers(s => s.User)
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.User.DateOfBirth)));
            CreateMap<Address, BankDetailDto>();
            CreateMap<Address, UserProfileDetailDto>()
                .IncludeMembers(s => s.User)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<ApplicationUser, GetDonorDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));
            CreateMap<ApplicationUser, GetDonorById>();
            CreateMap<ApplicationUser, AdminstrationDto>();
            CreateMap<ApplicationUser, UserProfileDetailDto>()
                .IncludeMembers(s => s.Address)
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

            CreateMap<Governorate, CityDto>();
            CreateMap<Governorate, GovernorateDto>()
                .ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.EnglishName));

            CreateMap<City, CityDto>()
                .IncludeMembers(s => s.Governorate)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EnglishName))
                .ForMember(dest => dest.GovernorateName,opt => opt.MapFrom(src => src.Governorate.EnglishName));

            CreateMap<UserProfileUpdateDto, ApplicationUser>();
            CreateMap<UserProfileUpdateDto, Address>();            
            CreateMap<UserProfileUpdateDto, ApplicationUser>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(src => src.Picture, opt => opt.Ignore());

            CreateMap<RegisterDto, Address>();
            CreateMap<RegisterDto, ApplicationUser>();

            CreateMap<Bank, BankDetailDto>()
                .IncludeMembers(s => s.Address)
                .ForMember(dest => dest.BloodGroups, opt => opt.MapFrom(src => src.BloodGroups))
                .ForMember(dest => dest.Moderators,opt => opt.MapFrom(src => src.Moderators));
            CreateMap<Bank, BankBlooGoupDto>()
                .IncludeMembers(s => s.BloodGroups)
                .ForMember(dest => dest.BloodGroups, opt => opt.MapFrom(src => src.BloodGroups));

            CreateMap<BankDto, Bank>()
                .ForMember(src => src.Picture, opt => opt.Ignore());
            CreateMap<BankDto,Address>();

            //CreateMap<Bank, BloodGroupDto>()
            //    .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.BloodGroups.Select(bg => bg.Group)))
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.BloodGroups.Select(bg => bg.Value)));
            CreateMap<Bank, BankBlooGoupDto>()
                .ForMember(dest => dest.BloodGroups, opt => opt.MapFrom(src => src.BloodGroups));
            CreateMap<ICollection<BloodGroup>, BankBlooGoupDto>();
            CreateMap<ICollection<BloodGroup>, ICollection<BloodGroupDto>>();
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
