using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroElectronic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuaranteePeriod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "BodyMaterial", "CategoryId", "Description", "GuaranteePeriod", "Name", "Power", "Price", "Size", "WorkingArea" },
                values: new object[] { 1, "Изготовлен из нержавеющей стали", 1, "Полуавтоматическая установка нанесения покрытий методом центрифугирования", "2 года", "UNIXX S760+", "400 (208) В, 3 фазы, 50-60 Гц", 450000m, "1250 х 1250/1510 х 2000/2500 мм", "500 x 750 мм" });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_CategoryId",
                table: "Equipments",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");
        }
    }
}
