using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee:BaseEntity
    {

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }


        [Range(20, 60, ErrorMessage = "Age must be Between 20 and 60")]
        public int Age { get; set; }


        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Adress must be like 123-Street-City-Country ")]
        [DisplayName("Address")]
        public string Adress { get; set; }


        [Required(ErrorMessage = "Salary is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? ImageName { get; set; }

        [Phone]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; }=DateTime.Now;
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; } //Nav prop
    
    }
}
