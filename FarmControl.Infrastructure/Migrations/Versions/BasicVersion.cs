using FluentMigrator.Builders.Create.Table;

namespace FarmControl.Infrastructure.Migrations.Versions;
public static class BaseVersion
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InsertDefaultColumn(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
    {
        return table
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("DataCriacao").AsDateTime().NotNullable();
    }
}
