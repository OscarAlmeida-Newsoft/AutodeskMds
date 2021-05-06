using Microsoft.AspNet.Identity.EntityFramework;
using SOMSightModels;
using SOMSightModels.Users;
using SOMSightRepositories.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightRepositories
{
    public class SOMSightContext : IdentityDbContext<User, Role, Guid, MyLogin, UserRole, MyClaim>
    {
        public SOMSightContext(): base ("Name=SOMSightContext")
        {
            Database.SetInitializer<SOMSightContext>(new MigrateDatabaseToLatestVersion<SOMSightContext, Configuration>(true));
        }


        public virtual DbSet<NS_TblAnswerXAssessment> NS_AnswerXAssessment { get; set; }
        public virtual DbSet<NS_TblAssessmentSummary> NS_AssessmentSummary { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
