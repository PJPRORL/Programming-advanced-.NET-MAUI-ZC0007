using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamenMuziekschool.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instrumenten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Categorie = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrumenten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lesgevers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Voornaam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesgevers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leerlingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Voornaam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Inschrijfdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InstrumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leerlingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leerlingen_Instrumenten_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrumenten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LesgeverInstrumenten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstrumentId = table.Column<int>(type: "integer", nullable: false),
                    LesgeverId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LesgeverInstrumenten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LesgeverInstrumenten_Instrumenten_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrumenten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LesgeverInstrumenten_Lesgevers_LesgeverId",
                        column: x => x.LesgeverId,
                        principalTable: "Lesgevers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Instrumenten",
                columns: new[] { "Id", "Categorie", "Naam" },
                values: new object[,]
                {
                    { 1, "Toetsinstrumenten", "Piano" },
                    { 2, "Snaarinstrumenten", "Gitaar" },
                    { 3, "Strijkinstrumenten", "Viool" },
                    { 4, "Slagwerk", "Drums" },
                    { 5, "Blaasinstrumenten", "Dwarsfluit" }
                });

            migrationBuilder.InsertData(
                table: "Lesgevers",
                columns: new[] { "Id", "Email", "Naam", "Voornaam" },
                values: new object[,]
                {
                    { 1, "ludwig@muziekschool.be", "Van Beethoven", "Ludwig" },
                    { 2, "clara.peeters@muziekschool.be", "Peeters", "Clara" },
                    { 3, "david.janssens@muziekschool.be", "Janssens", "David" },
                    { 4, "hanne.maes@muziekschool.be", "Maes", "Hanne" },
                    { 5, "bram.vermeulen@muziekschool.be", "Vermeulen", "Bram" }
                });

            migrationBuilder.InsertData(
                table: "Leerlingen",
                columns: new[] { "Id", "Email", "Inschrijfdatum", "InstrumentId", "Naam", "Voornaam" },
                values: new object[,]
                {
                    { 1, "mila.jacobs@mail.com", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Jacobs", "Mila" },
                    { 2, "noah.peeters@mail.com", new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Peeters", "Noah" },
                    { 3, "camille.willems@mail.com", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Willems", "Camille" },
                    { 4, "vince.claes@mail.com", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Claes", "Vince" },
                    { 5, "marie.peeters@mail.com", new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Peeters", "Marie" },
                    { 6, "lucas.dubois@mail.com", new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Dubois", "Lucas" }
                });

            migrationBuilder.InsertData(
                table: "LesgeverInstrumenten",
                columns: new[] { "Id", "InstrumentId", "LesgeverId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 1 },
                    { 5, 3, 4 },
                    { 6, 4, 5 },
                    { 7, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leerlingen_InstrumentId",
                table: "Leerlingen",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_LesgeverInstrumenten_InstrumentId",
                table: "LesgeverInstrumenten",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_LesgeverInstrumenten_LesgeverId",
                table: "LesgeverInstrumenten",
                column: "LesgeverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leerlingen");

            migrationBuilder.DropTable(
                name: "LesgeverInstrumenten");

            migrationBuilder.DropTable(
                name: "Instrumenten");

            migrationBuilder.DropTable(
                name: "Lesgevers");
        }
    }
}
