using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.Migrations
{
    public partial class migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientContact",
                table: "ClientContact");

            migrationBuilder.RenameTable(
                name: "ClientContact",
                newName: "ClientContacts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientContacts",
                table: "ClientContacts",
                column: "ClientContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientContacts",
                table: "ClientContacts");

            migrationBuilder.RenameTable(
                name: "ClientContacts",
                newName: "ClientContact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientContact",
                table: "ClientContact",
                column: "ClientContactId");
        }
    }
}
