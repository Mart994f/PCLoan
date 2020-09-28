using Dapper.Contrib.Extensions;

namespace PCLoan.Data.Library.Models
{
    [Table("Computer")]
    public class ComputerModelDAO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int StateId { get; set; }

        public bool Deactivated { get; set; }
    }
}
