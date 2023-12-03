using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.Migrations
{
    public partial class migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientClientContact");

            migrationBuilder.DropTable(
                name: "ClientContactContact");

            migrationBuilder.AddColumn<int>(
                name: "ClientContactId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientContactId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ClientContactId",
                table: "Contacts",
                column: "ClientContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientContactId",
                table: "Clients",
                column: "ClientContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientContacts_ClientContactId",
                table: "Clients",
                column: "ClientContactId",
                principalTable: "ClientContacts",
                principalColumn: "ClientContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ClientContacts_ClientContactId",
                table: "Contacts",
                column: "ClientContactId",
                principalTable: "ClientContacts",
                principalColumn: "ClientContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientContacts_ClientContactId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ClientContacts_ClientContactId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ClientContactId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientContactId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientContactId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ClientContactId",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientClientContact",
                columns: table => new
                {
                    ClientContactsClientContactId = table.Column<int>(type: "int", nullable: false),
                    ClientsClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClientContact", x => new { x.ClientContactsClientContactId, x.ClientsClientId });
                    table.ForeignKey(
                        name: "FK_ClientClientContact_ClientContacts_ClientContactsClientConta~",
                        column: x => x.ClientContactsClientContactId,
                        principalTable: "ClientContacts",
                        principalColumn: "ClientContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientClientContact_Clients_ClientsClientId",
                        column: x => x.ClientsClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClientContactContact",
                columns: table => new
                {
                    ClientContactsClientContactId = table.Column<int>(type: "int", nullable: false),
                    ContactsContactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContactContact", x => new { x.ClientContactsClientContactId, x.ContactsContactId });
                    table.ForeignKey(
                        name: "FK_ClientContactContact_ClientContacts_ClientContactsClientCont~",
                        column: x => x.ClientContactsClientContactId,
                        principalTable: "ClientContacts",
                        principalColumn: "ClientContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientContactContact_Contacts_ContactsContactId",
                        column: x => x.ContactsContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClientClientContact_ClientsClientId",
                table: "ClientClientContact",
                column: "ClientsClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContactContact_ContactsContactId",
                table: "ClientContactContact",
                column: "ContactsContactId");
        }
    }
}
