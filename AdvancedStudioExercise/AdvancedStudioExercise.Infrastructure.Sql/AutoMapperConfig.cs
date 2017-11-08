using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Infrastructure.Sql.Entities;
using AutoMapper;

namespace AdvancedStudioExercise.Infrastructure.Sql {
    internal class AutoMapperConfig {
        private static bool _isConfigured;
        private static readonly object Locker = new object();

        public static void Configure () {
            if ( !_isConfigured ) {
                lock ( Locker ) {
                    Mapper.Initialize( cfg => {
                        #region  Domain models => Storage objects

                        cfg.CreateMap<IdentifierTypeModel, IdentifierType>();
                        cfg.CreateMap<IdentifierModel, Identifier>();
                        cfg.CreateMap<PersonModel, Person>();
                        //.ForMember( dm => dm.Id, so => so.MapFrom(s => s.Id));

                        #endregion

                        #region Storage objects => Domain models

                        cfg.CreateMap<IdentifierType, IdentifierTypeModel>();
                        cfg.CreateMap<Identifier, IdentifierModel>();
                        cfg.CreateMap<Person, PersonModel>();
                        //.ForMember( dm => dm.Id, so => so.MapFrom(s => s.Id));

                        #endregion
                    } );
                    _isConfigured = true;
                }
            }
        }
    }
}