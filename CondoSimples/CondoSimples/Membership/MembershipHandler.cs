using CondoSimples.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace CondoSimples.Membership
{
    public class MembershipHandler
    {
        public const string SINDICOROLE = "Sindico";
        public const string CONDOMINOROLE = "Condomino";
        public const string EMPREGADOROLE = "Empregado";

        private UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
new UserStore<ApplicationUser>(new ApplicationDbContext()));

        private RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(
new RoleStore<IdentityRole>(new ApplicationDbContext()));

        //private ApplicationSignInManager _signInManager = new ApplicationSignInManage))

        public bool CreateUser(ApplicationUser user, string password)
        {
            var result = _userManager.Create(user, password);
            return result.Succeeded;
        }

        public void Login(string user)
        {
            FormsAuthentication.SetAuthCookie(user, false);
        }

        public void SetRoleSindico(string userId)
        {
            if (!_roleManager.RoleExists(SINDICOROLE))
            {
                IdentityRole role = new IdentityRole(SINDICOROLE);
                _roleManager.Create(role);
            }

            _userManager.AddToRole(userId, SINDICOROLE);
        }

        public void SetRoleCondomino(string userId)
        {
            if (!_roleManager.RoleExists(CONDOMINOROLE))
            {
                IdentityRole role = new IdentityRole(CONDOMINOROLE);
                _roleManager.Create(role);
            }

            _userManager.AddToRole(userId, CONDOMINOROLE);
        }

        public void SetRoleEmpregado(string userId)
        {
            if (!_roleManager.RoleExists(EMPREGADOROLE))
            {
                IdentityRole role = new IdentityRole(EMPREGADOROLE);
                _roleManager.Create(role);
            }

            _userManager.AddToRole(userId, EMPREGADOROLE);
        }
    }
}
