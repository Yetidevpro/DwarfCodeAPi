﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DwarfCodeData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250109081606_EliminatAnimeUser")]
    partial class EliminatAnimeUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DwarfCodeData.Models.Anime", b =>
                {
                    b.Property<int>("AnimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnimeId");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("DwarfCodeData.Models.AnimeScore", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("UserId", "AnimeId");

                    b.HasIndex("AnimeId");

                    b.ToTable("AnimeScores");
                });

            modelBuilder.Entity("DwarfCodeData.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pasword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DwarfCodeData.Models.AnimeScore", b =>
                {
                    b.HasOne("DwarfCodeData.Models.Anime", "Anime")
                        .WithMany("AnimeScores")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DwarfCodeData.Models.User", "User")
                        .WithMany("AnimeScores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DwarfCodeData.Models.Anime", b =>
                {
                    b.Navigation("AnimeScores");
                });

            modelBuilder.Entity("DwarfCodeData.Models.User", b =>
                {
                    b.Navigation("AnimeScores");
                });
#pragma warning restore 612, 618
        }
    }
}
