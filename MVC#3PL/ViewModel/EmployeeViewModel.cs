using DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_3PL.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; } //Nav prop
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

    }
}
