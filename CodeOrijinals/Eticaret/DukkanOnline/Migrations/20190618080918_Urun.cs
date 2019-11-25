using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DukkanOnline.Migrations
{
    public partial class Urun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AltKategori",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltKategori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Sicili = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Kategoris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    AltKategoriId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategoris_AltKategori_AltKategoriId",
                        column: x => x.AltKategoriId,
                        principalTable: "AltKategori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
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
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uruns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Stok = table.Column<int>(nullable: false),
                    KategoriId = table.Column<int>(nullable: false),
                    ToptanciId = table.Column<string>(nullable: true),
                    SaticiId = table.Column<string>(nullable: true),
                    BakiciId = table.Column<string>(nullable: true),
                    AliciId = table.Column<string>(nullable: true),
                    YorumcuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uruns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uruns_AspNetUsers_AliciId",
                        column: x => x.AliciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uruns_AspNetUsers_BakiciId",
                        column: x => x.BakiciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uruns_Kategoris_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uruns_AspNetUsers_SaticiId",
                        column: x => x.SaticiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uruns_AspNetUsers_ToptanciId",
                        column: x => x.ToptanciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uruns_AspNetUsers_YorumcuId",
                        column: x => x.YorumcuId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Envanter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrunId = table.Column<string>(nullable: true),
                    UrunId1 = table.Column<int>(nullable: true),
                    SaticiId = table.Column<string>(nullable: true),
                    BakiciId = table.Column<string>(nullable: true),
                    AliciId = table.Column<string>(nullable: true),
                    YorumcuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envanter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envanter_AspNetUsers_AliciId",
                        column: x => x.AliciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envanter_AspNetUsers_BakiciId",
                        column: x => x.BakiciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envanter_AspNetUsers_SaticiId",
                        column: x => x.SaticiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envanter_Uruns_UrunId1",
                        column: x => x.UrunId1,
                        principalTable: "Uruns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envanter_AspNetUsers_YorumcuId",
                        column: x => x.YorumcuId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sepet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnvanterId = table.Column<string>(nullable: true),
                    EnvanterId1 = table.Column<int>(nullable: true),
                    UrunId = table.Column<string>(nullable: true),
                    UrunId1 = table.Column<int>(nullable: true),
                    BakiciId = table.Column<string>(nullable: true),
                    AliciId = table.Column<string>(nullable: true),
                    YorumcuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sepet_AspNetUsers_AliciId",
                        column: x => x.AliciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sepet_AspNetUsers_BakiciId",
                        column: x => x.BakiciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sepet_Envanter_EnvanterId1",
                        column: x => x.EnvanterId1,
                        principalTable: "Envanter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sepet_Uruns_UrunId1",
                        column: x => x.UrunId1,
                        principalTable: "Uruns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sepet_AspNetUsers_YorumcuId",
                        column: x => x.YorumcuId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SatinAlma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SepetId = table.Column<string>(nullable: true),
                    SepetId1 = table.Column<int>(nullable: true),
                    EnvanterId = table.Column<string>(nullable: true),
                    EnvanterId1 = table.Column<int>(nullable: true),
                    UrunId = table.Column<string>(nullable: true),
                    UrunId1 = table.Column<int>(nullable: true),
                    AliciId = table.Column<string>(nullable: true),
                    YorumcuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatinAlma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SatinAlma_AspNetUsers_AliciId",
                        column: x => x.AliciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SatinAlma_Envanter_EnvanterId1",
                        column: x => x.EnvanterId1,
                        principalTable: "Envanter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SatinAlma_Sepet_SepetId1",
                        column: x => x.SepetId1,
                        principalTable: "Sepet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SatinAlma_Uruns_UrunId1",
                        column: x => x.UrunId1,
                        principalTable: "Uruns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SatinAlma_AspNetUsers_YorumcuId",
                        column: x => x.YorumcuId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Yorum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YorumMetni = table.Column<string>(maxLength: 600, nullable: true),
                    Puan = table.Column<int>(nullable: false),
                    SatinAlmaId = table.Column<string>(nullable: true),
                    SatinAlmaId1 = table.Column<int>(nullable: true),
                    SepetId = table.Column<string>(nullable: true),
                    SepetId1 = table.Column<int>(nullable: true),
                    EnvanterId = table.Column<string>(nullable: true),
                    EnvanterId1 = table.Column<int>(nullable: true),
                    UrunId = table.Column<string>(nullable: true),
                    UrunId1 = table.Column<int>(nullable: true),
                    YorumcuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yorum_Envanter_EnvanterId1",
                        column: x => x.EnvanterId1,
                        principalTable: "Envanter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yorum_SatinAlma_SatinAlmaId1",
                        column: x => x.SatinAlmaId1,
                        principalTable: "SatinAlma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yorum_Sepet_SepetId1",
                        column: x => x.SepetId1,
                        principalTable: "Sepet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yorum_Uruns_UrunId1",
                        column: x => x.UrunId1,
                        principalTable: "Uruns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yorum_AspNetUsers_YorumcuId",
                        column: x => x.YorumcuId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Envanter_AliciId",
                table: "Envanter",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_Envanter_BakiciId",
                table: "Envanter",
                column: "BakiciId");

            migrationBuilder.CreateIndex(
                name: "IX_Envanter_SaticiId",
                table: "Envanter",
                column: "SaticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Envanter_UrunId1",
                table: "Envanter",
                column: "UrunId1");

            migrationBuilder.CreateIndex(
                name: "IX_Envanter_YorumcuId",
                table: "Envanter",
                column: "YorumcuId");

            migrationBuilder.CreateIndex(
                name: "IX_Kategoris_AltKategoriId",
                table: "Kategoris",
                column: "AltKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_SatinAlma_AliciId",
                table: "SatinAlma",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_SatinAlma_EnvanterId1",
                table: "SatinAlma",
                column: "EnvanterId1");

            migrationBuilder.CreateIndex(
                name: "IX_SatinAlma_SepetId1",
                table: "SatinAlma",
                column: "SepetId1");

            migrationBuilder.CreateIndex(
                name: "IX_SatinAlma_UrunId1",
                table: "SatinAlma",
                column: "UrunId1");

            migrationBuilder.CreateIndex(
                name: "IX_SatinAlma_YorumcuId",
                table: "SatinAlma",
                column: "YorumcuId");

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_AliciId",
                table: "Sepet",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_BakiciId",
                table: "Sepet",
                column: "BakiciId");

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_EnvanterId1",
                table: "Sepet",
                column: "EnvanterId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_UrunId1",
                table: "Sepet",
                column: "UrunId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_YorumcuId",
                table: "Sepet",
                column: "YorumcuId");

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_AliciId",
                table: "Uruns",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_BakiciId",
                table: "Uruns",
                column: "BakiciId");

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_KategoriId",
                table: "Uruns",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_SaticiId",
                table: "Uruns",
                column: "SaticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_ToptanciId",
                table: "Uruns",
                column: "ToptanciId");

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_YorumcuId",
                table: "Uruns",
                column: "YorumcuId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_EnvanterId1",
                table: "Yorum",
                column: "EnvanterId1");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_SatinAlmaId1",
                table: "Yorum",
                column: "SatinAlmaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_SepetId1",
                table: "Yorum",
                column: "SepetId1");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_UrunId1",
                table: "Yorum",
                column: "UrunId1");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_YorumcuId",
                table: "Yorum",
                column: "YorumcuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Yorum");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "SatinAlma");

            migrationBuilder.DropTable(
                name: "Sepet");

            migrationBuilder.DropTable(
                name: "Envanter");

            migrationBuilder.DropTable(
                name: "Uruns");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Kategoris");

            migrationBuilder.DropTable(
                name: "AltKategori");
        }
    }
}
