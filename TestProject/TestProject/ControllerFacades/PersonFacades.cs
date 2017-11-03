using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestProject.ApiResponse;
using TestProject.ApiResponseResult;
using TestProject.DatabaseContext;
using TestProject.Entites;
using TestProject.Enum;
using TestProject.Exceptions;
using TestProject.ViewModel;

namespace TestProject.ControllerFacades
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonFacades
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private GenericDataRepository _repository = new GenericDataRepository();
        #endregion
        #region PersonWithIdentfiers

        /// <summary>
        /// PersonWithIdentfiers
        /// </summary>
        /// <returns></returns>
        public ApiResponse<List<PersonWithIdentfiersResult>> GetPersonWithIdentfier(TestProjectDbContext context)
        {
            try
            {
                var result = _repository.GetPersonWithIdentfier(context).Select(x => new PersonWithIdentfiersResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Identifiers = x.Identifiers.Select(a => new IdentifierResult
                    {
                        Id = a.Id,
                        Type = a.Type,
                        Value = a.Value
                    })
                }).ToList();

                return ApiResponse<List<PersonWithIdentfiersResult>>.SuccessResult(result);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public ApiResponse<List<PersonWithIdentfiersResult>> GetPersonByFirstNameOrLastName(TestProjectDbContext context, string firstname, string lastname)
        {
            try
            {
                var result = _repository.GetPersonByFirstNameOrLastName(context, firstname, lastname).Select(x => new PersonWithIdentfiersResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Identifiers = x.Identifiers.Select(a => new IdentifierResult
                    {
                        Id = a.Id,
                        Type = a.Type,
                        Value = a.Value
                    })
                }).ToList();
                return ApiResponse<List<PersonWithIdentfiersResult>>.SuccessResult(result);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ApiResponse<List<PersonWithIdentfiersResult>> GetPersonBySpecificIdentifiers(TestProjectDbContext context, IdentifiersType type)
        {
            try
            {
                var result = _repository.GetPersonBySpecificIdentifiers(context, type).Select(x => new PersonWithIdentfiersResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Identifiers = x.Identifiers.Select(a => new IdentifierResult
                    {
                        Id = a.Id,
                        Type = a.Type,
                        Value = a.Value
                    })

                }).ToList();
                return ApiResponse<List<PersonWithIdentfiersResult>>.SuccessResult(result);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<PersonWithIdentfiersResult>>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public ApiResponse<bool> DeletePerson(TestProjectDbContext context, Guid personId)
        {
            try
            {
                Person person = _repository.GetPersonbyId(context, personId);

                if (person == null)
                {
                    throw new ErrorException("Person Id is invalid");
                }

                _repository.DeletePerson(context, person);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        public ApiResponse<bool> UpdatePerson(TestProjectDbContext context, PersonUpdateViewModel model)
        {

            Person person = _repository.GetPersonbyId(context, model.PersonId);
            if (person == null)
            {
                throw new ErrorException("Person Id is invalid");
            }
            try
            {
                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                _repository.UpdatePerson(context, person);

                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public ApiResponse<bool> DeleteIdentifier(TestProjectDbContext context, Guid personId)
        {
            try
            {
                _repository.DeleteIdentifier(context, personId);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// Create Identifier
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResponse<bool> CreateIdentifier(TestProjectDbContext context, CreateIdentifierViewModel model)
        {
            try
            {
                Person person = _repository.GetPersonbyId(context, model.PersonId);

                if (person == null)
                {
                    throw new ErrorException("Person Id is invalid");
                }
                var identifier = new Identifier()
                {
                    PersonId = person.Id,
                    Id = Guid.NewGuid(),
                    Type = model.Type,
                    Value = model.Value
                };

                _repository.CreateIdentifier(context, identifier);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        public ApiResponse<bool> CreatePerson(TestProjectDbContext context, PersonViewModel model)
        {
            try
            {
                Person person = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Deleted = false,
                    Identifiers = model.Identifiers.Select(x => new Identifier
                    {
                        Id = Guid.NewGuid(),
                        Type = x.Type,
                        Value = x.Value
                    }).ToList()
                };
                _repository.CreatePerson(context, person);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }

        }


        /// <summary>
        /// Create Person Without Identifiers
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        public ApiResponse<bool> CreatePersonWithoutIdentifiers(TestProjectDbContext context, PersonWithoutIdentifiersViewModel model)
        {
            try
            {
                Person person = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Deleted = false
                };
                _repository.CreatePerson(context, person);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }

        }

        #endregion
    }
}
