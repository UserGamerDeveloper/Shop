using MicroElectronic.Domain.Enum;

namespace MicroElectronic.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public List<ApplicationItem> ApplicationItems { get; set; }

        public List<Order> Orders { get; set; }
    }
}
