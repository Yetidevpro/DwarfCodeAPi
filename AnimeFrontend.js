<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>API Data Display</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      margin: 20px;
    }
    ul {
      list-style-type: none;
      padding: 0;
    }
    li {
      padding: 8px;
      border: 1px solid #ddd;
      margin-bottom: 8px;
      background-color: #f9f9f9;
    }
  </style>
</head>
<body>
  <h1>Data from API</h1>
  <div id="data-list">
    <p>Loading...</p>
  </div>
 
  <script>document.addEventListener("DOMContentLoaded", function() {
  const apiUrl = 'http://your-api-url.com/data'; // Replace with your local API URL
  const dataListDiv = document.getElementById('data-list');
 
  // Function to fetch and display the list
  async function fetchData() {
    try {
      // Fetch data from API
      const response = await fetch(apiUrl);
 
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
 
      const data = await response.json();  // Assuming the API returns JSON
 
      // Check if data has 'values' array
      if (data && Array.isArray(data.values)) {
        // Clear loading text
        dataListDiv.innerHTML = '';
 
        // Create a list element
        const ul = document.createElement('ul');
 
        // Loop through data.values and create list items
        data.values.forEach(item => {
          const li = document.createElement('li');
          li.textContent = item.name; // Assuming each item in 'values' has a 'name' property
          ul.appendChild(li);
        });
 
        // Append the list to the div
        dataListDiv.appendChild(ul);
      } else {
        throw new Error("Data format is incorrect or 'values' is not an array");
      }
    } catch (error) {
      dataListDiv.innerHTML = `<p>Error: ${error.message}</p>`;
    }
  }
 
  // Call the fetchData function to get data when the page loads
  fetchData();
});
 
  </script>
</body>
</html>