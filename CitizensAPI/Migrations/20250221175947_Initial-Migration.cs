using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizensAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitizenDetails",
                columns: table => new
                {
                    CitizenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitizenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NationalIdExpirationDate = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenDetails", x => x.CitizenId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizenDetails");
        }
    }
}
