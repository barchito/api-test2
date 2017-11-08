using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Domain.Services.General;
using AdvancedStudioExercise.Infrastructure.Common;
using AdvancedStudioExercise.Infrastructure.Repositories;
using AdvancedStudioExercise.Infrastructure.UnitOfWork;
using Moq;
using Xunit;

namespace AdvancedStudioExercise.Tests {
    public class PersonServiceTests {
        [Fact]
        public async Task Create_Error () {
            //Arrange
            var resultGuid = Guid.NewGuid();
            var testModel = new PersonModel {
                FirstName = "test",
                LastName = "test"
            };
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Add( testModel ) ).Returns( Task.FromResult( resultGuid ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            unitOfWork.Setup( x => x.SaveChangesAsync() ).Throws( new Exception() );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act and Assert
            await Assert.ThrowsAsync<Exception>( () => personService.Create( testModel ) );
        }

        [Fact]
        public async Task Create_Success () {
            //Arrange
            var resultGuid = Guid.NewGuid();
            var testModel = new PersonModel {
                FirstName = "test",
                LastName = "test"
            };
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Add( testModel ) ).Returns( Task.FromResult( resultGuid ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act
            var id = await personService.Create( testModel );

            //Assert
            Assert.Equal( resultGuid, id );
        }

        [Fact]
        public async Task DeletePerson_Fail () {
            //Arrange
            var deleteGuid = Guid.NewGuid();
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Delete( deleteGuid ) ).Returns( Task.FromResult( 0 ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            unitOfWork.Setup( x => x.SaveChangesAsync() ).Throws( new Exception() );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act
            var result = await personService.DeletePerson( deleteGuid );

            //Assert
            Assert.Equal( false, result );
        }

        [Fact]
        public async Task DeletePerson_Success () {
            //Arrange
            var deleteGuid = Guid.NewGuid();
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Delete( deleteGuid ) ).Returns( Task.FromResult( 0 ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            unitOfWork.Setup( x => x.SaveChangesAsync() ).Returns( Task.FromResult( 0 ) );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act
            var result = await personService.DeletePerson( deleteGuid );

            //Assert
            Assert.Equal( true, result );
        }

        [Fact]
        public async Task GetAll_Error () {
            //Arrange
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Get() ).Throws( new Exception() );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act and Assert
            await Assert.ThrowsAsync<Exception>( () => personService.GetAll() );
        }

        [Fact]
        public async Task GetByFirstNameOrLastName_Error () {
            //Arrange
            var name = "test";
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.GetByFirstNameOrLastName( name ) ).Throws( new Exception() );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act and Assert
            await Assert.ThrowsAsync<Exception>( () => personService.GetByFirstNameOrLastName( name ) );
        }

        [Fact]
        public async Task UpdatePerson_Fail () {
            //Arrange
            var guid = Guid.NewGuid();
            var testModel = new PersonModel {
                FirstName = "test",
                LastName = "test",
                Id = guid
            };
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Update( testModel ) ).Returns( Task.FromResult( 0 ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            unitOfWork.Setup( x => x.SaveChangesAsync() ).Throws( new Exception() );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act
            var result = await personService.UpdatePerson( testModel );

            //Assert
            Assert.Equal( false, result );
        }

        [Fact]
        public async Task UpdatePerson_Success () {
            //Arrange
            var guid = Guid.NewGuid();
            var testModel = new PersonModel {
                FirstName = "test",
                LastName = "test",
                Id = guid
            };
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Update( testModel ) ).Returns( Task.FromResult( 0 ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            unitOfWork.Setup( x => x.SaveChangesAsync() ).Returns( Task.FromResult( 0 ) );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var personService = new PersonService( dependencyResolver.Object );

            //Act
            var result = await personService.UpdatePerson( testModel );

            //Assert
            Assert.Equal( true, result );
        }
    }
}