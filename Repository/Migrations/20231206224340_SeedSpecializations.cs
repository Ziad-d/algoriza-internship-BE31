using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SeedSpecializations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Psychiatry", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Pediatrics", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Orthopedics", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Genecology and Infertility", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Dentistry", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Ear, Nose and Throat", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Allergy and Immunology", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Cardiology and Vascular Disease", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Chest and Respiratory", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Dietitian and Nutrition", 0 }
            );

            migrationBuilder.InsertData(
               table: "Specializations",
               columns: new[] { "Name", "Requests" },
               values: new object[] { "Dermatology", 0 }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Specialization]");
        }
    }
}
