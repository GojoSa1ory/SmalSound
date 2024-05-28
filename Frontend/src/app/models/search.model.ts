import {UserModel} from "./user.model";
import {TrackModel} from "./track.model";

export type SearchModel = {
  user?: UserModel[]
  tracks?: TrackModel[]
}
