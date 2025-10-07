using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Markey.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occupation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Occupation_OccupationId",
                        column: x => x.OccupationId,
                        principalTable: "Occupation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Logins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Occupation",
                columns: new[] { "Id", "CreationDate", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("06aeafd6-3ea5-444e-a251-bb48ae89c9dc"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7459), null, "Periodista" },
                    { new Guid("24c41869-6627-460e-9609-3c5095c6e6ef"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7456), null, "Panadero" },
                    { new Guid("28134207-94c2-4b40-808a-0a98aa0bc3e4"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7461), null, "Músico" },
                    { new Guid("2a454763-42c8-4fa2-9333-13a714d15649"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7447), null, "Ingeniero" },
                    { new Guid("360ceb30-e678-4415-aeea-e3e01a4bbb25"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7464), null, "Bombero" },
                    { new Guid("4a8480dc-c700-489d-b407-dad996d492bf"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7448), null, "Abogado" },
                    { new Guid("4b75d907-631e-46d3-8218-bd77c922bfb5"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7446), null, "Médico" },
                    { new Guid("4f9121b8-9522-47c8-a587-ee8d6f5f079f"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7449), null, "Profesor" },
                    { new Guid("63a7628e-fafa-42f7-8e5b-9428ac7f14cb"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7452), null, "Carpintero" },
                    { new Guid("698434d2-213d-4aae-a616-bb798931741a"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7469), null, "Psicólogo" },
                    { new Guid("6fbbf7f5-3e99-4809-99ee-d534ca36aa41"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7451), null, "Arquitecto" },
                    { new Guid("8527d68a-1aa1-4e3c-8fdc-167ec525f536"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7450), null, "Enfermero" },
                    { new Guid("87a5a235-e7f1-4b62-aee1-075cc97b23ac"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7467), null, "Dentista" },
                    { new Guid("87a8b72c-d172-450f-8443-205326e92d89"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7453), null, "Electricista" },
                    { new Guid("ac492574-ef15-4e97-a8cc-d8ebef21f58b"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7460), null, "Actor" },
                    { new Guid("b081b1cf-7efc-4504-a5dd-48e390c7b113"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7463), null, "Policía" },
                    { new Guid("b486c08a-4918-452d-a2a8-4214297a7d5c"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7468), null, "Veterinario" },
                    { new Guid("b6d2be6d-65ce-448f-82cb-da80af7895f5"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7455), null, "Chef" },
                    { new Guid("cc1454ca-ee7f-4ea9-9e4d-2fd1a740200d"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7454), null, "Mecánico" },
                    { new Guid("d1c71d69-f89b-4d7c-b0f8-7c8508335e17"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7458), null, "Programador" },
                    { new Guid("d9c82c88-607e-4299-945f-0931cffb849c"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7457), null, "Diseñador Gráfico" },
                    { new Guid("d9f0ebe1-d4d4-4de1-a7aa-61a8d3554b3d"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7466), null, "Contador" },
                    { new Guid("eae95efd-c252-415c-8050-5d4fe7a2a252"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7465), null, "Militar" },
                    { new Guid("f2fe24ed-7d91-44af-8441-d05005c56293"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7445), null, "Piloto" },
                    { new Guid("f65b8bad-2a6d-4f7a-8524-5b63934cda3b"), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(7462), null, "Fotógrafo" }
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OccupationId",
                table: "AspNetUsers",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UserId",
                table: "Claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Occupation");
        }
    }
}
