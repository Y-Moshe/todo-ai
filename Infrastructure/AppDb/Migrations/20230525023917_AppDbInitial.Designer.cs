﻿// <auto-generated />
using Infrastructure.AppDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.AppDb.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230525023917_AppDbInitial")]
    partial class AppDbInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");
#pragma warning restore 612, 618
        }
    }
}
