using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Services;
using FluentValidation;
using PersonsAPI.Requests;
using System.Collections.Generic;


namespace BusinesLogic.ValidateControllerData
{
    public class PersonValidationAsyncService : AbstractValidator<PersonToPost>
    {
        IPersonService _personService;

        public PersonValidationAsyncService(IPersonService personService)
        {
            _personService = personService;


            RuleFor(x => x ).MustAsync(async (x, cancellation) => {
                IEnumerable<PersonToGet> persons = await _personService.GetPersonsByNameAsync(x.FirstName);

                bool exists = false;

                foreach (PersonToGet personSingle in persons)
                {
                    if (personSingle.FirstName.Equals(x.FirstName) 
                    && personSingle.LastName.Equals(x.LastName)
                    && personSingle.Age.Equals(x.Age)
                    && personSingle.Email.Equals(x.Email))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    return true;
                }

                return false;
            }).WithMessage("{PropertyName} - Person с такими данными уже существует.")
            .WithErrorCode("P-100.10");
        }
    }
}
