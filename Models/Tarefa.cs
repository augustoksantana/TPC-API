namespace TPC_API.Models
{
    public class Tarefa : Generic
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public int UsuarioId { get; set; }
        public User? User { get; set; }
    }

    public enum TarefaStatusEnum
    {
        Pendente = 0,
        Andamento = 1,
        Concluido = 2,
    }
}
