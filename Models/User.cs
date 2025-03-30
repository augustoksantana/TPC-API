namespace TPC_API.Models
{
    public class User : Generic
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public ICollection<Tarefa>? Tarefas { get; set; }
    }
}
