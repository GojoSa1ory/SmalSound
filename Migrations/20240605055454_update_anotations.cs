using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPCourseWork.Migrations
{
    /// <inheritdoc />
    public partial class update_anotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Users_UserId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteModelTrackModel_Favorite_FavoriteId",
                table: "FavoriteModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteModelTrackModel_Tracks_TracksId",
                table: "FavoriteModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistModelTrackModel_Playlists_PlaylistId",
                table: "PlaylistModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistModelTrackModel_Tracks_TracksId",
                table: "PlaylistModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UserId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_SubscribedToId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Genre_GenreId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Users_UserId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "genre");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "favorite");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Tracks",
                newName: "track");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "subscrription");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "role");

            migrationBuilder.RenameTable(
                name: "Playlists",
                newName: "playlist");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "genre",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "genre",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "favorite",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_UserId",
                table: "favorite",
                newName: "IX_favorite_UserId");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "user",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "user",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "user",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "user",
                newName: "image_path");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "user",
                newName: "IX_user_RoleId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "track",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "track",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "track",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "track",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "TrackImage",
                table: "track",
                newName: "image_path");

            migrationBuilder.RenameColumn(
                name: "Track",
                table: "track",
                newName: "track_path");

            migrationBuilder.RenameColumn(
                name: "ListenCount",
                table: "track",
                newName: "listen_count");

            migrationBuilder.RenameIndex(
                name: "IX_Tracks_UserId",
                table: "track",
                newName: "IX_track_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tracks_GenreId",
                table: "track",
                newName: "IX_track_GenreId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "subscrription",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SubscriberId",
                table: "subscrription",
                newName: "subscriber_id");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_SubscribedToId",
                table: "subscrription",
                newName: "IX_subscrription_SubscribedToId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "role",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "role",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "playlist",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "playlist",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "playlist",
                newName: "imagePath");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserId",
                table: "playlist",
                newName: "IX_playlist_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_genre",
                table: "genre",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favorite",
                table: "favorite",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_track",
                table: "track",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subscrription",
                table: "subscrription",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role",
                table: "role",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_playlist",
                table: "playlist",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_favorite_user_UserId",
                table: "favorite",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteModelTrackModel_favorite_FavoriteId",
                table: "FavoriteModelTrackModel",
                column: "FavoriteId",
                principalTable: "favorite",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteModelTrackModel_track_TracksId",
                table: "FavoriteModelTrackModel",
                column: "TracksId",
                principalTable: "track",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_playlist_user_UserId",
                table: "playlist",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistModelTrackModel_playlist_PlaylistId",
                table: "PlaylistModelTrackModel",
                column: "PlaylistId",
                principalTable: "playlist",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistModelTrackModel_track_TracksId",
                table: "PlaylistModelTrackModel",
                column: "TracksId",
                principalTable: "track",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subscrription_user_SubscribedToId",
                table: "subscrription",
                column: "SubscribedToId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_track_genre_GenreId",
                table: "track",
                column: "GenreId",
                principalTable: "genre",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_track_user_UserId",
                table: "track",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_RoleId",
                table: "user",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favorite_user_UserId",
                table: "favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteModelTrackModel_favorite_FavoriteId",
                table: "FavoriteModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteModelTrackModel_track_TracksId",
                table: "FavoriteModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_playlist_user_UserId",
                table: "playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistModelTrackModel_playlist_PlaylistId",
                table: "PlaylistModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistModelTrackModel_track_TracksId",
                table: "PlaylistModelTrackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_subscrription_user_SubscribedToId",
                table: "subscrription");

            migrationBuilder.DropForeignKey(
                name: "FK_track_genre_GenreId",
                table: "track");

            migrationBuilder.DropForeignKey(
                name: "FK_track_user_UserId",
                table: "track");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_RoleId",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_genre",
                table: "genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favorite",
                table: "favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_track",
                table: "track");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subscrription",
                table: "subscrription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_playlist",
                table: "playlist");

            migrationBuilder.RenameTable(
                name: "genre",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "favorite",
                newName: "Favorite");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "track",
                newName: "Tracks");

            migrationBuilder.RenameTable(
                name: "subscrription",
                newName: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "playlist",
                newName: "Playlists");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Genre",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Genre",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Favorite",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_favorite_UserId",
                table: "Favorite",
                newName: "IX_Favorite_UserId");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "image_path",
                table: "Users",
                newName: "ProfilePicture");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "createdAt");

            migrationBuilder.RenameIndex(
                name: "IX_user_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tracks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tracks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Tracks",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "track_path",
                table: "Tracks",
                newName: "Track");

            migrationBuilder.RenameColumn(
                name: "listen_count",
                table: "Tracks",
                newName: "ListenCount");

            migrationBuilder.RenameColumn(
                name: "image_path",
                table: "Tracks",
                newName: "TrackImage");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Tracks",
                newName: "createdAt");

            migrationBuilder.RenameIndex(
                name: "IX_track_UserId",
                table: "Tracks",
                newName: "IX_Tracks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_track_GenreId",
                table: "Tracks",
                newName: "IX_Tracks_GenreId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Subscriptions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "subscriber_id",
                table: "Subscriptions",
                newName: "SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_subscrription_SubscribedToId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_SubscribedToId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Playlists",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Playlists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "imagePath",
                table: "Playlists",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_playlist_UserId",
                table: "Playlists",
                newName: "IX_Playlists_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Users_UserId",
                table: "Favorite",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteModelTrackModel_Favorite_FavoriteId",
                table: "FavoriteModelTrackModel",
                column: "FavoriteId",
                principalTable: "Favorite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteModelTrackModel_Tracks_TracksId",
                table: "FavoriteModelTrackModel",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistModelTrackModel_Playlists_PlaylistId",
                table: "PlaylistModelTrackModel",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistModelTrackModel_Tracks_TracksId",
                table: "PlaylistModelTrackModel",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UserId",
                table: "Playlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_SubscribedToId",
                table: "Subscriptions",
                column: "SubscribedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Genre_GenreId",
                table: "Tracks",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Users_UserId",
                table: "Tracks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
