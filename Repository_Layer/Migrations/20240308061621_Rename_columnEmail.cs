using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Rename_columnEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "Email",
               table: "Collaborator",
               newName: "Collaborator_Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
                         migrationBuilder.RenameColumn(
                           name: "Collaborator_Email",
                           table: "Collaborator",
                           newName: "Email");
        }
    }
}
