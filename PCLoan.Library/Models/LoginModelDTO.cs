namespace PCLoan.Logic.Library.Models
{
    /// <summary>
    /// A model to hold login data
    /// </summary>
    public class LoginModelDTO
    {
        /// <summary>
        /// The username used to login
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The Password used to log in
        /// </summary>
        public string Password { get; set; }
    }
}
