using TPC_API.Contexts;
using TPC_API.Models;
using TPC_API.Utils;

namespace TPC_API.Services
{
    public class TarefaService
    {
        private readonly DataBaseContext _context;

        public TarefaService(DataBaseContext context)
        {
            _context = context;
        }

        public Result<Tarefa> Create(Tarefa tarefa, int id)
        {
            if (!ExistsUser(id)) return Result.Fail<Tarefa>("Usuário não cadastrado");
            if (_context.Tarefas.Any(x => x.Titulo == tarefa.Titulo)) return Result.Fail<Tarefa>("Titulo já cadastrado");
            try
            {
                tarefa = _context.Tarefas.Add(tarefa).Entity;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result.Fail<Tarefa>(ex.Message);
            }
            return Result.Ok(tarefa);

        }

        public Result<Tarefa> GetById(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null) return Result.Fail<Tarefa>("ID da tarefa não foi encontrado");
            return Result.Ok(tarefa);
        }

        public IQueryable<Tarefa> GetAll() => _context.Tarefas;

        public IQueryable<Tarefa> GetTarefasUserId(int userId) => _context.Tarefas.Where(x => x.UsuarioId == userId);

        public Result<Tarefa> Update(Tarefa tarefa)
        {
            if (!ExistsUser(tarefa.UsuarioId)) return Result.Fail<Tarefa>("Usuário não cadastrado");
            var tarefa_context = _context.Tarefas.Find(tarefa.Id);
            try
            {
                if (tarefa_context == null) return Result.Fail<Tarefa>("Tarefa não existe");
                tarefa_context.Titulo = tarefa.Titulo;
                tarefa_context.Descricao = tarefa.Descricao;
                tarefa_context.Status = tarefa.Status;
                tarefa_context.UsuarioId = tarefa.UsuarioId;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result.Fail<Tarefa>(ex.Message);
            }

            return Result.Ok(tarefa_context);
        }

        public Result RemuveById(int id)
        {
            try
            {
                var tarefa_context = _context.Tarefas.Find(id);
                if (tarefa_context == null) return Result.Fail<Tarefa>("Tarefa não existe");
                _context.Tarefas.Remove(tarefa_context);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result.Fail<Tarefa>(ex.Message);
            }

            return Result.Ok();
        }
         private bool ExistsUser(int id) => _context.Users.Any(u => u.Id == id);



    }
}

