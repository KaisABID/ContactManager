using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Contacts",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Contacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactTel1",
                table: "Contacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactTel2",
                table: "Contacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactTel1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactTel2",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Contacts",
                newName: "Email");
        }
    }
}
