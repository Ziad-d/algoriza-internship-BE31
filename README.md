# Veezeta API
A Medical Appointment System built in Onion Architecture based on the following Design Patterns:
<blockquote>
 
- Repository Design Pattern.
- UnitOfWork Design Pattern.

</blockquote>

## Project Overview

This project is designed to offer practical experience with various technologies and concepts including ASP.NET Core, SQL Server, ASP.NET Core APIs, Identity Core, JWT Authentication and Authorization, Entity Framework, and Onion Architecture.

## Project Structure

The project structure is organized as follows:
- **Controllers**: Contains API endpoints for handling HTTP requests.
- **Services**: Contains business logic for various functionalities.
- **Repositories**: Contains data access logic using Entity Framework Core.
- **Models**: Contains domain models representing entities in the application.
- **DTOs**: Contains Data Transfer Objects used for data exchange between layers.
- **Utilities**: Contains utility classes and helper methods.

## Configuration

Ensure the database connection string is properly configured in the `appsettings.json` file.

## Endpoints

### Admin Doctor Endpoints
- `POST /admin/doctor/registerDoctor`: Creates a new doctor record.
- `PUT /admin/doctor/updateDoctor`: Updates an existing doctor record.
- `DELETE /admin/doctor/deleteDoctor/id={doctorId}`: Deletes a doctor record.
- `GET /admin/doctor/getAllSpecializations`: Retrieves a list of all available specializations.
- `GET /admin/doctor/getAllDoctors`: Retrieves a list of all doctors.
- `GET /admin/doctor/getDoctor/id={doctorId}`: Gets details of a specific doctor by ID.

### Admin Patient Endpoints
- `GET /admin/patient/getAllPatients`: Retrieves a paginated list of patients. Supports optional filtering.
- `GET /admin/patient/getPatient/id={patientId}`: Retrieves detailed information for a specific patient.

### Admin Settings Endpoints
- `POST /admin/settings/addDiscountCode`: Creates a discount code.
- `GET /admin/settings/getAllCodes`: Retrieves all discount codes.
- `PUT /admin/settings/deactivateCoupon/id={codeId}`: Deactivates a discount code.
- `DELETE /admin/settings/deleteCoupon/id={codeId}`: Deletes a discount code.

### Admin Stats Endpoints
- `GET /admin/stats/numberOfDoctors`: Retrieves the total count of registered doctors on the platform.
- `GET /admin/stats/numberOfPatients`: Retrieves the total count of registered patients on the platform.
- `GET /admin/stats/numberOfRequests`: Retrieves the total number of requests (presumably appointment requests) in the system.
- `GET /admin/stats/topFiveSpecializations`: Retrieves the top 5 most popular specializations based on the number of associated requests.

### Doctor Endpoints
- `GET /doctor/getAppointments`: Retrieves a paginated list of the logged-in doctor's appointments.
- `POST /doctor/login`: Authenticates a doctor and returns a JWT for subsequent requests.
- `POST /doctor/CreateAppointment`: Creates a new appointment slot for the logged-in doctor.
- `PUT /doctor/confirmCheckUp/id={bookingId}`: Allows the logged-in doctor to confirm a completed check-up for a specific booking.
- `PUT /doctor/updateAppointment/id={appointmentId}`: Allows the doctor to update the details of an existing appointment.
- `DELETE /doctor/deleteAppointment/id={appointmentId}`: Allows the doctor to delete an appointment slot.
  
### Patient Endpoints
- `POST /patient/register`: Creates a new patient record.
- `POST /patient/login`: Logs a patient in and returns an authentication token.
- `GET /patient/getDoctorsAppointments`: Retrieves a list of a patient's upcoming doctor appointments.
- `GET /patient/book/id={timeId}`: Books an appointment for a patient by appointment ID.
- `GET /patient/getAllBookings`: Retrieves a list of all of a patient's past and upcoming bookings.
- `DELETE /patient/cancelBooking/id=(bookingId)	`: Cancels a patient's existing appointment by booking ID.

## Getting Started

To get started with this updated project, follow these steps:

1. **Install .NET Core 6**: If you haven't already installed .NET Core 6, you can download and install it from the official website:
   - [Download .NET](https://dotnet.microsoft.com/download)
2. Clone the repository to your local development environment.
3. Open the Package Manager Console in Visual Studio (or use the command-line equivalent) and run the following command to create the database tables based on the migrations:
   ```Shell
   Update-Database
4. Test endpoints on Postman or use the Swagger Documentation.
