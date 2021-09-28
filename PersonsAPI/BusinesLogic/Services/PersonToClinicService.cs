using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Services;
using DataLayer.Abstraction.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class PersonToClinicService : IPersonToClinicService
    {
        private IPersonToClinicRepository _repository;

        public PersonToClinicService(IPersonToClinicRepository repository)
        {
            _repository = repository;
        }

        public async Task SetClinicToPersonByIDAsync(int personId, int clinicId)
        {
            await _repository.SetClinicToPersonByIDAsync(personId, clinicId);
        }

        public async Task<IEnumerable<ClinicToGet>> GetClinicFromPersonIdAsync(int personId)
        {
            var result = await _repository.GetClinicFromPersonIdAsync(personId);
            return result.Select(p => new ClinicToGet()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        public async Task<IEnumerable<PersonToGet>> GetPersonsFromClinicIdAsync(int clinicId)
        {
            var result = await _repository.GetPersonsFromClinicIdAsync(clinicId);
            return result.Select(p => new PersonToGet()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        public async Task<IEnumerable<ClinicToGet>> GetClinicFromPersonIdWithPaginationAsync(int personId, int skip, int take)
        {
            var result = await _repository.GetClinicFromPersonIdWithPaginationAsync(personId, skip, take);
            return result.Select(p => new ClinicToGet()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        public async Task<IEnumerable<PersonToGet>> GetPersonsFromClinicIdWithPaginationAsync(int clinicId, int skip, int take)
        {
            var result = await _repository.GetPersonsFromClinicIdWithPaginationAsync(clinicId, skip, take);
            return result.Select(p => new PersonToGet()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        public async Task DeleteLinkPersonToClinicAsync(int personId, int clinicId)
        {
            await _repository.DeleteLinkPersonToClinicAsync(personId, clinicId);
        }
    }
}
