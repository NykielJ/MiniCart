# Demo Minicart for e-commerce

## Description
Demo Minicart is an application that allows users to manage their shopping cart. This project was created as part of a E-commerce application and utilizes .NET for the backend and Vue.js for the frontend.

## Technologies
- **Backend**: .NET
- **Frontend**: Vue.js
- **Styling**: CSS
- **Data Format**: JSON

## Features
- Add products to the cart
- Display a list of products in the cart
- Change product quantities
- Show order summary

## Installation
1. Clone the repository
2. Navigate to the `DemoMinicart` directory
3. Run the following commands:
    - `dotnet restore`
    - `dotnet run`
4. Navigate to the `frontend` directory
5. Run the following commands:
    - `npm install`
    - `npm run serve`
6. Open a browser and navigate to `http://localhost:8080/`

## Usage
1. Add products to the cart by clicking use the swagger UI or by sending a POST request to `https://localhost:5001/api/cart/add` with the following body:
    ```json
    {
  "id": 0,
  "name": "string",
  "price": 0,
  "quantity": 0,
  "size": "string",
  "color": "string",
  "pattern": "string"
    }
    ```
2. View the cart by clicking the cart icon in the top right corner
3. Change the quantity of a product by clicking the `+` or `-` buttons
4. Remove a product from the cart by clicking the `x` button
5. View the order summary by clicking the `Order Summary` button (this is not yet implemented)

## in progress
- Add tests
- Add more features
- Improve styling
