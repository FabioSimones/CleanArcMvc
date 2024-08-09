using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArcMvc.Domain.Account
{
    public interface ISeedUserRoleInitial
    {
        void SeedRoles();
        void SeedUsers();
    }
}
