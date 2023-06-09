using rtperson.DatabaseModels;
using rtperson.Models;
using rtperson.Repositories;
using rtperson.Services.Responses;
using rtperson.Services.ValidationServices;

namespace rtperson.Services
{
    public class PersonsService : BaseService
    {
        private readonly IGenericRepository<Person> _repo;
        private readonly PersonValidationService _validationService;

        public PersonsService(IGenericRepository<Person> repo, IValidationService<Person> validationService,
            ILogService logService) : base(logService)
        {
            _repo = repo;
            _validationService = (PersonValidationService)validationService;
        }

        public async Task<ServiceResponse<List<PersonSummaryModel>>> GetSummaryListAsync()
        {
            var response = new ServiceResponse<List<PersonSummaryModel>>();

            try
            {
                response.SetData(await _repo.GetListAsync(_ => true, s => new PersonSummaryModel(s.Id, s.Name, s.Surname)));
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse<PersonDetailModel>> GetPersonWithContacts(Guid personId)
        {
            var response = new ServiceResponse<PersonDetailModel>();

            try
            {
                var personDetail = await _repo.GetAsync(s => s.Id == personId,
                    s => new PersonDetailModel(s.Name, s.Surname, s.Company, s.Contacts
                    .Select(contact => new ContactModel(contact.Id, contact.Type.ToString(), contact.Value)).ToList()));

                response.SetData(personDetail);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse<List<Person>>> GetListAsync()
        {
            var response = new ServiceResponse<List<Person>>();

            try
            {
                response.SetData(await _repo.GetListAsync());
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse<Guid>> CreateAsync(Person person)
        {
            var response = new ServiceResponse<Guid>();

            try
            {
                var validationResponse = await _validationService.ValidateCreateAsync(person);

                if (validationResponse.Success)
                {
                    await _repo.CreateAsync(person);
                    response.SetData(person.Id);
                }
                else
                    response.AddError(validationResponse.Errors, ErrorTypeEnum.Validation);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse<Guid>> UpdateAsync(Person person)
        {
            var response = new ServiceResponse<Guid>();

            try
            {
                var validationResponse = await _validationService.ValidateUpdateAsync(person);

                if (validationResponse.Success)
                {
                    await _repo.UpdateAsync(person);
                    response.SetData(person.Id);
                }
                else
                    response.AddError(validationResponse.Errors, ErrorTypeEnum.Validation);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(Guid personId)
        {
            var response = new ServiceResponse();

            try
            {
                var validationResponse = await _validationService.ValidateDeleteAsync(personId);

                if (validationResponse.Success)
                    await _repo.DeleteAsync(personId);
                else
                    response.AddError(validationResponse.Errors, ErrorTypeEnum.Validation);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse> InsertContact(Guid personId, Contact contact)
        {
            var response = new ServiceResponse();

            try
            {
                var validationResponse = await _validationService.ValidateInsertContactAsync(personId, contact);

                if (validationResponse.Success)
                {
                    var person = await _repo.GetAsync(s => s.Id == personId);

                    person.Contacts.Add(contact);

                    await _repo.UpdateAsync(person);
                }
                else
                    response.AddError(validationResponse.Errors, ErrorTypeEnum.Validation);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteContact(Guid personId, Guid contactId)
        {
            var response = new ServiceResponse();

            try
            {
                var validationResponse = await _validationService.ValidateDeleteContactAsync(personId, contactId);

                if (validationResponse.Success)
                {
                    var person = await _repo.GetAsync(s => s.Id == personId);

                    person.Contacts.RemoveAll(s => s.Id == contactId);

                    await _repo.UpdateAsync(person);
                }
                else
                    response.AddError(validationResponse.Errors, ErrorTypeEnum.Validation);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                response.AddError("Internal error occured.", ErrorTypeEnum.InternalError);
            }

            return response;
        }
    }
}