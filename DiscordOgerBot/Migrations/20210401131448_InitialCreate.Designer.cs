﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DiscordOgerBot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DiscordOgerBot.Migrations
{
    [DbContext(typeof(OgerBotDataBaseContext))]
    [Migration("20210401131448_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DiscordOgerBot.Models.DiscordUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<List<string>>("ActiveGuildsId")
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("TimeSpendWorking")
                        .HasColumnType("interval");

                    b.Property<long>("TimesBotUsed")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("DiscordUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
