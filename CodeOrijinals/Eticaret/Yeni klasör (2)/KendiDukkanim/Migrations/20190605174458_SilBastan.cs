using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KendiDukkanim.Migrations
{
    public partial class SilBastan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    KategoriID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adi = table.Column<string>(maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.KategoriID);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    SiparisDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Urun",
                columns: table => new
                {
                    UrunID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Adi = table.Column<string>(nullable: true),
                    Stok = table.Column<int>(nullable: false),
                    KisaAciklama = table.Column<string>(nullable: true),
                    UzunAciklama = table.Column<string>(nullable: true),
                    Puani = table.Column<int>(nullable: false),
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
                name: "Satici",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    UrunID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Satici_Urun_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siparis",
                columns: table => new
                {
                    SiparisID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrunID = table.Column<int>(nullable: false),
                    MusteriID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparis", x => x.SiparisID);
                    table.ForeignKey(
                        name: "FK_Siparis_Musteri_MusteriID",
                        column: x => x.MusteriID,
                        principalTable: "Musteri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparis_Urun_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dukkan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adi = table.Column<string>(nullable: true),
                    Yerleske = table.Column<string>(maxLength: 50, nullable: true),
                    SaticiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dukkan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dukkan_Satici_SaticiId",
                        column: x => x.SaticiId,
                        principalTable: "Satici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Satici1",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    DukkanlariId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satici1", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Satici1_Dukkan_DukkanlariId",
                        column: x => x.DukkanlariId,
                        principalTable: "Dukkan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Satis",
                columns: table => new
                {
                    SatisId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SatisZamani = table.Column<DateTime>(nullable: false),
                    SaticiID = table.Column<int>(nullable: false),
                    UrunID = table.Column<int>(nullable: false),
                    Satici1ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satis", x => x.SatisId);
                    table.ForeignKey(
                        name: "FK_Satis_Satici1_Satici1ID",
                        column: x => x.Satici1ID,
                        principalTable: "Satici1",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Satis_Satici_SaticiID",
                        column: x => x.SaticiID,
                        principalTable: "Satici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Satis_Urun_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dukkan_SaticiId",
                table: "Dukkan",
                column: "SaticiId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Satici_UrunID",
                table: "Satici",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_Satici1_DukkanlariId",
                table: "Satici1",
                column: "DukkanlariId");

            migrationBuilder.CreateIndex(
                name: "IX_Satis_Satici1ID",
                table: "Satis",
                column: "Satici1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Satis_SaticiID",
                table: "Satis",
                column: "SaticiID");

            migrationBuilder.CreateIndex(
                name: "IX_Satis_UrunID",
                table: "Satis",
                column: "UrunID");

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
                name: "Satis");

            migrationBuilder.DropTable(
                name: "Siparis");

            migrationBuilder.DropTable(
                name: "Satici1");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "Dukkan");

            migrationBuilder.DropTable(
                name: "Satici");

            migrationBuilder.DropTable(
                name: "Urun");

            migrationBuilder.DropTable(
                name: "Kategori");
        }
    }
}
