using AutoMapper;
using PCLoan.Data.Library.Models;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;

namespace PCLoan.Presentation.Web
{
    /// <summary>
    /// profiles of how to map from one calss to another.
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Map from UserModel to UserModelDTO..
            CreateMap<UserModel, UserModelDTO>();
            // and back
            CreateMap<UserModelDTO, UserModel>();

            // mak from UserModelDTO to UserModelDAO..
            CreateMap<UserModelDTO, UserModelDAO>();
            // and back
            CreateMap<UserModelDAO, UserModelDTO>();
        }
    }
}
