using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Services;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class PersonByIdValidationAsyncService : AbstractValidator<PersonToGet>
    {
        IPersonService _personService;

        public PersonByIdValidationAsyncService(IPersonService personService)
        {
            _personService = personService;

            RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
            {
                PersonToGet exists = await _personService.GetPersonByIdAsync(id);

                if (exists.Id != id)//не найдено 
                {
                    return false;
                }

                return true;
            });
        }
    }
}
