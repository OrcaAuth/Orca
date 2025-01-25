﻿// <auto-generated />
using System;
using Orca.Store.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContosoUniversity.EntityFrameworkCore.Store.Infrastructure.Data
{
    [DbContext(typeof(ContosoDbContext))]
    partial class ContosoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.DelegationEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<string>("WhoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WhomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("WhoId");

                    b.HasIndex("WhomId");

                    b.HasIndex("From", "To");

                    b.ToTable("Delegations");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.PermissionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.PolicyEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RoleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RoleMappingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Mapping")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Mapping", "RoleId")
                        .IsUnique();

                    b.ToTable("RoleMappings");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RolePermissionEntity", b =>
                {
                    b.Property<string>("PermissionId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RoleSubjectEntity", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SubjectId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleSubjects");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.SubjectEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Sub")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Sub")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.DelegationEntity", b =>
                {
                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.SubjectEntity", "Who")
                        .WithMany()
                        .HasForeignKey("WhoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.SubjectEntity", "Whom")
                        .WithMany()
                        .HasForeignKey("WhomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Who");

                    b.Navigation("Whom");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RoleMappingEntity", b =>
                {
                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.RoleEntity", "Role")
                        .WithMany("Mappings")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RolePermissionEntity", b =>
                {
                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.PermissionEntity", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RoleSubjectEntity", b =>
                {
                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orca.Store.EntityFrameworkCore.Entities.SubjectEntity", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Orca.Store.EntityFrameworkCore.Entities.RoleEntity", b =>
                {
                    b.Navigation("Mappings");
                });
#pragma warning restore 612, 618
        }
    }
}
