using Core.StudentManager;
using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SubjectManager
{
    public class SubjectBL : IRepositoryBL<Subject>
    {
        private readonly IRepositoryDal<Subject> _subjectDal;
        public SubjectBL()
        {
            _subjectDal = new SubjectDal();
        }
        public Task<Subject> Create(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subject>> Get()
        {
            var allsubjects =  _subjectDal.Get();
            return allsubjects;
        }

        public Task<Subject> GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
