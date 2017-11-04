using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core2APIExercise.Data.Entities;

namespace Core2APIExercise.Common.Initializer
{
    public class SeedData<TContext> where TContext : DbContext
    {
        private readonly UnitOfWork<TContext> _unitOfWork;
        private readonly ILogger _logger;
        //private readonly SampleData _data;

        //public SeedData(ILogger logger, UnitOfWork<TContext> unitOfWork, SampleData data)
        public SeedData(ILogger logger, UnitOfWork<TContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            //_data = data;
        }

        public void Initialize()
        {
            InitializePersons();
        }

        private void InitializePersons()
        {
            if (IsInitialized(_unitOfWork.PersonRepository.GetAll()))
            {
                return;
            }
            //if (_data.Persons != null)
            //{
            //    var persons = _data.Persons.Select(i => new Person
            //    {
            //        FirstName = i.FirstName,
            //        LastName = i.LastName
            //    });

            //    _logger.LogInformation("Seeding Person table");
            //    foreach (Person i in persons)
            //    {
            //        _unitOfWork.PersonRepository.Add(i);
            //    }

            //    _unitOfWork.Commit();
            //}
            _logger.LogInformation("Seeding Person table");

            var person1 = _unitOfWork.PersonRepository.Add(new Person
            {
                FirstName = "Abhishek",
                LastName = "Bhalani",
                CreatedDate = DateTime.UtcNow
            });

            _unitOfWork.Commit();

            _logger.LogInformation("Seeding Identifier for person1 ");

            _unitOfWork.IdentifierRepository.Add(new Identifier()
            {
                Person = person1,
                IdentifierType = Data.Enum.IdentifierType.Email,
                Value = "bhalaniabhishek@gmail.com"
            });
            _unitOfWork.Commit();


            _logger.LogInformation("Seeding other persons ");

            var person2 = _unitOfWork.PersonRepository.Add(new Person
            {
                FirstName = "Sanju",
                LastName = "Bhalani",
                CreatedDate = DateTime.UtcNow
            });
            var person3 = _unitOfWork.PersonRepository.Add(new Person
            {
                FirstName = "Akash",
                LastName = "Bhalani",
                CreatedDate = DateTime.UtcNow
            });
            _unitOfWork.Commit();
        }
        private bool IsInitialized(IQueryable<BaseEntity> query) => query.Count() > 0;
    }
}
