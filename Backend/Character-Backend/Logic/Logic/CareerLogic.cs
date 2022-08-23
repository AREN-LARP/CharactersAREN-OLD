using DAL.DALInterfaces;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class CareerLogic : ICareerLogic
    {
        private readonly ICareerRepository _repo;

        public CareerLogic(ICareerRepository repo)
        {
            _repo = repo;
        }
        public async Task<Career> DeleteCareer(Career career)
        {
            return await _repo.DeleteEntity(career);
        }

        public async Task<Career> GetCareer(int id)
        {
            var career = await _repo.GetCareerById(id);

            return career;
        }

        public async Task<IEnumerable<Career>> GetCareers()
        {
            return await _repo.GetCareers();
        }

        public async Task<Career> PostCareer(Career career)
        {
            var exExist = await _repo.CareerExists(career.Name);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(career.GetType().Name, career.Name);
            }
            else
            {
                return await _repo.AddEntity(career);
            }
        }

        public async Task<Career> PutCareer(int id, Career career)
        {
            if (career.Id != id)
            {
                throw new ArgumentException($"{nameof(PutCareer)}: id in url is not equal to career id.");
            }

            return await _repo.PutEntity(career);
        }
    }
}
