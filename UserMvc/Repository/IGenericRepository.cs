using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserMvc.Models;

namespace UserMvc.Repository
{
        public interface IGenericRepository<T> where T : class
        {
            //IEnumerable<T> GetAll();
             public  List<User> GetAll();
             public  void Add(T entity);
             public void Edit(T entity);
             public void Save();
    }
}
