using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BURN_SOCIETY.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPNResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTrackingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderMerchantReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPNResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    confirmation_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_tracking_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_status_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    call_back_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status_code = table.Column<int>(type: "int", nullable: false),
                    merchant_reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    account_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_status_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    error_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attend = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cadre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentConfirmation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ammount = table.Column<float>(type: "real", nullable: false),
                    ReffCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPNResponses");

            migrationBuilder.DropTable(
                name: "PaymentResponse");

            migrationBuilder.DropTable(
                name: "Registrations");
        }
    }
}
