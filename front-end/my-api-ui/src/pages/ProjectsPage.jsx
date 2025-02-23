import React, { useState, useEffect } from "react";
import { fetchData, createData, updateData, deleteData } from "../api/api";

const ProjectsPage = () => {
  const [data, setData] = useState([]);
  const [editingId, setEditingId] = useState(null);
  const [editValues, setEditValues] = useState({});
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [statusId, setStatusId] = useState("");
  const [customerId, setCustomerId] = useState("");
  const [employeeId, setEmployeeId] = useState("");
  const [serviceId, setServiceId] = useState("");
  const [statuses, setStatuses] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [employees, setEmployees] = useState([]);
  const [services, setServices] = useState([]);

  useEffect(() => {
    const fetchDropdownData = async () => {
      setStatuses(await fetchData("StatusType"));
      setCustomers(await fetchData("Customers"));
      setEmployees(await fetchData("Employees"));
      setServices(await fetchData("Services"));
    };
    fetchDropdownData();
  }, []);

  const handleFetch = async () => {
    const result = await fetchData("Projects");
    setData(result);
  };

  const handleCreate = async () => {
    if (
      !name.trim() ||
      !description.trim() ||
      !startDate ||
      !endDate ||
      !statusId ||
      !customerId ||
      !employeeId ||
      !serviceId
    ) {
      alert("All fields are required");
      return;
    }

    try {
      const newProject = await createData("Projects", {
        name,
        description,
        startDate,
        endDate,
        statusId: parseInt(statusId, 10),
        customerId: parseInt(customerId, 10),
        employeeId: parseInt(employeeId, 10),
        serviceId: parseInt(serviceId, 10),
      });
      setData([...data, newProject]);
      setName("");
      setDescription("");
      setStartDate("");
      setEndDate("");
      setStatusId("");
      setCustomerId("");
      setEmployeeId("");
      setServiceId("");
    } catch (error) {
      alert("Failed to create project");
    }
  };

  const handleEdit = (project) => {
    setEditingId(project.id);
    setEditValues({ ...project });
  };

  const handleChange = (e) => {
    setEditValues({ ...editValues, [e.target.name]: e.target.value });
    console.log("Editing:", { ...editValues, [e.target.name]: e.target.value });
  };

  const handleUpdate = async () => {
    try {
      const updatedData = {
        id: editingId,
        name: editValues.name,
        description: editValues.description,
        startDate: editValues.startDate,
        endDate: editValues.endDate,
        statusId: parseInt(editValues.statusId, 10),
        customerId: parseInt(editValues.customerId, 10),
        employeeId: parseInt(editValues.employeeId, 10),
        serviceId: parseInt(editValues.serviceId, 10),
      };

      console.log("Sending update:", updatedData);

      await updateData("Projects", editingId, updatedData);
      setEditingId(null);
      handleFetch();
    } catch (error) {
      console.error("Failed to update project", error);
    }
  };


  const handleDelete = async (id) => {
    try {
      await deleteData("Projects", id); 
      setData(data.filter((project) => project.id !== id));
    } catch (error) {
      console.error("Failed to delete project", error);
      alert("Failed to delete project. Please check the console for details.");
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Projects</h2>
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Projects
      </button>
      <div className="mb-4">
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Enter Project Name"
          className="border p-2 rounded mr-2"
        />
        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Enter Description"
          className="border p-2 rounded mr-2"
        />
        <input
          type="datetime-local"
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
          className="border p-2 rounded mr-2"
        />
        <input
          type="datetime-local"
          value={endDate}
          onChange={(e) => setEndDate(e.target.value)}
          className="border p-2 rounded mr-2"
        />
        <select
          value={statusId}
          onChange={(e) => setStatusId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Status</option>
          {statuses.map((status) => (
            <option key={status.id} value={status.id}>
              {status.status}
            </option>
          ))}
        </select>
        <select
          value={customerId}
          onChange={(e) => setCustomerId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Customer</option>
          {customers.map((customer) => (
            <option key={customer.id} value={customer.id}>
              {customer.name}
            </option>
          ))}
        </select>
        <select
          value={employeeId}
          onChange={(e) => setEmployeeId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Employee</option>
          {employees.map((employee) => (
            <option key={employee.id} value={employee.id}>
              {employee.firstName} {employee.lastName}
            </option>
          ))}
        </select>
        <select
          value={serviceId}
          onChange={(e) => setServiceId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Service</option>
          {services.map((service) => (
            <option key={service.id} value={service.id}>
              {service.serviceName}
            </option>
          ))}
        </select>
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Project
        </button>
      </div>

      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">Name</th>
            <th className="border p-2">Description</th>
            <th className="border p-2">Start Date</th>
            <th className="border p-2">End Date</th>
            <th className="border p-2">Status</th>
            <th className="border p-2">Customer</th>
            <th className="border p-2">Employee</th>
            <th className="border p-2">Service</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((project) => (
            <tr key={project.id} className="border">
              <td className="border p-2">{project.id}</td>
              {editingId === project.id ? (
                <>
                  <td className="border p-2">
                    <input
                      name="name"
                      value={editValues.name}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <input
                      name="description"
                      value={editValues.description}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <input
                      name="startDate"
                      type="datetime-local"
                      value={editValues.startDate}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <input
                      name="endDate"
                      type="datetime-local"
                      value={editValues.endDate}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <select
                      name="statusId"
                      value={editValues.statusId}
                      onChange={handleChange}
                      className="w-full"
                    >
                      {statuses.map((status) => (
                        <option key={status.id} value={status.id}>
                          {status.status}
                        </option>
                      ))}
                    </select>
                  </td>
                  <td className="border p-2">
                    <select
                      name="customerId"
                      value={editValues.customerId}
                      onChange={handleChange}
                      className="w-full"
                    >
                      {customers.map((customer) => (
                        <option key={customer.id} value={customer.id}>
                          {customer.name}
                        </option>
                      ))}
                    </select>
                  </td>
                  <td className="border p-2">
                    <select
                      name="employeeId"
                      value={editValues.employeeId}
                      onChange={handleChange}
                      className="w-full"
                    >
                      {employees.map((employee) => (
                        <option key={employee.id} value={employee.id}>
                          {`${employee.firstName} ${employee.lastName}`}
                        </option>
                      ))}
                    </select>
                  </td>
                  <td className="border p-2">
                    <select
                      name="serviceId"
                      value={editValues.serviceId}
                      onChange={handleChange}
                      className="w-full"
                    >
                      {services.map((service) => (
                        <option key={service.id} value={service.id}>
                          {service.serviceName}
                        </option>
                      ))}
                    </select>
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
                  <td className="border p-2">{project.name}</td>
                  <td className="border p-2">{project.description}</td>
                  <td className="border p-2">{project.startDate}</td>
                  <td className="border p-2">{project.endDate}</td>
                  <td className="border p-2">
                    {statuses.find((s) => s.id === project.statusId)?.status ||
                      "Unknown"}
                  </td>
                  <td className="border p-2">
                    {customers.find((c) => c.id === project.customerId)?.name ||
                      "Unknown"}
                  </td>
                  <td className="border p-2">
                    {`${
                      employees.find((e) => e.id === project.employeeId)
                        ?.firstName || ""
                    } ${
                      employees.find((e) => e.id === project.employeeId)
                        ?.lastName || "Unknown"
                    }`}
                  </td>
                  <td className="border p-2">
                    {services.find((s) => s.id === project.serviceId)
                      ?.serviceName || "Unknown"}
                  </td>
                  <td className="border p-2">
                    <button
                      onClick={() => handleEdit(project)}
                      className="bg-yellow-500 text-white px-2 py-1 rounded mr-2"
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(project.id)}
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

export default ProjectsPage;
