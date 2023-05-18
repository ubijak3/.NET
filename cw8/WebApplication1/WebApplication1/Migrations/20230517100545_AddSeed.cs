using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "JanKowalski@o2.pl", "Jan", "Kowalski" },
                    { 2, "Franciszek@o2.pl", "Franciszek", "Franciszewski" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "suplement na stawy", "Medistenol", "suplement" },
                    { 2, "Lek na bol glowy", "Apap", "Lek przeciwbolowy" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "Birthdate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pawel", "Nowak" },
                    { 2, new DateTime(1990, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jurek", "Jurkowski" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 17, 12, 5, 45, 636, DateTimeKind.Local).AddTicks(6810), new DateTime(2023, 5, 27, 12, 5, 45, 636, DateTimeKind.Local).AddTicks(6857), 1, 1 },
                    { 2, new DateTime(2023, 5, 17, 12, 5, 45, 636, DateTimeKind.Local).AddTicks(6865), new DateTime(2023, 6, 1, 12, 5, 45, 636, DateTimeKind.Local).AddTicks(6866), 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[,]
                {
                    { 1, 1, "Przyjmowac 2 kapsulki dziennie(rano i wieczorem)", 2 },
                    { 1, 2, "Przyjmowac 1 kapsulke co 5-6 godzin, max 4 dziennie", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);
        }
    }
}
