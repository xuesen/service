using AutoMapper;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Quality;

namespace Runtime.Interface.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Terminal, TerminalDTO>()
                    .ForPath(dto => dto.Pdline.Id, (map) => map.MapFrom(m => m.ProductLine.Id))
                    .ForPath(dto => dto.Pdline.Code, (map) => map.MapFrom(m => m.ProductLine.Code))
                    .ForPath(dto => dto.Pdline.Name, (map) => map.MapFrom(m => m.ProductLine.Name))
                    .ForPath(dto => dto.Process.Id, (map) => map.MapFrom(m => m.Process.Id))
                    .ForPath(dto => dto.Process.Code, (map) => map.MapFrom(m => m.Process.Code))
                    .ForPath(dto => dto.Process.Name, (map) => map.MapFrom(m => m.Process.Name))
                    .ForPath(dto => dto.Process.Type.Id, (map) => map.MapFrom(m => m.Process.ProcessType.Id))
                    .ForPath(dto => dto.Process.Type.Code, (map) => map.MapFrom(m => m.Process.ProcessType.Code))
                    .ForPath(dto => dto.Process.Type.Name, (map) => map.MapFrom(m => m.Process.ProcessType.Name))
                    .ForPath(dto => dto.Equipment.Id, (map) => map.MapFrom(m => m.Equipment.Id))
                    .ForPath(dto => dto.Equipment.Code, (map) => map.MapFrom(m => m.Equipment.Code))
                    .ForPath(dto => dto.Equipment.Name, (map) => map.MapFrom(m => m.Equipment.Name));

            CreateMap<Symptom, CommonDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.Code, (map) => map.MapFrom(m => m.Code))
                    .ForPath(dto => dto.Name, (map) => map.MapFrom(m => m.Name));

            CreateMap<SymptomType, CommonDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.Code, (map) => map.MapFrom(m => m.Code))
                    .ForPath(dto => dto.Name, (map) => map.MapFrom(m => m.Name));

            CreateMap<Cause, CommonDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.Code, (map) => map.MapFrom(m => m.Code))
                    .ForPath(dto => dto.Name, (map) => map.MapFrom(m => m.Name));

            CreateMap<Duty, CommonDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.Code, (map) => map.MapFrom(m => m.Code))
                    .ForPath(dto => dto.Name, (map) => map.MapFrom(m => m.Name));

            CreateMap<RepairType, CommonDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.Code, (map) => map.MapFrom(m => m.Code))
                    .ForPath(dto => dto.Name, (map) => map.MapFrom(m => m.Name));  

            CreateMap<Process, CommonDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.Code, (map) => map.MapFrom(m => m.Code))
                    .ForPath(dto => dto.Name, (map) => map.MapFrom(m => m.Name));     

            CreateMap<Part, PartItemDTO>()
                    .ForPath(dto => dto.Id, (map) => map.MapFrom(m => m.Id))
                    .ForPath(dto => dto.PartNo, (map) => map.MapFrom(m => m.PartNo))
                    .ForPath(dto => dto.PartName, (map) => map.MapFrom(m => m.PartName))  
                    .ForPath(dto => dto.SPEC1, (map) => map.MapFrom(m => m.Spec1));
        }
    }
}