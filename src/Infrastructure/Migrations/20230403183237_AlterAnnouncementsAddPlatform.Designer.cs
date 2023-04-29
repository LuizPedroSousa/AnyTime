﻿// <auto-generated />
using System;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(PersistenceDatabaseContext))]
    [Migration("20230403183237_AlterAnnouncementsAddPlatform")]
    partial class AlterAnnouncementsAddPlatform
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Announcement", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("authorid")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("platform")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tags")
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("url")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("authorid");

                    b.HasIndex("url")
                        .IsUnique();

                    b.ToTable("announcements");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Author", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("avatar_url")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Job", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("proposalid")
                        .HasColumnType("text");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("proposalid");

                    b.ToTable("jobs");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Proposal", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("announcementid")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<int>("price")
                        .HasColumnType("integer");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("announcementid");

                    b.ToTable("proposals");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Announcement", b =>
                {
                    b.HasOne("AnyTime.Core.Domain.Modules.Jobs.Author", "author")
                        .WithMany("announcements")
                        .HasForeignKey("authorid");

                    b.Navigation("author");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Job", b =>
                {
                    b.HasOne("AnyTime.Core.Domain.Modules.Jobs.Proposal", "proposal")
                        .WithMany()
                        .HasForeignKey("proposalid");

                    b.Navigation("proposal");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Proposal", b =>
                {
                    b.HasOne("AnyTime.Core.Domain.Modules.Jobs.Announcement", "announcement")
                        .WithMany()
                        .HasForeignKey("announcementid");

                    b.Navigation("announcement");
                });

            modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Author", b =>
                {
                    b.Navigation("announcements");
                });
#pragma warning restore 612, 618
        }
    }
}