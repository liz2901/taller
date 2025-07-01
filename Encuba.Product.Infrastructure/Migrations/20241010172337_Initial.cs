using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encuba.Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicAccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessToken = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Scope = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ExpiresIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicAccessToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    UserType = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SecondName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FirstLastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SecondLastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedTermsAndCondition = table.Column<bool>(type: "bit", nullable: false),
                    AcceptedTermsAndConditionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResetPasswordToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResetPasswordTokenExpiresIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublicAccessTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_PublicAccessToken_PublicAccessTokenId",
                        column: x => x.PublicAccessTokenId,
                        principalTable: "PublicAccessToken",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPublicAccessToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicAccessTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPublicAccessToken", x => new { x.UserId, x.PublicAccessTokenId });
                    table.ForeignKey(
                        name: "FK_UserPublicAccessToken_PublicAccessToken_PublicAccessTokenId",
                        column: x => x.PublicAccessTokenId,
                        principalTable: "PublicAccessToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPublicAccessToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicAccessToken_AccessToken",
                table: "PublicAccessToken",
                column: "AccessToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicAccessToken_RefreshToken",
                table: "PublicAccessToken",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PublicAccessTokenId",
                table: "User",
                column: "PublicAccessTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPublicAccessToken_PublicAccessTokenId",
                table: "UserPublicAccessToken",
                column: "PublicAccessTokenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPublicAccessToken");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PublicAccessToken");
        }
    }
}
