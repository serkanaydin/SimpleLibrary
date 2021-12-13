﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleLibrary.Persistence;

namespace SimpleLibrary.Persistence.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20211210081225_bookTypeExtension")]
    partial class bookTypeExtension
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("SimpleLibrary.Domain.Book.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<string>("Author")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("SimpleLibrary.Domain.BookType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<int?>("BookId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Type")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookType");
                });

            modelBuilder.Entity("SimpleLibrary.Domain.BookType", b =>
                {
                    b.HasOne("SimpleLibrary.Domain.Book.Book", null)
                        .WithMany("BookTypes")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("SimpleLibrary.Domain.Book.Book", b =>
                {
                    b.Navigation("BookTypes");
                });
#pragma warning restore 612, 618
        }
    }
}