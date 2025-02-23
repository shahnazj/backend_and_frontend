import React, { useState } from "react";
import { fetchData, createData, deleteData } from "../api/api";
import Table from "../components/Table";

const StatusTypesPage = () => {
  const [data, setData] = useState([]);
  const [statusName, setStatusName] = useState("");

  const handleFetch = async () => {
    const result = await fetchData("StatusType");
    setData(result);
  };

  const handleCreate = async () => {
    if (!statusName.trim()) {
      alert("Status name is required");
      return;
    }

    try {
      const newStatus = await createData("StatusType", { status: statusName });
      setData([...data, newStatus]);
      setStatusName("");
    } catch (error) {
      alert("Failed to create status type");
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteData("StatusType", id);
      setData(data.filter((status) => status.id !== id));
    } catch (error) {
      console.error("Failed to delete status type", error);
      alert(
        "Failed to delete status type. Please check the console for details."
      );
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Status Types</h2>

      {/* Fetch Button */}
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Status Types
      </button>

      {/* Create Status Type Form */}
      <div className="mb-4">
        <input
          type="text"
          value={statusName}
          onChange={(e) => setStatusName(e.target.value)}
          placeholder="Enter Status Name"
          className="border p-2 rounded mr-2"
        />
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Status Type
        </button>
      </div>

      {/* Table to Show Status Types */}
      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">Status Name</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((status) => (
            <tr key={status.id} className="border">
              <td className="border p-2">{status.id}</td>
              <td className="border p-2">{status.status}</td>
              <td className="border p-2">
                <button
                  onClick={() => handleDelete(status.id)}
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

export default StatusTypesPage;
