using AutoMapper;
using PCLoan.Data.Library.Models;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCLoan.Logic.Library.Controllers
{
    public class ComputerController : IComputerController
    {
        #region Private Fields

        private ILoanRepository _loanRepository;

        private IMapper _mapper;

        private IUserRepository _userRepository;

        private IComputerRepository _computerRepository;

        private IStateRepository _stateRepository;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public ComputerController(ILoanRepository loanRepository, IMapper mapper, IUserRepository userRepository,
                                  IComputerRepository computerRepository, IStateRepository stateRepository)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _computerRepository = computerRepository;
            _stateRepository = stateRepository;
        }

        #endregion

        public void CreateLoan(string username, LoanModelDTO model)
        {
            model.LoanDate = DateTime.UtcNow;
            model.UserId = _userRepository.GetIdByname(username);
            ComputerModelDTO computer = _mapper.Map<ComputerModelDTO>(_computerRepository.Get(model.ComputerId));
            computer.StateId = _stateRepository.GetAll().First(s => s.State == "Udlånt").Id;

            try
            {
                _loanRepository.Insert(_mapper.Map<LoanModelDAO>(model));
                _computerRepository.Update(_mapper.Map<ComputerModelDAO>(computer));
                // TODO: Implement log
            }
            catch (Exception ex)
            {
                // TODO: Implement log and handle error
                throw;
            }
        }

        public void ReturnLoan(string username)
        {
            LoanModelDTO model = GetCurrentLoan(username);
            model.ReturnedDate = DateTime.UtcNow;

            try
            {
                _loanRepository.Update(_mapper.Map<LoanModelDAO>(model));
                model.Computers.First().StateId = 1;
                _computerRepository.Update(_mapper.Map<ComputerModelDAO>(model.Computers.First()));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public LoanModelDTO GetCurrentLoan(string username)
        {

            LoanModelDTO model = _mapper.Map<LoanModelDTO>(_loanRepository.GetAll().OrderByDescending(o => o.LoanDate).ThenByDescending(o => o.Id).First(l => l.UserId == _userRepository.GetIdByname(username)));
            model = AddLentComputer(model);
            return model;
        }

        private LoanModelDTO AddLentComputer(LoanModelDTO model)
        {
            var computer = _mapper.Map<ComputerModelDTO>(_computerRepository.Get(model.ComputerId));
            model.Computers = new List<ComputerModelDTO>();
            model.Computers.Add(computer);
            return model;
        }

        public LoanModelDTO GetNewLoanModel()
        {
            LoanModelDTO model = new LoanModelDTO();
            model = AddAvailableComputers(model);
            return model;
        }

        private LoanModelDTO AddAvailableComputers(LoanModelDTO model)
        {
            model.Computers = _mapper.Map<List<ComputerModelDTO>>(_computerRepository.GetAvailableComputers());
            return model;
        }
    }
}