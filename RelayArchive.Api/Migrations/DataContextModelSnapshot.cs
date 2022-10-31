﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RelayArchive.Api.AppData;

#nullable disable

namespace RelayArchive.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RelayArchive.Api.Models.RelayInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Chain")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("chain");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("EmitterAddressHex")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("emitter_address_hex");

                    b.Property<string>("PayloadHex")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payload_hex");

                    b.Property<decimal>("Sequence")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("sequence");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("StreamEscrow")
                        .HasColumnType("text")
                        .HasColumnName("stream_escrow");

                    b.HasKey("Id");

                    b.HasIndex("Chain", "EmitterAddressHex", "Sequence")
                        .IsUnique();

                    b.ToTable("relay_infos");
                });

            modelBuilder.Entity("RelayArchive.Api.Models.Signature", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.Property<Guid>("RelayInfoId")
                        .HasColumnType("uuid")
                        .HasColumnName("relay_info_id");

                    b.HasKey("Value");

                    b.HasIndex("RelayInfoId");

                    b.ToTable("signatures");
                });

            modelBuilder.Entity("RelayArchive.Api.Models.Signature", b =>
                {
                    b.HasOne("RelayArchive.Api.Models.RelayInfo", null)
                        .WithMany("Signatures")
                        .HasForeignKey("RelayInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RelayArchive.Api.Models.RelayInfo", b =>
                {
                    b.Navigation("Signatures");
                });
#pragma warning restore 612, 618
        }
    }
}
