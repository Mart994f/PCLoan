using AutoMapper;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace PCLoan.Logic.Library.Controllers
{
    public class AdminController : IAdminController
    {
        #region Private Fields

        private IComputerRepository _computerRepository;

        private IStateRepository _stateRepository;

        private ILoanRepository _loanRepository;

        private IUserRepository _userRepository;

        private IMapper _mapper;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public AdminController(IComputerRepository computerRepository, IStateRepository stateRepository, ILoanRepository loanRepository, IUserRepository userRepository, IMapper mapper)
        {
            _computerRepository = computerRepository;
            _stateRepository = stateRepository;
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public ComputerModelDTO GetComputer(int id)
        {
            ComputerModelDTO computer = _mapper.Map<ComputerModelDTO>(_computerRepository.Get(id));

            computer.States = _mapper.Map<IEnumerable<StateModelDTO>>(_stateRepository.GetAll()).ToList();

            LoanModelDTO loan = _mapper.Map<IEnumerable<LoanModelDTO>>(_loanRepository.GetAll()).Where(l => l.ComputerId == computer.Id).OrderByDescending(l => l.LoanDate).FirstOrDefault();

            if (loan != null)
            {
                computer.LendBy = _userRepository.GetUsernameById(loan.UserId);

                if (loan.LoanDate != null)
                {
                    computer.LoanDate = loan.LoanDate;
                }

                if (loan.ReturnedDate != null)
                {
                    computer.ReturnedDate = loan.ReturnedDate;
                }
            }

            return computer;
        }

        public ComputerModelDTO GetNewComputerModel()
        {
            ComputerModelDTO model = new ComputerModelDTO();
            model.States = _mapper.Map<IEnumerable<StateModelDTO>>(_stateRepository.GetAll()).ToList();

            return model;
        }

        public IEnumerable<ComputerModelDTO> GetAllComputersWithCurrentLoan()
        {
            IEnumerable<ComputerModelDTO> computers = _mapper.Map<IEnumerable<ComputerModelDTO>>(_computerRepository.GetAll());

            computers = AddStatesToComputer(computers);
            computers = MapLoanToComputer(computers);

            return computers;
        }

        #endregion

        #region Private Helper Methods

        private IEnumerable<ComputerModelDTO> AddStatesToComputer(IEnumerable<ComputerModelDTO> computers)
        {
            IEnumerable<StateModelDTO> states = _mapper.Map<IEnumerable<StateModelDTO>>(_stateRepository.GetAll());

            foreach (ComputerModelDTO computer in computers)
            {
                computer.States = states.ToList();
            }

            return computers;
        }

        private IEnumerable<ComputerModelDTO> MapLoanToComputer(IEnumerable<ComputerModelDTO> computers)
        {
            IEnumerable<LoanModelDTO> loans = _mapper.Map<IEnumerable<LoanModelDTO>>(_loanRepository.GetAll());

            foreach (ComputerModelDTO computer in computers)
            {
                LoanModelDTO loan = loans.Where(l => l.ComputerId == computer.Id).OrderByDescending(l => l.LoanDate).FirstOrDefault();

                if (loan != null)
                {
                    computer.LendBy = _userRepository.GetUsernameById(loan.UserId);

                    if (loan.LoanDate != null)
                    {
                        computer.LoanDate = loan.LoanDate;
                    }

                    if (loan.ReturnedDate != null)
                    {
                        computer.ReturnedDate = loan.ReturnedDate;
                    }
                }
            }

            return computers;
        }

        #endregion
    }
}
