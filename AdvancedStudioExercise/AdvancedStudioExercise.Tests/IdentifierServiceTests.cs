using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Exceptions;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Domain.Services.General;
using AdvancedStudioExercise.Infrastructure.Common;
using AdvancedStudioExercise.Infrastructure.Repositories;
using AdvancedStudioExercise.Infrastructure.UnitOfWork;
using Moq;
using Xunit;

namespace AdvancedStudioExercise.Tests {
    public class IdentifierServiceTests {
        [Fact]
        public async Task AddIdentifierToPerson_Success () {
            //Arrange
            var resultGuid = Guid.NewGuid();
            var testModel = new IdentifierModel {
                Value = "test",
                PersonId = Guid.NewGuid()
            };
            var identifierRepository = new Mock<IIdentifiersRepository>();
            identifierRepository.Setup( x => x.Add( testModel ) ).Returns( Task.FromResult( resultGuid ) );
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup( x => x.Get( testModel.PersonId ) ).Returns( Task.FromResult( new PersonModel() ) );
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet( x => x.IdentifiersRepository ).Returns( identifierRepository.Object );
            unitOfWork.SetupGet( x => x.PersonsRepository ).Returns( personsRepository.Object );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup( x => x.Resolve<IUnitOfWork>() ).Returns( unitOfWork.Object );
            var identifierService = new IdentifierService( dependencyResolver.Object );

            //Act
            var id = await identifierService.AddIdentifierToPerson( testModel );

            //Assert
            Assert.Equal( resultGuid, id );
        }

        [Fact]
        public async Task AddIdentifierToPerson_NotFoundPerson () {
            //Arrange
            var resultGuid = Guid.NewGuid();
            var testModel = new IdentifierModel {
                Value = "test",
                PersonId = Guid.NewGuid()
            };
            var identifierRepository = new Mock<IIdentifiersRepository>();
            identifierRepository.Setup(x => x.Add(testModel)).Returns(Task.FromResult(resultGuid));
            var personsRepository = new Mock<IPersonsRepository>();
            personsRepository.Setup(x => x.Get(testModel.PersonId)).Throws( new PersonNotFoundException());
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.IdentifiersRepository).Returns(identifierRepository.Object);
            unitOfWork.SetupGet(x => x.PersonsRepository).Returns(personsRepository.Object);
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup(x => x.Resolve<IUnitOfWork>()).Returns(unitOfWork.Object);
            var identifierService = new IdentifierService(dependencyResolver.Object);

            //Act and Assert
            await Assert.ThrowsAsync<PersonNotFoundException>( () =>  identifierService.AddIdentifierToPerson(testModel));
        }

        [Fact]
        public async Task DeleteIdentifier_Success () {
            //Arrange
            var deleteGuid = Guid.NewGuid();
            var identifierRepository = new Mock<IIdentifiersRepository>();
            identifierRepository.Setup(x => x.Delete(deleteGuid)).Returns(Task.FromResult(0));
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.IdentifiersRepository).Returns(identifierRepository.Object);
            unitOfWork.Setup( x => x.SaveChangesAsync() ).Returns( Task.FromResult( 0 ) );
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup(x => x.Resolve<IUnitOfWork>()).Returns(unitOfWork.Object);
            var identifierService = new IdentifierService(dependencyResolver.Object);

            //Act
            var result = await identifierService.DeleteIdentifier(deleteGuid);

            //Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task DeleteIdentifier_Fail () {
            //Arrange
            var deleteGuid = Guid.NewGuid();
            var identifierRepository = new Mock<IIdentifiersRepository>();
            identifierRepository.Setup(x => x.Delete(deleteGuid)).Returns(Task.FromResult(0));
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.IdentifiersRepository).Returns(identifierRepository.Object);
            unitOfWork.Setup(x => x.SaveChangesAsync()).Throws( new Exception());
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup(x => x.Resolve<IUnitOfWork>()).Returns(unitOfWork.Object);
            var identifierService = new IdentifierService(dependencyResolver.Object);

            //Act
            var result = await identifierService.DeleteIdentifier(deleteGuid);

            //Assert
            Assert.Equal(false, result);
        }
    }
}