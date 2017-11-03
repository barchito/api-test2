using System.Collections.Generic;
using System.Net;

namespace TestProject.ApiResponse
{

    #region Api Response

    /// <summary>
    /// Api Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// 200 - All went well, some data is returned
        /// 400 - There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied
        /// 401 - Access to resource is unauthorized
        /// 500 - An error occurred in the server while processing the request, i.e. an exception was thrown
        /// </summary>
        private HttpStatusCode _statusCode { get; set; }

        /// <summary>
        /// Status code of API.
        /// </summary>
        public int StatusCode => (int)_statusCode;

        /// <summary>
        /// A meaningful, end-user friendly message, explaining what went wrong
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data payload if request was successful
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// List of errors if fail result
        /// </summary>
        public List<Error> Errors { get; set; }

        /// <summary>
        /// Success result of Api response.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> SuccessResult(T data)
        {
            return new ApiResponse<T>
            {
                _statusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        /// <summary>
        /// Fail Result of Api.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ApiResponse<T> FailResult(string message, List<Error> errors = null)
        {
            return new ApiResponse<T>
            {
                _statusCode = HttpStatusCode.BadRequest,
                Errors = errors
            };
        }

        /// <summary>
        /// Error result of Api Response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> ErrorResult(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "", T data = default(T))
        {
            return new ApiResponse<T>
            {
                _statusCode = statusCode,
                Message = message
            };
        }


        /// <summary>
        /// Unauthorized Result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<T> UnauthorizedResult(string message)
        {
            return new ApiResponse<T>
            {
                _statusCode = HttpStatusCode.Unauthorized,
                Message = message
            };
        }
    }

    #endregion

    #region Error
    /// <summary>
    /// Error
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Title of error
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Message of error
        /// </summary>
        public string Message { get; set; }
    }
    #endregion
}
