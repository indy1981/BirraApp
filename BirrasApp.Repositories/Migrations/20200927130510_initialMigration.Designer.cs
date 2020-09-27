﻿// <auto-generated />
using System;
using BirrasApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BirrasApp.Repositories.Migrations
{
    [DbContext(typeof(BirrasAppContext))]
    [Migration("20200927130510_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BirrasApp.Repositories.Models.Meetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("MeetupDate");

                    b.Property<string>("Name");

                    b.Property<string>("Subject");

                    b.Property<float>("Temperature");

                    b.HasKey("Id");

                    b.ToTable("Meetups");
                });

            modelBuilder.Entity("BirrasApp.Repositories.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("UserRol");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "aosanchez@gmail.com",
                            PasswordHash = new byte[] { 13, 166, 151, 18, 151, 113, 126, 244, 219, 39, 249, 97, 208, 10, 97, 231, 180, 104, 108, 137, 41, 147, 161, 204, 148, 4, 218, 198, 78, 46, 197, 103, 178, 155, 13, 16, 222, 67, 189, 26, 62, 0, 200, 58, 25, 225, 46, 23, 169, 128, 167, 89, 127, 118, 5, 175, 214, 75, 173, 167, 84, 16, 13, 52 },
                            PasswordSalt = new byte[] { 248, 227, 237, 23, 22, 162, 181, 232, 245, 153, 51, 255, 190, 165, 205, 247, 190, 245, 151, 96, 87, 137, 253, 110, 93, 16, 158, 144, 118, 17, 231, 64, 164, 171, 49, 112, 23, 114, 108, 85, 194, 252, 109, 118, 75, 199, 142, 83, 159, 212, 19, 34, 75, 46, 239, 151, 180, 155, 118, 42, 243, 91, 235, 128, 231, 72, 155, 61, 215, 168, 28, 2, 240, 66, 201, 70, 182, 31, 23, 246, 123, 155, 147, 26, 51, 255, 2, 49, 90, 209, 232, 58, 134, 182, 151, 202, 220, 252, 97, 32, 33, 144, 112, 151, 130, 106, 245, 124, 44, 203, 192, 219, 154, 17, 182, 137, 135, 108, 169, 222, 38, 79, 176, 138, 41, 11, 163, 89 },
                            UserRol = 0,
                            Username = "userAdmin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user1@santander.com",
                            PasswordHash = new byte[] { 107, 169, 203, 47, 101, 60, 184, 38, 178, 153, 200, 241, 247, 148, 243, 227, 234, 40, 181, 186, 55, 208, 147, 116, 152, 176, 26, 163, 17, 240, 118, 120, 33, 27, 202, 115, 67, 129, 43, 174, 63, 198, 8, 187, 164, 208, 82, 101, 211, 225, 58, 248, 132, 140, 191, 84, 54, 93, 64, 75, 243, 74, 96, 161 },
                            PasswordSalt = new byte[] { 125, 251, 250, 205, 96, 43, 50, 192, 252, 95, 173, 249, 230, 84, 166, 188, 192, 56, 30, 59, 144, 68, 253, 131, 27, 47, 11, 7, 177, 194, 199, 40, 228, 17, 98, 218, 10, 72, 143, 209, 222, 196, 229, 10, 51, 222, 205, 163, 85, 205, 174, 219, 112, 138, 207, 138, 17, 37, 211, 196, 120, 56, 248, 77, 203, 96, 53, 1, 135, 64, 39, 1, 36, 245, 231, 99, 188, 69, 113, 13, 213, 205, 8, 19, 129, 68, 226, 189, 48, 82, 108, 24, 131, 32, 186, 208, 137, 202, 59, 252, 120, 62, 71, 134, 236, 191, 121, 168, 254, 128, 195, 128, 205, 196, 43, 215, 222, 137, 218, 37, 123, 202, 118, 97, 125, 38, 85, 217 },
                            UserRol = 1,
                            Username = "user1"
                        },
                        new
                        {
                            Id = 3,
                            Email = "user2@santander.com",
                            PasswordHash = new byte[] { 61, 84, 164, 106, 210, 235, 243, 75, 234, 246, 114, 144, 171, 62, 33, 54, 99, 39, 209, 218, 139, 30, 196, 194, 220, 248, 184, 217, 220, 136, 253, 193, 15, 4, 147, 51, 63, 130, 250, 54, 20, 186, 143, 193, 79, 111, 11, 230, 24, 230, 9, 232, 153, 138, 209, 98, 92, 219, 109, 187, 198, 38, 99, 228 },
                            PasswordSalt = new byte[] { 115, 7, 52, 76, 112, 68, 64, 182, 148, 240, 66, 39, 5, 90, 234, 8, 140, 17, 25, 141, 88, 153, 68, 179, 172, 216, 8, 210, 37, 226, 90, 238, 122, 243, 177, 6, 7, 17, 5, 188, 75, 111, 197, 98, 33, 8, 246, 33, 195, 60, 252, 157, 198, 34, 52, 117, 118, 221, 192, 155, 47, 140, 228, 41, 181, 211, 169, 173, 99, 36, 142, 160, 247, 229, 67, 161, 104, 124, 151, 102, 96, 33, 234, 116, 132, 140, 70, 154, 150, 241, 41, 208, 188, 85, 228, 81, 82, 215, 86, 36, 100, 105, 31, 7, 71, 134, 202, 119, 149, 111, 97, 214, 242, 105, 108, 124, 22, 226, 53, 131, 92, 237, 84, 48, 251, 184, 48, 57 },
                            UserRol = 1,
                            Username = "user2"
                        });
                });

            modelBuilder.Entity("BirrasApp.Repositories.Models.UserMeetUp", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("MeetupId");

                    b.Property<bool>("CheckedIn");

                    b.HasKey("UserId", "MeetupId");

                    b.HasIndex("MeetupId");

                    b.ToTable("UserMeetUps");
                });

            modelBuilder.Entity("BirrasApp.Repositories.Models.UserRequestToMeetUp", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("MeetupId");

                    b.HasKey("UserId", "MeetupId");

                    b.HasIndex("MeetupId");

                    b.ToTable("UserRequestToMeetUp");
                });

            modelBuilder.Entity("BirrasApp.Repositories.Models.UserMeetUp", b =>
                {
                    b.HasOne("BirrasApp.Repositories.Models.Meetup", "Meetup")
                        .WithMany("UserMeetUps")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BirrasApp.Repositories.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BirrasApp.Repositories.Models.UserRequestToMeetUp", b =>
                {
                    b.HasOne("BirrasApp.Repositories.Models.Meetup", "Meetup")
                        .WithMany("Requests")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BirrasApp.Repositories.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}