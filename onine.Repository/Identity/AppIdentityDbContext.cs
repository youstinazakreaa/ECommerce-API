using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using onine.core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onine.Repository.Identity
{
    public class AppIdentityDbContext :IdentityDbContext <AppUser>
    {
        public AppIdentityDbContext(
            DbContextOptions<AppIdentityDbContext>options):base(options)

        {
            
        }
    }
}
