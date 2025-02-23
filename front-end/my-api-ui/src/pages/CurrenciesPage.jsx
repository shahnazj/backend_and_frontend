import React, { useState } from "react";
import { fetchData, createData, deleteData } from "../api/api"; 

const CurrenciesPage = () => {
  const [data, setData] = useState([]);
  const [name, setCurrencyName] = useState("");

  const handleFetch = async () => {
    const result = await fetchData("Currencies");
    setData(result);
  };

  const handleCreate = async () => {
    if (!name.trim()) {
      alert("Currency name is required");
      return;
    }

    try {
      const newCurrency = await createData("Currencies", { name });
      setData([...data, newCurrency]); 
      setCurrencyName("");
    } catch (error) {
      alert("Failed to create currency");
    }
  };


  const handleDelete = async (id) => {
    try {
      await deleteData("Currencies", id);
      setData(data.filter((currency) => currency.id !== id)); 
    } catch (error) {
      console.error("Failed to delete currency", error);
      alert("Failed to delete currency. Please check the console for details.");
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Currencies</h2>

      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Currencies
      </button>

      <div className="mb-4">
        <input
          type="text"
          value={name}
          onChange={(e) => setCurrencyName(e.target.value)}
          placeholder="Enter Currency Name"
          className="border p-2 rounded mr-2"
        />
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Currency
        </button>
      </div>

      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">Name</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((currency) => (
            <tr key={currency.id} className="border">
              <td className="border p-2">{currency.id}</td>
              <td className="border p-2">{currency.name}</td>
              <td className="border p-2">
                <button
                  onClick={() => handleDelete(currency.id)}
                  className="bg-red-500 text-white px-2 py-1 rounded"
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default CurrenciesPage;
