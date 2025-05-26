using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCondoApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialForSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accountschart",
                columns: table => new
                {
                    accountschartid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parentaccountid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    formattedcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    accounttype = table.Column<string>(type: "varchar(30)", nullable: false),
                    acceptsreleases = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accountschart", x => x.accountschartid);
                    table.ForeignKey(
                        name: "fk_accountschart_accountschart_parentaccountid",
                        column: x => x.parentaccountid,
                        principalTable: "accountschart",
                        principalColumn: "accountschartid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_accountschart_parentaccountid",
                table: "accountschart",
                column: "parentaccountid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accountschart");
        }
    }
}
