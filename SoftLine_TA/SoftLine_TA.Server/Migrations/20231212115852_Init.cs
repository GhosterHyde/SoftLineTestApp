using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftLine_TA.Server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasks_Statuses_Status_ID",
                        column: x => x.Status_ID,
                        principalTable: "Statuses",
                        principalColumn: "Status_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Status_ID", "Status_name" },
                values: new object[] { 1, "Создана" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Status_ID", "Status_name" },
                values: new object[] { 2, "В работе" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Status_ID", "Status_name" },
                values: new object[] { 3, "Завершена" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Status_ID",
                table: "Tasks",
                column: "Status_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
