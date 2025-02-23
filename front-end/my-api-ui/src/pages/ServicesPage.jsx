import React, { useState, useEffect } from "react";
import { fetchData, createData, updateData, deleteData } from "../api/api";

const ServicesPage = () => {
  const [data, setData] = useState([]);
  const [editingId, setEditingId] = useState(null);
  const [editValues, setEditValues] = useState({});
  const [units, setUnits] = useState([]);
  const [currencies, setCurrencies] = useState([]);

  const [serviceName, setServiceName] = useState("");
  const [price, setPrice] = useState("");
  const [unitId, setUnitId] = useState("");
  const [currencyId, setCurrencyId] = useState("");

  useEffect(() => {
    const fetchDropdownData = async () => {
      setUnits(await fetchData("Units"));
      setCurrencies(await fetchData("Currencies"));
    };
    fetchDropdownData();
  }, []);

  const handleFetch = async () => {
    const result = await fetchData("Services");
    setData(
      result.map((service) => ({
        ...service,
        unitName: units.find((u) => u.id === service.unitId)?.name || "Unknown",
        currencyName:
          currencies.find((c) => c.id === service.currencyId)?.name ||
          "Unknown",
      }))
    );
  };

  const handleCreate = async () => {
    if (!serviceName.trim() || !price || !unitId || !currencyId) {
      alert("All fields are required");
      return;
    }

    try {
      const newService = await createData("Services", {
        serviceName,
        price: parseFloat(price),
        unitId: parseInt(unitId, 10),
        currencyId: parseInt(currencyId, 10),
      });

      const unitName =
        units.find((u) => u.id === newService.unitId)?.name || "Unknown";
      const currencyName =
        currencies.find((c) => c.id === newService.currencyId)?.name ||
        "Unknown";

      setData([...data, { ...newService, unitName, currencyName }]);
      setServiceName("");
      setPrice("");
      setUnitId("");
      setCurrencyId("");
    } catch (error) {
      console.error("Failed to create service", error);
      alert("Failed to create service. Please check the console for details.");
    }
  };

  const handleEdit = (service) => {
    setEditingId(service.id);
    setEditValues({ ...service });
  };

  const handleChange = (e) => {
    setEditValues({ ...editValues, [e.target.name]: e.target.value });
  };

  const handleUpdate = async () => {
    try {
      const updatedData = {
        id: editingId,
        serviceName: editValues.serviceName,
        price: parseFloat(editValues.price),
        unitId: parseInt(editValues.unitId, 10),
        currencyId: parseInt(editValues.currencyId, 10),
      };

      await updateData("Services", editingId, updatedData);
      setEditingId(null);
      handleFetch();
    } catch (error) {
      console.error("Failed to update service", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteData("Services", id);
      setData(data.filter((service) => service.id !== id));
    } catch (error) {
      console.error("Failed to delete service", error);
      alert("Failed to delete service. Please check the console for details.");
    }
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Services</h2>
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Services
      </button>

      <div className="mb-4">
        <input
          type="text"
          value={serviceName}
          onChange={(e) => setServiceName(e.target.value)}
          placeholder="Service Name"
          className="border p-2 rounded mr-2"
        />
        <input
          type="number"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          placeholder="Price"
          className="border p-2 rounded mr-2"
        />
        <select
          value={unitId}
          onChange={(e) => setUnitId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Unit</option>
          {units.map((unit) => (
            <option key={unit.id} value={unit.id}>
              {unit.name}
            </option>
          ))}
        </select>
        <select
          value={currencyId}
          onChange={(e) => setCurrencyId(e.target.value)}
          className="border p-2 rounded mr-2"
        >
          <option value="">Select Currency</option>
          {currencies.map((currency) => (
            <option key={currency.id} value={currency.id}>
              {currency.name}
            </option>
          ))}
        </select>
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Service
        </button>
      </div>

      <table className="min-w-full border">
        <thead>
          <tr>
            <th>ID</th>
            <th>Service Name</th>
            <th>Price</th>
            <th>Unit</th>
            <th>Currency</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((service) => (
            <tr key={service.id} className="border">
              <td>{service.id}</td>
              {editingId === service.id ? (
                <>
                  <td>
                    <input
                      name="serviceName"
                      value={editValues.serviceName}
                      onChange={handleChange}
                    />
                  </td>
                  <td>
                    <input
                      name="price"
                      type="number"
                      value={editValues.price}
                      onChange={handleChange}
                    />
                  </td>
                  <td>
                    <select
                      name="unitId"
                      value={editValues.unitId}
                      onChange={handleChange}
                    >
                      {units.map((unit) => (
                        <option key={unit.id} value={unit.id}>
                          {unit.name}
                        </option>
                      ))}
                    </select>
                  </td>
                  <td>
                    <select
                      name="currencyId"
                      value={editValues.currencyId}
                      onChange={handleChange}
                    >
                      {currencies.map((currency) => (
                        <option key={currency.id} value={currency.id}>
                          {currency.name}
                        </option>
                      ))}
                    </select>
                  </td>
                  <td>
                    <button
                      onClick={handleUpdate}
                      className="bg-green-500 text-white px-2 py-1 rounded"
                    >
                      Save
                    </button>
                  </td>
                </>
              ) : (
                <>
                  <td>{service.serviceName}</td>
                  <td>{service.price}</td>
                  <td>{service.unitName}</td>
                  <td>{service.currencyName}</td>
                  <td>
                    <button
                      onClick={() => handleEdit(service)}
                      className="bg-yellow-500 text-white px-2 py-1 rounded"
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(service.id)}
                      className="bg-red-500 text-white px-2 py-1 rounded ml-2"
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

export default ServicesPage;
