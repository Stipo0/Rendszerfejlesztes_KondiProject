using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KondiProject.API.Migrations
{
    /// <inheritdoc />
    public partial class random : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MachineName",
                table: "GymMachines",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "GymMachines",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "GymMachines");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GymMachines",
                newName: "MachineName");
        }
    }
}
