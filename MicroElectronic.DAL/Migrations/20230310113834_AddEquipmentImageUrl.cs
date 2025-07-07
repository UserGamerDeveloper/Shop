using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroElectronic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/lib/images/equipments/poluavtomaticheskaya_ustanovka_naneseniya_pokrytij_metodom_centrifugirovaniya_unixx_s760.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Equipments");
        }
    }
}
