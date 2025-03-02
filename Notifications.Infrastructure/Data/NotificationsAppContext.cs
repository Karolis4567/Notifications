using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public class NotificationsAppContext : DbContext
    {
        public NotificationsAppContext(DbContextOptions<NotificationsAppContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyMarket> CompanyMarkets { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyNotificationSchedule> CompanyNotificationSchedules { get; set; }
        public DbSet<CompanyNotificationScheduleAssignRules> CompanyNotificationScheduleAssignsRules { get; set; }
        public DbSet<NotificationSchedule> NotificationSchedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyMarket>(entity =>
            {
                entity.HasKey(a => a.companyMarketId);
                entity.Property(a => a.companyMarketId).ValueGeneratedOnAdd();
                entity.ToTable("CompanyMarkets");

                entity.Property(a => a.companyMarketId).HasColumnName("CompanyMarketId");
                entity.Property(a => a.companyMarketCode).HasColumnName("CompanyMarketCode").HasMaxLength(2);
                entity.Property(a => a.companyMarketName).HasColumnName("CompanyMarketName").HasMaxLength(100);
                entity.Property(a => a.creationDate).HasColumnName("CreationDate").HasDefaultValue(DateTime.Now);

                entity.HasData(
                    new CompanyMarket()
                    {
                        companyMarketId = 1,
                        companyMarketCode = "DK",
                        companyMarketName = "Denmark"                       
                    },
                    new CompanyMarket()
                    {
                        companyMarketId = 2,
                        companyMarketCode = "NO",
                        companyMarketName = "Norway"            
                    },
                    new CompanyMarket()
                    {
                        companyMarketId = 3,
                        companyMarketCode = "SE",
                        companyMarketName = "Sweden"                  
                    },
                    new CompanyMarket()
                    {
                        companyMarketId = 4,
                        companyMarketCode = "FI",
                        companyMarketName = "Finland"                 
                    }
                 );
            });
            
            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.HasKey(a => a.companyTypeId);
                entity.Property(a => a.companyTypeId).ValueGeneratedOnAdd();
                entity.ToTable("CompanyTypes");

                entity.Property(a => a.companyTypeId).HasColumnName("CompanyTypeId");
                entity.Property(a => a.companyTypeCode).HasColumnName("CompanyTypeCode").HasMaxLength(1);
                entity.Property(a => a.companyTypeName).HasColumnName("CompanyTypeName").HasMaxLength(6);
                entity.Property(a => a.creationDate).HasColumnName("CreationDate").HasDefaultValue(DateTime.Now);

                entity.HasData(
                    new CompanyType
                    {
                        companyTypeId = 1,
                        companyTypeCode = "s",
                        companyTypeName = "small"
                    },
                    new CompanyType
                    {
                        companyTypeId = 2,
                        companyTypeCode = "m",
                        companyTypeName = "medium"                        
                    },
                    new CompanyType
                    {
                        companyTypeId = 3,
                        companyTypeCode = "l",
                        companyTypeName = "large"                    
                    }
                );
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(a => a.companyId);
                entity.Property(a => a.companyId).ValueGeneratedNever();
                entity.ToTable("Companies");

                entity.Property(a => a.companyId).HasColumnName("CompanyId");
                entity.Property(a => a.companyNumber).HasColumnName("CompanyNumber").HasMaxLength(10);
                entity.Property(a => a.companyName).HasColumnName("CompanyName").HasMaxLength(255);
                entity.Property(a => a.companyTypeId).HasColumnName("CompanyTypeId");
                entity.Property(a => a.companyMarketId).HasColumnName("CompanyMarketId");
                entity.Property(a => a.creationDate).HasColumnName("CreationDate").HasDefaultValue(DateTime.Now);
                entity.Property(a => a.updateDate).HasColumnName("UpdateDate").HasDefaultValue(DateTime.Now);

                entity
                  .HasOne<CompanyMarket>(p => p.companyMarket)
                  .WithMany(p => p.companies)
                  .HasForeignKey(p => p.companyMarketId)
                  .IsRequired(true)
                  .OnDelete(DeleteBehavior.NoAction);

                entity
                  .HasOne<CompanyType>(p => p.companyType)
                  .WithMany(p => p.companies)
                  .HasForeignKey(p => p.companyTypeId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.NoAction);



            });

            modelBuilder.Entity<NotificationSchedule>(entity =>
            {
                entity.HasKey(a => a.notificationScheduleId);
                entity.Property(a => a.notificationScheduleId).ValueGeneratedOnAdd();
                entity.ToTable("NotificationSchedules");

                entity.Property(a => a.notificationScheduleId).HasColumnName("NotificationScheduleId");
                entity.Property(a => a.days).HasColumnName("Days").HasMaxLength(100);
                entity.Property(a => a.creationDate).HasColumnName("CreationDate").HasDefaultValue(DateTime.Now);
                entity.Property(a => a.updateDate).HasColumnName("UpdateDate").HasDefaultValue(DateTime.Now);

                
                entity.HasData(
                    new NotificationSchedule()
                    {
                        notificationScheduleId = 1,
                        days = "1,5,10,15,20",
                        creationDate = DateTime.Now,
                        updateDate = DateTime.Now
                    },
                    new NotificationSchedule()
                    {
                        notificationScheduleId = 2,
                        days = "1,5,10,20",
                        creationDate = DateTime.Now,
                        updateDate = DateTime.Now
                    },
                    new NotificationSchedule()
                    {
                        notificationScheduleId = 3,
                        days = "1,7,14,28",
                        creationDate = DateTime.Now,
                        updateDate = DateTime.Now
                    }
                );
                
            });

            modelBuilder.Entity<CompanyNotificationScheduleAssignRules>(entity =>
            {
                entity.HasKey(a => a.companyNotificationScheduleAssignRulesId);
                entity.Property(a => a.companyNotificationScheduleAssignRulesId).ValueGeneratedOnAdd();
                entity.ToTable("CompanyNotificationsScheduleAssignRules");

                entity.Property(a => a.companyNotificationScheduleAssignRulesId).HasColumnName("CompanyNotificationScheduleAssignRulesId");
                entity.Property(a => a.companyMarketId).HasColumnName("CompanyMarketId");
                entity.Property(a => a.companyTypeId).HasColumnName("CompanyTypeId");
                entity.Property(a => a.notificationScheduleId).HasColumnName("NotificationScheduleId");
                entity.Property(a => a.creationDate).HasColumnName("CreationDate").HasDefaultValue(DateTime.Now);
                entity.Property(a => a.updateDate).HasColumnName("UpdateDate").HasDefaultValue(DateTime.Now);

                entity
                    .HasOne<CompanyMarket>(p => p.companyMarket)
                    .WithMany(p => p.scheduleAssignRules)
                    .HasForeignKey(p => p.companyMarketId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.NoAction);

                entity
                    .HasOne<CompanyType>(p => p.companyType)
                    .WithMany(p => p.scheduleAssignRules)
                    .HasForeignKey(p => p.companyTypeId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.NoAction);

                entity
                    .HasOne<NotificationSchedule>(p => p.notificationSchedule)
                    .WithMany(p => p.scheduleAssignRules)
                    .HasForeignKey(p => p.notificationScheduleId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.NoAction);

                
                entity.HasData(
                   new CompanyNotificationScheduleAssignRules()
                   {
                       companyNotificationScheduleAssignRulesId = 1,
                       companyMarketId = 1,
                       companyTypeId = 1,
                       notificationScheduleId = 1
                   },
                    new CompanyNotificationScheduleAssignRules()
                    {
                        companyNotificationScheduleAssignRulesId = 2,
                        companyMarketId = 1,
                        companyTypeId = 2,
                        notificationScheduleId = 1
                    },
                    new CompanyNotificationScheduleAssignRules()
                    {
                        companyNotificationScheduleAssignRulesId = 3,
                        companyMarketId = 1,
                        companyTypeId = 3,
                        notificationScheduleId = 1
                    },
                     new CompanyNotificationScheduleAssignRules()
                     {
                         companyNotificationScheduleAssignRulesId = 4,
                         companyMarketId = 2,
                         companyTypeId = 1,
                         notificationScheduleId = 2
                     },
                     new CompanyNotificationScheduleAssignRules()
                     {
                         companyNotificationScheduleAssignRulesId = 5,
                         companyMarketId = 2,
                         companyTypeId = 2,
                         notificationScheduleId = 2
                     },
                     new CompanyNotificationScheduleAssignRules()
                     {
                         companyNotificationScheduleAssignRulesId = 6,
                         companyMarketId = 2,
                         companyTypeId = 3,
                         notificationScheduleId = 2
                     },
                      new CompanyNotificationScheduleAssignRules()
                      {
                          companyNotificationScheduleAssignRulesId = 7,
                          companyMarketId = 3,
                          companyTypeId = 1,
                          notificationScheduleId = 3
                      },
                      new CompanyNotificationScheduleAssignRules()
                      {
                          companyNotificationScheduleAssignRulesId = 8,
                          companyMarketId = 3,
                          companyTypeId = 2,
                          notificationScheduleId = 3
                      },
                       new CompanyNotificationScheduleAssignRules()
                       {
                           companyNotificationScheduleAssignRulesId = 9,
                           companyMarketId = 4,
                           companyTypeId = 3,
                           notificationScheduleId = 1
                       }
                    );
                
            });


            modelBuilder.Entity<CompanyNotificationSchedule>(entity =>
            {
                entity.HasKey(a => a.companyNotificationsScheduleId);
                entity.Property(a => a.companyNotificationsScheduleId).ValueGeneratedOnAdd();
                entity.ToTable("CompanyNotificationSchedules");

                entity.Property(a => a.companyNotificationsScheduleId).HasColumnName("CompanyNotificationsScheduleId");
                entity.Property(a => a.companyId).HasColumnName("CompanyId");
                entity.Property(a => a.notificationScheduleId).HasColumnName("NotificationScheduleId");
                entity.Property(a => a.creationDate).HasColumnName("CreationDate").HasDefaultValue(DateTime.Now);
                entity.Property(a => a.updateDate).HasColumnName("UpdateDate").HasDefaultValue(DateTime.Now);
            });

            

            


           

           

         

           

            

         

           

            

            #region NotificationSchedule
            
           

            #endregion

        }
    }
}
