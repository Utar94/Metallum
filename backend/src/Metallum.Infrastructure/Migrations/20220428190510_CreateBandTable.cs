using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Metallum.Infrastructure.Migrations
{
    public partial class CreateBandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Genre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Href = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MetallumId = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uuid", nullable: true),
                    Key = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bands_CreatedById",
                table: "Bands",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_Deleted",
                table: "Bands",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_DeletedById",
                table: "Bands",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_Genre",
                table: "Bands",
                column: "Genre");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_Key",
                table: "Bands",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_Location",
                table: "Bands",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_MetallumId",
                table: "Bands",
                column: "MetallumId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_Name",
                table: "Bands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_Status",
                table: "Bands",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_UpdatedById",
                table: "Bands",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bands");
        }
    }
}
