﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace ShopTaskBd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("category")
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<decimal>("price")
                        .HasColumnType("numeric");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.OwnsOne("Rating", "rating", b1 =>
                        {
                            b1.Property<int>("Productid")
                                .HasColumnType("integer");

                            b1.Property<int>("count")
                                .HasColumnType("integer");

                            b1.Property<int>("id")
                                .HasColumnType("integer");

                            b1.Property<decimal>("rate")
                                .HasColumnType("numeric");

                            b1.HasKey("Productid");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("Productid");
                        });

                    b.Navigation("rating");
                });
#pragma warning restore 612, 618
        }
    }
}