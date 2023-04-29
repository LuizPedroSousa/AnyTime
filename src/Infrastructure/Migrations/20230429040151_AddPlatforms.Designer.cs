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
  [Migration("20230429040151_AddPlatforms")]
  partial class AddPlatforms
  {
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "7.0.4")
          .HasAnnotation("Relational:MaxIdentifierLength", 63);

      NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Announcements.Announcement", b =>
          {
            b.Property<string>("id")
                      .HasColumnType("text");

            b.Property<string>("authorid")
                      .HasColumnType("text");

            b.Property<DateTime>("createdAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<string>("description")
                      .HasColumnType("text");

            b.Property<string>("platformid")
                      .HasColumnType("text");

            b.Property<DateTime>("publishedAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<int>("status")
                      .HasColumnType("integer");

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

            b.HasIndex("platformid");

            b.HasIndex("url")
                      .IsUnique();

            b.ToTable("announcements");
          });

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Announcements.Author", b =>
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

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Proposals.Proposal", b =>
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

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Platforms.Platform", b =>
          {
            b.Property<string>("id")
                      .HasColumnType("text");

            b.Property<DateTime>("createdAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<string>("icon_url")
                      .HasColumnType("text");

            b.Property<string>("name")
                      .HasColumnType("text");

            b.Property<string>("thumb_url")
                      .HasColumnType("text");

            b.Property<DateTime>("updatedAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<string>("url")
                      .HasColumnType("text");

            b.HasKey("id");

            b.HasIndex("url")
                      .IsUnique();

            b.ToTable("platforms");

            b.HasData(
                      new
                      {
                        id = "162b482f-a5c9-4ce4-8921-bc0b39eb1fe9",
                        createdAt = new DateTime(2023, 4, 29, 4, 1, 51, 564, DateTimeKind.Utc).AddTicks(6448),
                        icon_url = "https://icones.pro/wp-content/uploads/2021/03/symbole-de-plume-bleue.png",
                        name = "freelancerbr",
                        thumb_url = "https://freelancer.com.br/images/main-page/find-section-bg.jpg",
                        updatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        url = "https://freelancer.com.br"
                      },
                      new
                      {
                        id = "69f1f410-a320-417b-9d44-762ac5970810",
                        createdAt = new DateTime(2023, 4, 29, 4, 1, 51, 564, DateTimeKind.Utc).AddTicks(6467),
                        icon_url = "https://play-lh.googleusercontent.com/yNBbWLb1_Te7UELDjx4OjKpkYcW79nklxeB-xJThgaN0hiXpIDs2hHt3P9XISi4fEjwH=w240-h480-rw",
                        name = "99freelas",
                        thumb_url = "https://s3-sa-east-1.amazonaws.com/projetos-artes/fullsize%2F2014%2F04%2F07%2F16%2FWDL-Logo-39095_16236_061350792_1651536830.jpg",
                        updatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        url = "https://www.99freelas.com.br"
                      });
          });

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Announcements.Announcement", b =>
          {
            b.HasOne("AnyTime.Core.Domain.Modules.Announcements.Author", "author")
                      .WithMany("announcements")
                      .HasForeignKey("authorid");

            b.HasOne("AnyTime.Core.Domain.Modules.Platforms.Platform", "platform")
                      .WithMany()
                      .HasForeignKey("platformid");

            b.Navigation("author");

            b.Navigation("platform");
          });

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Jobs.Job", b =>
          {
            b.HasOne("AnyTime.Core.Domain.Modules.Proposals.Proposal", "proposal")
                      .WithMany()
                      .HasForeignKey("proposalid");

            b.Navigation("proposal");
          });

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Proposals.Proposal", b =>
          {
            b.HasOne("AnyTime.Core.Domain.Modules.Announcements.Announcement", "announcement")
                      .WithMany()
                      .HasForeignKey("announcementid");

            b.Navigation("announcement");
          });

      modelBuilder.Entity("AnyTime.Core.Domain.Modules.Announcements.Author", b =>
          {
            b.Navigation("announcements");
          });
#pragma warning restore 612, 618
    }
  }
}
