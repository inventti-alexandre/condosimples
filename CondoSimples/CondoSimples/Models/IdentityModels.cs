using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondoSimples.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int Condo_ID { get; set; }

        [ForeignKey("Condo_ID")]
        public CondoModel Condo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CondoSimples.Models.BoardModel> BoardModels { get; set; }
        public System.Data.Entity.DbSet<CondoSimples.Models.CondoModel> CondoModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.TowerModel> TowerModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.UnitModel> UnitModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.EmployeeModel> EmployeeModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.UserModel> UserModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.BorrowModel> BorrowModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.OutSourcerModel> OutSourcerModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.AddressModel> AddressModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.OrderModel> OrderModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.OccurrenceModel> OccurrenceModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.NotificationModel> NotificationModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.CommonPlaceModel> CommonPlaceModels { get; set; }

        public System.Data.Entity.DbSet<CondoSimples.Models.ScheduleModel> ScheduleModels { get; set; }
    }
}