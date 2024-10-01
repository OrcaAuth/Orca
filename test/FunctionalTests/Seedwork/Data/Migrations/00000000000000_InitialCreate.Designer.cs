﻿// <auto-generated />
using System;
using Balea.Store.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunctionalTests.Migrations
{
    [DbContext(typeof(BaleaDbContext))]
    [Migration("00000000000000_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.ApplicationEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.DelegationEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ApplicationId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<string>("Who")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Whom")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Whom");

                    b.HasIndex("From", "To");

                    b.ToTable("Delegations");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.PermissionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ApplicationId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.PolicyEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ApplicationId")
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

                    b.HasIndex("ApplicationId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ApplicationId")
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

                    b.HasIndex("ApplicationId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleMappingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Mapping")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Mapping", "RoleId")
                        .IsUnique();

                    b.ToTable("RoleMappings");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RolePermissionEntity", b =>
                {
                    b.Property<string>("PermissionId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleSubjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sub")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Sub", "RoleId")
                        .IsUnique();

                    b.ToTable("RoleSubjects");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.DelegationEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.ApplicationEntity", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.PermissionEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.ApplicationEntity", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.PolicyEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.ApplicationEntity", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.ApplicationEntity", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleMappingEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.RoleEntity", "Role")
                        .WithMany("Mappings")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RolePermissionEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.PermissionEntity", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleSubjectEntity", b =>
                {
                    b.HasOne("Balea.Store.EntityFrameworkCore.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Balea.Store.EntityFrameworkCore.Entities.RoleEntity", b =>
                {
                    b.Navigation("Mappings");
                });
#pragma warning restore 612, 618
        }
    }
}
