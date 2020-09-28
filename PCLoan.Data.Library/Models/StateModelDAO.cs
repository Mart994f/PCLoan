using Dapper.Contrib.Extensions;

namespace PCLoan.Data.Library.Models
{
    [Table("State")]
    public class StateModelDAO
    {
        [Key]
        public int Id { get; set; }

        public string State { get; set; }

    }
}
