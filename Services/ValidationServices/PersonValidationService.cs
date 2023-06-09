using rtperson.DatabaseModels;
using rtperson.Repositories;
using System.Runtime.CompilerServices;

namespace rtperson.Services.ValidationServices
{
    public class PersonValidationService : IValidationService<Person>
    {
        private readonly IGenericRepository<Person> repo;
        public PersonValidationService(IGenericRepository<Person> repo) =>
            this.repo = repo;

        public async Task<ValidationResponse> ValidateCreateAsync(Person entity)
        {
            var response = new ValidationResponse();

            if (string.IsNullOrWhiteSpace(entity.Name))
                response.AddError("Name is required.");

            if (string.IsNullOrWhiteSpace(entity.Surname))
                response.AddError("Surname is required.");

            return await Task.FromResult(response);
        }

        public async Task<ValidationResponse> ValidateUpdateAsync(Person entity)
        {
            var response = new ValidationResponse();

            if (string.IsNullOrWhiteSpace(entity.Name))
                response.AddError("Name is required.");

            if (string.IsNullOrWhiteSpace(entity.Surname))
                response.AddError("Surname is required.");

            return await Task.FromResult(response);
        }

        public async Task<ValidationResponse> ValidateDeleteAsync(Guid id)
        {
            var response = new ValidationResponse();

            var exist = await repo.GetAsync(s => s.Id == id);
            if (exist == null)
                response.AddError("Type does not exist.");

            return response;
        }

        public async Task<ValidationResponse> ValidateInsertContactAsync(Guid personId, Contact entity)
        {
            var response = new ValidationResponse();

            var exist = await repo.GetAsync(s => s.Id == personId);
            if (exist == null)
                response.AddError("Person does not exist.");

            if (string.IsNullOrWhiteSpace(entity.Value))
                response.AddError("Contact Value is required.");

            return await Task.FromResult(response);
        }

        public async Task<ValidationResponse> ValidateDeleteContactAsync(Guid personId, Guid contactId)
        {
            var response = new ValidationResponse();

            var exist = await repo.GetAsync(s => s.Id == personId);
            if (exist == null)
            {
                response.AddError("Person does not exist.");
                return await Task.FromResult(response);
            }

            var contactExist = exist.Contacts.FirstOrDefault(s => s.Id == contactId);
            if (contactExist == null)
                response.AddError("Contact does not exist.");

            return await Task.FromResult(response);
        }
    }
}
