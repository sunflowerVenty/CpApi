using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CpApi.Migrations
{
    /// <inheritdoc />
    public partial class bigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_User_id",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Films_Film_Id",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_Recipient_Id",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_User_Id",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Recipient_Id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Recipient_Id",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "AboutMe",
                table: "Users",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id_User",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Messages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "dateTimeSent",
                table: "Messages",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "Film_Id",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "Id_Message",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_User_Id",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_Film_Id",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Logins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "id_Login",
                table: "Logins",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Logins_User_id",
                table: "Logins",
                newName: "IX_Logins_UserId");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Logins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatFilm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatFilm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatFilm_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatFilm_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Email",
                table: "Logins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatFilm_MovieId",
                table: "ChatFilm",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatFilm_SenderId",
                table: "ChatFilm",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Users_UserId",
                table: "Logins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_UserId",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChatFilm");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Logins_Email",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Users",
                newName: "AboutMe");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id_User");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Messages",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Messages",
                newName: "dateTimeSent");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Messages",
                newName: "Film_Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "Id_Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                newName: "IX_Messages_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_Film_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Logins",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Logins",
                newName: "id_Login");

            migrationBuilder.RenameIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                newName: "IX_Logins_User_id");

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recipient_Id",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    id_Genre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGenre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.id_Genre);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    id_Film = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre_id = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.id_Film);
                    table.ForeignKey(
                        name: "FK_Films_Genres_Genre_id",
                        column: x => x.Genre_id,
                        principalTable: "Genres",
                        principalColumn: "id_Genre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Recipient_Id",
                table: "Messages",
                column: "Recipient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Genre_id",
                table: "Films",
                column: "Genre_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Users_User_id",
                table: "Logins",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Films_Film_Id",
                table: "Messages",
                column: "Film_Id",
                principalTable: "Films",
                principalColumn: "id_Film",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_Recipient_Id",
                table: "Messages",
                column: "Recipient_Id",
                principalTable: "Users",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_User_Id",
                table: "Messages",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
