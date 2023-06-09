using Microsoft.AspNetCore.Mvc;
using rtperson.DatabaseModels;
using rtperson.Models;
using rtperson.Responses;
using rtperson.Services;

namespace rtperson.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonsService personsService;

        public PersonsController(PersonsService personsService) => this.personsService = personsService;

        [HttpGet, Route("get-list")]
        public async Task<GenericResponse<List<PersonSummaryModel>>> GetPersons()
        {
            var response = new GenericResponse<List<PersonSummaryModel>>();
            var serviceResponse = await personsService.GetSummaryListAsync();

            if (serviceResponse.Success)
                response.SetSuccess(serviceResponse.Data!);

            else
                response.SetFailure(serviceResponse.GetErrors());

            return response;
        }

        [HttpPost, Route("add")]
        public async Task<GenericResponse<Guid>> CreatePerson([FromBody] CreatePersonModel model)
        {
            var response = new GenericResponse<Guid>();
            var serviceResponse = await personsService.CreateAsync(new Person() { Company = model.Company, Name = model.Name, Surname = model.Surname, });

            if (serviceResponse.Success)
                response.SetSuccess(serviceResponse.Data!);
            else
                response.SetFailure(serviceResponse.GetErrors());

            return response;
        }

        [HttpDelete, Route("remove/{personId}")]
        public async Task<GenericResponse> DeletePerson(Guid personId)
        {
            var response = new GenericResponse();
            var serviceResponse = await personsService.DeleteAsync(personId);

            if (!serviceResponse.Success)
                response.SetFailure(serviceResponse.GetErrors());

            return response;
        }

        [HttpGet, Route("get-detail/{personId}")]
        public async Task<GenericResponse<PersonDetailModel>> GetPersonDetail(Guid personId)
        {
            var response = new GenericResponse<PersonDetailModel>();
            var serviceResponse = await personsService.GetPersonWithContacts(personId);

            if (serviceResponse.Success)
                response.SetSuccess(serviceResponse.Data!);

            else
                response.SetFailure(serviceResponse.GetErrors());

            return response;
        }

        [HttpPost, Route("{personId}/add-contact")]
        public async Task<GenericResponse> AddContact([FromRoute] Guid personId, [FromBody] CreateContactModel model)
        {
            var response = new GenericResponse();
            var serviceResponse = await personsService.InsertContact(personId, new Contact(model.Value, model.Type));

            if (!serviceResponse.Success)
                response.SetFailure(serviceResponse.GetErrors());

            return response;
        }

        [HttpDelete, Route("{personId}/remove-contact/{contactId}")]
        public async Task<GenericResponse> RemoveContact([FromRoute] Guid personId, [FromRoute] Guid contactId)
        {
            var response = new GenericResponse();
            var serviceResponse = await personsService.DeleteContact(personId, contactId);

            if (!serviceResponse.Success)
                response.SetFailure(serviceResponse.GetErrors());

            return response;
        }

    }
}