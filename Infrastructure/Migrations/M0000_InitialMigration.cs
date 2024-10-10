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

        Create.Table("posts")
            .WithColumn("id").AsInt64().Identity().NotNullable().PrimaryKey()
            .WithColumn("author_id").AsInt64().ForeignKey().NotNullable()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("content").AsString().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable()
            .WithColumn("status").AsString().NotNullable();

        Create.Table("images")
            .WithColumn("id").AsInt64().Identity().NotNullable().PrimaryKey()
            .WithColumn("post_id").AsInt64().ForeignKey().NotNullable()
            .WithColumn("image_uuid").AsString().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("posts");
    }
}
