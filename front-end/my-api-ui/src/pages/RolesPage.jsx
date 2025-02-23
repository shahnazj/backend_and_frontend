import React, { useState } from "react";
import { fetchData, createData, deleteData } from "../api/api";
import Table from "../components/Table";

const RolesPage = () => {
  const [data, setData] = useState([]);
  const [roleName, setRoleName] = useState("");

  const handleFetch = async () => {
    const result = await fetchData("Roles");
    setData(result);
  };

  const handleCreate = async () => {
    if (!roleName.trim()) {
      alert("Role name is required");
      return;
    }

    try {
      const newRole = await createData("Roles", { roleName });
      setData([...data, newRole]); 
      setRoleName(""); 
    } catch (error) {
      alert("Failed to create role");
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteData("Roles", id); 
      setData(data.filter((role) => role.id !== id)); 
    } catch (error) {
      console.error("Failed to delete role", error);
      alert("Failed to delete role. Please check the console for details.");
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Roles</h2>

      {/* Fetch Roles Button */}
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Roles
      </button>

      {/* Create Role Form */}
      <div className="mb-4">
        <input
          type="text"
          value={roleName}
          onChange={(e) => setRoleName(e.target.value)}
          placeholder="Enter Role Name"
          className="border p-2 rounded mr-2"
        />
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Role
        </button>
      </div>

      {/* Table to Show Roles */}
      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">Role Name</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((role) => (
            <tr key={role.id} className="border">
              <td className="border p-2">{role.id}</td>
              <td className="border p-2">{role.roleName}</td>
              <td className="border p-2">
                <button
                  onClick={() => handleDelete(role.id)}
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

export default RolesPage;
