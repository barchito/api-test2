using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestProject.ViewModel;
using TestProject.ControllerFacades;
using TestProject.DatabaseContext;
using TestProject.ApiResponse;
using TestProject.ApiResponseResult;
using TestProject.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using TestProject.Enum;

namespace TestProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        #region Properties
        PersonFacades _personFacades = new PersonFacades();
        public TestProjectDbContext _context;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PersonsController(TestProjectDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesAttribute(typeof(ApiResponse<List<PersonWithIdentfiersResult>>))]
        public IActionResult Get()
        {
            var response = _personFacades.GetPersonWithIdentfier(_context);
            return new ObjectResult(response);
        }

        /// <summary>
        /// Get all the person by firstname and lastname
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{firstname}/{lastname}")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesAttribute(typeof(ApiResponse<List<PersonWithIdentfiersResult>>))]
        public IActionResult Get(string firstname, string lastname)
        {
            var response = _personFacades.GetPersonByFirstNameOrLastName(_context, firstname, lastname);
            return new ObjectResult(response);
        }


        /// <summary>
        ///  Get all the person who has specific identifiers
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{type}")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesAttribute(typeof(ApiResponse<List<PersonWithIdentfiersResult>>))]
        public IActionResult Get(IdentifiersType type)
        {
            var response = _personFacades.GetPersonBySpecificIdentifiers(_context, type);
            return new ObjectResult(response);
        }


        /// <summary>
        ///  Create a Person with idetifiers.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [SwaggerResponse(422, type: null, description: "Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public IActionResult Create([FromBody]PersonViewModel model)
        {
            var response = _personFacades.CreatePerson(_context, model);
            return new ObjectResult(response);
        }

        /// <summary>
        /// Create a person without identifiers
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("CreatePerson")]
        [Produces("application/json")]
        [SwaggerResponse(422, type: null, description: "Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public IActionResult CreatePerson([FromBody]PersonWithoutIdentifiersViewModel model)
        {
            var response = _personFacades.CreatePersonWithoutIdentifiers(_context, model);
            return new ObjectResult(response);
        }

        /// <summary>
        /// Add new identifier to a person
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut,Route("CreateIdentifier")]
        [Produces("application/json")]
        [SwaggerResponse(422, type: null, description: "Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public IActionResult CreateIdentifier([FromBody]CreateIdentifierViewModel model)
        {
            var response = _personFacades.CreateIdentifier(_context, model);
            return new ObjectResult(response);
        }

        /// <summary>
        /// Delete identifier form a person
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteIdentifier/{personId}")]
        [Produces("application/json")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public IActionResult DeleteIdentifier(Guid personId)
        {
            var response = _personFacades.DeleteIdentifier(_context, personId);
            return new ObjectResult(response);
        }


        /// <summary>
        /// Delete on a person
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{personId}")]
        [Produces("application/json")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public IActionResult Delete(Guid personId)
        {
            var response = _personFacades.DeletePerson(_context, personId);
            return new ObjectResult(response);
        }


        /// <summary>
        /// Update a person without changing identifiers 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [SwaggerResponse(422, type: null, description: "Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public IActionResult Update([FromBody]PersonUpdateViewModel model)
        {
            var response = _personFacades.UpdatePerson(_context, model);
            return new ObjectResult(response);
        }
    }
}
