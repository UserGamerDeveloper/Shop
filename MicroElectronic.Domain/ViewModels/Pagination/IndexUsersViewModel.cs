using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Pagination
{
    public class IndexUsersViewModel
    {
        public IEnumerable<MicroElectronic.Domain.ViewModels.User.UserViewModel> Users { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
