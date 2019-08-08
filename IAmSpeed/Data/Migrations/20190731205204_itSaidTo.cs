using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmSpeed.Data.Migrations
{
    public partial class itSaidTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GameIdFromAPI",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2757609-25fe-4fcb-96b2-cc007ea4ad63", "AQAAAAEAACcQAAAAELolGwoAwvpsH+ak/ey/tccQ8zS852zfeDSVx+CaCFdLQ7xHnsy07NPWGmDPA/blag==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameIdFromAPI",
                table: "Games",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a2a1f28-0556-4cca-8fcb-3c5238bbe422", "AQAAAAEAACcQAAAAENexIXUD2WJiFKXisAh12MZ5N/dAFWcoqrzMjPrvf3SaA1guluNkASfUeoEdGsWxXA==" });
        }
    }
}
