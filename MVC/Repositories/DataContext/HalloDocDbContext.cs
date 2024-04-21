using Microsoft.EntityFrameworkCore;
using Repositories.DataModels;

namespace Repositories.DataContext;

public partial class HalloDocDbContext : DbContext
{
    public HalloDocDbContext()
    {
    }

    public HalloDocDbContext(DbContextOptions<HalloDocDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminRegion> AdminRegions { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<BlockRequest> BlockRequests { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<CaseTag> CaseTags { get; set; }

    public virtual DbSet<CloseRequest> CloseRequests { get; set; }

    public virtual DbSet<Concierge> Concierges { get; set; }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<HealthProfessional> HealthProfessionals { get; set; }

    public virtual DbSet<HealthProfessionalType> HealthProfessionalTypes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Physician> Physicians { get; set; }

    public virtual DbSet<PhysicianLocation> PhysicianLocations { get; set; }

    public virtual DbSet<PhysicianNotification> PhysicianNotifications { get; set; }

    public virtual DbSet<PhysicianRegion> PhysicianRegions { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestBusiness> RequestBusinesses { get; set; }

    public virtual DbSet<RequestClient> RequestClients { get; set; }

    public virtual DbSet<RequestConcierge> RequestConcierges { get; set; }

    public virtual DbSet<RequestNote> RequestNotes { get; set; }

    public virtual DbSet<RequestStatusLog> RequestStatusLogs { get; set; }

    public virtual DbSet<RequestType> RequestTypes { get; set; }

    public virtual DbSet<RequestWiseFile> RequestWiseFiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftDetail> ShiftDetails { get; set; }

    public virtual DbSet<ShiftDetailRegion> ShiftDetailRegions { get; set; }

    public virtual DbSet<Smslog> Smslogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID = postgres;Password=aVI@12345;Server=localhost;Port=5432;Database=HalloDoc;Integrated Security=true;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("Admin_pkey");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.AdminAspNetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Admin_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AdminCreatedByNavigations).HasConstraintName("Admin_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AdminModifiedByNavigations).HasConstraintName("Admin_ModifiedBy_fkey");
        });

        modelBuilder.Entity<AdminRegion>(entity =>
        {
            entity.HasKey(e => e.AdminRegionId).HasName("AdminRegion_pkey");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminRegion_AdminId");

            entity.HasOne(d => d.Region).WithMany(p => p.AdminRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminRegion_RegionId");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AspNetRoles_pkey");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AspNetUsers_pkey");
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("AspNetUserRoles_pkey");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetUserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserRoles_RoleId_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.AspNetUserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserRoles_UserId_fkey");
        });

        modelBuilder.Entity<BlockRequest>(entity =>
        {
            entity.HasKey(e => e.BlockRequestId).HasName("BlockRequests_pkey");

            entity.HasOne(d => d.Request).WithMany(p => p.BlockRequests).HasConstraintName("requestId");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("Business_pkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessCreatedByNavigations).HasConstraintName("Business_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessModifiedByNavigations).HasConstraintName("Business_ModifiedBy_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Businesses).HasConstraintName("Business_RegionId_fkey");
        });

        modelBuilder.Entity<CaseTag>(entity =>
        {
            entity.HasKey(e => e.CaseTagId).HasName("CaseTag_pkey");
        });

        modelBuilder.Entity<CloseRequest>(entity =>
        {
            entity.HasKey(e => e.RequestClosedId).HasName("RequestClosed_pkey");

            entity.Property(e => e.RequestClosedId).HasDefaultValueSql("nextval('\"RequestClosed_RequestClosedId_seq\"'::regclass)");

            entity.HasOne(d => d.Request).WithMany(p => p.CloseRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestClosed_RequestId_fkey");

            entity.HasOne(d => d.RequestStatusLog).WithMany(p => p.CloseRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestClosed_RequestStatusLogId_fkey");
        });

        modelBuilder.Entity<Concierge>(entity =>
        {
            entity.HasKey(e => e.ConciergeId).HasName("Concierge_pkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Concierges).HasConstraintName("Concierge_RegionId_fkey");
        });

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.HasKey(e => e.EmailLogId).HasName("EmailLog_pkey");

            entity.HasOne(d => d.Role).WithMany(p => p.EmailLogs).HasConstraintName("Role");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.EncounterId).HasName("Encounter_pkey");

            entity.HasOne(d => d.Request).WithMany(p => p.Encounters).HasConstraintName("requestId");
        });

        modelBuilder.Entity<HealthProfessional>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("HealthProfessionals_pkey");

            entity.HasOne(d => d.ProfessionNavigation).WithMany(p => p.HealthProfessionals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HealthProfessionals_Profession_fkey");
        });

        modelBuilder.Entity<HealthProfessionalType>(entity =>
        {
            entity.HasKey(e => e.HealthProfessionalId).HasName("HealthProfessionalType_pkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("Menu_pkey");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderDetails_pkey");
        });

        modelBuilder.Entity<Physician>(entity =>
        {
            entity.HasKey(e => e.PhysicianId).HasName("Physician_pkey");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.PhysicianAspNetUsers).HasConstraintName("Physician_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PhysicianCreatedByNavigations).HasConstraintName("Physician_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PhysicianModifiedByNavigations).HasConstraintName("Physician_ModifiedBy_fkey");
        });

        modelBuilder.Entity<PhysicianLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PhysicianLocation_pkey");
        });

        modelBuilder.Entity<PhysicianNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PhysicianNotification_pkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianNotifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianNotification_PhysicianId_fkey");
        });

        modelBuilder.Entity<PhysicianRegion>(entity =>
        {
            entity.HasKey(e => e.PhysicianRegionId).HasName("PhysicianRegion_pkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianRegion_PhysicianId_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.PhysicianRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianRegion_RegionId_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("Region_pkey");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("Request_pkey");

            entity.HasOne(d => d.CaseTag).WithMany(p => p.Requests).HasConstraintName("Request_CaseTagId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Requests).HasConstraintName("Request_UserId_fkey");
        });

        modelBuilder.Entity<RequestBusiness>(entity =>
        {
            entity.HasKey(e => e.RequestBusinessId).HasName("RequestBusiness_pkey");

            entity.HasOne(d => d.Business).WithMany(p => p.RequestBusinesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestBusiness_BusinessId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestBusinesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestBusiness_RequestId_fkey");
        });

        modelBuilder.Entity<RequestClient>(entity =>
        {
            entity.HasKey(e => e.RequestClientId).HasName("RequestClient_pkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestClients).HasConstraintName("RequestClient_PhysicianId_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.RequestClients).HasConstraintName("RequestClient_RegionId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestClients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestClient_RequestId_fkey");
        });

        modelBuilder.Entity<RequestConcierge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RequestConcierge_pkey");

            entity.HasOne(d => d.Concierge).WithMany(p => p.RequestConcierges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestConcierge_ConciergeId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestConcierges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestConcierge_RequestId_fkey");
        });

        modelBuilder.Entity<RequestNote>(entity =>
        {
            entity.HasKey(e => e.RequestNotesId).HasName("RequestNotes_pkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RequestNoteCreatedByNavigations).HasConstraintName("RequestNotes_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RequestNoteModifiedByNavigations).HasConstraintName("RequestNotes_ModifiedBy_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestNotes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestNotes_RequestId_fkey");
        });

        modelBuilder.Entity<RequestStatusLog>(entity =>
        {
            entity.HasKey(e => e.RequestStatusLogId).HasName("RequestStatusLog_pkey");

            entity.HasOne(d => d.Admin).WithMany(p => p.RequestStatusLogs).HasConstraintName("RequestStatusLog_AdminId_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestStatusLogPhysicians).HasConstraintName("RequestStatusLog_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestStatusLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestStatusLog_RequestId_fkey");

            entity.HasOne(d => d.TransToPhysician).WithMany(p => p.RequestStatusLogTransToPhysicians).HasConstraintName("RequestStatusLog_TransToPhysicianId_fkey");
        });

        modelBuilder.Entity<RequestType>(entity =>
        {
            entity.HasKey(e => e.RequestTypeId).HasName("RequestType_pkey");
        });

        modelBuilder.Entity<RequestWiseFile>(entity =>
        {
            entity.HasKey(e => e.RequestWiseFileId).HasName("RequestWiseFile_pkey");

            entity.HasOne(d => d.Admin).WithMany(p => p.RequestWiseFiles).HasConstraintName("RequestWiseFile_AdminId_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestWiseFiles).HasConstraintName("RequestWiseFile_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestWiseFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestWiseFile_RequestId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pkey");

            entity.HasOne(d => d.AccountTypeNavigation).WithMany(p => p.Roles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Role_AccountType_fkey");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("RoleMenu_pkey");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleMenu_MenuId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleMenu_RoleId_fkey");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("Shift_pkey");

            entity.Property(e => e.WeekDays).IsFixedLength();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Shifts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shift_CreatedBy_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.Shifts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shift_PhysicianId_fkey");
        });

        modelBuilder.Entity<ShiftDetail>(entity =>
        {
            entity.HasKey(e => e.ShiftDetailId).HasName("ShiftDetail_pkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShiftDetails).HasConstraintName("ShiftDetail_ModifiedBy_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.ShiftDetails).HasConstraintName("ShiftDetail_RegionId_fkey");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetail_ShiftId_fkey");
        });

        modelBuilder.Entity<ShiftDetailRegion>(entity =>
        {
            entity.HasKey(e => e.ShiftDetailRegionId).HasName("ShiftDetailRegion_pkey");

            entity.HasOne(d => d.Region).WithMany(p => p.ShiftDetailRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetailRegion_RegionId_fkey");

            entity.HasOne(d => d.ShiftDetail).WithMany(p => p.ShiftDetailRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetailRegion_ShiftDetailId_fkey");
        });

        modelBuilder.Entity<Smslog>(entity =>
        {
            entity.HasKey(e => e.SmslogId).HasName("SMSLog_pkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Smslogs).HasConstraintName("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.Property(e => e.IsMobile).HasDefaultValueSql("(1)::bit(1)");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.UserAspNetUsers).HasConstraintName("User_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.UserModifiedByNavigations).HasConstraintName("User_ModifiedBy_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
