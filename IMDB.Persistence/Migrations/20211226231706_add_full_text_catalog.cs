using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imdb.Persistence.Migrations
{
    public partial class add_full_text_catalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE FULLTEXT CATALOG ftCatalog  AS DEFAULT", true);
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON Movies(Title,Description) KEY INDEX PK_Movies;", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
