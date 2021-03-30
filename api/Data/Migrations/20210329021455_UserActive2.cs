using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Data.Migrations
{
    public partial class UserActive2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //var context = new DataContext()
            //context.Users.FromSqlRaw("update users set active = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
