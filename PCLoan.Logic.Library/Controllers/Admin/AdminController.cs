using AutoMapper;
using PCLoan.Data.Library.Models;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Exceptions;
using PCLoan.Logic.Library.Models;
using PCLoan.Logic.Library.Services;
using PCLoan.Logic.Library.Values;
using System.Collections.Generic;
using System.Linq;

namespace PCLoan.Logic.Library.Controllers
{
    // TODO: Implement logging to log
    public class AdminController : IAdminController
    {
        #region Private Fields

        private IComputerRepository _computerRepository;

        private IStateRepository _stateRepository;

        private ILoanRepository _loanRepository;

        private IUserRepository _userRepository;

        private IMapper _mapper;

        private ILoggingService _loggingService;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public AdminController(IComputerRepository computerRepository, IStateRepository stateRepository,
                               ILoanRepository loanRepository, IUserRepository userRepository, IMapper mapper,
                               ILoggingService loggingService)
        {
            _computerRepository = computerRepository;
            _stateRepository = stateRepository;
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _loggingService = loggingService;
        }

        #endregion

        #region Public Methods

        public void CreateComputer(int userId, ComputerModelDTO model)
        {
            // Create the new computer..
            _computerRepository.Insert(_mapper.Map<ComputerModelDAO>(model));

            // and log it
            _loggingService.Log(userId, Actions.Create, _computerRepository.GetAll().Last().Id);
        }

        public ComputerModelDTO GetComputerWithLoan(int id)
        {
            // Get the ComputerModel
            ComputerModelDTO computerModel = _mapper.Map<ComputerModelDTO>(_computerRepository.Get(id));

            // Add available states, and add them to the ComputerModel
            computerModel.States = GetStates();

            // Get latest loan, if any
            LoanModelDTO loanModel = GetLatestLoan(computerModel.Id);

            // If the computer have been lend before
            if (loanModel != null)
            {
                // then add the loan data
                computerModel.LoanDate = loanModel.LoanDate;
                computerModel.ReturnedDate = loanModel.ReturnedDate;
                computerModel.LendBy = _userRepository.GetUsernameById(loanModel.UserId);
            }

            // And finally return it to the view controller
            return computerModel;
        }

        public List<ComputerModelDTO> GetComputersWithLoan()
        {
            // Get the list ComputerModels
            List<ComputerModelDTO> computers = _mapper.Map<List<ComputerModelDTO>>(_computerRepository.GetAll()).Where(c => c.Deactivated != true).ToList();

            foreach (ComputerModelDTO computer in computers)
            {
                // Add available states, and add them to the ComputerModel
                computer.States = GetStates();

                // Get latest loan, if any
                LoanModelDTO loanModel = GetLatestLoan(computer.Id);

                // If the computer have been lend before
                if (loanModel != null)
                {
                    // then add the loan data
                    computer.LoanDate = loanModel.LoanDate;
                    computer.ReturnedDate = loanModel.ReturnedDate;
                    computer.LendBy = _userRepository.GetUsernameById(loanModel.UserId);
                }
            }

            // And finally return it to the view controller
            return computers;
        }


        public void UpdateComputer(int userId, ComputerModelDTO model)
        {
            _computerRepository.Update(_mapper.Map<ComputerModelDAO>(model));

            // and log it
            _loggingService.Log(userId, Actions.Edit, model.Id);
        }

        public void DeactivateComputer(int userId, int computerId)
        {

            if (GetState(computerId) == State.Lend)
            {
                throw new CanNotDeleteComputerException("Computeren kan ikke slettes når der er et aktivt lån");
            }

            _computerRepository.DeactivateComputer(computerId);

            // and log it
            _loggingService.Log(userId, Actions.Delete, computerId);
        }

        public List<StateModelDTO> GetStates()
        {
            return _mapper.Map<List<StateModelDTO>>(_stateRepository.GetAll());
        }

        #endregion

        #region Private Helper Methods

        private LoanModelDTO GetLatestLoan(int computerId)
        {
            IEnumerable<LoanModelDTO> loans = _mapper.Map<IEnumerable<LoanModelDTO>>(_loanRepository.GetAll());

            return loans.Where(l => l.ComputerId == computerId).OrderByDescending(l => l.LoanDate).ThenByDescending(l => l.Id).FirstOrDefault();
        }

        private State GetState(int computerId)
        {
            return (State)_computerRepository.Get(computerId).StateId;
        }

        #endregion
    }
}
