import {UserModel} from "./user.model";

export type TrackModel =  {
  id: number
  name: string
  trackImage: string
  track: string
  isPlaying: boolean
  user: UserModel
};
