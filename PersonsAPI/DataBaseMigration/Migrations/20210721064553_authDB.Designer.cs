﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBaseMigration.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20210721064553_authDB")]
    partial class authDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ClinicDataLayerPersonDataLayer", b =>
                {
                    b.Property<int>("ClinicsId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonsId")
                        .HasColumnType("integer");

                    b.HasKey("ClinicsId", "PersonsId");

                    b.HasIndex("PersonsId");

                    b.ToTable("ClinicDataLayerPersonDataLayer");
                });

            modelBuilder.Entity("DataLayer.Abstraction.Entityes.ClinicDataLayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clinics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Eye Clinic1"
                        },
                        new
                        {
                            Id = 2,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Eye Clinic2"
                        },
                        new
                        {
                            Id = 3,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Eye Clinic3"
                        },
                        new
                        {
                            Id = 4,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Eye Clinic4"
                        },
                        new
                        {
                            Id = 5,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Tooth Clinic1"
                        },
                        new
                        {
                            Id = 6,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Tooth Clinic2"
                        },
                        new
                        {
                            Id = 7,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Tooth Clinic3"
                        },
                        new
                        {
                            Id = 8,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "Tooth Clinic4"
                        },
                        new
                        {
                            Id = 9,
                            Adress = "132123 444ывадтфывафыва 1",
                            Name = "ass Clinic1 "
                        },
                        new
                        {
                            Id = 10,
                            Adress = "132123 333ывадтфывафыва 1",
                            Name = "ass Clinic2"
                        },
                        new
                        {
                            Id = 11,
                            Adress = "132123 444ывадтфывафыва 1",
                            Name = "ass Clinic3"
                        },
                        new
                        {
                            Id = 12,
                            Adress = "132123 555ывадтфывафыва 1",
                            Name = "ass Clinic4"
                        });
                });

            modelBuilder.Entity("DataLayer.Abstraction.Entityes.PersonDataLayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 42,
                            Company = "Quisque Ac Libero LLP",
                            Email = "ligula@necluctus.edu",
                            FirstName = "Veda",
                            LastName = "Richmond"
                        },
                        new
                        {
                            Id = 2,
                            Age = 31,
                            Company = "Nulla Facilisi Foundation",
                            Email = "feugiat.metus@penatibuset.org",
                            FirstName = "Demetria",
                            LastName = "Andrews"
                        },
                        new
                        {
                            Id = 3,
                            Age = 63,
                            Company = "Et Associates",
                            Email = "neque.Sed.eget@non.co.uk",
                            FirstName = "Byron",
                            LastName = "Holmes"
                        },
                        new
                        {
                            Id = 4,
                            Age = 23,
                            Company = "Vel Institute",
                            Email = "egestas.ligula@ultricesDuisvolutpat.ca",
                            FirstName = "Alexander",
                            LastName = "Cummings"
                        },
                        new
                        {
                            Id = 5,
                            Age = 64,
                            Company = "Eu Nibh Vulputate Company",
                            Email = "justo.nec.ante@nonummyFusce.ca",
                            FirstName = "Melinda",
                            LastName = "Miles"
                        },
                        new
                        {
                            Id = 6,
                            Age = 35,
                            Company = "Nec Diam Incorporated",
                            Email = "iaculis@afeugiat.edu",
                            FirstName = "Dustin",
                            LastName = "Beck"
                        },
                        new
                        {
                            Id = 7,
                            Age = 22,
                            Company = "Enim Corp.",
                            Email = "ipsum@vulputatelacus.co.uk",
                            FirstName = "Ralph",
                            LastName = "Maddox"
                        },
                        new
                        {
                            Id = 8,
                            Age = 57,
                            Company = "Sodales At Velit Corp.",
                            Email = "lectus.a.sollicitudin@nuncQuisque.com",
                            FirstName = "Levi",
                            LastName = "Zamora"
                        },
                        new
                        {
                            Id = 9,
                            Age = 37,
                            Company = "Id Mollis Nec LLC",
                            Email = "Phasellus@Craspellentesque.org",
                            FirstName = "Driscoll",
                            LastName = "Estrada"
                        },
                        new
                        {
                            Id = 10,
                            Age = 59,
                            Company = "Donec Tincidunt Donec Industries",
                            Email = "lacus.Mauris@semper.ca",
                            FirstName = "Hiram",
                            LastName = "Mejia"
                        },
                        new
                        {
                            Id = 11,
                            Age = 65,
                            Company = "Lectus Justo Ltd",
                            Email = "Integer.vitae.nibh@nibh.co.uk",
                            FirstName = "Mason",
                            LastName = "Jefferson"
                        },
                        new
                        {
                            Id = 12,
                            Age = 52,
                            Company = "Tristique Ac Ltd",
                            Email = "id@faucibusleoin.net",
                            FirstName = "Nigel",
                            LastName = "Rich"
                        },
                        new
                        {
                            Id = 13,
                            Age = 58,
                            Company = "Lacus Varius Et Associates",
                            Email = "enim@ultricesDuisvolutpat.edu",
                            FirstName = "Tarik",
                            LastName = "Hughes"
                        },
                        new
                        {
                            Id = 14,
                            Age = 29,
                            Company = "Rhoncus Id Corporation",
                            Email = "Curabitur.ut.odio@anteMaecenasmi.co.uk",
                            FirstName = "Wallace",
                            LastName = "Gross"
                        },
                        new
                        {
                            Id = 15,
                            Age = 59,
                            Company = "Vivamus Corporation",
                            Email = "magna.nec.quam@lobortis.net",
                            FirstName = "Arden",
                            LastName = "Rivers"
                        },
                        new
                        {
                            Id = 16,
                            Age = 54,
                            Company = "Imperdiet Dictum Magna Foundation",
                            Email = "faucibus.Morbi.vehicula@ipsumdolor.edu",
                            FirstName = "Vincent",
                            LastName = "Fox"
                        },
                        new
                        {
                            Id = 17,
                            Age = 27,
                            Company = "Mattis Foundation",
                            Email = "ac@scelerisquesedsapien.org",
                            FirstName = "Aphrodite",
                            LastName = "Randolph"
                        },
                        new
                        {
                            Id = 18,
                            Age = 25,
                            Company = "Rutrum Non Hendrerit Consulting",
                            Email = "montes@scelerisque.edu",
                            FirstName = "Alisa",
                            LastName = "Riggs"
                        },
                        new
                        {
                            Id = 19,
                            Age = 61,
                            Company = "Ut LLC",
                            Email = "velit.Quisque.varius@aliquetmolestie.net",
                            FirstName = "Jaime",
                            LastName = "Lott"
                        },
                        new
                        {
                            Id = 20,
                            Age = 61,
                            Company = "Curabitur Sed Tortor Ltd",
                            Email = "arcu.eu.odio@congue.ca",
                            FirstName = "Jamalia",
                            LastName = "Buchanan"
                        },
                        new
                        {
                            Id = 21,
                            Age = 43,
                            Company = "In Inc.",
                            Email = "Integer.sem.elit@bibendumsedest.net",
                            FirstName = "Raya",
                            LastName = "Mckenzie"
                        },
                        new
                        {
                            Id = 22,
                            Age = 48,
                            Company = "Nec Foundation",
                            Email = "Cras.eget.nisi@Vestibulum.edu",
                            FirstName = "Dante",
                            LastName = "Blackwell"
                        },
                        new
                        {
                            Id = 23,
                            Age = 60,
                            Company = "Augue Scelerisque Institute",
                            Email = "facilisis@doloregestas.co.uk",
                            FirstName = "Kato",
                            LastName = "Dickson"
                        },
                        new
                        {
                            Id = 24,
                            Age = 29,
                            Company = "Dui Augue Eu Limited",
                            Email = "tincidunt@eget.edu",
                            FirstName = "Clio",
                            LastName = "Shaffer"
                        },
                        new
                        {
                            Id = 25,
                            Age = 26,
                            Company = "Enim Incorporated",
                            Email = "magna@felisegetvarius.net",
                            FirstName = "Hamilton",
                            LastName = "Kidd"
                        },
                        new
                        {
                            Id = 26,
                            Age = 48,
                            Company = "Interdum Inc.",
                            Email = "Suspendisse.eleifend@Crasdolor.com",
                            FirstName = "Kerry",
                            LastName = "Oneil"
                        },
                        new
                        {
                            Id = 27,
                            Age = 34,
                            Company = "Facilisis Eget Ipsum Inc.",
                            Email = "elit.pretium.et@malesuadafamesac.com",
                            FirstName = "Mohammad",
                            LastName = "Thompson"
                        },
                        new
                        {
                            Id = 28,
                            Age = 35,
                            Company = "Iaculis Quis Consulting",
                            Email = "felis@conguea.org",
                            FirstName = "Vernon",
                            LastName = "Cardenas"
                        },
                        new
                        {
                            Id = 29,
                            Age = 63,
                            Company = "Integer Urna Institute",
                            Email = "Proin@feugiatnecdiam.org",
                            FirstName = "Murphy",
                            LastName = "Weaver"
                        },
                        new
                        {
                            Id = 30,
                            Age = 39,
                            Company = "Orci Industries",
                            Email = "facilisis.facilisis.magna@loremutaliquam.net",
                            FirstName = "Xena",
                            LastName = "Hart"
                        },
                        new
                        {
                            Id = 31,
                            Age = 30,
                            Company = "Ante Foundation",
                            Email = "Proin.ultrices.Duis@lacuspede.com",
                            FirstName = "Ivor",
                            LastName = "Lara"
                        },
                        new
                        {
                            Id = 32,
                            Age = 53,
                            Company = "Eget Industries",
                            Email = "et.magnis@Sed.edu",
                            FirstName = "Dana",
                            LastName = "Merritt"
                        },
                        new
                        {
                            Id = 33,
                            Age = 45,
                            Company = "Lorem Vehicula Et Foundation",
                            Email = "elit.Nulla@magna.edu",
                            FirstName = "Brielle",
                            LastName = "Woodward"
                        },
                        new
                        {
                            Id = 34,
                            Age = 49,
                            Company = "Magna Suspendisse Consulting",
                            Email = "et@nislsem.co.uk",
                            FirstName = "Hasad",
                            LastName = "Duran"
                        },
                        new
                        {
                            Id = 35,
                            Age = 32,
                            Company = "Eros Institute",
                            Email = "Proin.sed.turpis@imperdiet.co.uk",
                            FirstName = "Quamar",
                            LastName = "Moses"
                        },
                        new
                        {
                            Id = 36,
                            Age = 58,
                            Company = "Aenean Massa Consulting",
                            Email = "nisl.sem.consequat@idnunc.co.uk",
                            FirstName = "Scarlet",
                            LastName = "Barlow"
                        },
                        new
                        {
                            Id = 37,
                            Age = 47,
                            Company = "Mauris Associates",
                            Email = "urna@mauris.org",
                            FirstName = "Courtney",
                            LastName = "Foley"
                        },
                        new
                        {
                            Id = 38,
                            Age = 40,
                            Company = "Id Nunc Interdum LLC",
                            Email = "Cras@Nullam.org",
                            FirstName = "Kennedy",
                            LastName = "Shields"
                        },
                        new
                        {
                            Id = 39,
                            Age = 30,
                            Company = "Pellentesque Ultricies Associates",
                            Email = "metus.sit@lorem.ca",
                            FirstName = "Eve",
                            LastName = "Maynard"
                        },
                        new
                        {
                            Id = 40,
                            Age = 24,
                            Company = "Nulla Tincidunt Industries",
                            Email = "Nullam@pretium.ca",
                            FirstName = "Debra",
                            LastName = "Ellis"
                        },
                        new
                        {
                            Id = 41,
                            Age = 48,
                            Company = "Id Consulting",
                            Email = "ornare@at.net",
                            FirstName = "Vivian",
                            LastName = "Mcguire"
                        },
                        new
                        {
                            Id = 42,
                            Age = 48,
                            Company = "Netus Et Inc.",
                            Email = "tempor.augue@purusNullam.com",
                            FirstName = "Jason",
                            LastName = "Mckinney"
                        },
                        new
                        {
                            Id = 43,
                            Age = 61,
                            Company = "Hendrerit Institute",
                            Email = "fringilla@Proinsed.co.uk",
                            FirstName = "Patrick",
                            LastName = "Small"
                        },
                        new
                        {
                            Id = 44,
                            Age = 55,
                            Company = "Penatibus Corp.",
                            Email = "scelerisque.scelerisque@velit.org",
                            FirstName = "Drew",
                            LastName = "Travis"
                        },
                        new
                        {
                            Id = 45,
                            Age = 41,
                            Company = "Quis Diam Pellentesque PC",
                            Email = "Suspendisse@aliquet.net",
                            FirstName = "Burke",
                            LastName = "Miller"
                        },
                        new
                        {
                            Id = 46,
                            Age = 55,
                            Company = "Lorem Ipsum Dolor Corp.",
                            Email = "Class.aptent.taciti@mauris.edu",
                            FirstName = "Ralph",
                            LastName = "Medina"
                        },
                        new
                        {
                            Id = 47,
                            Age = 33,
                            Company = "Euismod Est Arcu Institute",
                            Email = "at.velit.Cras@aptenttacitisociosqu.net",
                            FirstName = "Alana",
                            LastName = "Madden"
                        },
                        new
                        {
                            Id = 48,
                            Age = 37,
                            Company = "Purus PC",
                            Email = "magna.Duis@Phasellus.org",
                            FirstName = "Salvador",
                            LastName = "Cohen"
                        },
                        new
                        {
                            Id = 49,
                            Age = 56,
                            Company = "Lectus Justo Incorporated",
                            Email = "adipiscing.Mauris.molestie@liberoduinec.ca",
                            FirstName = "Jenette",
                            LastName = "Dejesus"
                        },
                        new
                        {
                            Id = 50,
                            Age = 24,
                            Company = "Imperdiet Dictum LLP",
                            Email = "massa.Vestibulum@lectuspede.ca",
                            FirstName = "Ramona",
                            LastName = "Gilliam"
                        });
                });

            modelBuilder.Entity("DataLayer.Abstraction.Entityes.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("DataLayer.Abstraction.Entityes.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "test",
                            Password = "testtest"
                        },
                        new
                        {
                            Id = 2,
                            Login = "user2",
                            Password = "12334bd4b"
                        },
                        new
                        {
                            Id = 3,
                            Login = "user2",
                            Password = "w34f5v4w5b6"
                        });
                });

            modelBuilder.Entity("ClinicDataLayerPersonDataLayer", b =>
                {
                    b.HasOne("DataLayer.Abstraction.Entityes.ClinicDataLayer", null)
                        .WithMany()
                        .HasForeignKey("ClinicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Abstraction.Entityes.PersonDataLayer", null)
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Abstraction.Entityes.RefreshToken", b =>
                {
                    b.HasOne("DataLayer.Abstraction.Entityes.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Abstraction.Entityes.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
