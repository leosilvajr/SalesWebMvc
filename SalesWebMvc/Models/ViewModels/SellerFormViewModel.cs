using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    /*
     * Classe que vai conter os dados para o formulario de cadastro de vendedor{Seller}
     */
    public class SellerFormViewModel
    {
        //Dados Necessario
        public Seller Seller { get; set; }

        //Lista de Coleção do Tipo Generica Departamento
        public ICollection<Department> Departments { get; set; }
    }
}
