import {Component, OnInit, WritableSignal} from '@angular/core';
import {GenreModel, SetTrackModel} from "../../models/track.model";
import {TrackService} from "../../service/track.service";


@Component({
  selector: 'app-upload-track',
  standalone: true,
  imports: [],
  templateUrl: './upload-track.component.html',
  styleUrl: './upload-track.component.scss'
})
export class UploadTrackComponent implements OnInit{

  track: SetTrackModel = {
    track: null,
    trackImage: null,
    name: "",
    genreId: null
  }

  genres!: WritableSignal<GenreModel[] | []>

  selectedImage!: any
  selectedTrack!: any
  imageUrl: string | ArrayBuffer | null = null;


  constructor(private trackService: TrackService) {
    this.genres = this.trackService.genres
  }

  ngOnInit(): void {
    this.trackService.getGenres()
  }

  setTrackImage (event: any) {
    this.selectedImage = event.target.files[0]
    if (this.selectedImage) {
      const reader = new FileReader();
      reader.onload = (e) => {
        // @ts-ignore
        this.imageUrl = e.target?.result;
      };
      reader.readAsDataURL(this.selectedImage);
    }
  }

  setTrackName(event: any) {
    this.track.name = event.target.value
  }

  setTrack(event: any) {
    this.selectedTrack = event.target.files[0]
  }

  setGenre (event: any) {
    this.track.genreId = event.target.value;
    console.log(this.track.genreId)
  }

  uploadTrack (event: any) {
    console.log(this.track)
    const request = new FormData()
    request.append("Name", this.track.name)
    request.append("TrackImage", this.selectedImage)
    request.append("Track", this.selectedTrack)
    request.append("GenreId", String(this.track.genreId))
    console.log(request)
    event.preventDefault()
    this.trackService.uploadTrack(request)
  }
}
