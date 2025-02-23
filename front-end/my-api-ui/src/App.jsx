import React, { useState } from "react";
import StatusTypesPage from "./pages/StatusTypesPage";
import RolesPage from "./pages/RolesPage";
import ProjectsPage from "./pages/ProjectsPage";
import EmployeesPage from "./pages/EmployeesPage";
import CustomersPage from "./pages/CustomersPage";
import ContactPersonsPage from "./pages/ContactPersonsPage";
import CurrenciesPage from "./pages/CurrenciesPage";
import UnitsPage from "./pages/UnitPage";
import ServicesPage from "./pages/ServicesPage";

const App = () => {
  const [currentPage, setCurrentPage] = useState(null);

  const renderPage = () => {
    switch (currentPage) {
      case "StatusTypes":
        return <StatusTypesPage />;
      case "Roles":
        return <RolesPage />;
      case "Projects":
        return <ProjectsPage />;
      case "Employees":
        return <EmployeesPage />;
      case "Customers":
        return <CustomersPage />;
      case "ContactPersons":
        return <ContactPersonsPage />;
      case "Currencies":
        return <CurrenciesPage />;
      case "Units":
        return <UnitsPage />;
      case "Services":
        return <ServicesPage />;
      default:
        return <p>Select an API to fetch data.</p>;
    }
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">API Dashboard</h1>
      <div>
        <button
          onClick={() => setCurrentPage("Projects")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Projects API
        </button>

        <button
          onClick={() => setCurrentPage("Employees")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Employees API
        </button>

        <button
          onClick={() => setCurrentPage("Customers")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Customers API
        </button>

        <button
          onClick={() => setCurrentPage("ContactPersons")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          ContactPersons API
        </button>
        <button
          onClick={() => setCurrentPage("Services")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Services API
        </button>
        <button
          onClick={() => setCurrentPage("Currencies")} 
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Currencies API
        </button>

        <button
          onClick={() => setCurrentPage("Units")} 
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Units API
        </button>

        <button
          onClick={() => setCurrentPage("StatusTypes")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          StatusType API
        </button>

        <button
          onClick={() => setCurrentPage("Roles")}
          className="bg-blue-500 text-white px-4 py-2 m-2 rounded"
        >
          Roles API
        </button>
      </div>
      <div className="mt-4">{renderPage()}</div>
    </div>
  );
};

export default App;
