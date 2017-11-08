using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Web.Models.Identifiers;
using AdvancedStudioExercise.Web.Models.Persons;
using AutoMapper;

namespace AdvancedStudioExercise.Web {
    public class WebMappingsProfileConfiguration : Profile {
        public WebMappingsProfileConfiguration ()
            : this( "WebMappingsProfile" ) {
        }

        protected WebMappingsProfileConfiguration ( string profileName )
            : base( profileName ) {
            #region View Model => Domain Model

            CreateMap<IdentifierTypeViewModel, IdentifierTypeModel>();
            CreateMap<IdentifierViewModel, IdentifierModel>()
                .ForMember(dm => dm.IdentifierTypeId, vm => vm.MapFrom(o => o.Type));
            CreateMap<AddIdentifierViewModel, IdentifierModel>()
                .ForMember(dm => dm.IdentifierTypeId, vm => vm.MapFrom(o => o.Type));
            CreateMap<AddPersonIdentifierViewModel, IdentifierModel>()
                .ForMember(dm => dm.IdentifierTypeId, vm => vm.MapFrom(o => o.Type));
            CreateMap<PersonViewModel, PersonModel>();
            CreateMap<BasePersonViewModel, PersonModel>();
            CreateMap<AddPersonViewModel, PersonModel>();
            //.ForMember(dm => dm.Id, vm => vm.MapFrom(o => o.Id));

            #endregion

            #region Domain Model => View Model

            CreateMap<IdentifierTypeModel, IdentifierTypeViewModel>();
            CreateMap<IdentifierModel, IdentifierViewModel>()
                .ForMember(dm => dm.Type, vm => vm.MapFrom(o => o.IdentifierTypeId));
            CreateMap<IdentifierModel, AddIdentifierViewModel>()
                .ForMember(dm => dm.Type, vm => vm.MapFrom(o => o.IdentifierTypeId));
            CreateMap<IdentifierModel, AddPersonIdentifierViewModel>()
                .ForMember(dm => dm.Type, vm => vm.MapFrom(o => o.IdentifierTypeId));
            CreateMap<PersonModel, BasePersonViewModel>();
            CreateMap<PersonModel, PersonViewModel>();
            CreateMap<PersonModel, AddPersonViewModel>();
            //.ForMember(vm => vm.Id, dm => dm.MapFrom(o => o.Id));

            #endregion
        }
    }
}