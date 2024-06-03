import { UserModel } from "./user.model";

export type TrackModel = {
    id: number;
    name: string;
    trackImage: string;
    track: string;
    isPlaying: boolean;
    user: UserModel;
    listenCount: number;
};

export type SetTrackModel = {
    name: string;
    trackImage: FormData | null;
    track: FormData | null;
    genreId: number | null
};

export type GenreModel = {
    id: number;
    name: string;
};
