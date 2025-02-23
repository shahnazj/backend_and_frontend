import React, { useState, useEffect } from "react";
import {
  fetchData,
  createData,
  updateData,
  addContactPersonToCustomer,
  removeContactPersonFromCustomer,
  deleteData,
} from "../api/api";

const CustomersPage = () => {
  const [data, setData] = useState([]);
  const [projects, setProjects] = useState({});
  const [contactPersons, setContactPersons] = useState({});
  const [allContactPersons, setAllContactPersons] = useState([]);
  const [editingId, setEditingId] = useState(null);
  const [editValues, setEditValues] = useState({});

  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");

  const handleFetch = async () => {
    const result = await fetchData("Customers");
    setData(result);
  };

  const fetchProjects = async (customerId) => {
    const projectData = await fetchData(`Projects/customer/${customerId}`);
    setProjects((prev) => ({
      ...prev,
      [customerId]: projectData.map((p) => p.name),
    }));
  };

  const fetchContactPersons = async (customerId) => {
    const contactData = await fetchData(
      `Customers/${customerId}/contactpersons`
    );
    setContactPersons((prev) => ({
      ...prev,
      [customerId]: contactData.map((cp) => ({
        id: cp.id,
        name: `${cp.firstName} ${cp.lastName}`,
      })),
    }));
  };

  const fetchAllContactPersons = async () => {
    const result = await fetchData("ContactPersons");
    setAllContactPersons(result);
  };

  const handleCreate = async () => {
    if (!name.trim() || !email.trim() || !phoneNumber.trim()) {
      alert("All fields are required");
      return;
    }

    try {
      const newCustomer = await createData("Customers", {
        name,
        email,
        phoneNumber,
      });

      setData([...data, newCustomer]);

      setName("");
      setEmail("");
      setPhoneNumber("");
    } catch (error) {
      console.error("Failed to create customer", error);
      alert("Failed to create customer. Please check the console for details.");
    }
  };

  const handleEdit = (customer) => {
    setEditingId(customer.id);
    setEditValues({ ...customer });
    fetchAllContactPersons();
    fetchContactPersons(customer.id);
  };

  const handleChange = (e) => {
    setEditValues({ ...editValues, [e.target.name]: e.target.value });
  };

  const handleUpdate = async () => {
    try {
      await updateData("Customers", editingId, editValues);
      setEditingId(null);
      handleFetch();
    } catch (error) {
      console.error("Failed to update customer", error);
    }
  };

  const handleAddContactPerson = async (customerId, contactPersonId) => {
    try {
      await addContactPersonToCustomer(customerId, contactPersonId);
      await fetchContactPersons(customerId);
    } catch (error) {
      console.error("Failed to add contact person", error);
    }
  };

  const handleRemoveContactPerson = async (customerId, contactPersonId) => {
    try {
      await removeContactPersonFromCustomer(customerId, contactPersonId);
      await fetchContactPersons(customerId);
    } catch (error) {
      console.error("Failed to remove contact person", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteData("Customers", id);
      setData(data.filter((customer) => customer.id !== id));
    } catch (error) {
      console.error("Failed to delete customer", error);
      alert("Failed to delete customer. Please check the console for details.");
    }
  };

  useEffect(() => {
    data.forEach((customer) => {
      fetchProjects(customer.id);
      fetchContactPersons(customer.id);
    });
  }, [data]);

  const getAvailableContactPersons = (customerId) => {
    const assignedContactPersonIds = new Set(
      (contactPersons[customerId] || []).map((cp) => cp.id)
    );
    return allContactPersons.filter(
      (cp) => !assignedContactPersonIds.has(cp.id)
    );
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Customers</h2>
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Customers
      </button>

      {/*Customer Form */}
      <div className="mb-4">
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Name"
          className="border p-2 rounded mr-2"
        />
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Email"
          className="border p-2 rounded mr-2"
        />
        <input
          type="text"
          value={phoneNumber}
          onChange={(e) => setPhoneNumber(e.target.value)}
          placeholder="Phone Number"
          className="border p-2 rounded mr-2"
        />
        <button
          onClick={handleCreate}
          className="bg-green-500 text-white px-4 py-2 rounded"
        >
          Create Customer
        </button>
      </div>

      {/* Table */}
      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">Name</th>
            <th className="border p-2">Email</th>
            <th className="border p-2">Phone</th>
            <th className="border p-2">Projects</th>
            <th className="border p-2">Contact Persons</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((customer) => (
            <tr key={customer.id} className="border">
              <td className="border p-2">{customer.id}</td>
              {editingId === customer.id ? (
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
                      name="email"
                      value={editValues.email}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    <input
                      name="phoneNumber"
                      value={editValues.phoneNumber}
                      onChange={handleChange}
                      className="w-full"
                    />
                  </td>
                  <td className="border p-2">
                    {projects[customer.id]?.join(", ") || "No Projects"}
                  </td>
                  <td className="border p-2">
                    <div className="flex gap-4">
                      <div className="w-1/2">
                        <h4 className="font-bold mb-2">
                          Available Contact Persons
                        </h4>
                        <div className="max-h-40 overflow-y-auto border rounded p-2">
                          {getAvailableContactPersons(customer.id).map(
                            (contactPerson) => (
                              <div
                                key={contactPerson.id}
                                className="flex justify-between items-center mb-1"
                              >
                                <span>{`${contactPerson.firstName} ${contactPerson.lastName}`}</span>
                                <button
                                  onClick={() =>
                                    handleAddContactPerson(
                                      customer.id,
                                      contactPerson.id
                                    )
                                  }
                                  className="bg-green-500 text-white px-2 py-1 rounded text-sm"
                                >
                                  Add
                                </button>
                              </div>
                            )
                          )}
                        </div>
                      </div>
                      <div className="w-1/2">
                        <h4 className="font-bold mb-2">
                          Assigned Contact Persons
                        </h4>
                        <div className="max-h-40 overflow-y-auto border rounded p-2">
                          {contactPersons[customer.id]?.map((contactPerson) => (
                            <div
                              key={contactPerson.id}
                              className="flex justify-between items-center mb-1"
                            >
                              <span>{contactPerson.name}</span>
                              <button
                                onClick={() =>
                                  handleRemoveContactPerson(
                                    customer.id,
                                    contactPerson.id
                                  )
                                }
                                className="bg-red-500 text-white px-2 py-1 rounded text-sm"
                              >
                                Remove
                              </button>
                            </div>
                          ))}
                        </div>
                      </div>
                    </div>
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
                  <td className="border p-2">{customer.name}</td>
                  <td className="border p-2">{customer.email}</td>
                  <td className="border p-2">{customer.phoneNumber}</td>
                  <td className="border p-2">
                    {projects[customer.id]?.join(", ") || "No Projects"}
                  </td>
                  <td className="border p-2">
                    {contactPersons[customer.id]
                      ?.map((cp) => cp.name)
                      .join(", ") || "No Contact Persons"}
                  </td>
                  <td className="border p-2">
                    <button
                      onClick={() => handleEdit(customer)}
                      className="bg-yellow-500 text-white px-2 py-1 rounded mr-2"
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(customer.id)}
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

export default CustomersPage;
