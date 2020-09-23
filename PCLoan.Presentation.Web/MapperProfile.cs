using AutoMapper;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;

namespace PCLoan.Presentation.Web
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserModelDTO>();
            CreateMap<UserModelDTO, UserModel>();
        }
    }
}
