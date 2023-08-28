﻿// <auto-generated />
using System;
using ContactAppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactAppCore.Migrations
{
    [DbContext(typeof(ContactContext))]
    [Migration("20230825173704_AddProfileCategory")]
    partial class AddProfileCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactAppCore.Data.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowBeta")
                        .HasColumnType("bit");

                    b.Property<bool>("AllowPeople")
                        .HasColumnType("bit");

                    b.Property<int>("AreaType")
                        .HasColumnType("int");

                    b.Property<string>("Audience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InternalOnly")
                        .HasColumnType("bit");

                    b.Property<int>("InternalOrder")
                        .HasColumnType("int");

                    b.Property<string>("InternalUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeopleRefreshUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PictureHeight")
                        .HasColumnType("int");

                    b.Property<int>("PictureWidth")
                        .HasColumnType("int");

                    b.Property<string>("SearchTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.EmployeeActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeProfileId")
                        .HasColumnType("int");

                    b.Property<int>("InternalOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearEnded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearStarted")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeProfileId");

                    b.ToTable("EmployeeActivities");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.EmployeeLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployeeProfileId")
                        .HasColumnType("int");

                    b.Property<int>("InternalOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeProfileId");

                    b.ToTable("EmployeeLinks");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.EmployeeProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CVUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPhoneHidden")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfficeInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredNameLast")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredPronouns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrimaryProfile")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeProfiles");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.ExternalLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExternalLinks");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.JobProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeProfileId")
                        .HasColumnType("int");

                    b.Property<int>("InternalOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeProfileId");

                    b.HasIndex("OfficeId");

                    b.ToTable("JobProfiles");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.JobProfileTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("JobProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("JobProfileId");

                    b.ToTable("JobProfileTags");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Audience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Building")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildingCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CovidSupport")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursFridayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursFridayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HoursIncludeHolidayMessage")
                        .HasColumnType("bit");

                    b.Property<string>("HoursMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursMondayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursMondayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursSaturdayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursSaturdayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursSundayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursSundayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursThursdayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursThursdayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursTuesdayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursTuesdayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursWednesdayEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursWednesdayStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InternalOnly")
                        .HasColumnType("bit");

                    b.Property<int>("InternalOrder")
                        .HasColumnType("int");

                    b.Property<string>("InternalUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeType")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Room")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<bool>("CanEditAllPeopleInUnit")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFullAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OfficeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("OfficeId");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            CanEditAllPeopleInUnit = false,
                            IsActive = true,
                            IsFullAdmin = true,
                            LastUpdated = new DateTime(2023, 8, 25, 12, 37, 3, 236, DateTimeKind.Local).AddTicks(1508),
                            Title = "jonker@illinois.edu"
                        },
                        new
                        {
                            Id = -2,
                            CanEditAllPeopleInUnit = false,
                            IsActive = true,
                            IsFullAdmin = true,
                            LastUpdated = new DateTime(2023, 8, 25, 12, 37, 3, 241, DateTimeKind.Local).AddTicks(8892),
                            Title = "rbwatson@illinois.edu"
                        });
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.EmployeeActivity", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.EmployeeProfile", "EmployeeProfile")
                        .WithMany("EmployeeActivities")
                        .HasForeignKey("EmployeeProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeProfile");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.EmployeeLink", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.EmployeeProfile", null)
                        .WithMany("EmployeeLinks")
                        .HasForeignKey("EmployeeProfileId");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.JobProfile", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.EmployeeProfile", "EmployeeProfile")
                        .WithMany("Jobs")
                        .HasForeignKey("EmployeeProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactAppCore.Data.Models.Office", "Office")
                        .WithMany("JobProfiles")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeProfile");

                    b.Navigation("Office");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.JobProfileTag", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.JobProfile", null)
                        .WithMany("Tags")
                        .HasForeignKey("JobProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Office", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.Area", "Area")
                        .WithMany("Offices")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Person", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.Area", null)
                        .WithMany("Admins")
                        .HasForeignKey("AreaId");

                    b.HasOne("ContactAppCore.Data.Models.Office", null)
                        .WithMany("Admins")
                        .HasForeignKey("OfficeId");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Tag", b =>
                {
                    b.HasOne("ContactAppCore.Data.Models.Office", null)
                        .WithMany("Tags")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Area", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Offices");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.EmployeeProfile", b =>
                {
                    b.Navigation("EmployeeActivities");

                    b.Navigation("EmployeeLinks");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.JobProfile", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("ContactAppCore.Data.Models.Office", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("JobProfiles");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
