import {TrackModel} from "./track.model";
import {UserModel} from "./user.model";

export type FavoriteModel = {
  id: number,
  tracks: TrackModel[],
  user: UserModel
}
