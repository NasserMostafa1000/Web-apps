import { useEffect, useState } from "react";
import "../Styles/AboutUs.css";
const _HttpClient = "https://localhost:7116/api/";

export default function AboutUs() {
  const [orgInfo, setOrgInfo] = useState({
    callNumber: "",
    whastAppNumber: "",
    email: "",
    about: "",
  });
  useEffect(() => {
    const FetchOrgInfo = async () => {
      try {
        const response = await fetch(
          `${_HttpClient}FoundationInfo/FoundationInformation`
        );
        if (response.ok) {
          const data = await response.json();
          setOrgInfo(data); // Assuming the API returns the exact structure
        }
      } catch (error) {
        console.error("Error fetching organization information:", error);
      }
    };

    FetchOrgInfo();
  }, []);
  return (
    <div className="about-us-bar">
      <div className="contact-info">
        <div>
          <p className="title">Call:</p>
          <p>{orgInfo.callNumber}</p>
        </div>
        <div>
          <p className="title">WhatsApp:</p>
          <p>{orgInfo.whastAppNumber}</p>
        </div>
        <div>
          <p className="title">Email:</p>
          <p>{orgInfo.email}</p>
        </div>
        <div>
          <p className="title">About:</p>
          <p>{orgInfo.about}</p>
        </div>
      </div>
    </div>
  );
}
