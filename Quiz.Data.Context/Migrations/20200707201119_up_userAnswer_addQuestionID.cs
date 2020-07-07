using Microsoft.EntityFrameworkCore.Migrations;

namespace Quiz.Data.Context.Migrations
{
    public partial class up_userAnswer_addQuestionID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuestionID",
                table: "UserAnswer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_QuestionID",
                table: "UserAnswer",
                column: "QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Question_QuestionID",
                table: "UserAnswer",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Question_QuestionID",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_QuestionID",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "QuestionID",
                table: "UserAnswer");
        }
    }
}
