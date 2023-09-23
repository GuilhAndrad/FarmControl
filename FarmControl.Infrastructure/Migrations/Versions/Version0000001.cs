using FarmControl.Infrastructure.Migrations;
using FarmControl.Infrastructure.Migrations.Versions;
using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Versions
{
    [Migration((long)NumberOfVersions.CriarTabelaUser, "Cria tabela a User")]
    public class Version0000001 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            var table = BaseVersion.InsertDefaultColumn(Create.Table("Users"));

            table
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("Phone").AsString(14).NotNullable();
        }
    }
}
