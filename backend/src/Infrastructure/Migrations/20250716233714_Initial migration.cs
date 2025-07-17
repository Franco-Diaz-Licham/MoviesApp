using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_url = table.Column<string>(type: "text", nullable: false),
                    public_id = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "theatres",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    longitude = table.Column<int>(type: "integer", nullable: true),
                    latitude = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_theatres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    photo_id = table.Column<int>(type: "integer", nullable: false),
                    biography = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actors", x => x.id);
                    table.ForeignKey(
                        name: "fk_actors_photos_photo_id",
                        column: x => x.photo_id,
                        principalTable: "photos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    in_theatres_flag = table.Column<bool>(type: "boolean", nullable: false),
                    up_coming_flag = table.Column<bool>(type: "boolean", nullable: false),
                    photo_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movies", x => x.id);
                    table.ForeignKey(
                        name: "fk_movies_photos_photo_id",
                        column: x => x.photo_id,
                        principalTable: "photos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "movie_actors",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "integer", nullable: false),
                    actor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_actors", x => new { x.movie_id, x.actor_id });
                    table.ForeignKey(
                        name: "fk_movie_actors_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "actors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_movie_actors_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_genres",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "integer", nullable: false),
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_genres", x => new { x.movie_id, x.genre_id });
                    table.ForeignKey(
                        name: "fk_movie_genres_genres_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_movie_genres_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_theatres",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "integer", nullable: false),
                    theatre_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_theatres", x => new { x.movie_id, x.theatre_id });
                    table.ForeignKey(
                        name: "fk_movie_theatres_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movie_theatres_theatres_theatre_id",
                        column: x => x.theatre_id,
                        principalTable: "theatres",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_actors_photo_id",
                table: "actors",
                column: "photo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_movie_actors_actor_id",
                table: "movie_actors",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_movie_genres_genre_id",
                table: "movie_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_movie_theatres_theatre_id",
                table: "movie_theatres",
                column: "theatre_id");

            migrationBuilder.CreateIndex(
                name: "ix_movies_photo_id",
                table: "movies",
                column: "photo_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_actors");

            migrationBuilder.DropTable(
                name: "movie_genres");

            migrationBuilder.DropTable(
                name: "movie_theatres");

            migrationBuilder.DropTable(
                name: "actors");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "theatres");

            migrationBuilder.DropTable(
                name: "photos");
        }
    }
}
