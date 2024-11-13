﻿// <auto-generated />
using System;
using CryptoDCACalculator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoDCACalculator.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241113092056_Remove_Test_Table")]
    partial class Remove_Test_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CryptoDCACalculator.Entities.CryptoPrice", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CryptocurrencyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CryptocurrencyID");

                    b.ToTable("CryptoPrices");
                });

            modelBuilder.Entity("CryptoDCACalculator.Entities.Cryptocurrency", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Cryptocurrencies");
                });

            modelBuilder.Entity("CryptoDCACalculator.Entities.CryptoPrice", b =>
                {
                    b.HasOne("CryptoDCACalculator.Entities.Cryptocurrency", null)
                        .WithMany("CryptoPrices")
                        .HasForeignKey("CryptocurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CryptoDCACalculator.Entities.Cryptocurrency", b =>
                {
                    b.Navigation("CryptoPrices");
                });
#pragma warning restore 612, 618
        }
    }
}