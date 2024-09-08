﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientApi.Data;

#nullable disable

namespace PatientApi.Migrations
{
    [DbContext(typeof(PatientContext))]
    partial class PatientContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.33");

            modelBuilder.Entity("PatientApi.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientApi.Models.Patient", b =>
                {
                    b.OwnsOne("PatientApi.Models.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("PatientId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Family")
                                .HasColumnType("TEXT")
                                .HasColumnName("Family");

                            b1.Property<string>("Given")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Given");

                            b1.Property<string>("Use")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.Navigation("Name");
                });
#pragma warning restore 612, 618
        }
    }
}
