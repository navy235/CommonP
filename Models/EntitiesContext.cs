using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Maitonn.Core;

namespace CommonP.Models
{
    public class EntitiesContext : UnitOfWork
    {
        public EntitiesContext()
            : base("commonp_db")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Action>()
             .HasRequired(p => p.Controller)
             .WithMany(d => d.Action)
             .HasForeignKey(p => p.ControllerID);

            modelBuilder.Entity<Action>()
             .HasRequired(p => p.Controller)
             .WithMany(d => d.Action)
             .HasForeignKey(p => p.ControllerID);


            modelBuilder.Entity<Action>()
            .HasMany(g => g.Role)
            .WithMany(r => r.Action)
            .Map
            (
                m =>
                {
                    m.MapLeftKey("ActionID");
                    m.MapRightKey("RoleID");
                    m.ToTable("Action_Role");
                }
            );

            modelBuilder.Entity<Member>()
            .HasRequired(p => p.Group)
            .WithMany(d => d.Member)
            .HasForeignKey(p => p.GroupID);

            modelBuilder.Entity<Group>()
            .HasMany(g => g.Role)
            .WithMany(r => r.Group)
            .Map
            (
                m =>
                {
                    m.MapLeftKey("GroupID");
                    m.MapRightKey("RoleID");
                    m.ToTable("Group_Role");
                }
            );




            base.OnModelCreating(modelBuilder);
        }
    }
}