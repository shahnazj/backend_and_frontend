import React from "react";

const Table = ({ data }) => {
  if (!data || data.length === 0) return <p>No data available.</p>;

  const headers = Object.keys(data[0]);

  return (
    <table className="border-collapse border border-gray-500 w-full mt-4">
      <thead>
        <tr className="bg-gray-200">
          {headers.map((header) => (
            <th key={header} className="border border-gray-400 p-2">
              {header}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {data.map((row, index) => (
          <tr key={index} className="hover:bg-gray-100">
            {headers.map((header) => (
              <td key={header} className="border border-gray-400 p-2">
                {row[header]}
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default Table;
