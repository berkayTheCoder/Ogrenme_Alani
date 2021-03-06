﻿// <auto-generated />
using System;
using BerkayMarket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BerkayMarket.Migrations
{
    [DbContext(typeof(MarketContext))]
    partial class MarketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("BerkayMarket.Models.Envanter", b =>
                {
                    b.Property<int>("UrunID");

                    b.Property<int>("SaticiID");

                    b.Property<int>("EnvanterID");

                    b.HasKey("UrunID", "SaticiID");

                    b.HasAlternateKey("EnvanterID");

                    b.HasIndex("SaticiID");

                    b.ToTable("Envanter");
                });

            modelBuilder.Entity("BerkayMarket.Models.Kategori", b =>
                {
                    b.Property<int>("KategoriID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("SaticiID");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("KategoriID");

                    b.HasIndex("SaticiID");

                    b.ToTable("Kategori");
                });

            modelBuilder.Entity("BerkayMarket.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("SaticiID");

                    b.Property<string>("Location")
                        .HasMaxLength(50);

                    b.HasKey("SaticiID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("BerkayMarket.Models.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("BerkayMarket.Models.Siparis", b =>
                {
                    b.Property<int>("SiparisID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Grade");

                    b.Property<int>("MusteriID");

                    b.Property<int>("UrunID");

                    b.HasKey("SiparisID");

                    b.HasIndex("MusteriID");

                    b.HasIndex("UrunID");

                    b.ToTable("Siparis");
                });

            modelBuilder.Entity("BerkayMarket.Models.Urun", b =>
                {
                    b.Property<int>("UrunID");

                    b.Property<int>("Credits");

                    b.Property<int>("KategoriID");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("UrunID");

                    b.HasIndex("KategoriID");

                    b.ToTable("Urun");
                });

            modelBuilder.Entity("BerkayMarket.Models.Musteri", b =>
                {
                    b.HasBaseType("BerkayMarket.Models.Person");

                    b.Property<DateTime>("SiparisDate");

                    b.ToTable("Musteri");

                    b.HasDiscriminator().HasValue("Musteri");
                });

            modelBuilder.Entity("BerkayMarket.Models.Satici", b =>
                {
                    b.HasBaseType("BerkayMarket.Models.Person");

                    b.Property<DateTime>("HireDate");

                    b.ToTable("Satici");

                    b.HasDiscriminator().HasValue("Satici");
                });

            modelBuilder.Entity("BerkayMarket.Models.Envanter", b =>
                {
                    b.HasOne("BerkayMarket.Models.Satici", "Satici")
                        .WithMany("Envanters")
                        .HasForeignKey("SaticiID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.Urun", "Urun")
                        .WithMany("Envanters")
                        .HasForeignKey("UrunID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BerkayMarket.Models.Kategori", b =>
                {
                    b.HasOne("BerkayMarket.Models.Satici", "Administrator")
                        .WithMany()
                        .HasForeignKey("SaticiID");
                });

            modelBuilder.Entity("BerkayMarket.Models.OfficeAssignment", b =>
                {
                    b.HasOne("BerkayMarket.Models.Satici", "Satici")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("BerkayMarket.Models.OfficeAssignment", "SaticiID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BerkayMarket.Models.Siparis", b =>
                {
                    b.HasOne("BerkayMarket.Models.Musteri", "Musteri")
                        .WithMany("Sipariss")
                        .HasForeignKey("MusteriID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BerkayMarket.Models.Urun", "Urun")
                        .WithMany("Sipariss")
                        .HasForeignKey("UrunID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BerkayMarket.Models.Urun", b =>
                {
                    b.HasOne("BerkayMarket.Models.Kategori", "Kategori")
                        .WithMany("Uruns")
                        .HasForeignKey("KategoriID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
