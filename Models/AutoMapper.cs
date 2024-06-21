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

        }
    }
}
