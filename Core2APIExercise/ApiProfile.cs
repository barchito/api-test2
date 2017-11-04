using AutoMapper;
using Core2APIExercise.Data.Models;
using Core2APIExercise.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2APIExercise
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<PersonModel, Person>().ReverseMap();
            CreateMap<PersonIndentifierModel, Person>().ReverseMap();
            CreateMap<IdentifierModel, Identifier>().ReverseMap();
        }
    }
}
