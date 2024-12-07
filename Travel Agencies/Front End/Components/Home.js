import { useState, useEffect } from "react";
import Visa from "./Visa";
import "../Styles/Home.css";
import AboutUs from "../Components/AboutUs";
var _HttpClient = "https://localhost:7116/api/";

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

  // دالة لإظهار/إخفاء الشريط
  const toggleAboutUs = () => {
    setShowBar((prev) => !prev); // عكس القيمة الحالية
  };

  return (
    <div>
      {/* عرض الفيز باستخدام map */}
      <div>
        {visas.map((visa) => (
          <Visa key={visa.visaId} VisaObj={visa} />
        ))}
      </div>

      {/* زر لعرض أو إخفاء "About Us" */}
      <div className="button-container">
        <button
          onClick={toggleAboutUs}
          style={{ margin: "20px", padding: "10px" }}
        >
          {showBar ? "Hide" : "Show About Us"}
        </button>
      </div>

      {/* إذا كانت حالة showBar صحيحة، يتم عرض "About Us" */}
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
