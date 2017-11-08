﻿// <auto-generated />
using AdvancedStudioExercise.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AdvancedStudioExercise.Infrastructure.Sql.Migrations
{
    [DbContext(typeof(AdvancedStudioExerciseContext))]
    partial class AdvancedStudioExerciseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvancedStudioExercise.Infrastructure.Sql.Entities.Identifier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("IdentifierTypeId");

                    b.Property<Guid>("PersonId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdentifierTypeId");

                    b.HasIndex("PersonId");

                    b.ToTable("Identifiers");
                });

            modelBuilder.Entity("AdvancedStudioExercise.Infrastructure.Sql.Entities.IdentifierType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("IdentifierTypes");
                });

            modelBuilder.Entity("AdvancedStudioExercise.Infrastructure.Sql.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("AdvancedStudioExercise.Infrastructure.Sql.Entities.Identifier", b =>
                {
                    b.HasOne("AdvancedStudioExercise.Infrastructure.Sql.Entities.IdentifierType", "IdentifierType")
                        .WithMany("Identifiers")
                        .HasForeignKey("IdentifierTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdvancedStudioExercise.Infrastructure.Sql.Entities.Person", "Person")
                        .WithMany("Identifiers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
