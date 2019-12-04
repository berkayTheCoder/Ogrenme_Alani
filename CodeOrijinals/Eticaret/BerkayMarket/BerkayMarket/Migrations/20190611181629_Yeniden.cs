using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BerkayMarket.Migrations
{
    public partial class Yeniden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    SiparisDate = table.Column<DateTime>(nullable: true),
                    HireDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    KategoriID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    SaticiID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.KategoriID);
                    table.ForeignKey(
                        name: "FK_Kategori_Person_SaticiID",
                        column: x => x.SaticiID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficeAssignment",
                columns: table => new
                {
                    SaticiID = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeAssignment", x => x.SaticiID);
                    table.ForeignKey(
                        name: "FK_OfficeAssignment_Person_SaticiID",
                        column: x => x.SaticiID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urun",
                columns: table => new
                {
                    UrunID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Credits = table.Column<int>(nullable: false),
                    KategoriID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urun", x => x.UrunID);
                    table.ForeignKey(
                        name: "FK_Urun_Kategori_KategoriID",
                        column: x => x.KategoriID,
                        principalTable: "Kategori",
                        principalColumn: "KategoriID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Envanter",
                columns: table => new
                {
                    SaticiID = table.Column<int>(nullable: false),
                    UrunID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envanter", x => new { x.UrunID, x.SaticiID });
                    table.ForeignKey(
                        name: "FK_Envanter_Person_SaticiID",
                        column: x => x.SaticiID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envanter_Urun_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Siparis",
                columns: table => new
                {
                    SiparisID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrunID = table.Column<int>(nullable: false),
                    MusteriID = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparis", x => x.SiparisID);
                    table.ForeignKey(
                        name: "FK_Siparis_Person_MusteriID",
                        column: x => x.MusteriID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparis_Urun_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Envanter_SaticiID",
                table: "Envanter",
                column: "SaticiID");

            migrationBuilder.CreateIndex(
                name: "IX_Kategori_SaticiID",
                table: "Kategori",
                column: "SaticiID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_MusteriID",
                table: "Siparis",
                column: "MusteriID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_UrunID",
                table: "Siparis",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_Urun_KategoriID",
                table: "Urun",
                column: "KategoriID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Envanter");

            migrationBuilder.DropTable(
                name: "OfficeAssignment");

            migrationBuilder.DropTable(
                name: "Siparis");

            migrationBuilder.DropTable(
                name: "Urun");

            migrationBuilder.DropTable(
                name: "Kategori");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
