using AutoMapper;
using Project_PRN231_API.Models;
using Project_PRN231_API.ViewModel.CareProcess;
using Project_PRN231_API.ViewModel.Crop;
using Project_PRN231_API.ViewModel.Harvesting;
using Project_PRN231_API.ViewModel.Storage;
using Project_PRN231_API.ViewModel.User;
using System.Runtime.InteropServices;

namespace Project_PRN231_API.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Crop, CropVM>()
                .ForMember(dest => dest.CropId, otp => otp.MapFrom(src => src.CropId))
                .ForMember(dest => dest.CropName, otp => otp.MapFrom(src => src.CropName))
                .ForMember(dest => dest.PlantingDate, otp => otp.MapFrom(src => src.PlantingDate))
                .ForMember(dest => dest.ExpectedHarvestDate, otp => otp.MapFrom(src => src.ExpectedHarvestDate))
                .ForMember(dest => dest.ActualHarvestDate, otp => otp.MapFrom(src => src.ActualHarvestDate)).ReverseMap();

            CreateMap<CareProcess, CareProcessVM>()
                .ForMember(dest => dest.CareProcessId, otp => otp.MapFrom(src => src.CareProcessId))
                .ForMember(dest => dest.CropId, otp => otp.MapFrom(src => src.CropId))
                .ForMember(dest => dest.CropName, otp => otp.MapFrom(src => src.Crop.CropName))
                .ForMember(dest => dest.Description, otp => otp.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartDate, otp => otp.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, otp => otp.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.PerformedBy, otp => otp.MapFrom(src => src.PerformedBy))
                .ReverseMap();
            CreateMap<CareProcessVM, CareProcess>()
          .ForMember(dest => dest.Crop, opt => opt.Ignore());

            CreateMap<Harvesting, HarvestingVM>()
            .ForMember(dest => dest.HarvestId, opt => opt.MapFrom(src => src.HarvestId))
            .ForMember(dest => dest.CropName, opt => opt.MapFrom(src => src.Crop != null ? src.Crop.CropName : ""))
            .ForMember(dest => dest.HarvestDate, opt => opt.MapFrom(src => src.HarvestDate))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));

            CreateMap<HarvestingVM, Harvesting>()
                .ForMember(dest => dest.HarvestId, opt => opt.MapFrom(src => src.HarvestId))
                .ForMember(dest => dest.HarvestDate, opt => opt.MapFrom(src => src.HarvestDate))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
                .ForPath(dest => dest.Crop.CropName, opt => opt.MapFrom(src => src.CropName));

            CreateMap<Storage, StorageViewModel>();
            CreateMap<StorageViewModel, Storage>();

            CreateMap<User, UserVM>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
              .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : null));
        }
    }
}
