import { useState } from "react";
var _HttpClient = "https://localhost:7116/api/";

export default function SignUp() {
  const [Firstname, setFirstName] = useState("");
  const [Midname, setMidName] = useState("");
  const [Lastname, setLastName] = useState("");
  const [Phone, setPhone] = useState("");
  const [Email, SetEmail] = useState("");
  const [Password, setPassword] = useState("");
  const [repeatPassword, setrepeatPassword] = useState("");
  const [notification, setNotification] = useState("");

  const OnSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(`${_HttpClient}Clients/PostNewClient`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          firstName: Firstname,
          midName: Midname,
          lastName: Lastname,
          email: Email,
          password: Password,
          phoneNumber: Phone,
        }),
      });

      if (!response.ok) {
        // قراءة محتوى الاستجابة عند وجود خطأ
        const errorData = await response.json();
        throw new Error(errorData.errorMessage || "Something went wrong.");
      }

      const data = await response.json();
      console.log(Number(data.id));
      localStorage.setItem("Id", Number(data.id));
      setNotification("Done Successfully");
      setTimeout(() => {
        setNotification("");
      }, 2000);
      window.location.pathname = "/UpdateImages";
    } catch (error) {
      // عرض رسالة الخطأ القادمة من السيرفر
      console.log(error.message); // تحقق مما إذا كانت الرسالة تصل هنا
      setNotification(error.message); // عرض الخطأ في الإشعار
      setTimeout(() => {
        setNotification("");
      }, 3000);
    }
  };

  return (
    <div className="SignUp">
      {notification && (
        <div
          className={
            notification === "Done Successfully"
              ? "Goodnotification"
              : "Badnotification"
          }
        >
          {notification}
        </div>
      )}{" "}
      {/* إشعار النجاح */}
      <form onSubmit={OnSubmit}>
        <h1>Sign Up 🔑</h1>

        <input
          type="text"
          id="FirstName"
          value={Firstname}
          onChange={(e) => setFirstName(e.target.value)}
          placeholder="First Name"
          required
        />

        <input
          type="text"
          id="MidName"
          value={Midname}
          onChange={(e) => setMidName(e.target.value)}
          placeholder="Mid Name"
          required
        />

        <input
          type="text"
          id="LastName"
          value={Lastname}
          onChange={(e) => setLastName(e.target.value)}
          placeholder="Last Name"
          required
        />

        <input
          type="text"
          id="Phone"
          value={Phone}
          onChange={(e) => setPhone(e.target.value)}
          placeholder="Phone Number"
          required
        />

        <input
          type="email"
          id="Email"
          value={Email}
          onChange={(e) => SetEmail(e.target.value)}
          placeholder="example@gmail.com"
          required
        />
        {!Email.endsWith("@gmail.com") && Email !== "" && (
          <p style={{ color: "red" }}>Email should end with @gmail.com ❌</p>
        )}

        <input
          type="password"
          id="Password"
          value={Password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Password"
          required
        />
        {Password !== "" && Password.length < 6 && (
          <p style={{ color: "red" }}>
            Password Length Must be more than 6 chars ❌
          </p>
        )}

        <input
          type="password"
          id="RepeatPassword"
          value={repeatPassword}
          onChange={(e) => setrepeatPassword(e.target.value)}
          placeholder="Repeat Password"
          required
        />
        {Password !== repeatPassword && (
          <p style={{ color: "red" }}>Password does not match ❌</p>
        )}

        <button type="submit">Sign Up</button>
      </form>
    </div>
  );
}
