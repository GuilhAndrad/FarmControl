using FluentMigrator;

namespace FarmControl.Infrastructure.Migrations.Versions;

[Migration((long)NumberOfVersions.CriarTabelaFazenda, "Cria a tabela Fazenda")]
public class Version0000001 : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        var table = BaseVersion.InsertDefaultColumn(Create.Table("Fazendas"));

        table
            .WithColumn("Nome").AsString(255).NotNullable()
            .WithColumn("Localizacao").AsString(255).NotNullable()
            .WithColumn("Descricao").AsString(500).Nullable()
            .WithColumn("EstoqueGado").AsInt32().NotNullable().WithDefaultValue(0);
    }
}
