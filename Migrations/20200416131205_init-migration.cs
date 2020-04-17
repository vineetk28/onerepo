using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MantiScanServices.Migrations
{
    public partial class initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MantiScanRole",
                schema: "public",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantiScanRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                schema: "public",
                columns: table => new
                {
                    ModuleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModuleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "public",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    LogoFile = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "RolePrivilege",
                schema: "public",
                columns: table => new
                {
                    RolePrivilegeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PrivilegeId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivilege", x => x.RolePrivilegeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleDetail",
                schema: "public",
                columns: table => new
                {
                    RoleDetailId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Add = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    Edit = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    View = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDetail", x => x.RoleDetailId);
                    table.ForeignKey(
                        name: "FK_RoleDetail_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "public",
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleDetail_MantiScanRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "MantiScanRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AdminId = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                schema: "public",
                columns: table => new
                {
                    IncidentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IncidentCauseId = table.Column<int>(nullable: false),
                    IncidentDate = table.Column<DateTime>(nullable: false),
                    IncidentName = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    OtherDamage = table.Column<string>(nullable: true),
                    TowerId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Incident_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incident_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlateForm",
                schema: "public",
                columns: table => new
                {
                    PlateFormId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AreaCode = table.Column<string>(nullable: true),
                    AuthorityNumber = table.Column<string>(nullable: true),
                    AuthorityStatus = table.Column<string>(nullable: true),
                    AuthorityType = table.Column<string>(nullable: true),
                    BlockNumber = table.Column<string>(nullable: true),
                    BusAscName = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    InstallDate = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    StructureName = table.Column<string>(nullable: true),
                    StructureNumber = table.Column<string>(nullable: true),
                    StructureTypeCode = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateForm", x => x.PlateFormId);
                    table.ForeignKey(
                        name: "FK_PlateForm_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlateForm_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tower",
                schema: "public",
                columns: table => new
                {
                    TowerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TowerName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tower", x => x.TowerId);
                    table.ForeignKey(
                        name: "FK_Tower_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleDetail",
                schema: "public",
                columns: table => new
                {
                    UserRoleDetailId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleDetail", x => x.UserRoleDetailId);
                    table.ForeignKey(
                        name: "FK_UserRoleDetail_MantiScanRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "MantiScanRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleDetail_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OilSpillReport",
                schema: "public",
                columns: table => new
                {
                    OilSpillReportId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    APIgravity = table.Column<string>(nullable: true),
                    ActionNotes = table.Column<string>(nullable: true),
                    ActualSpill = table.Column<bool>(nullable: false),
                    Agent = table.Column<string>(nullable: true),
                    AirTemp = table.Column<int>(nullable: false),
                    AreaBlock = table.Column<string>(nullable: true),
                    BarelyVisible = table.Column<double>(nullable: false),
                    BrigthlyCovered = table.Column<double>(nullable: false),
                    CallSign = table.Column<string>(nullable: true),
                    CaptainAddress = table.Column<string>(nullable: true),
                    CaptainName = table.Column<string>(nullable: true),
                    CaptainPhone = table.Column<string>(nullable: true),
                    CardNo = table.Column<string>(nullable: true),
                    Ceiling = table.Column<double>(nullable: false),
                    CeilingUnitId = table.Column<string>(nullable: true),
                    CurrentDirection = table.Column<double>(nullable: false),
                    CurrentVelocity = table.Column<string>(nullable: true),
                    Damage = table.Column<int>(nullable: false),
                    Dark = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Discription = table.Column<string>(nullable: true),
                    Distance = table.Column<int>(nullable: false),
                    Drill = table.Column<bool>(nullable: false),
                    DtIncidentOccurred = table.Column<DateTime>(nullable: false),
                    DtIncidentQiIc = table.Column<DateTime>(nullable: false),
                    Dull = table.Column<double>(nullable: false),
                    Evacuated = table.Column<int>(nullable: false),
                    Facility = table.Column<string>(nullable: true),
                    Fatalities = table.Column<int>(nullable: false),
                    Flag = table.Column<string>(nullable: true),
                    FormPreparedBy = table.Column<string>(nullable: true),
                    Injuries = table.Column<int>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Length = table.Column<int>(nullable: false),
                    LengthUnitId = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true),
                    NRC = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NearestCity = table.Column<string>(nullable: true),
                    Ocsg = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    PercentageOfSlick = table.Column<double>(nullable: false),
                    PlateFormId = table.Column<int>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    QtyUnitId = table.Column<string>(nullable: true),
                    Quanity = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    ReportName = table.Column<string>(nullable: true),
                    ReportingCompany = table.Column<string>(nullable: true),
                    ReportingPhone = table.Column<string>(nullable: true),
                    Seas = table.Column<double>(nullable: false),
                    SeasUnitId = table.Column<string>(nullable: true),
                    Silvery = table.Column<double>(nullable: false),
                    SlightlyColored = table.Column<double>(nullable: false),
                    SourceContinous = table.Column<double>(nullable: false),
                    SourceContinousUnitId = table.Column<string>(nullable: true),
                    SourceSecuredAt = table.Column<DateTime>(nullable: false),
                    SuspectedCompany = table.Column<string>(nullable: true),
                    SuspectedPhone = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    VesselName = table.Column<string>(nullable: true),
                    VesselOwner = table.Column<string>(nullable: true),
                    Visibility = table.Column<double>(nullable: false),
                    VisibilityUnitId = table.Column<string>(nullable: true),
                    WaterTemp = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    WidthUnitId = table.Column<string>(nullable: true),
                    WindDirection = table.Column<double>(nullable: false),
                    WindVelocity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilSpillReport", x => x.OilSpillReportId);
                    table.ForeignKey(
                        name: "FK_OilSpillReport_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OilSpillReport_PlateForm_PlateFormId",
                        column: x => x.PlateFormId,
                        principalSchema: "public",
                        principalTable: "PlateForm",
                        principalColumn: "PlateFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OilSpillReport_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationAgency",
                schema: "public",
                columns: table => new
                {
                    NotificationAgencyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Agency = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IncidentNo = table.Column<string>(nullable: true),
                    OilSpillReportId = table.Column<int>(nullable: false),
                    ReportedBy = table.Column<string>(nullable: true),
                    ReportedTo = table.Column<string>(nullable: true),
                    TimeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationAgency", x => x.NotificationAgencyId);
                    table.ForeignKey(
                        name: "FK_NotificationAgency_OilSpillReport_OilSpillReportId",
                        column: x => x.OilSpillReportId,
                        principalSchema: "public",
                        principalTable: "OilSpillReport",
                        principalColumn: "OilSpillReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationCompany",
                schema: "public",
                columns: table => new
                {
                    NotificationCompanyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    OilSpillReportId = table.Column<int>(nullable: false),
                    ReportedBy = table.Column<string>(nullable: true),
                    ReportedToName = table.Column<string>(nullable: true),
                    ReportedToPosition = table.Column<string>(nullable: true),
                    TimeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationCompany", x => x.NotificationCompanyId);
                    table.ForeignKey(
                        name: "FK_NotificationCompany_OilSpillReport_OilSpillReportId",
                        column: x => x.OilSpillReportId,
                        principalSchema: "public",
                        principalTable: "OilSpillReport",
                        principalColumn: "OilSpillReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "public",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "public",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganizationId",
                schema: "public",
                table: "AspNetUsers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_OrganizationId",
                schema: "public",
                table: "Incident",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_UserId",
                schema: "public",
                table: "Incident",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAgency_OilSpillReportId",
                schema: "public",
                table: "NotificationAgency",
                column: "OilSpillReportId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationCompany_OilSpillReportId",
                schema: "public",
                table: "NotificationCompany",
                column: "OilSpillReportId");

            migrationBuilder.CreateIndex(
                name: "IX_OilSpillReport_OrganizationId",
                schema: "public",
                table: "OilSpillReport",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OilSpillReport_PlateFormId",
                schema: "public",
                table: "OilSpillReport",
                column: "PlateFormId");

            migrationBuilder.CreateIndex(
                name: "IX_OilSpillReport_UserId",
                schema: "public",
                table: "OilSpillReport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlateForm_OrganizationId",
                schema: "public",
                table: "PlateForm",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlateForm_UserId",
                schema: "public",
                table: "PlateForm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDetail_ModuleId",
                schema: "public",
                table: "RoleDetail",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDetail_RoleId",
                schema: "public",
                table: "RoleDetail",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tower_UserId",
                schema: "public",
                table: "Tower",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleDetail_RoleId",
                schema: "public",
                table: "UserRoleDetail",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleDetail_UserId",
                schema: "public",
                table: "UserRoleDetail",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Incident",
                schema: "public");

            migrationBuilder.DropTable(
                name: "NotificationAgency",
                schema: "public");

            migrationBuilder.DropTable(
                name: "NotificationCompany",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RoleDetail",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RolePrivilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Tower",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserRoleDetail",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OilSpillReport",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Module",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MantiScanRole",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PlateForm",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "public");
        }
    }
}
