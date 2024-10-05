using FluentMigrator;

namespace EducationService.Migrations;

[Migration(1)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().Identity().NotNullable().PrimaryKey()
            .WithColumn("email").AsString().Unique().NotNullable()
            .WithColumn("hashed_password").AsString().NotNullable()
            .WithColumn("role").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}
