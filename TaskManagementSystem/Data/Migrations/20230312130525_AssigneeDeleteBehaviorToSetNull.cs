using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    public partial class AssigneeDeleteBehaviorToSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
