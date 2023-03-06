using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeoPet.Migrations
{
    /// <inheritdoc />
    public partial class Initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pet_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "CEP", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "01001000", "admin@email.com", "Admin", "$2b$10$i/dgw2inj3yD4cH0cbvBJ.i6cR/uZqYTua3Ao7wmsk/mytI1opAbS" },
                    { 2, "01001000", "email@email.com", "Pessoa1", "$2b$10$i/dgw2inj3yD4cH0cbvBJ.i6cR/uZqYTua3Ao7wmsk/mytI1opAbS" },
                    { 3, "01001000", "email2@email.com", "Pessoa2", "$2b$10$i/dgw2inj3yD4cH0cbvBJ.i6cR/uZqYTua3Ao7wmsk/mytI1opAbS" }
                });

            migrationBuilder.InsertData(
                table: "Pet",
                columns: new[] { "Id", "Age", "Breed", "Name", "OwnerId", "Size" },
                values: new object[,]
                {
                    { new Guid("08148b33-d29c-41b8-b797-ec903d9ede71"), 6, "Pug", "Pet2", 2, "pequeno" },
                    { new Guid("d386421c-b9dd-46f0-8ec1-5d788c1a33a6"), 5, "Pastor-alemão", "Pet1", 3, "grande" },
                    { new Guid("e646f236-e19d-4c3a-99b9-1fd09e21eb4f"), 6, "Pug", "Pet3", 2, "pequeno" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owner_Email",
                table: "Owner",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_OwnerId",
                table: "Pet",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
