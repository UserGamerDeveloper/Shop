using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroElectronic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_param : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParamNameEight",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameEleven",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameFive",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameFour",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameNine",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameOne",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameSeven",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameSix",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameTen",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameThree",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameTwelve",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamNameTwo",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueEight",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueEleven",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueFive",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueFour",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueNine",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueOne",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueSeven",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueSix",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueTen",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueThree",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueTwelve",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValueTwo",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParamNameEight",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameEleven",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameFive",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameFour",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameNine",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameOne",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameSeven",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameSix",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameTen",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameThree",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameTwelve",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamNameTwo",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueEight",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueEleven",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueFive",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueFour",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueNine",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueOne",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueSeven",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueSix",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueTen",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueThree",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueTwelve",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ParamValueTwo",
                table: "Equipments");
        }
    }
}
