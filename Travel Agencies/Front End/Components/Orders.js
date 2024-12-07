import { useEffect, useState } from "react";
import OrderCom from "./OrderCom";
import "../Styles/Orders.css";

const httpClient = "https://localhost:7116/api/";

export default function Orders() {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    console.log("useEffect triggered");
    const fetchOrders = async () => {
      const token = localStorage.getItem("Token");
      const clientId = Number(localStorage.getItem("Id"));

      if (!token || isNaN(clientId)) {
        setError("Missing or invalid Token/ClientId");
        setLoading(false);
        return;
      }

      console.log("Token:", token);
      console.log("ClientId:", clientId);

      try {
        const response = await fetch(
          `${httpClient}Orders/FindOrdersBelongsToSpecificClient`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              token,
              clientId,
            }),
          }
        );

        if (response.ok) {
          const data = await response.json();
          console.log("Fetched orders:", data);
          setOrders(data);
        } else {
          setError(`Failed to fetch orders. Status: ${response.status}`);
        }
      } catch (err) {
        setError("An error occurred while fetching orders.");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, []);

  if (loading) {
    return <p>Loading orders...</p>;
  }

  if (error) {
    return <p className="error-message">{error}</p>;
  }

  if (orders.length === 0) {
    return <p>No orders found.</p>;
  }

  return (
    <div className="Order-list">
      {orders.map((order) => (
        <OrderCom key={order.orderID} orderObj={order} />
      ))}
    </div>
  );
}
