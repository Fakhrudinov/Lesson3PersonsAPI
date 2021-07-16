using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
using DataLayer.Abstraction.Entityes;
using DataLayer.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    class ClinicService : IClinicService
    {
        private IClinicRepository _repository;

        public ClinicService(IClinicRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClinicToGet>> GetClinicsAsync()
        {
            var result = await _repository.GetClinicsAsync();
            return result.Select(c => new ClinicToGet()
            {
                Id = c.Id,
                Name = c.Name,
                Adress = c.Adress,
            }).ToArray();
        }

        public async Task<ClinicToGet> GetClinicByIdAsync(int id)
        {
            var result = await _repository.GetClinicByIdAsync(id);

            ClinicToGet findedClinic = new ClinicToGet();

            if (result != null)
            {
                findedClinic.Id = result.Id;
                findedClinic.Name = result.Name;
                findedClinic.Adress = result.Adress;
            }
            return findedClinic;
        }

        public async Task RegisterClinicAsync(ClinicToPost clinic)
        {
            ClinicDataLayer newClinic = new ClinicDataLayer();

            newClinic.Name = clinic.Name;
            newClinic.Adress = clinic.Adress;

            await _repository.RegisterClinicAsync(newClinic);
        }
    }
}
