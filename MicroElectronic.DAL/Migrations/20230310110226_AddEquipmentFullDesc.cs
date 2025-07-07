using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroElectronic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentFullDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Equipments",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullDescription",
                value: "Полуавтоматическая автономная установка UNIXX S 760+ предназначена для нанесения покрытий методом центрифугирования на крупноразмерные подложки. В установке применяется технология CCP (Covered Chuck Processor – центрифугирование с закрытой крышкой), позволяющей наносить покрытия с превосходной однородностью и повторяемостью.\r\nПрограммное обеспечение установки имеет дружественный пользовательский интерфейс со всеми необходимыми функциями, такими как создание и редактирование рабочих программ, администрирование пользователей, отслеживание состояния системы. Подвод необходимых сред (воздух, азот, вакуум) выполняется подключением через быстроразъемные соединения и управляется программным обеспечением.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Equipments");
        }
    }
}
