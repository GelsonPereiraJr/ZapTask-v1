using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProximaTentativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProximaTentativaEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProximaTentativaEm",
                table: "Tarefas");
        }
    }
}
