﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryBL<T> where T : class
    {
        Task<T> GetbyId(int id);
        Task<IEnumerable<T>> Get();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
    }
}