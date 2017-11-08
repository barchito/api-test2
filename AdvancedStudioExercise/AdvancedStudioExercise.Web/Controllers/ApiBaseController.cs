using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedStudioExercise.Web.Controllers {
    public class ApiBaseController : Controller {
        protected readonly IMapper Mapper;

        public ApiBaseController ( IMapper mapper) {
            Mapper = mapper;
        }
    }
}