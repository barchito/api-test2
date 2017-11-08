using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Exceptions;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Domain.Services;
using AdvancedStudioExercise.Web.Controllers;
using AdvancedStudioExercise.Web.Enums;
using AdvancedStudioExercise.Web.Models;
using AdvancedStudioExercise.Web.Models.Identifiers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedStudioExercise.Web.Areas.v1.Controllers {
    [Route( "api/v1/identifiers" )]
    public class IdentifiersController : ApiBaseController {
        private readonly IIdentifierService _identifierService;

        public IdentifiersController (
            IMapper mapper,
            IIdentifierService identifierService ) : base( mapper ) {
            _identifierService = identifierService;
        }

        [HttpDelete]
        [Route( "{id}" )]
        public async Task<IActionResult> Delete ( Guid id ) {
            var response = new ApiResponseModel();

            var isDeleted = await _identifierService.DeleteIdentifier( id );

            if ( isDeleted ) {
                response.Status = Status.SUCCESS;
                response.Message = "Deleted!";
                return Ok( response );
            }

            response.Message = "Unable to delete identifier.";
            return BadRequest( response );
        }

        [HttpPost]
        [Route( "" )]
        public async Task<IActionResult> Add ( [FromBody] AddIdentifierViewModel model ) {
            var response = new ApiResponseModel();

            if ( model == null ) {
                response.Message = "Identifier body can not be empty.";
                return BadRequest( response );
            }

            if ( model.Type <= 0 ) {
                response.Message = "Invalid identifier type.";
                return BadRequest( response );
            }

            if ( model.PersonId == null ) {
                response.Message = "Invalid person id.";
                return BadRequest( response );
            }

            if ( string.IsNullOrEmpty( model.Value ) ) {
                response.Message = "Identifier value can not be empty.";
                return BadRequest( response );
            }

            try {
                var domainModel = Mapper.Map<IdentifierModel>( model );
                response.Data = await _identifierService.AddIdentifierToPerson( domainModel );
                response.Status = Status.SUCCESS;
                response.Message = "Added!";
                return Ok( response );
            } catch ( PersonNotFoundException ) {
                response.Message = "Person with given id not found.";
                return NotFound( response );
            } catch {
                response.Message = "Unknown error. Please, contact support team.";
                return StatusCode(500, response);
            }
        }
    }
}