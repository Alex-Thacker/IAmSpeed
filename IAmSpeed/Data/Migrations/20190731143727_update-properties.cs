using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmSpeed.Data.Migrations
{
    public partial class updateproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PBTime",
                table: "Segments",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ReleaseDate",
                table: "Games",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a2a1f28-0556-4cca-8fcb-3c5238bbe422", "AQAAAAEAACcQAAAAENexIXUD2WJiFKXisAh12MZ5N/dAFWcoqrzMjPrvf3SaA1guluNkASfUeoEdGsWxXA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PBTime",
                table: "Segments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "664c1abc-1c9e-4d79-b929-ea7d4624fc4e", "AQAAAAEAACcQAAAAEMeQ1MZqUMCqVEvJfpXTs95yCqDGl0l8bhIfsAGH7XEqOEfWDrMdlAkl1nMzN97fSg==" });
        }
    }
}
