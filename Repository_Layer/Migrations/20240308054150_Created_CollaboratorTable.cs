using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Created_CollaboratorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                  name: "Collaborator",
                  columns: table => new
                  {
                      Collaborator_id = table.Column<int>(type: "int", nullable: false)
                          .Annotation("SqlServer:Identity", "1, 1"),
                      Note_Id = table.Column<int>(type: "int", nullable: false),
                      User_Id = table.Column<int>(type: "int", nullable: false)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_Collaborator", x => x.Collaborator_id);
                      table.ForeignKey(
                          name: "FK_Collaborator_Notes_Note_Id",
                          column: x => x.Note_Id,
                          principalTable: "Notes",
                          principalColumn: "NoteId",
                          onDelete: ReferentialAction.Cascade);
                      table.ForeignKey(
                          name: "FK_Collaborator_Users_User_Id",
                          column: x => x.User_Id,
                          principalTable: "Users",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                  });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_Note_Id",
                table: "Collaborator",
                column: "Note_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_User_Id",
                table: "Collaborator",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborator");
        }

       
    }
}
