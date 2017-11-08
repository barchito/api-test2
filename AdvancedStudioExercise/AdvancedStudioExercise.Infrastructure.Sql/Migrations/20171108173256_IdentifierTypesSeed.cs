using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedStudioExercise.Infrastructure.Sql.Migrations {
    public partial class IdentifierTypesSeed : Migration {
        private const string MigrationSqlScript = @"AdvancedStudioExercise.Infrastructure.Sql\Migrations\Scripts\20171108173256_IdentifierTypesSeed.sql";

        protected override void Up ( MigrationBuilder migrationBuilder ) {
            var sql = Path.Combine( Directory.GetParent( Directory.GetCurrentDirectory() ).FullName, MigrationSqlScript );
            migrationBuilder.Sql( File.ReadAllText( sql ) );
        }

        protected override void Down ( MigrationBuilder migrationBuilder ) {
        }
    }
}