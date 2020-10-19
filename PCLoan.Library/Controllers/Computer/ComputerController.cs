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

        private IComputerRepository _computerRepository;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public ComputerController(ILoanRepository loanRepository, IMapper mapper, IComputerRepository computerRepository)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _computerRepository = computerRepository;
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
                if (!CreateLoan(model))
                {
                    // TODO: Log exception occurred
                    throw new LoanNotCreatedException("Der skete en fejl ved oprettelsen af lånet.");
                }

                //  Update the computers state
                UpdateComputerState(model.ComputerId, States.Lend);

                // Log it
                // TODO: Implement log
            }
        }

        /// <summary>
        /// Registers that a user have returned a loan
        /// </summary>
        /// <param name="userId">Id of the user</param>
        public void RegisterLoanReturned(int userId)
        {
            // Get users current loan
            LoanModelDTO model = _mapper.Map<LoanModelDTO>(_loanRepository.GetAll().OrderBy(l => l.LoanDate)
                                                                          .SingleOrDefault(l => l.UserId == userId));

            // Save the updated loan
            ReturnLoan(model.Id);

            // Update computers state
            UpdateComputerState(model.ComputerId, States.ReadyForLoan);

            // Log it
            // TODO: Implement log
        }

        /// <summary>
        /// Gets the users current loan 
        /// </summary>
        /// <param name="userID">Id of the user</param>
        /// <returns>A <see cref="LoanModelDTO"/> contaning data about the loan</returns>
        public LoanModelDTO GetUsersCurrentLoan(int userID)
        {
            return _mapper.Map<LoanModelDTO>(_loanRepository.GetAll().OrderByDescending(l => l.LoanDate)
                                                            .Single(l => l.UserId == userID && l.ReturnedDate == null));
        }

        /// <summary>
        /// Gets all computers som are available for loan
        /// </summary>
        /// <returns>A list of all the available computers</returns>
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
            catch (Exception ex)
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
                _computerRepository.UpdateState(computerId, int.Parse(stateId.ToString()));
            }
            catch (Exception ex)
            {
                // TODO: Implement exception handling
            }
        }

        #endregion
    }
}