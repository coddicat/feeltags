using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeelTags.WebApi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddBiasRandomByDateFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCreateFunction = @"
CREATE FUNCTION `BiasRandomByDate`(dateTime DATETIME)
RETURNS DOUBLE
DETERMINISTIC
BEGIN
    DECLARE result DOUBLE;
    SET result = RAND() / (DATEDIFF(CURDATE(), dateTime) + 1);
    RETURN result;
END";

            migrationBuilder.Sql(sqlCreateFunction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS `BiasRandomByDate`");
        }
    }
}
