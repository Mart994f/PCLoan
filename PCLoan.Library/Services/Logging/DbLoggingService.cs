using AutoMapper;
using PCLoan.Data.Library.Models;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Models;
using System;

namespace PCLoan.Logic.Library.Services
{
    public class DbLoggingService : ILoggingService
    {
        #region Private Fields

        private ILogRepository _logRepository;

        private IMapper _mapper;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public DbLoggingService(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public void Log(int userId, string description, int? computerId = null)
        {
            LogModelDTO log = new LogModelDTO()
            {
                Timestamp = DateTime.UtcNow,
                UserId = userId,
                ComputerId = computerId,
                Description = description
            };

            _logRepository.Insert(_mapper.Map<LogModelDAO>(log));
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
