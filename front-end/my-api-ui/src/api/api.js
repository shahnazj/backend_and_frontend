import axios from "axios";

const BASE_URL = "http://localhost:5218/api";

export const fetchData = async (endpoint) => {
  try {
    const response = await axios.get(`${BASE_URL}/${endpoint}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    return [];
  }
};

export const createData = async (endpoint, data) => {
  try {
    const response = await axios.post(`${BASE_URL}/${endpoint}`, data, {
      headers: { "Content-Type": "application/json" },
    });
    return response.data;
  } catch (error) {
    console.error("Error creating data:", error);
    throw error;
  }
};

export const updateData = async (endpoint, id, data) => {
    try {
      console.log("the data in api is: ", data)
      const response = await axios.put(`${BASE_URL}/${endpoint}/${id}`, data, {
        headers: { "Content-Type": "application/json" },
      });
      console.log(response.data)
      return response.data;
    } catch (error) {
      console.error("Error updating data:", error);
      throw error;
    }
  };
  

  export const addCustomerToContactPerson = async (contactPersonId, customerId) => {
    try {
      const response = await axios.post(`${BASE_URL}/ContactPersons/${contactPersonId}/customers/${customerId}`);
      return response.data;
    } catch (error) {
      console.error("Error adding customer:", error);
      throw error;
    }
  };
  
  export const removeCustomerFromContactPerson = async (contactPersonId, customerId) => {
    try {
      const response = await axios.delete(`${BASE_URL}/ContactPersons/${contactPersonId}/customers/${customerId}`);
      return response.data;
    } catch (error) {
      console.error("Error removing customer:", error);
      throw error;
    }
  };

  export const addContactPersonToCustomer = async (customerId, contactPersonId) => {
    try {
      const response = await axios.post(`${BASE_URL}/Customers/${customerId}/contactpersons/${contactPersonId}`);
      return response.data;
    } catch (error) {
      console.error("Error adding contact person:", error);
      throw error;
    }
  };
  
  export const removeContactPersonFromCustomer = async (customerId, contactPersonId) => {
    try {
      const response = await axios.delete(`${BASE_URL}/Customers/${customerId}/contactpersons/${contactPersonId}`);
      return response.data;
    } catch (error) {
      console.error("Error removing contact person:", error);
      throw error;
    }
  };

  export const deleteData = async (endpoint, id) => {
    try {
      const response = await axios.delete(`${BASE_URL}/${endpoint}/${id}`);
      return response.data;
    } catch (error) {
      console.error("Error deleting data:", error);
      throw error;
    }
  };