import React from "react";
import "../Styles/Visa.css"; // يمكنك إضافة ملف CSS لتنسيق المكونات

export default function Visa({ VisaObj }) {
  if (VisaObj.moreDetails === "غير متوفر") {
    return null; // لا يتم عرض الفيز إذا كانت التفاصيل غير متوفرة
  }

  function handleIsuueClick(Price, id) {
    console.log("Issue ID " + id);
    console.log("Issue Price" + Price);
  }

  function handleRenewClick(Price, id) {
    console.log("Issue ID " + id);
    console.log("Issue Price" + Price);
  }

  return (
    <div className="visa-container">
      <div className="visa-image">
        <img src={VisaObj.imagePath} alt={VisaObj.name} />
      </div>
      <div className="visa-details">
        <h2 className="visa-name"> {VisaObj.name}</h2>
        <div className="visa-info">
          <div className="visa-price">
            <span className="label">Issuance Price:</span>
            <span className="amount"> {VisaObj.issuancePrice} AED</span>
          </div>
          <div className="visa-price">
            <span className="label">Renewal Price:</span>
            <span className="amount"> {VisaObj.renewalPrice} AED</span>
          </div>
          <div className="visa-duration">
            <span className="label">Visa Duration:</span>
            <span className="duration">{VisaObj.period} Month/s</span>
          </div>
        </div>
        {/* قسم معلومات إضافية عن الفيزه */}
        <div className="visa-extra-info">
          <h3 className="extra-info-title">Visa Information</h3>
          <p className="extra-info-description">{VisaObj.moreDetails}</p>
        </div>
        <div className="visa-buttons">
          <button
            className="visa-button issue-button"
            onClick={() =>
              handleIsuueClick(VisaObj.issuancePrice, VisaObj.visaId)
            }
          >
            Issue Visa
          </button>
          <button
            className="visa-button renew-button"
            onClick={() =>
              handleRenewClick(VisaObj.renewalPrice, VisaObj.visaId)
            }
          >
            Renew Visa
          </button>
        </div>
      </div>
    </div>
  );
}
