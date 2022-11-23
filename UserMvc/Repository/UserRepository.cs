using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserMvc.Models;

namespace UserMvc.Repository
{
    public class UserRepository : IGenericRepository<User>
    {
        protected readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(User entity)
        {
            _context.users.Add(entity);
            Save();
        }
        public void Edit(User user)
        {
            _context.users.Update(user);
            Save();

        }
        public List<User> GetAll()
        {
            return _context.users.ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
