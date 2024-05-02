import {UserModel} from "./UserModel";

export type TrackModel = {
  id: number,
  name: string,
  trackImage: string,
  track: string,
  isPlaying: boolean
  user: UserModel
}
