using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuthorizationLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Authorization");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Authorization",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(65)", maxLength: 65, nullable: false),
                    second_name = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_entered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    additional_info = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    refresh_token_expiration_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                schema: "Authorization",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credentials", x => x.id);
                    table.ForeignKey(
                        name: "fk_credentials_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "Authorization",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationProvider",
                schema: "Authorization",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_credential_id = table.Column<long>(type: "bigint", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    authorization_type = table.Column<int>(type: "integer", nullable: true),
                    o_auth_token = table.Column<string>(type: "text", nullable: true),
                    o_auth_client_id = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    last_modified_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authorization_provider", x => x.id);
                    table.ForeignKey(
                        name: "fk_authorization_provider_credentials_user_credential_id",
                        column: x => x.user_credential_id,
                        principalSchema: "Authorization",
                        principalTable: "Credentials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_authorization_provider_user_credential_id",
                schema: "Authorization",
                table: "AuthorizationProvider",
                column: "user_credential_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_credentials_email",
                schema: "Authorization",
                table: "Credentials",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_credentials_user_id",
                schema: "Authorization",
                table: "Credentials",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizationProvider",
                schema: "Authorization");

            migrationBuilder.DropTable(
                name: "Credentials",
                schema: "Authorization");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Authorization");
        }
    }
}
