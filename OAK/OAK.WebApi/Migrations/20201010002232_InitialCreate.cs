using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace OAK.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    PropertyValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EPartTypeFrnGrpTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SequenceNumber = table.Column<int>(nullable: false),
                    EstatePartTypeId = table.Column<int>(nullable: false),
                    FurnitureGroupTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPartTypeFrnGrpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstatePartFurnitures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    EstatePartId = table.Column<int>(nullable: false),
                    FurnitureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatePartFurnitures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationKey",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationKey", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PropertyJson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    JsonObject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyJson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentStatusTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentStatusTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStatusTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStatusTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    IsoCode2 = table.Column<string>(maxLength: 10, nullable: false),
                    IsoCode3 = table.Column<string>(maxLength: 10, nullable: false),
                    CultureName = table.Column<string>(maxLength: 10, nullable: false),
                    CountryCode = table.Column<string>(maxLength: 10, nullable: false),
                    CountryCodeLength = table.Column<int>(maxLength: 10, nullable: false),
                    AreaCodes = table.Column<string>(nullable: true),
                    PhoneAreaCodeMinLength = table.Column<int>(nullable: false),
                    PhoneAreaCodeMaxLength = table.Column<int>(nullable: false),
                    PhoneSubscriberNumberLengthMin = table.Column<int>(nullable: false),
                    PhoneSubscriberNumberLengthMax = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.UniqueConstraint("AK_Country_IsoCode2", x => x.IsoCode2);
                    table.ForeignKey(
                        name: "FK_Country_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerType_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandStatusTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandStatusTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureCalculationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureCalculationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FurnitureCalculationTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureGroupTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureGroupTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FurnitureGroupTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    LocalizationKeyKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CultureName = table.Column<string>(maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_LocalizationKey_LocalizationKeyKey",
                        column: x => x.LocalizationKeyKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    LocalizationKeyKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationStatusTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationStatusTypes_LocalizationKey_LocalizationKeyKey",
                        column: x => x.LocalizationKeyKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    LocalizationKeyKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: false),
                    CurrencyShortCode = table.Column<string>(nullable: false),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    WorkerHourRate = table.Column<decimal>(nullable: false),
                    DriverHourRate = table.Column<decimal>(nullable: false),
                    DailyRoomRate = table.Column<decimal>(nullable: false),
                    PricePerKilometer = table.Column<decimal>(nullable: false),
                    SprinterDailyRentPrice = table.Column<decimal>(nullable: false),
                    LKWDailyRentPrice = table.Column<decimal>(nullable: false),
                    OilPrice = table.Column<decimal>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyParameters_LocalizationKey_LocalizationKeyKey",
                        column: x => x.LocalizationKeyKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyParameters_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstatePartTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsOuterPart = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatePartTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstatePartTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstatePartTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstateTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlatTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlatTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenericAddressTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericAddressTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericAddressTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GenericAddressTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    LocalizationKeyKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    SprinterAvgVelocity = table.Column<decimal>(nullable: false),
                    SprinterAvgOilConsumption = table.Column<decimal>(nullable: false),
                    SprinterMaxVolume = table.Column<decimal>(nullable: false),
                    SprinterDailyKM = table.Column<decimal>(nullable: false),
                    LKWAvgVelocity = table.Column<decimal>(nullable: false),
                    LKWAvgOilConsumption = table.Column<decimal>(nullable: false),
                    LKWMaxVolume = table.Column<decimal>(nullable: false),
                    LKWDailyKM = table.Column<decimal>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_LocalizationKey_LocalizationKeyKey",
                        column: x => x.LocalizationKeyKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parameters_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    LocalizationKeyKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationTypes_LocalizationKey_LocalizationKeyKey",
                        column: x => x.LocalizationKeyKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    CompanyStatusTypeId = table.Column<int>(nullable: false),
                    GenericAddressId = table.Column<int>(nullable: false),
                    TaxNumber = table.Column<string>(nullable: false),
                    Guid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyStatusTypes_CompanyStatusTypeId",
                        column: x => x.CompanyStatusTypeId,
                        principalTable: "CompanyStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCodeData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsoCountryCode = table.Column<string>(fixedLength: true, maxLength: 2, nullable: false),
                    PostCode = table.Column<string>(maxLength: 20, nullable: false),
                    PlaceName = table.Column<string>(maxLength: 180, nullable: false),
                    AdminName1 = table.Column<string>(maxLength: 100, nullable: true),
                    AdminCode1 = table.Column<string>(maxLength: 20, nullable: true),
                    AdminName2 = table.Column<string>(maxLength: 100, nullable: true),
                    AdminCode2 = table.Column<string>(maxLength: 20, nullable: true),
                    AdminName3 = table.Column<string>(maxLength: 100, nullable: true),
                    AdminCode3 = table.Column<string>(maxLength: 20, nullable: true),
                    Lattitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Accuracy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCodeData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCodeData_Country_IsoCountryCode",
                        column: x => x.IsoCountryCode,
                        principalTable: "Country",
                        principalColumn: "IsoCode2",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportedPostCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    PostCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedPostCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportedPostCodes_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Username = table.Column<string>(maxLength: 150, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    LoginAttempts = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    EmailActivationDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastPasswordChangeDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    IsEmailActivated = table.Column<bool>(nullable: false),
                    TwoFactorAuthenticationEnabled = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCompanyOwner = table.Column<bool>(nullable: false, defaultValue: false),
                    ActivationCode = table.Column<int>(nullable: false),
                    FcmToken = table.Column<string>(nullable: true),
                    TempVerificationString = table.Column<string>(nullable: true),
                    VerificationValidityTime = table.Column<DateTime>(nullable: true),
                    CustomerTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_CustomerType_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FurnitureCalculationTypeId = table.Column<int>(nullable: false),
                    FurnitureGroupTypeId = table.Column<int>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false),
                    Assemblable = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AssembleCost = table.Column<decimal>(nullable: true),
                    DisassembleCost = table.Column<decimal>(nullable: true),
                    FlatRate = table.Column<decimal>(nullable: true),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FurnitureTypes_FurnitureCalculationTypes_FurnitureCalculati~",
                        column: x => x.FurnitureCalculationTypeId,
                        principalTable: "FurnitureCalculationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureTypes_FurnitureGroupTypes_FurnitureGroupTypeId",
                        column: x => x.FurnitureGroupTypeId,
                        principalTable: "FurnitureGroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureTypes_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FurnitureTypes_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationText",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    CultureName = table.Column<string>(maxLength: 10, nullable: false),
                    LocalKey = table.Column<string>(maxLength: 100, nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationText", x => x.Id);
                    table.UniqueConstraint("AK_LocalizationText_LanguageId_LocalKey", x => new { x.LanguageId, x.LocalKey });
                    table.ForeignKey(
                        name: "FK_LocalizationText_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalizationText_LocalizationKey_LocalKey",
                        column: x => x.LocalKey,
                        principalTable: "LocalizationKey",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    EstateTypeId = table.Column<int>(nullable: false),
                    PropertyValues = table.Column<string>(nullable: true),
                    NumberOfFloors = table.Column<int>(nullable: false),
                    NumberOfRooms = table.Column<int>(nullable: false),
                    TotalSquareMeter = table.Column<int>(nullable: false),
                    ElevatorAvailability = table.Column<int>(nullable: false),
                    WaitingPermission = table.Column<bool>(nullable: false),
                    FurnitureMontage = table.Column<bool>(nullable: false),
                    KitchenMontage = table.Column<bool>(nullable: false),
                    PackingService = table.Column<bool>(nullable: false),
                    HasLoft = table.Column<bool>(nullable: false),
                    HasGardenGarage = table.Column<bool>(nullable: false),
                    HasCellar = table.Column<bool>(nullable: false),
                    LoftFloor = table.Column<int>(nullable: false),
                    GardenGarageFloor = table.Column<int>(nullable: false),
                    CellarFloor = table.Column<int>(nullable: false),
                    LoftSqMt = table.Column<int>(nullable: false),
                    GardenGarageSqMt = table.Column<int>(nullable: false),
                    CellarSqMt = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estates_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateTypeEPartTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SequenceNumber = table.Column<int>(nullable: false),
                    EstateTypeId = table.Column<int>(nullable: false),
                    EstatePartTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateTypeEPartTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateTypeEPartTypes_EstatePartTypes_EstatePartTypeId",
                        column: x => x.EstatePartTypeId,
                        principalTable: "EstatePartTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateTypeEPartTypes_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenericAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    Town = table.Column<string>(maxLength: 255, nullable: true),
                    Street = table.Column<string>(maxLength: 255, nullable: false),
                    HouseNumber = table.Column<string>(nullable: false),
                    PostCode = table.Column<string>(maxLength: 20, nullable: false),
                    PlaceName = table.Column<string>(nullable: false),
                    GenericAddressTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericAddresses_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GenericAddresses_GenericAddressTypes_GenericAddressTypeId",
                        column: x => x.GenericAddressTypeId,
                        principalTable: "GenericAddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyOfficialDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOfficialDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOfficialDocuments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyOfficialDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPostCodeData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CountryId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    PostCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPostCodeData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPostCodeData_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPostCodeData_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPublicDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPublicDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPublicDocuments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPublicDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    CommentTypeId = table.Column<int>(nullable: false),
                    CommentStatusTypeId = table.Column<int>(nullable: false),
                    PropertyValues = table.Column<int>(nullable: false),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    ParentCommentId = table.Column<int>(nullable: true),
                    CommentNote = table.Column<string>(nullable: true),
                    CommentLike = table.Column<int>(nullable: false),
                    CommentDislike = table.Column<int>(nullable: false),
                    CommentUsefull = table.Column<int>(nullable: false),
                    CommentScore = table.Column<int>(nullable: false),
                    PropertyJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_PropertyJson_PropertyJsonId",
                        column: x => x.PropertyJsonId,
                        principalTable: "PropertyJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DemandTypeId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    DemandEstimatedValue = table.Column<decimal>(nullable: false),
                    DemandMaxOfferedValue = table.Column<decimal>(nullable: false),
                    DemandMinOfferedValue = table.Column<decimal>(nullable: false),
                    DemandAverageOfferedValue = table.Column<decimal>(nullable: false),
                    DemandNumberOfOffers = table.Column<int>(nullable: false),
                    AcceptedOfferId = table.Column<int>(nullable: true),
                    DemandContractValue = table.Column<decimal>(nullable: false),
                    DemandVAT = table.Column<decimal>(nullable: false),
                    DemandGrossValue = table.Column<decimal>(nullable: false),
                    DemandCommission = table.Column<decimal>(nullable: false),
                    DemandStatusTypeId = table.Column<int>(nullable: false),
                    DemandOwnerId = table.Column<int>(nullable: true),
                    PropertyValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demands_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demands_DemandStatusTypes_DemandStatusTypeId",
                        column: x => x.DemandStatusTypeId,
                        principalTable: "DemandStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demands_DemandTypes_DemandTypeId",
                        column: x => x.DemandTypeId,
                        principalTable: "DemandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstatesFlat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    FlatTypeId = table.Column<int>(nullable: false),
                    EstateId = table.Column<int>(nullable: false),
                    FloorOfEstate = table.Column<int>(nullable: false),
                    NumberOfRooms = table.Column<int>(nullable: false),
                    SqMtOfFloor = table.Column<int>(nullable: false),
                    TargetFloor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatesFlat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstatesFlat_Estates_EstateId",
                        column: x => x.EstateId,
                        principalTable: "Estates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstatesFlat_FlatTypes_FlatTypeId",
                        column: x => x.FlatTypeId,
                        principalTable: "FlatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDemandServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    DemandStatusTypeId = table.Column<int>(nullable: false),
                    DemandId = table.Column<int>(nullable: false),
                    OfferAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDemandServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDemandServices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDemandServices_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandChats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DemandId = table.Column<int>(nullable: false),
                    FromAccountId = table.Column<int>(nullable: false),
                    ToAccountId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandChats_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandChats_Account_ToAccountId",
                        column: x => x.ToAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    DemandId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandComments_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DemandId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    CountryPhoneCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PreferredCulture = table.Column<string>(nullable: true),
                    AlternativeCulture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandOwners_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DemandId = table.Column<int>(nullable: false),
                    TransportationTypeId = table.Column<int>(nullable: false),
                    TransportationStatusTypeId = table.Column<int>(nullable: false),
                    NumberOfPeople = table.Column<int>(nullable: false),
                    InitialTransportationDate = table.Column<DateTime>(nullable: false),
                    FinalTransportationDate = table.Column<DateTime>(nullable: false),
                    DateFlexibility = table.Column<bool>(nullable: false),
                    TransportationDistanceMin = table.Column<int>(nullable: false),
                    TransportationDistanceMax = table.Column<int>(nullable: false),
                    TransportationEstimatedValue = table.Column<int>(nullable: false),
                    TransportationMaxOfferedValue = table.Column<int>(nullable: false),
                    TransportationMinOfferedValue = table.Column<int>(nullable: false),
                    TransportationAverageOfferedValue = table.Column<int>(nullable: false),
                    TransportationNumberOfOffers = table.Column<int>(nullable: false),
                    TransportationContractValue = table.Column<decimal>(nullable: false),
                    TransportationVAT = table.Column<decimal>(nullable: false),
                    TransportationGrossValue = table.Column<decimal>(nullable: false),
                    TransportationCommission = table.Column<decimal>(nullable: false),
                    IsFixedPrice = table.Column<bool>(nullable: false),
                    PropertyJsonValues = table.Column<string>(nullable: true),
                    FromEstateId = table.Column<int>(nullable: false),
                    ToEstateId = table.Column<int>(nullable: false),
                    FromAddressId = table.Column<int>(nullable: false),
                    ToAddressId = table.Column<int>(nullable: false),
                    ExtraInfoLanguageId = table.Column<int>(nullable: true),
                    ExtraInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportations_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_GenericAddresses_FromAddressId",
                        column: x => x.FromAddressId,
                        principalTable: "GenericAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_Estates_FromEstateId",
                        column: x => x.FromEstateId,
                        principalTable: "Estates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_GenericAddresses_ToAddressId",
                        column: x => x.ToAddressId,
                        principalTable: "GenericAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_Estates_ToEstateId",
                        column: x => x.ToEstateId,
                        principalTable: "Estates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    EstatesFlatId = table.Column<int>(nullable: false),
                    EstatePartTypeId = table.Column<int>(nullable: false),
                    PropertyValues = table.Column<string>(nullable: true),
                    TargetFloor = table.Column<int>(nullable: true),
                    EstatePartFurnitureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateParts_EstatePartFurnitures_EstatePartFurnitureId",
                        column: x => x.EstatePartFurnitureId,
                        principalTable: "EstatePartFurnitures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstateParts_EstatePartTypes_EstatePartTypeId",
                        column: x => x.EstatePartTypeId,
                        principalTable: "EstatePartTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateParts_EstatesFlat_EstatesFlatId",
                        column: x => x.EstatesFlatId,
                        principalTable: "EstatesFlat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportationComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    TransportationDemandId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false),
                    DemandId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportationComments_Transportations_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Transportations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    TransportationId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportationDocuments_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Furnitures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    FurnitureTypeId = table.Column<int>(nullable: false),
                    EstatePartId = table.Column<int>(nullable: false),
                    PropertyValues = table.Column<string>(nullable: true),
                    NumberOfFurnitures = table.Column<int>(nullable: false),
                    DoAssemble = table.Column<bool>(nullable: false),
                    TargetFloor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnitures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Furnitures_EstateParts_EstatePartId",
                        column: x => x.EstatePartId,
                        principalTable: "EstateParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CustomerTypeId",
                table: "Account",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_PhoneNumber",
                table: "Account",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Account_TempVerificationString",
                table: "Account",
                column: "TempVerificationString",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AccountId",
                table: "Comments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PropertyJsonId",
                table: "Comments",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentStatusTypes_LocalKey",
                table: "CommentStatusTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTypes_LocalKey",
                table: "CommentTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTypes_PropertyJsonId",
                table: "CommentTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyStatusTypeId",
                table: "Companies",
                column: "CompanyStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDemandServices_CompanyId",
                table: "CompanyDemandServices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDemandServices_DemandId",
                table: "CompanyDemandServices",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOfficialDocuments_CompanyId",
                table: "CompanyOfficialDocuments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOfficialDocuments_DocumentId",
                table: "CompanyOfficialDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostCodeData_CompanyId",
                table: "CompanyPostCodeData",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostCodeData_CountryId",
                table: "CompanyPostCodeData",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPublicDocuments_CompanyId",
                table: "CompanyPublicDocuments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPublicDocuments_DocumentId",
                table: "CompanyPublicDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStatusTypes_LocalKey",
                table: "CompanyStatusTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_Country_LocalKey",
                table: "Country",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyParameters_LocalizationKeyKey",
                table: "CurrencyParameters",
                column: "LocalizationKeyKey");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyParameters_PropertyJsonId",
                table: "CurrencyParameters",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerType_LocalKey",
                table: "CustomerType",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_DemandChats_DemandId",
                table: "DemandChats",
                column: "DemandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DemandChats_ToAccountId",
                table: "DemandChats",
                column: "ToAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DemandComments_CommentId",
                table: "DemandComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandComments_DemandId",
                table: "DemandComments",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandOwners_DemandId",
                table: "DemandOwners",
                column: "DemandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demands_AccountId",
                table: "Demands",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_DemandStatusTypeId",
                table: "Demands",
                column: "DemandStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_DemandTypeId",
                table: "Demands",
                column: "DemandTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandStatusTypes_LocalKey",
                table: "DemandStatusTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_DemandTypes_LocalKey",
                table: "DemandTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_DemandTypes_PropertyJsonId",
                table: "DemandTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_LocalKey",
                table: "DocumentTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_PropertyJsonId",
                table: "DocumentTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateParts_EstatePartFurnitureId",
                table: "EstateParts",
                column: "EstatePartFurnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateParts_EstatePartTypeId",
                table: "EstateParts",
                column: "EstatePartTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateParts_EstatesFlatId",
                table: "EstateParts",
                column: "EstatesFlatId");

            migrationBuilder.CreateIndex(
                name: "IX_EstatePartTypes_LocalKey",
                table: "EstatePartTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_EstatePartTypes_PropertyJsonId",
                table: "EstatePartTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_Estates_EstateTypeId",
                table: "Estates",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EstatesFlat_EstateId",
                table: "EstatesFlat",
                column: "EstateId");

            migrationBuilder.CreateIndex(
                name: "IX_EstatesFlat_FlatTypeId",
                table: "EstatesFlat",
                column: "FlatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypeEPartTypes_EstatePartTypeId",
                table: "EstateTypeEPartTypes",
                column: "EstatePartTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypeEPartTypes_EstateTypeId",
                table: "EstateTypeEPartTypes",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypes_LocalKey",
                table: "EstateTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypes_PropertyJsonId",
                table: "EstateTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatTypes_LocalKey",
                table: "FlatTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_FlatTypes_PropertyJsonId",
                table: "FlatTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureCalculationTypes_LocalKey",
                table: "FurnitureCalculationTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureGroupTypes_LocalKey",
                table: "FurnitureGroupTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_Furnitures_EstatePartId",
                table: "Furnitures",
                column: "EstatePartId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureTypes_FurnitureCalculationTypeId",
                table: "FurnitureTypes",
                column: "FurnitureCalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureTypes_FurnitureGroupTypeId",
                table: "FurnitureTypes",
                column: "FurnitureGroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureTypes_LocalKey",
                table: "FurnitureTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureTypes_PropertyJsonId",
                table: "FurnitureTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericAddresses_CountryId",
                table: "GenericAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericAddresses_GenericAddressTypeId",
                table: "GenericAddresses",
                column: "GenericAddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericAddressTypes_LocalKey",
                table: "GenericAddressTypes",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_GenericAddressTypes_PropertyJsonId",
                table: "GenericAddressTypes",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LocalizationKeyKey",
                table: "Languages",
                column: "LocalizationKeyKey");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationText_LocalKey",
                table: "LocalizationText",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_LocalizationKeyKey",
                table: "Parameters",
                column: "LocalizationKeyKey");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_PropertyJsonId",
                table: "Parameters",
                column: "PropertyJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCodeData_IsoCountryCode",
                table: "PostCodeData",
                column: "IsoCountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Role_LocalKey",
                table: "Role",
                column: "LocalKey");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedPostCodes_CountryId",
                table: "SupportedPostCodes",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationComments_CommentId",
                table: "TransportationComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationComments_DemandId",
                table: "TransportationComments",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationDocuments_DocumentId",
                table: "TransportationDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationDocuments_TransportationId",
                table: "TransportationDocuments",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_DemandId",
                table: "Transportations",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_FromAddressId",
                table: "Transportations",
                column: "FromAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_FromEstateId",
                table: "Transportations",
                column: "FromEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_ToAddressId",
                table: "Transportations",
                column: "ToAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_ToEstateId",
                table: "Transportations",
                column: "ToEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationStatusTypes_LocalizationKeyKey",
                table: "TransportationStatusTypes",
                column: "LocalizationKeyKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationTypes_LocalizationKeyKey",
                table: "TransportationTypes",
                column: "LocalizationKeyKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationTypes_PropertyJsonId",
                table: "TransportationTypes",
                column: "PropertyJsonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "CommentStatusTypes");

            migrationBuilder.DropTable(
                name: "CommentTypes");

            migrationBuilder.DropTable(
                name: "CompanyDemandServices");

            migrationBuilder.DropTable(
                name: "CompanyOfficialDocuments");

            migrationBuilder.DropTable(
                name: "CompanyPostCodeData");

            migrationBuilder.DropTable(
                name: "CompanyPublicDocuments");

            migrationBuilder.DropTable(
                name: "CurrencyParameters");

            migrationBuilder.DropTable(
                name: "DemandChats");

            migrationBuilder.DropTable(
                name: "DemandComments");

            migrationBuilder.DropTable(
                name: "DemandOwners");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "EPartTypeFrnGrpTypes");

            migrationBuilder.DropTable(
                name: "EstateTypeEPartTypes");

            migrationBuilder.DropTable(
                name: "Furnitures");

            migrationBuilder.DropTable(
                name: "FurnitureTypes");

            migrationBuilder.DropTable(
                name: "LocalizationText");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "PostCodeData");

            migrationBuilder.DropTable(
                name: "SupportedPostCodes");

            migrationBuilder.DropTable(
                name: "TransportationComments");

            migrationBuilder.DropTable(
                name: "TransportationDocuments");

            migrationBuilder.DropTable(
                name: "TransportationStatusTypes");

            migrationBuilder.DropTable(
                name: "TransportationTypes");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "EstateParts");

            migrationBuilder.DropTable(
                name: "FurnitureCalculationTypes");

            migrationBuilder.DropTable(
                name: "FurnitureGroupTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "CompanyStatusTypes");

            migrationBuilder.DropTable(
                name: "EstatePartFurnitures");

            migrationBuilder.DropTable(
                name: "EstatePartTypes");

            migrationBuilder.DropTable(
                name: "EstatesFlat");

            migrationBuilder.DropTable(
                name: "Demands");

            migrationBuilder.DropTable(
                name: "GenericAddresses");

            migrationBuilder.DropTable(
                name: "Estates");

            migrationBuilder.DropTable(
                name: "FlatTypes");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DemandStatusTypes");

            migrationBuilder.DropTable(
                name: "DemandTypes");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "GenericAddressTypes");

            migrationBuilder.DropTable(
                name: "EstateTypes");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "PropertyJson");

            migrationBuilder.DropTable(
                name: "LocalizationKey");
        }
    }
}
