using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index() //Chamo o controlador
        {
            //Controlador acessou o Modesl, pegou o dado na list e encaminha os dados para a view
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Vamos criar a ação Create
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); //Busca do Banco de Dados, todos os departamentos
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };
            //Passando o objeto viewModel para o retorno da View
            return View(viewModel);
            //Abrir formulario para cadastrar vendedor
        }

        //Inserir Vendedor no Banco de Dados
        [HttpPost]
        [ValidateAntiForgeryToken]//Previnir ataques CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));//Retorna o Index de Sellers
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));//Retorna o Index de Sellers

        }

        public IActionResult Details(int? id)
        {//Logica bem parecida com o DeleteGet
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {   //Abrir tela pra Editar o Vendedor
            if (id == null)
            {
                return NotFound();            
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();            
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id) //
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

    }
}
