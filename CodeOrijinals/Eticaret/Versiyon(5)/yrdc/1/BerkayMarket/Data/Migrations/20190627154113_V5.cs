using Microsoft.EntityFrameworkCore.Migrations;

namespace BerkayMarket.Data.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urun_BmUser_BmUserId",
                table: "Urun");

            migrationBuilder.DropForeignKey(
                name: "FK_Urun_Kategori_KategoriId",
                table: "Urun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urun",
                table: "Urun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategori",
                table: "Kategori");

            migrationBuilder.RenameTable(
                name: "Urun",
                newName: "Uruns");

            migrationBuilder.RenameTable(
                name: "Kategori",
                newName: "Kategoris");

            migrationBuilder.RenameIndex(
                name: "IX_Urun_KategoriId",
                table: "Uruns",
                newName: "IX_Uruns_KategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_Urun_BmUserId",
                table: "Uruns",
                newName: "IX_Uruns_BmUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uruns",
                table: "Uruns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategoris",
                table: "Kategoris",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uruns_BmUser_BmUserId",
                table: "Uruns",
                column: "BmUserId",
                principalTable: "BmUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uruns_Kategoris_KategoriId",
                table: "Uruns",
                column: "KategoriId",
                principalTable: "Kategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uruns_BmUser_BmUserId",
                table: "Uruns");

            migrationBuilder.DropForeignKey(
                name: "FK_Uruns_Kategoris_KategoriId",
                table: "Uruns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Uruns",
                table: "Uruns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategoris",
                table: "Kategoris");

            migrationBuilder.RenameTable(
                name: "Uruns",
                newName: "Urun");

            migrationBuilder.RenameTable(
                name: "Kategoris",
                newName: "Kategori");

            migrationBuilder.RenameIndex(
                name: "IX_Uruns_KategoriId",
                table: "Urun",
                newName: "IX_Urun_KategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_Uruns_BmUserId",
                table: "Urun",
                newName: "IX_Urun_BmUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urun",
                table: "Urun",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategori",
                table: "Kategori",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Urun_BmUser_BmUserId",
                table: "Urun",
                column: "BmUserId",
                principalTable: "BmUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Urun_Kategori_KategoriId",
                table: "Urun",
                column: "KategoriId",
                principalTable: "Kategori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
