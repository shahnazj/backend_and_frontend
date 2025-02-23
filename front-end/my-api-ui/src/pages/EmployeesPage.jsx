

import React, { useState, useEffect } from "react";
import { fetchData, createData, updateData, deleteData } from "../api/api";

const EmployeesPage = () => {
  const [data, setData] = useState([]);
  const [projects, setProjects] = useState({});
  const [editingId, setEditingId] = useState(null);
  const [editValues, setEditValues] = useState({});
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [roleId, setRoleId] = useState("");
  const [roles, setRoles] = useState([]);

  useEffect(() => {
    const fetchRoles = async () => {
      const rolesData = await fetchData("Roles");
      setRoles(rolesData);
    };
    fetchRoles();
  }, []);

  const handleFetch = async () => {
    const result = await fetchData("Employees");
    setData(result);
    result.forEach((employee) => fetchEmployeeProjects(employee.id));
  };

  const fetchEmployeeProjects = async (employeeId) => {
    try {
      const projectData = await fetchData(`Projects/employee/${employeeId}`);
      setProjects((prevProjects) => ({
        ...prevProjects,
        [employeeId]: projectData.map((project) => project.name),
      }));
    } catch (error) {
      console.error(
        `Failed to fetch projects for employee ${employeeId}`,
        error
      );
    }
  };

  const handleCreate = async () => {
    if (!firstName.trim() || !lastName.trim() || !roleId) {
      alert("All fields are required");
      return;
    }

    try {
      const newEmployee = await createData("Employees", {
        firstName,
        lastName,
        roleId: parseInt(roleId, 10),
      });
      setData([...data, newEmployee]);
      setFirstName("");
      setLastName("");
      setRoleId("");
    } catch (error) {
      alert("Failed to create employee");
    }
  };

  const handleEdit = (employee) => {
    setEditingId(employee.id);
    setEditValues({ ...employee });
  };

  const handleChange = (e) => {
    setEditValues({ ...editValues, [e.target.name]: e.target.value });
  };

  const handleUpdate = async () => {
    try {
      const updatedData = {
        id: editingId,
        firstName: editValues.firstName,
        lastName: editValues.lastName,
        roleId: parseInt(editValues.roleId, 10),
      };

      await updateData("Employees", editingId, updatedData);
      setEditingId(null);
      handleFetch();
    } catch (error) {
      console.error("Failed to update employee", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteData("Employees", id);
      setData(data.filter((employee) => employee.id !== id));
    } catch (error) {
      console.error("Failed to delete employee", error);
      alert("Failed to delete employee. Please check the console for details.");
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Employees</h2>
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Employees
      </button>

      {/* Create Form */}
      <div className="mb-4">
        <input
          type="text"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          placeholder="Enter First Name"
          className="border p-2 rounded mr-2"
        />
        <input
          type="text"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          placeholder="Enter Last Name"
          className="border p-2 rounded mr-2"
        />
        <select
          value={roleId}
          onChange={(e) => setRoleId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Role</option>
          {roles.map((role) => (
            <option key={role.id} value={role.id}>
              {role.roleName}
            </option>
          ))}
        </select>
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Employee
        </button>
      </div>

      {/* Table */}
      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">First Name</th>
            <th className="border p-2">Last Name</th>
            <th className="border p-2">Role</th>
            <th className="border p-2">Projects</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((employee) => (
            <tr key={employee.id} className="border">
              <td className="border p-2">{employee.id}</td>
              {editingId === employee.id ? (
                <>
                  <td className="border p-2">
                    <input
                      name="firstName"
                      value={editValues.firstName}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <input
                      name="lastName"
                      value={editValues.lastName}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <select
                      name="roleId"
                      value={editValues.roleId}
                      onChange={handleChange}
                      className="w-full"
                    >
                      {roles.map((role) => (
                        <option key={role.id} value={role.id}>
                          {role.roleName}
                        </option>
                      ))}
                    </select>
                  </td>
                  <td className="border p-2">
                    {projects[employee.id]?.join(", ") || "No Projects"}
                  </td>
                  <td className="border p-2">
                    <button
                      onClick={handleUpdate}
                      className="bg-green-500 text-white px-2 py-1 rounded mr-2"
                    >
                      Save
                    </button>
                    <button
                      onClick={() => setEditingId(null)}
                      className="bg-red-500 text-white px-2 py-1 rounded"
                    >
                      Cancel
                    </button>
                  </td>
                </>
              ) : (
                <>
                  <td className="border p-2">{employee.firstName}</td>
                  <td className="border p-2">{employee.lastName}</td>
                  <td className="border p-2">
                    {roles.find((r) => r.id === employee.roleId)?.roleName ||
                      "Unknown"}
                  </td>
                  <td className="border p-2">
                    {projects[employee.id]?.join(", ") || "No Projects"}
                  </td>
                  <td className="border p-2">
                    <button
                      onClick={() => handleEdit(employee)}
                      className="bg-yellow-500 text-white px-2 py-1 rounded mr-2"
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(employee.id)}
                      className="bg-red-500 text-white px-2 py-1 rounded"
                    >
                      Delete
                    </button>
                  </td>
                </>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default EmployeesPage;
