using AutoMapper;
using EmployeeHub_MinimalAPI.Models;

namespace EmployeeHub_MinimalAPI {

    public class MappingConfig : Profile {

        //when we add DTOs we'll need to add them here as well
        public MappingConfig() {
            CreateMap<Employee, Employee>().ReverseMap();
            //Example:
            //CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
        }

    }

}