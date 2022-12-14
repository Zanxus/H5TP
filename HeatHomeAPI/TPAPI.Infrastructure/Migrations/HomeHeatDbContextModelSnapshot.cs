// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPAPI.Infrastructure;

#nullable disable

namespace TPAPI.Infrastructure.Migrations
{
    [DbContext(typeof(HomeHeatDbContext))]
    partial class HomeHeatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TPAPI.Domain.Entities.GeoCoordinate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.HardwareType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HardwareTypes");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.HardwareUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HardwareTypeId")
                        .HasColumnType("int");

                    b.Property<string>("MacAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HardwareTypeId");

                    b.ToTable("HardwareUnits");

                    b.HasDiscriminator<string>("Discriminator").HasValue("HardwareUnit");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.Temperature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HeatingUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("HeatingUnitId");

                    b.ToTable("Temperature");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.HeatingUnit", b =>
                {
                    b.HasBaseType("TPAPI.Domain.Entities.HardwareUnit");

                    b.Property<Guid?>("CoordinateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsOn")
                        .HasColumnType("bit");

                    b.Property<float>("TargetTemperature")
                        .HasColumnType("real");

                    b.HasIndex("CoordinateId");

                    b.HasDiscriminator().HasValue("HeatingUnit");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.HardwareUnit", b =>
                {
                    b.HasOne("TPAPI.Domain.Entities.HardwareType", "HardwareType")
                        .WithMany()
                        .HasForeignKey("HardwareTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HardwareType");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.Temperature", b =>
                {
                    b.HasOne("TPAPI.Domain.Entities.HeatingUnit", null)
                        .WithMany("Temperatures")
                        .HasForeignKey("HeatingUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.HeatingUnit", b =>
                {
                    b.HasOne("TPAPI.Domain.Entities.GeoCoordinate", "Coordinate")
                        .WithMany()
                        .HasForeignKey("CoordinateId");

                    b.Navigation("Coordinate");
                });

            modelBuilder.Entity("TPAPI.Domain.Entities.HeatingUnit", b =>
                {
                    b.Navigation("Temperatures");
                });
#pragma warning restore 612, 618
        }
    }
}
