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
        public Task<Subject> CreateAsync(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            var allsubjects = await _subjectDal.GetAllAsync();
                if (allsubjects == null)return null;
            return allsubjects;
        }

        public Task<Subject> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> UpdateAsync(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
