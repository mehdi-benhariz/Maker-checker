﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using maker_checker_v1.data;

#nullable disable

namespace maker_checker_v1.Migrations
{
    [DbContext(typeof(RequestContext))]
    [Migration("20220711221823_addCreationDate")]
    partial class addCreationDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("maker_checker_v1.models.entities.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("validationProgressId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("userId", "validationProgressId");

                    b.HasIndex("validationProgressId");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("REAL")
                        .HasDefaultValue(0f);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Rule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte>("Nbr")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ValidationId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ValidationProgressId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ValidationId");

                    b.HasIndex("ValidationProgressId");

                    b.ToTable("Rule");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.ServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ServiceType");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Validation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeId")
                        .IsUnique();

                    b.ToTable("Validation");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.ValidationProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RequestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("ValidationProgress");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Operation", b =>
                {
                    b.HasOne("maker_checker_v1.models.entities.User", "User")
                        .WithMany("Operations")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maker_checker_v1.models.entities.ValidationProgress", "ValidationProgress")
                        .WithMany("Operations")
                        .HasForeignKey("validationProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("ValidationProgress");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Request", b =>
                {
                    b.HasOne("maker_checker_v1.models.entities.ServiceType", "ServiceType")
                        .WithMany("Requests")
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maker_checker_v1.models.entities.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Rule", b =>
                {
                    b.HasOne("maker_checker_v1.models.entities.Role", "Role")
                        .WithMany("Rules")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maker_checker_v1.models.entities.Validation", "Validation")
                        .WithMany("Rules")
                        .HasForeignKey("ValidationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maker_checker_v1.models.entities.ValidationProgress", "ValidationProgress")
                        .WithMany()
                        .HasForeignKey("ValidationProgressId");

                    b.Navigation("Role");

                    b.Navigation("Validation");

                    b.Navigation("ValidationProgress");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.User", b =>
                {
                    b.HasOne("maker_checker_v1.models.entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Validation", b =>
                {
                    b.HasOne("maker_checker_v1.models.entities.ServiceType", "ServiceType")
                        .WithOne("Validation")
                        .HasForeignKey("maker_checker_v1.models.entities.Validation", "ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.ValidationProgress", b =>
                {
                    b.HasOne("maker_checker_v1.models.entities.Request", "Request")
                        .WithOne("ValidationProgress")
                        .HasForeignKey("maker_checker_v1.models.entities.ValidationProgress", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Request", b =>
                {
                    b.Navigation("ValidationProgress");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Role", b =>
                {
                    b.Navigation("Rules");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.ServiceType", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("Validation");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.User", b =>
                {
                    b.Navigation("Operations");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.Validation", b =>
                {
                    b.Navigation("Rules");
                });

            modelBuilder.Entity("maker_checker_v1.models.entities.ValidationProgress", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
