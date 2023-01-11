using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //Registrar o serviço no Startup.cs Injeção de independencia.


        private readonly SalesWebMvcContext _context;
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            //Acessar a fonte de dados relacionados a tabela vendedores. e Converter para lista;
            return await _context.Seller.ToListAsync();
        }

        //public List<Seller> FindAll()
        //{
        //    //Acessar a fonte de dados relacionados a tabela vendedores. e Converter para lista;
        //    return _context.Seller.ToList();
        //}

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //public void Insert(Seller obj)
        //{
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}


        public async Task<Seller> FindByIdAsync(int id)
        {                           //Eager loading = Join
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync( obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {

                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny =await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) //Any server para verificar se existe no banco de dados com a condição informada como parametro
            {
                throw new NotFoundException("Id not found.");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
