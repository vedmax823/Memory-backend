﻿// <auto-generated />
using System;
using Memory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Memory.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240827173843_AddUserTurn")]
    partial class AddUserTurn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameUser", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("GamesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GameUser");
                });

            modelBuilder.Entity("Memory.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Col")
                        .HasColumnType("integer");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Row")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Memory.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CardsCount")
                        .HasColumnType("integer");

                    b.Property<int>("Cols")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxPlayersCount")
                        .HasColumnType("integer");

                    b.Property<int>("Rows")
                        .HasColumnType("integer");

                    b.Property<Guid>("TurnUser")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Memory.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserNumber"));

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameUser", b =>
                {
                    b.HasOne("Memory.Entities.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Memory.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Memory.Entities.Card", b =>
                {
                    b.HasOne("Memory.Entities.Game", null)
                        .WithMany("Field")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Memory.Entities.Game", b =>
                {
                    b.Navigation("Field");
                });
#pragma warning restore 612, 618
        }
    }
}
