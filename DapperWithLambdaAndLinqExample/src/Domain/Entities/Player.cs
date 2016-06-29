namespace Domain.Entities
{
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; } // muito importante declarar as colunas de chave estrangeira da tabela

        public Team Team { get; set; }

    }
}
