namespace FarmControl.Domain.Entities;
public class Fazenda : BaseEntitie
{
    public string Nome { get; set; }
    public string Localizacao { get; set; }
    public string Descricao { get; set; }
    public int EstoqueGado { get; set; }
}
