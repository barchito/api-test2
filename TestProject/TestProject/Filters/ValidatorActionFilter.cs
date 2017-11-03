using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Filters
{
    /// <summary>
    /// ValidatorActionFilter
    /// </summary>
    public class ValidatorActionFilter : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new ValidationFailedResult(filterContext.ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }

    #region ValidationFailedResult
    /// <summary>
    /// Validation Failed Result
    /// </summary>
    public class ValidationFailedResult : ObjectResult
    {
        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="modelState"></param>
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }

    /// <summary>
    /// Validation Error
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Error Fieldss
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Constrator 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="message"></param>
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    /// <summary>
    /// Validation Result
    /// </summary>
    public class ValidationResultModel
    {
        /// <summary>
        /// Error Message 
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// List of Errors
        /// </summary>
        public List<ValidationError> Errors { get; }

        /// <summary>
        /// Validation Result Model.
        /// </summary>
        /// <param name="modelState"></param>
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }

    #endregion
}
