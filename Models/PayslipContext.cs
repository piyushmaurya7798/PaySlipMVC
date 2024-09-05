using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaySlipMVC.Models;

public partial class PayslipContext : DbContext
{
    public PayslipContext()
    {
    }

    public PayslipContext(DbContextOptions<PayslipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<Leaveclone> Leaveclones { get; set; }

    public virtual DbSet<Payslip> Payslips { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        if (!optionsBuilder.IsConfigured)
        {
            
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1ACBA10BF");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3213E83FCD25D4DA");

            entity.ToTable("Event");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Eventdate).HasColumnName("eventdate");
            entity.Property(e => e.Eventdesc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("eventdesc");
            entity.Property(e => e.Flag).HasColumnName("flag");
            entity.Property(e => e.Vartype)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("vartype");
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leave__3213E83FA8C91B1F");

            entity.ToTable("Leave");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cl).HasColumnName("cl");
            entity.Property(e => e.Fromdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fromdate");
            entity.Property(e => e.Pl).HasColumnName("pl");
            entity.Property(e => e.Reason)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Sitem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sitem");
            entity.Property(e => e.Sl).HasColumnName("sl");
            entity.Property(e => e.Todate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("todate");
        });

        modelBuilder.Entity<Leaveclone>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Leaveclone");

            entity.Property(e => e.Cl).HasColumnName("cl");
            entity.Property(e => e.Fromdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fromdate");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Pl).HasColumnName("pl");
            entity.Property(e => e.Reason)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Sitem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sitem");
            entity.Property(e => e.Sl).HasColumnName("sl");
            entity.Property(e => e.Todate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("todate");
        });

        modelBuilder.Entity<Payslip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payslip__3213E83FA3E35EE1");

            entity.ToTable("payslip");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Absent).HasColumnName("absent");
            entity.Property(e => e.Present).HasColumnName("present");
            entity.Property(e => e.Remain).HasColumnName("remain");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.Tdays).HasColumnName("tdays");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Payslips)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__payslip__userid__48CFD27E");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__ticket__3333C61052372F0B");

            entity.ToTable("ticket");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.Attachment)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("attachment");
            entity.Property(e => e.Closeddate)
                .HasColumnType("datetime")
                .HasColumnName("closeddate");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Raiseby)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("raiseby");
            entity.Property(e => e.Raiseddate)
                .HasColumnType("datetime")
                .HasColumnName("raiseddate");
            entity.Property(e => e.Raiseto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("raiseto");
            entity.Property(e => e.Solution)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("solution");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_acc__3214EC0743502EA7");

            entity.ToTable("User_account");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_Fname");
            entity.Property(e => e.UserLname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_Lname");
            entity.Property(e => e.UserMobile).HasColumnName("user_mobile");
            entity.Property(e => e.UserPass)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_pass");
            entity.Property(e => e.UserProfile)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_profile");
            entity.Property(e => e.UserRole)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
