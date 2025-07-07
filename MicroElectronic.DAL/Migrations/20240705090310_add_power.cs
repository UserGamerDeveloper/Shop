using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroElectronic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_power : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Power",
                table: "Equipments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Power",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "BodyMaterial", "CategoryId", "Description", "FullDescription", "GuaranteePeriod", "ImageUrl", "Name", "One", "Power", "Price", "Size", "Three", "Two", "WorkingArea" },
                values: new object[] { 1, "Изготовлен из нержавеющей стали", 1, "Полуавтоматическая установка нанесения покрытий методом центрифугирования", "Полуавтоматическая автономная установка UNIXX S 760+ предназначена для нанесения покрытий методом центрифугирования на крупноразмерные подложки. В установке применяется технология CCP (Covered Chuck Processor – центрифугирование с закрытой крышкой), позволяющей наносить покрытия с превосходной однородностью и повторяемостью.\r\nПрограммное обеспечение установки имеет дружественный пользовательский интерфейс со всеми необходимыми функциями, такими как создание и редактирование рабочих программ, администрирование пользователей, отслеживание состояния системы. Подвод необходимых сред (воздух, азот, вакуум) выполняется подключением через быстроразъемные соединения и управляется программным обеспечением.", "2 года", "/lib/images/equipments/poluavtomaticheskaya_ustanovka_naneseniya_pokrytij_metodom_centrifugirovaniya_unixx_s760.jpg", "UNIXX S760+", 0, "400 (208) В, 3 фазы, 50-60 Гц", 450000m, "1250 х 1250/1510 х 2000/2500 мм", 0, 0, "500 x 750 мм" });
        }
    }
}
