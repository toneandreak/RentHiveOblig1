using Microsoft.EntityFrameworkCore;
using RentHiveOblig.DAL.Repositories;
using RentHiveOblig.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentHiveOblig.DAL
{
    public class EiendomRepository : IEiendomRepository
    {
        private readonly ApplicationDbContext _context;

        public EiendomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Eiendom GetById(int id)
        {
            return _context.Eiendom.Find(id);
        }

        public List<Eiendom> GetAll()
        {
            return _context.Eiendom.ToList();
        }

        public void Add(Eiendom eiendom)
        {
            _context.Eiendom.Add(eiendom);
            _context.SaveChanges();
        }

        public void Update(Eiendom eiendom)
        {
            _context.Eiendom.Update(eiendom);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var eiendom = _context.Eiendom.Find(id);
            if (eiendom != null)
            {
                _context.Eiendom.Remove(eiendom);
                _context.SaveChanges();
            }
        }
    }
}