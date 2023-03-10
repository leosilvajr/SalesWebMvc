using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required.")]          //{0} pega o nome atribuido
        [StringLength(60, MinimumLength = 3,ErrorMessage = "{0} size should be between {2} and {1}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [EmailAddress(ErrorMessage ="Enter a valid email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Range(100.0, 5000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        //Associação para muitos.

        //Garantindo pro Entity Framework que o Id vai ter que existir, uma vez que o tipo ind nao pode ser nulo
        [Display(Name = "ID: Department")]
        public int DepartmentId { get; set; }

        //Add-Migration DepartmentForeignKey
        //Update-database

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        /// <summary>
        /// Calcular o total de vendas de um vendedor entre a data inicial e final e depois somar as vendas.
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public double TotalSales(DateTime initial, DateTime final)
        {
            //Vamos usar o Linq                                              //Soma das Vendas
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
