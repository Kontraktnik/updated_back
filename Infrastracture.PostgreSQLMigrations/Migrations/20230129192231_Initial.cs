using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastracture.PostgreSQLMigrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    RegionNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmyDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmyDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmyRanks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmyRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmyRegions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmyRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmyTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPositions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalSignAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttributeType = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Prefix = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalSignAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalSignBinaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalSignBinaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalSignInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotBefore = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NotAfter = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Iin = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true),
                    Bin = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    UserType = table.Column<string>(type: "text", nullable: true),
                    Issuer = table.Column<string>(type: "text", nullable: true),
                    SerialNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalSignInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverLicenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecretLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceYears",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    Max = table.Column<int>(type: "integer", nullable: false),
                    Min = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StepGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VtShes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtShes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankSalaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArmyRankId = table.Column<long>(type: "bigint", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RankSalaries_ArmyRanks_ArmyRankId",
                        column: x => x.ArmyRankId,
                        principalTable: "ArmyRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    AreaId = table.Column<long>(type: "bigint", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    IIN = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Verified = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    JobCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    SecretLevelId = table.Column<long>(type: "bigint", nullable: false),
                    ArmyTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryPositionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_ArmyTypes_ArmyTypeId",
                        column: x => x.ArmyTypeId,
                        principalTable: "ArmyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_CategoryPositions_CategoryPositionId",
                        column: x => x.CategoryPositionId,
                        principalTable: "CategoryPositions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_SecretLevels_SecretLevelId",
                        column: x => x.SecretLevelId,
                        principalTable: "SecretLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobYears",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceYearId = table.Column<long>(type: "bigint", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobYears_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobYears_ServiceYears_ServiceYearId",
                        column: x => x.ServiceYearId,
                        principalTable: "ServiceYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StepGroupId = table.Column<long>(type: "bigint", nullable: false),
                    TitleRu = table.Column<string>(type: "text", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: false),
                    TitleKz = table.Column<string>(type: "text", nullable: false),
                    RequestedRoleId = table.Column<long>(type: "bigint", nullable: false),
                    ConfirmedRoleId = table.Column<long>(type: "bigint", nullable: false),
                    IsFirst = table.Column<bool>(type: "boolean", nullable: false),
                    IsLast = table.Column<bool>(type: "boolean", nullable: false),
                    DayLimit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_Roles_ConfirmedRoleId",
                        column: x => x.ConfirmedRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Steps_Roles_RequestedRoleId",
                        column: x => x.RequestedRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Steps_StepGroups_StepGroupId",
                        column: x => x.StepGroupId,
                        principalTable: "StepGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Purpose = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    JobCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    AreaId = table.Column<long>(type: "bigint", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    ArmyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ArmyRegionId = table.Column<long>(type: "bigint", nullable: false),
                    SecretLevelId = table.Column<long>(type: "bigint", nullable: false),
                    QualificationId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    DescriptionRu = table.Column<string>(type: "text", nullable: false),
                    DescriptionEn = table.Column<string>(type: "text", nullable: true),
                    DescriptionKz = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_ArmyRegions_ArmyRegionId",
                        column: x => x.ArmyRegionId,
                        principalTable: "ArmyRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_ArmyTypes_ArmyTypeId",
                        column: x => x.ArmyTypeId,
                        principalTable: "ArmyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_SecretLevels_SecretLevelId",
                        column: x => x.SecretLevelId,
                        principalTable: "SecretLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StepOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StepId = table.Column<long>(type: "bigint", nullable: false),
                    PreviousStepId = table.Column<long>(type: "bigint", nullable: true),
                    NextStepId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepOrders_Steps_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "Steps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StepOrders_Steps_PreviousStepId",
                        column: x => x.PreviousStepId,
                        principalTable: "Steps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StepOrders_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IIN = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    BirthAreaId = table.Column<long>(type: "bigint", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    Home = table.Column<string>(type: "text", nullable: true),
                    Appartment = table.Column<string>(type: "text", nullable: true),
                    EducationId = table.Column<long>(type: "bigint", nullable: false),
                    Experienced = table.Column<bool>(type: "boolean", nullable: false),
                    Served = table.Column<bool>(type: "boolean", nullable: false),
                    ServedArmyNumber = table.Column<string>(type: "text", nullable: true),
                    PositionName = table.Column<string>(type: "text", nullable: true),
                    ArmyRankId = table.Column<long>(type: "bigint", nullable: true),
                    VTShServed = table.Column<bool>(type: "boolean", nullable: false),
                    VTShId = table.Column<long>(type: "bigint", nullable: true),
                    VTShYear = table.Column<string>(type: "text", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    ArmyNumber = table.Column<string>(type: "text", nullable: true),
                    ContractYear = table.Column<int>(type: "integer", nullable: false),
                    AreaId = table.Column<long>(type: "bigint", nullable: false),
                    VacancyId = table.Column<long>(type: "bigint", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    AutoBiography = table.Column<string>(type: "text", nullable: false),
                    EducationUrl = table.Column<string>(type: "text", nullable: false),
                    IncomePropertyUrl = table.Column<string>(type: "text", nullable: false),
                    EmploymentUrl = table.Column<string>(type: "text", nullable: false),
                    MillitaryUrl = table.Column<string>(type: "text", nullable: false),
                    SpecialCheckUrl = table.Column<string>(type: "text", nullable: false),
                    IdentityCardUrl = table.Column<string>(type: "text", nullable: false),
                    TuberUrl = table.Column<string>(type: "text", nullable: true),
                    DermatologUrl = table.Column<string>(type: "text", nullable: true),
                    PsychoNeurologicalUrl = table.Column<string>(type: "text", nullable: true),
                    NarcologicalUrl = table.Column<string>(type: "text", nullable: true),
                    Agreed = table.Column<bool>(type: "boolean", nullable: false),
                    SignKey = table.Column<string>(type: "text", nullable: false),
                    StepGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentStepId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    MedicalStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_Areas_BirthAreaId",
                        column: x => x.BirthAreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_ArmyRanks_ArmyRankId",
                        column: x => x.ArmyRankId,
                        principalTable: "ArmyRanks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surveys_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_MedicalStatuses_MedicalStatusId",
                        column: x => x.MedicalStatusId,
                        principalTable: "MedicalStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surveys_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_StepGroups_StepGroupId",
                        column: x => x.StepGroupId,
                        principalTable: "StepGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surveys_Steps_CurrentStepId",
                        column: x => x.CurrentStepId,
                        principalTable: "Steps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surveys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surveys_VtShes_VTShId",
                        column: x => x.VTShId,
                        principalTable: "VtShes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DigitalSigns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Signed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Verified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    InfoId = table.Column<long>(type: "bigint", nullable: true),
                    BinaryDataId = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: true),
                    StepId = table.Column<long>(type: "bigint", nullable: true),
                    WhoSignedId = table.Column<long>(type: "bigint", nullable: true),
                    SignedUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalSigns_DigitalSignBinaries_BinaryDataId",
                        column: x => x.BinaryDataId,
                        principalTable: "DigitalSignBinaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigitalSigns_DigitalSignInfos_InfoId",
                        column: x => x.InfoId,
                        principalTable: "DigitalSignInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DigitalSigns_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DigitalSigns_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DigitalSigns_Users_SignedUserId",
                        column: x => x.SignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StepGroupId = table.Column<long>(type: "bigint", nullable: false),
                    StepId = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    AreaId = table.Column<long>(type: "bigint", nullable: false),
                    RequestedUserId = table.Column<long>(type: "bigint", nullable: false),
                    RequestedUserIIN = table.Column<string>(type: "text", nullable: false),
                    RequestedStatus = table.Column<int>(type: "integer", nullable: false),
                    RequestedSIGN = table.Column<string>(type: "text", nullable: false),
                    ConfirmedUserId = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedUserIIN = table.Column<string>(type: "text", nullable: true),
                    ConfirmedStatus = table.Column<int>(type: "integer", nullable: true),
                    ConfirmedSIGN = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_StepGroups_StepGroupId",
                        column: x => x.StepGroupId,
                        principalTable: "StepGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Users_RequestedUserId",
                        column: x => x.RequestedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyDrivers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    DriverLicenseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyDrivers_DriverLicenses_DriverLicenseId",
                        column: x => x.DriverLicenseId,
                        principalTable: "DriverLicenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyDrivers_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyExecutors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    ExecutorId = table.Column<long>(type: "bigint", nullable: false),
                    DirectorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyExecutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyExecutors_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyExecutors_Users_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyExecutors_Users_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyRelatives",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RelativeId = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false),
                    Patronomic = table.Column<string>(type: "text", nullable: true),
                    IIN = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyRelatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyRelatives_Relatives_RelativeId",
                        column: x => x.RelativeId,
                        principalTable: "Relatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyRelatives_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfileId = table.Column<long>(type: "bigint", nullable: false),
                    File = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    isConfirmated = table.Column<bool>(type: "boolean", nullable: true),
                    isRequested = table.Column<bool>(type: "boolean", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileFiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileFiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DigitalSigns_BinaryDataId",
                table: "DigitalSigns",
                column: "BinaryDataId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalSigns_InfoId",
                table: "DigitalSigns",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalSigns_SignedUserId",
                table: "DigitalSigns",
                column: "SignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalSigns_StepId",
                table: "DigitalSigns",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalSigns_SurveyId",
                table: "DigitalSigns",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobYears_JobCategoryId",
                table: "JobYears",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobYears_ServiceYearId",
                table: "JobYears",
                column: "ServiceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNotifications_UserId",
                table: "PhoneNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ArmyTypeId",
                table: "Positions",
                column: "ArmyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CategoryPositionId",
                table: "Positions",
                column: "CategoryPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_JobCategoryId",
                table: "Positions",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SecretLevelId",
                table: "Positions",
                column: "SecretLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFiles_ProfileId",
                table: "ProfileFiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFiles_UserId",
                table: "ProfileFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AreaId",
                table: "Profiles",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ConfirmedUserId",
                table: "Profiles",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_RequestedUserId",
                table: "Profiles",
                column: "RequestedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_StepGroupId",
                table: "Profiles",
                column: "StepGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_StepId",
                table: "Profiles",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_SurveyId",
                table: "Profiles",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_RankSalaries_ArmyRankId",
                table: "RankSalaries",
                column: "ArmyRankId");

            migrationBuilder.CreateIndex(
                name: "IX_StepOrders_NextStepId",
                table: "StepOrders",
                column: "NextStepId");

            migrationBuilder.CreateIndex(
                name: "IX_StepOrders_PreviousStepId",
                table: "StepOrders",
                column: "PreviousStepId");

            migrationBuilder.CreateIndex(
                name: "IX_StepOrders_StepId",
                table: "StepOrders",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ConfirmedRoleId",
                table: "Steps",
                column: "ConfirmedRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RequestedRoleId",
                table: "Steps",
                column: "RequestedRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_StepGroupId",
                table: "Steps",
                column: "StepGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDrivers_DriverLicenseId",
                table: "SurveyDrivers",
                column: "DriverLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDrivers_SurveyId",
                table: "SurveyDrivers",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyExecutors_DirectorId",
                table: "SurveyExecutors",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyExecutors_ExecutorId",
                table: "SurveyExecutors",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyExecutors_SurveyId",
                table: "SurveyExecutors",
                column: "SurveyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyRelatives_RelativeId",
                table: "SurveyRelatives",
                column: "RelativeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyRelatives_SurveyId",
                table: "SurveyRelatives",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_AreaId",
                table: "Surveys",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ArmyRankId",
                table: "Surveys",
                column: "ArmyRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_BirthAreaId",
                table: "Surveys",
                column: "BirthAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CurrentStepId",
                table: "Surveys",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_EducationId",
                table: "Surveys",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_MedicalStatusId",
                table: "Surveys",
                column: "MedicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_PositionId",
                table: "Surveys",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_StepGroupId",
                table: "Surveys",
                column: "StepGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_VacancyId",
                table: "Surveys",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_VTShId",
                table: "Surveys",
                column: "VTShId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AreaId",
                table: "Users",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_AreaId",
                table: "Vacancies",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ArmyRegionId",
                table: "Vacancies",
                column: "ArmyRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ArmyTypeId",
                table: "Vacancies",
                column: "ArmyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_AuthorId",
                table: "Vacancies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_JobCategoryId",
                table: "Vacancies",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_PositionId",
                table: "Vacancies",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_QualificationId",
                table: "Vacancies",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_SecretLevelId",
                table: "Vacancies",
                column: "SecretLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmyDepartments");

            migrationBuilder.DropTable(
                name: "DigitalSignAttributes");

            migrationBuilder.DropTable(
                name: "DigitalSigns");

            migrationBuilder.DropTable(
                name: "JobYears");

            migrationBuilder.DropTable(
                name: "PhoneNotifications");

            migrationBuilder.DropTable(
                name: "ProfileFiles");

            migrationBuilder.DropTable(
                name: "RankSalaries");

            migrationBuilder.DropTable(
                name: "StepOrders");

            migrationBuilder.DropTable(
                name: "SurveyDrivers");

            migrationBuilder.DropTable(
                name: "SurveyExecutors");

            migrationBuilder.DropTable(
                name: "SurveyRelatives");

            migrationBuilder.DropTable(
                name: "DigitalSignBinaries");

            migrationBuilder.DropTable(
                name: "DigitalSignInfos");

            migrationBuilder.DropTable(
                name: "ServiceYears");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "DriverLicenses");

            migrationBuilder.DropTable(
                name: "Relatives");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "ArmyRanks");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "MedicalStatuses");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "VtShes");

            migrationBuilder.DropTable(
                name: "StepGroups");

            migrationBuilder.DropTable(
                name: "ArmyRegions");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ArmyTypes");

            migrationBuilder.DropTable(
                name: "CategoryPositions");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "SecretLevels");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
