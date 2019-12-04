﻿// <auto-generated />
using System;
using BerkayMarket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BerkayMarket.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190624165046_Kalemlik")]
    partial class Kalemlik
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BerkayMarket.Models.BmRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.Property<string>("RoleId1");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("UserId1");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("RoleId1");

                    b.Property<string>("UserId1");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("UserId1");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BerkayMarket.Models.Iyelik", b =>
                {
                    b.Property<int>("UrunId");

                    b.Property<string>("BmUserId");

                    b.HasKey("UrunId", "BmUserId");

                    b.HasIndex("BmUserId");

                    b.ToTable("Iyelik");
                });

            modelBuilder.Entity("BerkayMarket.Models.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adi");

                    b.HasKey("Id");

                    b.ToTable("Kategoris");
                });

            modelBuilder.Entity("BerkayMarket.Models.Urol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adi");

                    b.HasKey("Id");

                    b.ToTable("Urol");
                });

            modelBuilder.Entity("BerkayMarket.Models.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Aciklama");

                    b.Property<string>("Adi");

                    b.Property<string>("BmUserId");

                    b.Property<int>("KategoriId");

                    b.Property<string>("Resim");

                    b.Property<int>("Stok");

                    b.HasKey("Id");

                    b.HasIndex("BmUserId");

                    b.HasIndex("KategoriId");

                    b.ToTable("Uruns");
                });

            modelBuilder.Entity("BerkayMarket.Models.UrunUrol", b =>
                {
                    b.Property<int>("UrunId");

                    b.Property<int>("UrolId");

                    b.HasKey("UrunId", "UrolId");

                    b.HasIndex("UrolId");

                    b.ToTable("UrunUrol");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmRoleClaim", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.BmRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId1");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserClaim", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.BmUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserLogin", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.BmUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserRole", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.BmRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("BerkayMarket.Models.BmUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.BmUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("BerkayMarket.Models.BmUserToken", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.BmUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("BerkayMarket.Models.Iyelik", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmUser", "BmUser")
                        .WithMany("Iyeliks")
                        .HasForeignKey("BmUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.Urun", "Urun")
                        .WithMany("Iyeliks")
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BerkayMarket.Models.Urun", b =>
                {
                    b.HasOne("BerkayMarket.Models.BmUser", "BmUser")
                        .WithMany("Uruns")
                        .HasForeignKey("BmUserId");

                    b.HasOne("BerkayMarket.Models.Kategori", "Kategori")
                        .WithMany("Urunleri")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BerkayMarket.Models.UrunUrol", b =>
                {
                    b.HasOne("BerkayMarket.Models.Urol", "Urol")
                        .WithMany("UrunUrols")
                        .HasForeignKey("UrolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.Urun", "Urun")
                        .WithMany("UrunUrols")
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
