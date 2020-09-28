using AutoMapper;
<<<<<<< HEAD
=======
using PCLoan.Data.Library.Models;
>>>>>>> develop
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;

namespace PCLoan.Presentation.Web
{
<<<<<<< HEAD
=======
    /// <summary>
    /// profiles of how to map from one calss to another.
    /// </summary>
>>>>>>> develop
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
<<<<<<< HEAD
            CreateMap<UserModel, UserModelDTO>();
            CreateMap<UserModelDTO, UserModel>();
=======
            // ---------- ComputerModel Mapping ----------

            // Map from ComputerModel to ComputerModelDTO..
            CreateMap<ComputerModel, ComputerModelDTO>() ;
            // and back
            CreateMap<ComputerModelDTO, ComputerModel>();

            // Map from ComputerModelDTO to ComputerModelDAO..
            CreateMap<ComputerModelDTO, ComputerModelDAO>();
            // and back
            CreateMap<ComputerModelDAO, ComputerModelDTO>();

            // ---------- LoanModel Mapping ----------

            // Map from LoanModel to LoanModelDTO..
            CreateMap<LoanModel, LoanModelDTO>();
            // and back
            CreateMap<LoanModelDTO, LoanModel>();

            // Map from LoanModelDTO to LoanModelDAO..
            CreateMap<LoanModelDTO, LoanModelDAO>();
            // and back
            CreateMap<LoanModelDAO, LoanModelDTO>();

            // ---------- LogModel Mapping ----------

            // Map from LogModel to LogModelDTO..
            CreateMap<LogModel, LogModelDTO>();
            // and back
            CreateMap<LogModelDTO, LogModel>();

            // Map from LogModelDTO to LogModelDAO..
            CreateMap<LogModelDTO, LogModelDAO>();
            // and back
            CreateMap<LogModelDAO, LogModelDTO>();

            // ---------- StateModel Mapping ----------

            // Map from StateModel to StateModelDTO..
            CreateMap<StateModel, StateModelDTO>();
            // and back
            CreateMap<StateModelDTO, StateModel>();

            // Map from StateModelDTO to StateModelDAO..
            CreateMap<StateModelDTO, StateModelDAO>();
            // and back
            CreateMap<StateModelDAO, StateModelDTO>();

            // ---------- UserModel Mapping ----------

            // Map from UserModel to UserModelDTO..
            CreateMap<UserModel, UserModelDTO>();
            // and back
            CreateMap<UserModelDTO, UserModel>();

            // Map from UserModelDTO to UserModelDAO..
            CreateMap<UserModelDTO, UserModelDAO>();
            // and back
            CreateMap<UserModelDAO, UserModelDTO>();
>>>>>>> develop
        }
    }
}
