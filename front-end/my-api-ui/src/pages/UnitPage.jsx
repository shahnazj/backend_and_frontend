import React, { useState } from "react";
import { fetchData, createData, deleteData } from "../api/api";
import Table from "../components/Table";

const UnitsPage = () => {
  const [data, setData] = useState([]);
  const [name, setUnitName] = useState("");

  const handleFetch = async () => {
    const result = await fetchData("Units");
    setData(result);
  };

  const handleCreate = async () => {
    if (!name.trim()) {
      alert("Unit name is required");
      return;
    }

    try {
      const newUnit = await createData("Units", { name });
      setData([...data, newUnit]); 
      setUnitName("");
    } catch (error) {
      alert("Failed to create unit");
    }
  };


  const handleDelete = async (id) => {
    try {
      await deleteData("Units", id); 
      setData(data.filter((unit) => unit.id !== id)); 
    } catch (error) {
      console.error("Failed to delete unit", error);
      alert("Failed to delete unit. Please check the console for details.");
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Units</h2>

      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Units
      </button>

      <div className="mb-4">
        <input
          type="text"
          value={name}
          onChange={(e) => setUnitName(e.target.value)}
          placeholder="Enter Unit Name"
          className="border p-2 rounded mr-2"
        />
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Unit
        </button>
      </div>

      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">Unit Name</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((unit) => (
            <tr key={unit.id} className="border">
              <td className="border p-2">{unit.id}</td>
              <td className="border p-2">{unit.name}</td>
              <td className="border p-2">
                <button
                  onClick={() => handleDelete(unit.id)}
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

export default UnitsPage;
