﻿// <auto-generated />
using System;
using Actie.DAT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Actie.DAL.Migrations
{
    [DbContext(typeof(ActieDbContext))]
    [Migration("20230303160732_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Actie.DAL.Entities.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ActivityEntityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ActivityEntityId");

                    b.ToTable("TagEntity");
                });

            modelBuilder.Entity("Actie.DAT.Entities.ActivityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProjectEntityId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProjectEntityId1")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserEntityId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectEntityId");

                    b.HasIndex("ProjectEntityId1");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Actie.DAT.Entities.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserEntityId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Actie.DAT.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Photo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float?>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("UserEntity");
                });

            modelBuilder.Entity("Actie.DAL.Entities.TagEntity", b =>
                {
                    b.HasOne("Actie.DAT.Entities.ActivityEntity", null)
                        .WithMany("Tags")
                        .HasForeignKey("ActivityEntityId");
                });

            modelBuilder.Entity("Actie.DAT.Entities.ActivityEntity", b =>
                {
                    b.HasOne("Actie.DAT.Entities.ProjectEntity", null)
                        .WithMany("Activities")
                        .HasForeignKey("ProjectEntityId");

                    b.HasOne("Actie.DAT.Entities.ProjectEntity", null)
                        .WithMany("Users")
                        .HasForeignKey("ProjectEntityId1");

                    b.HasOne("Actie.DAT.Entities.UserEntity", null)
                        .WithMany("Activities")
                        .HasForeignKey("UserEntityId");
                });

            modelBuilder.Entity("Actie.DAT.Entities.ProjectEntity", b =>
                {
                    b.HasOne("Actie.DAT.Entities.UserEntity", null)
                        .WithMany("Projects")
                        .HasForeignKey("UserEntityId");
                });

            modelBuilder.Entity("Actie.DAT.Entities.ActivityEntity", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Actie.DAT.Entities.ProjectEntity", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Actie.DAT.Entities.UserEntity", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
