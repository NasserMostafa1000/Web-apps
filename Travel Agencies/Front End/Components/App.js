import NavBar from "./Components/NavBar";
import { Routes, Route, Navigate } from "react-router-dom";
import UpdateImages from "./Components/UploadAndUpdateImages";
import Home from "./Components/Home";
import MyProfile from "./Components/MyProfile";
import "./Styles/CompanyName.css";
import Orders from "./Components/Orders";
import Login from "./Components/Log in";
import SignUp from "./Components/Signup";
import PayWithVisa from "./Components/PayWithVisa";
import OurPrivacyAndTerms from "./Components/ourPrivacyAndTerms";
export default function App() {
  return (
    <div>
      <NavBar />
      <div className="CompanyName" style={{ marginTop: "50px" }}>
        <span style={{ fontSize: "40px" }}>💼</span>
        Mazaya AL Khair{" "}
        <span style={{ fontSize: "40px", color: "orange" }}>✈</span>
      </div>

      <Routes>
        <Route path="/" element={<Navigate to="/Home" />} />
        <Route path="/Home" element={<Home />} />
        <Route path="/Signup" element={<SignUp />} />
        <Route path="/OurPrivacyAndTerms" element={<OurPrivacyAndTerms />} />
        <Route path="/PayWithVisa" element={<PayWithVisa />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/UpdateImages" element={<UpdateImages />} />
        <Route path="/LogOut" element={<Login />} />
        <Route path="/MyProfile" element={<MyProfile />} />
        <Route path="/Orders" element={<Orders />} />
      </Routes>
    </div>
  );
}
