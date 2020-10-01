using AutoMapper;
using PCLoan.Data;
using PCLoan.Data.Library.Models;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCLoan.Logic.Library.Controllers
{
    public class ComputerController : IComputerController
    {
        #region Private Fields

        private ILoanRepository _loanRepository;

        private IMapper _mapper;

        private IUserRepository _userRepository;

        private IComputerRepository _computerRepository;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public ComputerController(ILoanRepository loanRepository, IMapper mapper, IUserRepository userRepository, IComputerRepository computerRepository)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _computerRepository = computerRepository;
        }

        #endregion

        public void CreateLoan(string username, LoanModelDTO model)
        {
            model.LoanDate = DateTime.UtcNow;
            model.UserId = _userRepository.GetIdByname(username);

            try
            {
                _loanRepository.Insert(_mapper.Map<LoanModelDAO>(model));
                // TODO: Implement log
            }
            catch (Exception ex)
            {
                // TODO: Implement log and handle error
                throw;
            }
        }

        public void ReturnLoan(LoanModelDTO model)
        {
            model.ReturnedDate = DateTime.UtcNow;

            try
            {
                _loanRepository.Update(_mapper.Map<LoanModelDAO>(model));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public LoanModelDTO GetCurrentLoan(string username)
        {

            LoanModelDTO model = _mapper.Map<LoanModelDTO>(_loanRepository.GetAll().OrderByDescending(o => o.LoanDate).First(l => l.UserId == _userRepository.GetIdByname(username)));
            model = AddLentComputer(model);
            return model;
        }

        private LoanModelDTO AddLentComputer(LoanModelDTO model)
        {
            model.Computers = _mapper.Map<List<ComputerModelDTO>>(_computerRepository.Get(model.ComputerId));
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