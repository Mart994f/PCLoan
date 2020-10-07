using AutoMapper;
using PCLoan.Data.Library.Models;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Exceptions;
using PCLoan.Logic.Library.Models;
using PCLoan.Logic.Library.Values;
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

        #region Public Methods      

        // Create a new loan
        public bool RegisterLoan(int userId, LoanModelDTO model)
        {
            bool created;

            // Check if the user already have a loan, if they do throw a UserAlreadyHaveLoanException..
            if (CheckIfUserHaveALoan(userId))
            {
                throw new UserAlreadyHaveLoanException("Brugeren har allerede et lån, Prøv igen senere.");
            }
            // else create the loan
            else
            {
                // Set the loan date..
                model.LoanDate = DateTime.UtcNow;

                // and the userId
                model.UserId = userId;

                // Save the loan..
                created = CreateLoan(model);

                //  and update the computers state
                UpdateComputerState(model.ComputerId, States.Lend);

                // and log it
                // TODO: Implement log
            }

            return created;
        }


        // Return current loan
        public void ReturnLoan()
        {

        }

        // Get users current loan
        public void GetUsersCurrentLoan()
        {

        }

        // Add available computers
        public LoanModelDTO AddAvailableComputers()
        {

        }


        public void CreateLoan(string username, LoanModelDTO model)
        {
            ComputerModelDTO computer = _mapper.Map<ComputerModelDTO>(_computerRepository.Get(model.ComputerId));
            computer.StateId = _stateRepository.GetAll().First(s => s.State == "Udlånt").Id;

            try
            {

                
            }
            catch (Exception ex)
            {
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

        #endregion

        #region Private Helper Methods

        // Check if user already have a loan
        private bool CheckIfUserHaveALoan(int userId)
        {

        }

        // Add computer information to the current loan
        private void GetLendComputer()
        {

        }

        // Write new loan to database
        private bool CreateLoan(LoanModelDTO model)
        {
            try
            {
                _loanRepository.Insert(_mapper.Map<LoanModelDAO>(model));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        // Get the available computers
        private void GetAvailableComputers()
        {

        }

        // Update the computers state
        private bool UpdateComputerState(int computerId, States stateId)
        {
            stateId.
            try
            {
                _computerRepository.Update(_mapper.Map<ComputerModelDAO>(model));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}