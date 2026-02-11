using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    style_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_styles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "temperature_suitabilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    temperature_suitability_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temperature_suitabilities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    profile_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_type_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_categories_types_type_id",
                        column: x => x.type_id,
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clothing_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    temperature_suitability_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    last_worn_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clothing_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_clothing_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_items_temperature_suitabilities_temperature_suitab",
                        column: x => x.temperature_suitability_id,
                        principalTable: "temperature_suitabilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_items_types_type_id",
                        column: x => x.type_id,
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_items_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    group_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_groups_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    temperature_suitability_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfits", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfits_temperature_suitabilities_temperature_suitability_id",
                        column: x => x.temperature_suitability_id,
                        principalTable: "temperature_suitabilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfits_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    profile_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "clothing_item_seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clothing_item_id = table.Column<int>(type: "integer", nullable: false),
                    season_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clothing_item_seasons", x => x.id);
                    table.ForeignKey(
                        name: "fk_clothing_item_seasons_clothing_items_clothing_item_id",
                        column: x => x.clothing_item_id,
                        principalTable: "clothing_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_item_seasons_seasons_season_id",
                        column: x => x.season_id,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clothing_item_styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clothing_item_id = table.Column<int>(type: "integer", nullable: false),
                    style_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clothing_item_styles", x => x.id);
                    table.ForeignKey(
                        name: "fk_clothing_item_styles_clothing_items_clothing_item_id",
                        column: x => x.clothing_item_id,
                        principalTable: "clothing_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_item_styles_styles_style_id",
                        column: x => x.style_id,
                        principalTable: "styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    dress_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    outfit_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_events_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_events_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_group_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_group_id = table.Column<int>(type: "integer", nullable: false),
                    outfit_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_group_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_group_items_outfit_groups_outfit_group_id",
                        column: x => x.outfit_group_id,
                        principalTable: "outfit_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_group_items_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    clothing_item_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_items_clothing_items_clothing_item_id",
                        column: x => x.clothing_item_id,
                        principalTable: "clothing_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_items_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    season_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_seasons", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_seasons_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_seasons_seasons_season_id",
                        column: x => x.season_id,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    style_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_styles", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_styles_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_styles_styles_style_id",
                        column: x => x.style_id,
                        principalTable: "styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_tags_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "followers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    follower_id = table.Column<int>(type: "integer", nullable: false),
                    following_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_followers", x => x.id);
                    table.ForeignKey(
                        name: "fk_followers_profiles_follower_id",
                        column: x => x.follower_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_followers_profiles_following_id",
                        column: x => x.following_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profile_id = table.Column<int>(type: "integer", nullable: false),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    commenting_options = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publications", x => x.id);
                    table.ForeignKey(
                        name: "fk_publications_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publications_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profile_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comments_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profile_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_likes", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_likes_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_likes_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publication_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publication_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_publication_tags_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publication_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saved_posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profile_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_saved_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_saved_posts_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_saved_posts_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profile_id = table.Column<int>(type: "integer", nullable: false),
                    comment_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_likes", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_likes_comments_comment_id",
                        column: x => x.comment_id,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_likes_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_seasons_clothing_item_id",
                table: "clothing_item_seasons",
                column: "clothing_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_seasons_season_id",
                table: "clothing_item_seasons",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_styles_clothing_item_id",
                table: "clothing_item_styles",
                column: "clothing_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_styles_style_id",
                table: "clothing_item_styles",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_category_id",
                table: "clothing_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_temperature_suitability_id",
                table: "clothing_items",
                column: "temperature_suitability_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_type_id",
                table: "clothing_items",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_user_id",
                table: "clothing_items",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_likes_comment_id",
                table: "comment_likes",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_likes_profile_id",
                table: "comment_likes",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_profile_id",
                table: "comments",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_publication_id",
                table: "comments",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_outfit_id",
                table: "events",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_user_id",
                table: "events",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_followers_follower_id",
                table: "followers",
                column: "follower_id");

            migrationBuilder.CreateIndex(
                name: "ix_followers_following_id",
                table: "followers",
                column: "following_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_event_id",
                table: "notifications",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_group_items_outfit_group_id",
                table: "outfit_group_items",
                column: "outfit_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_group_items_outfit_id",
                table: "outfit_group_items",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_groups_user_id",
                table: "outfit_groups",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_items_clothing_item_id",
                table: "outfit_items",
                column: "clothing_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_items_outfit_id",
                table: "outfit_items",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_seasons_outfit_id",
                table: "outfit_seasons",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_seasons_season_id",
                table: "outfit_seasons",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_styles_outfit_id",
                table: "outfit_styles",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_styles_style_id",
                table: "outfit_styles",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_tags_outfit_id",
                table: "outfit_tags",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_tags_tag_id",
                table: "outfit_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfits_temperature_suitability_id",
                table: "outfits",
                column: "temperature_suitability_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfits_user_id",
                table: "outfits",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_likes_profile_id",
                table: "post_likes",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_likes_publication_id",
                table: "post_likes",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_profiles_user_id",
                table: "profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_publication_tags_publication_id",
                table: "publication_tags",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_tags_tag_id",
                table: "publication_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_outfit_id",
                table: "publications",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_profile_id",
                table: "publications",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_saved_posts_profile_id",
                table: "saved_posts",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_saved_posts_publication_id",
                table: "saved_posts",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_categories_category_id",
                table: "type_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_categories_type_id",
                table: "type_categories",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clothing_item_seasons");

            migrationBuilder.DropTable(
                name: "clothing_item_styles");

            migrationBuilder.DropTable(
                name: "comment_likes");

            migrationBuilder.DropTable(
                name: "followers");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "outfit_group_items");

            migrationBuilder.DropTable(
                name: "outfit_items");

            migrationBuilder.DropTable(
                name: "outfit_seasons");

            migrationBuilder.DropTable(
                name: "outfit_styles");

            migrationBuilder.DropTable(
                name: "outfit_tags");

            migrationBuilder.DropTable(
                name: "post_likes");

            migrationBuilder.DropTable(
                name: "publication_tags");

            migrationBuilder.DropTable(
                name: "saved_posts");

            migrationBuilder.DropTable(
                name: "type_categories");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "outfit_groups");

            migrationBuilder.DropTable(
                name: "clothing_items");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "styles");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "types");

            migrationBuilder.DropTable(
                name: "outfits");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "temperature_suitabilities");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
