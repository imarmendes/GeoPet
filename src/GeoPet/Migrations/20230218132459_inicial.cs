using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoPet.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetParentDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetParentDtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogBreed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PetPerantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetDtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetDtos_PetParentDtos_PetPerantId",
                        column: x => x.PetPerantId,
                        principalTable: "PetParentDtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetDtos_PetPerantId",
                table: "PetDtos",
                column: "PetPerantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetDtos");

            migrationBuilder.DropTable(
                name: "PetParentDtos");
        }
    }
}
