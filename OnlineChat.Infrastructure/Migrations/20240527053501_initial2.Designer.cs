﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineChat.Infrastructure.DbContexts;

#nullable disable

namespace OnlineChat.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240527053501_initial2")]
    partial class initial2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineChat.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("boolean");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SeenAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("OnlineChat.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoName")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c6d6e9df-a709-404c-977c-bffca10922d4"),
                            CreatedAt = new DateTime(2024, 5, 27, 5, 35, 0, 642, DateTimeKind.Utc).AddTicks(151),
                            Email = "abduqodirovsarvar.2002@gmail.com",
                            FirstName = "SuperAdmin",
                            IsDeleted = false,
                            LastName = "SuperAdmin",
                            PasswordHash = "tyoclraLokrpjuQVLrZ0XVEwyxX/TlT7hVah4sGdM1m3eJgEO2qqj/qa7O9ayPHz1/qgUmfjfYIhLTVCRWVfpQ==",
                            Role = 3
                        },
                        new
                        {
                            Id = new Guid("f4770fa9-9fab-46b7-adcf-3e6dd964f7ff"),
                            CreatedAt = new DateTime(2024, 5, 27, 5, 35, 0, 645, DateTimeKind.Utc).AddTicks(7252),
                            Email = "user1@gmail.com",
                            FirstName = "User1 firstname",
                            IsDeleted = false,
                            LastName = "User1 lastname",
                            PasswordHash = "iXAw+vYbv5JgAJUS6jyaEVj/1cwgkS364EWBMV/6ENGBcDQfdQG2fpW3/nqYFXenz9gVAAdNC5hm8d2l/zhOjw==",
                            Role = 1
                        },
                        new
                        {
                            Id = new Guid("6ac565d4-b195-4914-89b4-c5e66250e216"),
                            CreatedAt = new DateTime(2024, 5, 27, 5, 35, 0, 649, DateTimeKind.Utc).AddTicks(3984),
                            Email = "user2@gmail.com",
                            FirstName = "User2 firstname",
                            IsDeleted = false,
                            LastName = "user2 lastname",
                            PasswordHash = "1tKDv2JuGuEDLVjB8GcCuDSTiym1EA2kwsfkahclfUhvO8FxnhtaqXHRozOrngn6ayXJQai7y0nRGaPpThND2A==",
                            Role = 1
                        });
                });

            modelBuilder.Entity("OnlineChat.Domain.Entities.Message", b =>
                {
                    b.HasOne("OnlineChat.Domain.Entities.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineChat.Domain.Entities.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("OnlineChat.Domain.Entities.User", b =>
                {
                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
