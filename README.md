
# HotelBooker

## Challenge

Create a program to preview hotel room availability and reservations.

The application should read from files containing hotel data and booking data, then allow a user to check room availability for a specified hotel, date range, and room type.

## Usage

### Prerequisites

- .NET SDK installed to build the program
- `hotels.json` and `bookings.json` files containing hotels and bookings data.

### Steps to Run

1. **Clone the Repository**

   ```bash
   git clone github.com/GuilhermeJacobRech/HotelBooker
   ```

2. **Build the Project**

   ```bash
   dotnet build
   ```

3. **Run the Program**
   Navigate to the directory containing the .exe. By default, it will be located in `bin/Release/net(your_framework_version_here)/`

   ```bash
   cd bin/Release/net8.0/
   ```

   Ensure the required JSON files (`hotels.json` and `bookings.json`) are placed in the same directory as the `.exe` file.

   Execute the program:

   ```bash
   ./HotelBooker.exe
   ```

### JSON File Requirements

- **hotels.json**:

    ```json
    [
        {
            "id": "string",
            "name": "string",
            "roomTypes": [
            {
                "code": "string",
                "description": "string",
                "amenities": [
                "string",
                "string"
                ],
                "features": [
                "string"
                ]
            }
            ],
            "rooms": [
            {
                "roomType": "string",
                "roomId": "string"
            }
            ]
        }
    ]
    ```

- **bookings.json**:

   ```json
   [
        {
            "hotelId": "string",
            "arrival": "yyyyMMdd",
            "departure": "yyyyMMdd",
            "roomType": "string",
            "roomRate": "string"
        }
   ]
   ```

### Usage Example

1. After available hotels are displayed, the user can input a query in the format:

   ```c#
   Availability(H1, 20240901-20240903, DBL)
   ```

   This checks availability for the specified hotel, date range, and room type.

2. The program will output the amount of rooms available (can be negative if overbooking occurs).

3. To exit, enter a blank line as input.
