﻿// <auto-generated />
using Metronik.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Metronik.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220410164931_InitialCreate2")]
    partial class InitialCreate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("Metronik.Enteties.AppOms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("omsId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Oms");
                });

            modelBuilder.Entity("Metronik.Enteties.AppOrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExpectedStartDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FactortyCountry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FactortyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FactoryAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FactoryId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PoNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductionLineId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppOrderId")
                        .IsUnique();

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Metronik.Enteties.AppOrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gtin")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT");

                    b.Property<ulong>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumberType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TemplateId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppOrderId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("Metronik.Entities.AppOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("expectedCompletionTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("omsId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("orderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Metronik.Entities.AppSerialNumbers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppOrderProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppOrderProductId");

                    b.ToTable("SerialNumbers");
                });

            modelBuilder.Entity("Metronik.Enteties.AppOrderDetails", b =>
                {
                    b.HasOne("Metronik.Entities.AppOrder", "AppOrder")
                        .WithOne("AppOrderDetails")
                        .HasForeignKey("Metronik.Enteties.AppOrderDetails", "AppOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppOrder");
                });

            modelBuilder.Entity("Metronik.Enteties.AppOrderProduct", b =>
                {
                    b.HasOne("Metronik.Entities.AppOrder", "AppOrder")
                        .WithMany("AppOrderProducts")
                        .HasForeignKey("AppOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppOrder");
                });

            modelBuilder.Entity("Metronik.Entities.AppSerialNumbers", b =>
                {
                    b.HasOne("Metronik.Enteties.AppOrderProduct", "AppOrderProduct")
                        .WithMany("SerialNumbers")
                        .HasForeignKey("AppOrderProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppOrderProduct");
                });

            modelBuilder.Entity("Metronik.Enteties.AppOrderProduct", b =>
                {
                    b.Navigation("SerialNumbers");
                });

            modelBuilder.Entity("Metronik.Entities.AppOrder", b =>
                {
                    b.Navigation("AppOrderDetails")
                        .IsRequired();

                    b.Navigation("AppOrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}