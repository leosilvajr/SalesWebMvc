using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departament
    {   //Correção
        public int Id { get; set; }
        public string Name { get; set; }

        //Implementando a associação do Departamento com o Seller
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Departament()
        {

        }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        /// <summary>
        /// Calcular total de vendas do departamento para o intervalo de Datas.
        /// Pegar a lista de vendas com todos os vendedores do Departamento e somar o total de vendas de cada vendedor no intervalo de data.
        /// 
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public double TotalSales(DateTime initial, DateTime final)
        {
            //Pegando cada vendedor da minha lista
            //Chamando o TotalSales do vendedor
            //E soma o resultado para todos os vendedores do departamento.
            return Sellers.Sum(seller => seller.TotalSales(initial,final));
        }
    }
}
