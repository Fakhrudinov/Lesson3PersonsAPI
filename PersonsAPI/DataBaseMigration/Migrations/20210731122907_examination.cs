using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBaseMigration.Migrations
{
    public partial class examination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcedureName = table.Column<string>(type: "text", nullable: true),
                    ProcedureDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProcedureCost = table.Column<int>(type: "integer", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    ClinicId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examinations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Examinations",
                columns: new[] { "Id", "ClinicId", "IsPaid", "PaidDate", "PersonId", "ProcedureCost", "ProcedureDate", "ProcedureName" },
                values: new object[,]
                {
                    { 1, 4, true, new DateTime(2021, 7, 31, 15, 29, 5, 540, DateTimeKind.Local).AddTicks(1035), 22, 100, new DateTime(2021, 7, 31, 14, 29, 5, 539, DateTimeKind.Local).AddTicks(356), "procedure 1" },
                    { 2, 4, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, 100, new DateTime(2021, 7, 31, 15, 29, 5, 540, DateTimeKind.Local).AddTicks(1525), "proc 2" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Login", "Password" },
                values: new object[] { "user1", "u/J44HZsYr9/fKnrSC7GkEPfGSdLlQY0rwmXjnj2V/M=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Login", "Password" },
                values: new object[] { "string", "gY5iKrx9HOv75+lUDwwSrR5sUirs2DknyeMqU8yHimw=" });

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ClinicId",
                table: "Examinations",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_PersonId",
                table: "Examinations",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Login", "Password" },
                values: new object[] { "test", "testtest" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Login", "Password" },
                values: new object[] { "user2", "12334bd4b" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password" },
                values: new object[] { 3, "user2", "w34f5v4w5b6" });
        }
    }
}
