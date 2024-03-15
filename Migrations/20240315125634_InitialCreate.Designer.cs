﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.DataModel;

#nullable disable

namespace ToDoList.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20240315125634_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ToDoList.DataModel.Todo", b =>
                {
                    b.Property<int>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("INTEGER");

                    b.HasKey("TodoId");

                    b.ToTable("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
