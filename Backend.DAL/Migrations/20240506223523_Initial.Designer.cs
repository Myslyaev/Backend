﻿// <auto-generated />
using System;
using Backend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.DAL.Migrations
{
    [DbContext(typeof(MamkinMainerContext))]
    [Migration("20240506223523_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.Core.DTOs.DeviceDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Adress")
                        .HasColumnType("text")
                        .HasColumnName("adress");

                    b.Property<int>("DeviceType")
                        .HasColumnType("integer")
                        .HasColumnName("device_type");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.HasKey("Id")
                        .HasName("pk_devices");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_devices_owner_id");

                    b.ToTable("devices", (string)null);
                });

            modelBuilder.Entity("Backend.Core.DTOs.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Backend.Core.DTOs.DeviceDto", b =>
                {
                    b.HasOne("Backend.Core.DTOs.UserDto", "Owner")
                        .WithMany("Devices")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("fk_devices_users_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Backend.Core.DTOs.UserDto", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
