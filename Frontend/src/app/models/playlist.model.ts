import {TrackModel} from "./track.model";
import {UserModel} from "./user.model";

export type PlaylistModel = {
  id: number
  name: string
  image: string
  tracks: TrackModel[]
  user: UserModel
  createdAt: string
  updatedAt: string
}

export type UpdatePlaylistModel = {
  name?: string
  image?: string
  tracks?: TrackModel[]
  user?: UserModel
  createdAt?: string
  updatedAt?: string
}
