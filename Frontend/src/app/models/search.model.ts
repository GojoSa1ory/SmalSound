import {UserModel} from "./user.model";
import {TrackModel} from "./track.model";
import {PlaylistModel} from "./playlist.model";

export type SearchModel = {
  user?: UserModel[]
  tracks?: TrackModel[]
  playlists?: PlaylistModel[]
}
