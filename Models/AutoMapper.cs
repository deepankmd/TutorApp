using AutoMapper;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssignmentViewModel, Assignment>().ReverseMap();
            CreateMap<TutorSubject, TutorSubjectViewModel>().ReverseMap();
            CreateMap<Assignment, AssignmentReadViewModel>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
            CreateMap<Notification, NotificationDetailViewModel>().ReverseMap();
            CreateMap<AssignmentApplied, AssignmentAppliedViewModel>().ReverseMap();
            CreateMap<ParentDetails, ParentDetailsViewModel>().ReverseMap();
        }
    }
}
