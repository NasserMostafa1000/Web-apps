import { useState, useEffect } from "react";
import Visa from "./Visa";
import "../Styles/Home.css";
import AboutUs from "../Components/AboutUs";
var _HttpClient = "https://ramysalama2000-001-site1.otempurl.com/api/";

export default function Home() {
  const [visas, setVisas] = useState([]);
  const [showBar, setShowBar] = useState(false); // حالة لتحديد إذا كان الشريط يظهر

  useEffect(() => {
    const fetchVisas = async () => {
      try {
        const response = await fetch(`${_HttpClient}Visas/GetAllVisas`);

        if (!response.ok) {
          throw new Error(`Failed to fetch visas: ${response.statusText}`);
        }

        const data = await response.json();
        setVisas(data);
      } catch (error) {
        console.error("Error fetching visas:", error.message);
      }
    };

    fetchVisas();
  }, []);

  const toggleAboutUs = () => {
    setShowBar((prev) => !prev); // عكس القيمة الحالية
  };

  return (
    <div>
      <div>
        {visas.map(
          (visa) =>
            visa.moreDetails !== "غير متوفر" && (
              <Visa key={visa.visaId} VisaObj={visa} />
            )
        )}
      </div>

      <div className="button-container">
        <button
          onClick={toggleAboutUs}
          style={{ margin: "20px", padding: "10px" }}
        >
          {showBar ? "Hide" : "Show About Us"}
        </button>
      </div>

      {showBar && (
        <div className="about-us-container">
          <AboutUs />
        </div>
      )}

      {/* العنصر الذي سيتم مراقبته عندما يصل المستخدم إلى أسفل الصفحة */}
      <div id="bottom" style={{ height: "1px" }}></div>
    </div>
  );
}
