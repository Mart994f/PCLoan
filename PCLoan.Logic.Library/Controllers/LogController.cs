using AutoMapper;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace PCLoan.Logic.Library.Controllers
{
    public class LogController : ILogController
    {
        private readonly ILogRepository _logRepository;
        private readonly IComputerRepository _computerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LogController(ILogRepository logRepository, IComputerRepository computerRepository,
                             IUserRepository userRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _computerRepository = computerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<LogModelDTO> GetLogs()
        {
            List<LogModelDTO> logs = _mapper.Map<List<LogModelDTO>>(_logRepository.GetAll());

            foreach (LogModelDTO log in logs)
            {
                log.Username = _mapper.Map<UserModelDTO>(_userRepository.Get(log.UserId)).UserName;
                log.Computername = _mapper.Map<ComputerModelDTO>(_computerRepository.Get((int)log.ComputerId)).Name;
            }

            return logs;
        }

        public List<LogModelDTO> GetLogByUsername(string username)
        {
            List<LogModelDTO> logs = _mapper.Map<List<LogModelDTO>>(_logRepository.GetAll());

            logs = logs.FindAll(l => l.UserId == _userRepository.GetIdByname(username.ToLower()));

            foreach (LogModelDTO log in logs)
            {
                log.Username = _mapper.Map<UserModelDTO>(_userRepository.Get(log.UserId)).UserName;
                log.Computername = _mapper.Map<ComputerModelDTO>(_computerRepository.Get((int)log.ComputerId)).Name;
            }

            return logs;
        }

        public List<LogModelDTO> GetLogByComputerId(int computerId)
        {
            List<LogModelDTO> logs = _mapper.Map<List<LogModelDTO>>(_logRepository.GetAll());

            logs = logs.FindAll(l => l.ComputerId == computerId);

            foreach (LogModelDTO log in logs)
            {
                log.Username = _mapper.Map<UserModelDTO>(_userRepository.Get(log.UserId)).UserName;
                log.Computername = _mapper.Map<ComputerModelDTO>(_computerRepository.Get((int)log.ComputerId)).Name;
            }

            return logs;
        }
    }
}
