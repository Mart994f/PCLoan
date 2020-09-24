using Dapper.Contrib.Extensions;

namespace PCLoan.Data.Library.Models
{
    [Table("[User]")]
    public class UserModelDAO
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
    }
}
