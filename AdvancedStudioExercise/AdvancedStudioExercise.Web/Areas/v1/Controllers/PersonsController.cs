using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Services;
using AdvancedStudioExercise.Web.Enums;
using AdvancedStudioExercise.Web.Models;
using AdvancedStudioExercise.Web.Models.Persons;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AdvancedStudioExercise.Web.Models.Identifiers;
using AdvancedStudioExercise.Domain.Models;
using System;

namespace AdvancedStudioExercise.Web.Controllers {
    [Route( "api/v1/persons" )]
    public class PersonsController : ApiBaseController {
        private readonly IPersonService _personService;

        public PersonsController (
            IMapper mapper,
            IPersonService personService ) : base( mapper ) {
            _personService = personService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get () {
            var response = new ApiResponseModel();

            try {
                var persons = await _personService.GetAll();
                response.Data = Mapper.Map<IEnumerable<PersonViewModel>>( persons );
                response.Status = Status.SUCCESS;
                return Ok( response );
            } catch (Exception ex) {
                response.Message = "Unknown error. Please, contact support team.";
                return StatusCode( 500, response );
            }
        }

        [HttpGet]
        [Route( "{firstNameOrLastName}" )]
        public async Task<IActionResult> GetByFirstNameOrLastName ( string firstNameOrLastName ) {
            var response = new ApiResponseModel();

            if ( string.IsNullOrEmpty( firstNameOrLastName ) ) {
                response.Message = "Please, provide first name or last name.";
                return BadRequest( response );
            }

            try {
                var persons = await _personService.GetByFirstNameOrLastName( firstNameOrLastName );
                response.Data = Mapper.Map<IEnumerable<PersonViewModel>>( persons );
                response.Status = Status.SUCCESS;
                return Ok( response );
            } catch {
                response.Message = "Unknown error. Please, contact support team.";
                return StatusCode( 500, response );
            }
        }

        [HttpGet]
        [Route("identifiers")]
        public async Task<IActionResult> GetByIdentifiers ( IEnumerable<Guid> identifiers ) {
            var response = new ApiResponseModel();

            if ( identifiers == null ) {
                response.Message = "Invalid identifiers array.";
                return BadRequest(response);
            }

            try {
                var persons = await _personService.GetWithSpesificIdentifiers(identifiers);
                response.Data = Mapper.Map<IEnumerable<PersonViewModel>>(persons);
                response.Status = Status.SUCCESS;
                return Ok(response);
            } catch {
                response.Message = "Unknown error. Please, contact support team.";
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create ( [FromBody] AddPersonViewModel model ) {
            var response = new ApiResponseModel();

            if ( model == null ) {
                response.Message = "Invalid input object.";
                return BadRequest(response);
            }

            if ( string.IsNullOrEmpty(model.FirstName) ) {
                response.Message = "First name can not be empty.";
                return BadRequest(response);
            }

            if ( string.IsNullOrEmpty(model.LastName) ) {
                response.Message = "Last name can not be empty.";
                return BadRequest(response);
            }

            try {
                var domainModel = Mapper.Map<PersonModel>(model);
                response.Data = await _personService.Create(domainModel);
                response.Status = Status.SUCCESS;
                response.Message = "Added!";
                return Ok(response);
            } catch {
                response.Message = "Unknown error. Please, contact support team.";
                return StatusCode(500, response);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete ( Guid id ) {
            var response = new ApiResponseModel();

            var isDeleted = await _personService.DeletePerson(id);

            if ( isDeleted ) {
                response.Status = Status.SUCCESS;
                response.Message = "Deleted!";
                return Ok(response);
            }

            response.Message = "Unable to delete person.";
            return BadRequest(response);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update ( Guid id, [FromBody] PersonViewModel model ) {
            var response = new ApiResponseModel();
            
            if ( string.IsNullOrEmpty(model.FirstName) ) {
                response.Message = "First name can not be empty.";
                return BadRequest(response);
            }

            if ( string.IsNullOrEmpty(model.LastName) ) {
                response.Message = "Last name can not be empty.";
                return BadRequest(response);
            }

            try {
                var domainModel = Mapper.Map<PersonModel>(model);
                var persons = await _personService.UpdatePerson(domainModel);
                response.Status = Status.SUCCESS;
                response.Message = "Updated!";
                return Ok(response);
            } catch {
                response.Message = "Unknown error. Please, contact support team.";
                return StatusCode(500, response);
            }
        }
    }
}