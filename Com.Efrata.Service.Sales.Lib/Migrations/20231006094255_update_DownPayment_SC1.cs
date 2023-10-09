using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Efrata.Service.Sales.Lib.Migrations
{
    public partial class update_DownPayment_SC1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DownPayment",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(double),
                oldMaxLength: 500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DownPayment",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(double),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
