using AutoMapper;
using backend.DTO_s;
using backend.Models;
using System.Globalization;

namespace backend.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Departamento
            //convert departament to class departmentDTO
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            #endregion

            #region Empleado
            //mapeo de nuevos campos en el DTO
            // destino hace alusion al EmpleadoDTO
            CreateMap<Empleado, EmpleadoDTO>()
               .ForMember(destino =>
                    destino.NombreDepartamento,
                    opt => opt.MapFrom(origen => origen.IdDepartamentoNavigation.Nombre)
                )
               .ForMember(destino =>
                    destino.FechaContrato,
                    opt => opt.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yyyy")));


            //EmpleadoDTO to Empleado
            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino =>
                    destino.IdDepartamentoNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.FechaContrato,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );

            #endregion


        }

    }
}
