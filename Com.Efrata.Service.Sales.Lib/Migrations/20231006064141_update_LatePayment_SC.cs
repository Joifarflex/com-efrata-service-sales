using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Efrata.Service.Sales.Lib.Migrations
{
    public partial class update_LatePayment_SC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LatePayment",
                table: "GarmentSalesContracts",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LatePayment",
                table: "GarmentSalesContracts",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
