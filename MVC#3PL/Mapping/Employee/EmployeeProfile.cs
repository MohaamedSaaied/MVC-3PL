using AutoMapper;
using DAL.Models;
using MVC_3PL.ViewModel;

namespace MVC_3PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
