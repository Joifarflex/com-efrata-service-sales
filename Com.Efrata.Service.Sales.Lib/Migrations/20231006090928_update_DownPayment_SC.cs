using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Efrata.Service.Sales.Lib.Migrations
{
    public partial class update_DownPayment_SC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DownPayment",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DownPayment",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(double),
                oldMaxLength: 500);
        }
    }
}
