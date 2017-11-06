using Core2APIExercise.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2APIExercise.Common.Interfaces
{
    public interface IIdentifierRepository<T> : IRepository<T> where T : Identifier
    {

       
    }
}
