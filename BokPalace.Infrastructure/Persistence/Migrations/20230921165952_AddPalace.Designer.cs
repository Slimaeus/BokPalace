﻿// <auto-generated />
using BokPalace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BokPalace.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230921165952_AddPalace")]
    partial class AddPalace
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("BokPalace.Domain.Palaces.Palace", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Palaces");
                });

            modelBuilder.Entity("BokPalace.Domain.Rooms.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PalaceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PalaceId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BokPalace.Domain.Rooms.Room", b =>
                {
                    b.HasOne("BokPalace.Domain.Palaces.Palace", "Palace")
                        .WithMany("Rooms")
                        .HasForeignKey("PalaceId");

                    b.Navigation("Palace");
                });

            modelBuilder.Entity("BokPalace.Domain.Palaces.Palace", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
