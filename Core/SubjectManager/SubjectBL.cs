using Core.StudentManager;
using Interface;
using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SubjectManager
{
    public class SubjectBL : ISubjectBL
    {
        private readonly ISubjectDAL _subjectDal;
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

        public async Task<IEnumerable<Subject>> GetAll()
        {
            var allsubjects =  await _subjectDal.GetAll();
            return allsubjects;
        }

        public Task<Subject> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
