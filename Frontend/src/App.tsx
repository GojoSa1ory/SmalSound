import { useState } from "react";
import "./App.css";
import axios from "axios";

function App() {
    const [TrackName, setTrackName] = useState<string>("");
    const [ArtistName, setArtistName] = useState<string>("");
    const [Image, setImage] = useState<File | null>();
    const [Track, setTrack] = useState<File | null>();
    const [AuditionCount, setAuditionCount] = useState<string>("");
    const [data, setData] = useState();

    const getImage = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedFile = e.target.files?.[0];
        setImage(selectedFile || null);
    };

    const getTrack = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedFile = e.target.files?.[0];
        setTrack(selectedFile || null);
    };

    const sendData = async (e: any) => {
        e.preventDefault();
        if (!Image || !Track) {
            console.error("No file selected.");
            return;
        }

        const formData = new FormData();
        formData.append("image", Image);
        formData.append("track", Track);
        formData.append("trackName", TrackName);
        formData.append("auditionCount", AuditionCount);
        formData.append("artistName", ArtistName);
        formData.append("userId", "4");

        await axios
            .post("http://localhost:5098/api/file/TrackUpload/add", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            })
            .then((data) => {
                setData(data.data.data.track);
            });
    };

    return (
        <>
            <form>
                <p>Track name</p>
                <input
                    type="text"
                    value={TrackName}
                    onChange={(e) => setTrackName(e.target.value)}
                />
                <p>Artist name</p>
                <input
                    type="text"
                    value={ArtistName}
                    onChange={(e) => setArtistName(e.target.value)}
                />
                <p>AuditionCount</p>
                <input
                    type="text"
                    value={AuditionCount}
                    onChange={(e) => setAuditionCount(e.target.value)}
                />
                <p>Image</p>
                <input type="file" onChange={(e) => getImage(e)} />
                <p>Track</p>
                <input type="file" onChange={(e) => getTrack(e)} />

                <button onClick={(e) => sendData(e)}>Send data</button>

                <audio controls src={data}></audio>

                {/* <img src={data}/> */}
            </form>
        </>
    );
}

export default App;
