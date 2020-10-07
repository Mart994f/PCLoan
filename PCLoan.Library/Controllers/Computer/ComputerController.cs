using AutoMapper;
using PCLoan.Data.Library.Models;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Exceptions;
using PCLoan.Logic.Library.Models;
using PCLoan.Logic.Library.Values;
using System;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public class ComputerController
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

        /// <summary>
        /// Registers a loan. If the user already have a loan, then a <see cref="UserAlreadyHaveLoanException"/> is thrown.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="model">A <see cref="LoanModelDTO"/> containing the loan details</param>
        public void RegisterLoan(int userId, LoanModelDTO model)
        {
            // Check if the user already have a loan, if they do throw a UserAlreadyHaveLoanException..
            if (CheckUserHaveLoan(userId))
            {
                // TODO: Log exception occurred
                throw new UserAlreadyHaveLoanException("Brugeren har allerede et lån, og kan derfor ikke låne igen.");
            }
            // else create the loan
            else
            {
                // Set the loan date..
                model.LoanDate = DateTime.UtcNow;

                // and the userId
                model.UserId = userId;

                // Save the loan
                if (CreateLoan(model))
                {
                    // TODO: Log exception occurred
                    // TODO: Throw LoanNotCreatedException
                }

                //  Update the computers state
                UpdateComputerState(model.ComputerId, States.Lend);

                // Log it
                // TODO: Implement log
            }
        }

        // Return current loan
        public void ReturnLoan()
        {
            // Get users current loan

            // Set return date

            // Save the updated loan

            // Update computers state

            // Log it
        }

        // Get users current loan
        public void GetUsersCurrentLoan()
        {

        }

        // Add available computers
        public List<ComputerModelDTO> GetAvailableComputers()
        {
            return _mapper.Map<List<ComputerModelDTO>>(_computerRepository.GetAvailableComputers());
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Checks if a user already have a loan
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>True if the user have a loan, else False</returns>
        private bool CheckUserHaveLoan(int userId)
        {
            return _loanRepository.CheckUserHaveLoan(userId);
        }

        /// <summary>
        /// Gets the lend computer
        /// </summary>
        /// <param name="computerId">Id of the computer</param>
        /// <returns>An <see cref="ComputerModelDTO"/> containing the computer details</returns>
        private ComputerModelDTO GetLendComputer(int computerId)
        {
            return _mapper.Map<ComputerModelDTO>(_computerRepository.Get(computerId));
        }

        /// <summary>
        /// Creates the new loan
        /// </summary>
        /// <param name="model">A <see cref="LoanModelDTO"/> containing the loan details</param>
        /// <returns>True if successful, else False</returns>
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

        /// <summary>
        /// Returns the loan
        /// </summary>
        /// <param name="loanId">The id of the loan to be returned</param>
        private void ReturnLoan(int loanId)
        {
            try
            {
                _loanRepository.ReturnLoan(loanId, DateTime.UtcNow);
            }
            catch (Exception)
            {
                // TODO: Implement exception handling
            }
        }

        /// <summary>
        /// Updates the state of the computer
        /// </summary>
        /// <param name="computerId">Id of the computer to update</param>
        /// <param name="stateId">Id of the new state</param>
        private void UpdateComputerState(int computerId, States stateId)
        {
            try
            {
                _computerRepository.UpdateState(computerId, stateId);
            }
            catch (Exception)
            {
                // TODO: Implement exception handling
            }
        }

        #endregion
    }
}