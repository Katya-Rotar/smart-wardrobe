using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedProfileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comment_likes_profiles_profile_id",
                table: "comment_likes");

            migrationBuilder.DropForeignKey(
                name: "fk_comments_profiles_profile_id",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "fk_followers_profiles_follower_id",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "fk_followers_profiles_following_id",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "fk_post_likes_profiles_profile_id",
                table: "post_likes");

            migrationBuilder.DropForeignKey(
                name: "fk_publications_profiles_profile_id",
                table: "publications");

            migrationBuilder.DropForeignKey(
                name: "fk_saved_posts_profiles_profile_id",
                table: "saved_posts");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "saved_posts",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_saved_posts_profile_id",
                table: "saved_posts",
                newName: "ix_saved_posts_user_id");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "publications",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_publications_profile_id",
                table: "publications",
                newName: "ix_publications_user_id");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "post_likes",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_likes_profile_id",
                table: "post_likes",
                newName: "ix_post_likes_user_id");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "comments",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_comments_profile_id",
                table: "comments",
                newName: "ix_comments_user_id");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "comment_likes",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_comment_likes_profile_id",
                table: "comment_likes",
                newName: "ix_comment_likes_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_comment_likes_users_user_id",
                table: "comment_likes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_comments_users_user_id",
                table: "comments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_followers_users_follower_id",
                table: "followers",
                column: "follower_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_followers_users_following_id",
                table: "followers",
                column: "following_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_likes_users_user_id",
                table: "post_likes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_publications_users_user_id",
                table: "publications",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_saved_posts_users_user_id",
                table: "saved_posts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comment_likes_users_user_id",
                table: "comment_likes");

            migrationBuilder.DropForeignKey(
                name: "fk_comments_users_user_id",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "fk_followers_users_follower_id",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "fk_followers_users_following_id",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "fk_post_likes_users_user_id",
                table: "post_likes");

            migrationBuilder.DropForeignKey(
                name: "fk_publications_users_user_id",
                table: "publications");

            migrationBuilder.DropForeignKey(
                name: "fk_saved_posts_users_user_id",
                table: "saved_posts");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "saved_posts",
                newName: "profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_saved_posts_user_id",
                table: "saved_posts",
                newName: "ix_saved_posts_profile_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "publications",
                newName: "profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_publications_user_id",
                table: "publications",
                newName: "ix_publications_profile_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "post_likes",
                newName: "profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_likes_user_id",
                table: "post_likes",
                newName: "ix_post_likes_profile_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "comments",
                newName: "profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_comments_user_id",
                table: "comments",
                newName: "ix_comments_profile_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "comment_likes",
                newName: "profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_comment_likes_user_id",
                table: "comment_likes",
                newName: "ix_comment_likes_profile_id");

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    profile_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_profiles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "profiles",
                columns: new[] { "id", "bio", "profile_image", "user_id", "username" },
                values: new object[,]
                {
                    { 1, "Lover of vintage style and cozy sweaters.", "https://i.pinimg.com/564x/39/33/f6/3933f64de1724bb67264818810e3f2cb.jpg", 1, "fashionlover123" },
                    { 2, "Streetwear is my passion.", "https://cdn.expertphotography.com/wp-content/uploads/2020/08/social-media-profile-photos.jpg", 2, "Anna" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_profiles_user_id",
                table: "profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_comment_likes_profiles_profile_id",
                table: "comment_likes",
                column: "profile_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_comments_profiles_profile_id",
                table: "comments",
                column: "profile_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_followers_profiles_follower_id",
                table: "followers",
                column: "follower_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_followers_profiles_following_id",
                table: "followers",
                column: "following_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_likes_profiles_profile_id",
                table: "post_likes",
                column: "profile_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_publications_profiles_profile_id",
                table: "publications",
                column: "profile_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_saved_posts_profiles_profile_id",
                table: "saved_posts",
                column: "profile_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
