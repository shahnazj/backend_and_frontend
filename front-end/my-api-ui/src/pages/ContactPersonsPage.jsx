import React, { useState, useEffect } from "react";
import {
  fetchData,
  createData,
  updateData,
  addCustomerToContactPerson,
  removeCustomerFromContactPerson,
  deleteData,
} from "../api/api";

const ContactPersonsPage = () => {
  const [data, setData] = useState([]);
  const [customers, setCustomers] = useState({});
  const [allCustomers, setAllCustomers] = useState([]);
  const [editingId, setEditingId] = useState(null);
  const [editValues, setEditValues] = useState({});

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");

  const handleFetch = async () => {
    const result = await fetchData("ContactPersons");
    setData(result);
  };

  const fetchAllCustomers = async () => {
    const result = await fetchData("Customers");
    setAllCustomers(result);
  };

  const fetchCustomers = async (contactPersonId) => {
    const customerData = await fetchData(
      `ContactPersons/${contactPersonId}/customers`
    );
    setCustomers((prev) => ({
      ...prev,
      [contactPersonId]: customerData,
    }));
  };

  const handleCreate = async () => {
    if (
      !firstName.trim() ||
      !lastName.trim() ||
      !email.trim() ||
      !phoneNumber.trim()
    ) {
      alert("All fields are required");
      return;
    }

    try {
      const newContactPerson = await createData("ContactPersons", {
        firstName,
        lastName,
        email,
        phoneNumber,
      });
      setData([...data, newContactPerson]);
      setFirstName("");
      setLastName("");
      setEmail("");
      setPhoneNumber("");
    } catch (error) {
      console.error("Failed to create contact person", error);
    }
  };

  const handleEdit = async (contactPerson) => {
    setEditingId(contactPerson.id);
    setEditValues({ ...contactPerson });
    await fetchAllCustomers();
    await fetchCustomers(contactPerson.id);
  };

  const handleChange = (e) => {
    setEditValues({ ...editValues, [e.target.name]: e.target.value });
  };

  const handleUpdate = async () => {
    try {
      await updateData("ContactPersons", editingId, editValues);
      setEditingId(null);
      handleFetch();
    } catch (error) {
      console.error("Failed to update contact person", error);
    }
  };

  const handleAddCustomer = async (contactPersonId, customerId) => {
    try {
      await addCustomerToContactPerson(contactPersonId, customerId);
      await fetchCustomers(contactPersonId);
    } catch (error) {
      console.error("Failed to add customer", error);
    }
  };

  const handleRemoveCustomer = async (contactPersonId, customerId) => {
    try {
      await removeCustomerFromContactPerson(contactPersonId, customerId);
      await fetchCustomers(contactPersonId);
    } catch (error) {
      console.error("Failed to remove customer", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteData("ContactPersons", id);
      setData(data.filter((contactPerson) => contactPerson.id !== id));
    } catch (error) {
      console.error("Failed to delete contact person", error);
      alert(
        "Failed to delete contact person. Please check the console for details."
      );
    }
  };

  useEffect(() => {
    data.forEach((contactPerson) => {
      fetchCustomers(contactPerson.id);
    });
  }, [data]);

  const getAvailableCustomers = (contactPersonId) => {
    const assignedCustomerIds = new Set(
      (customers[contactPersonId] || []).map((c) => c.id)
    );
    return allCustomers.filter((c) => !assignedCustomerIds.has(c.id));
  };

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Contact Persons</h2>
      <button
        onClick={handleFetch}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Fetch Contact Persons
      </button>

      <div className="mb-4">
        <input
          type="text"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          placeholder="First Name"
          className="border p-2 rounded mr-2"
        />
        <input
          type="text"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          placeholder="Last Name"
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
          Create Contact Person
        </button>
      </div>

      <table className="min-w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">ID</th>
            <th className="border p-2">First Name</th>
            <th className="border p-2">Last Name</th>
            <th className="border p-2">Email</th>
            <th className="border p-2">Phone</th>
            <th className="border p-2">Customers</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((contactPerson) => (
            <tr key={contactPerson.id} className="border">
              <td className="border p-2">{contactPerson.id}</td>
              {editingId === contactPerson.id ? (
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
                    <div className="flex gap-4">
                      <div className="w-1/2">
                        <h4 className="font-bold mb-2">Available Customers</h4>
                        <div className="max-h-40 overflow-y-auto border rounded p-2">
                          {getAvailableCustomers(contactPerson.id).map(
                            (customer) => (
                              <div
                                key={customer.id}
                                className="flex justify-between items-center mb-1"
                              >
                                <span>{customer.name}</span>
                                <button
                                  onClick={() =>
                                    handleAddCustomer(
                                      contactPerson.id,
                                      customer.id
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
                        <h4 className="font-bold mb-2">Assigned Customers</h4>
                        <div className="max-h-40 overflow-y-auto border rounded p-2">
                          {customers[contactPerson.id]?.map((customer) => (
                            <div
                              key={customer.id}
                              className="flex justify-between items-center mb-1"
                            >
                              <span>{customer.name}</span>
                              <button
                                onClick={() =>
                                  handleRemoveCustomer(
                                    contactPerson.id,
                                    customer.id
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
                  <td className="border p-2">{contactPerson.firstName}</td>
                  <td className="border p-2">{contactPerson.lastName}</td>
                  <td className="border p-2">{contactPerson.email}</td>
                  <td className="border p-2">{contactPerson.phoneNumber}</td>
                  <td className="border p-2">
                    {customers[contactPerson.id]
                      ?.map((c) => c.name)
                      .join(", ") || "No Customers"}
                  </td>
                  <td className="border p-2">
                    <button
                      onClick={() => handleEdit(contactPerson)}
                      className="bg-yellow-500 text-white px-2 py-1 rounded mr-2"
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(contactPerson.id)}
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

export default ContactPersonsPage;
