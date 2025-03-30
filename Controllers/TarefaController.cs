using Microsoft.AspNetCore.Mvc;
using TPC_API.Models;
using TPC_API.Services;

namespace TPC_API.Controllers
{
    [ApiController]
    public class TarefaController : ControllerBase
    {

        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        [Route("[controller]")]
        public ActionResult Get()
        {
            return Ok(_tarefaService.GetAll());
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public ActionResult GetById(int id)
        {
            var result = _tarefaService.GetById(id);
            if (result.Failure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpGet]
        [Route("Tarefa/{id}/Usuario")]
        public ActionResult GetTarefasByUserId(int id)=> Ok(_tarefaService.GetTarefasUserId(id));
        
        [HttpPost]
        [Route("Tarefa/{id}/Usuario")]
        public ActionResult Post(Tarefa tarefa,int id)
        {
            var result = _tarefaService.Create(tarefa,id);
            if (result.Failure) return BadRequest(result.Error);
            tarefa = result.Value;
            return Created($"{Request.Path}/{tarefa.Id}", tarefa);
        }

        [HttpPut]
        [Route("[controller]/{id}")]
        public ActionResult Put(Tarefa tarefa, int id)
        {
            if (tarefa == null) return BadRequest();
            tarefa.Id = id;
            var result = _tarefaService.Update(tarefa);
            if (result.Failure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public ActionResult Delete(int id)
        {
            var result = _tarefaService.RemuveById(id);
            if (result.Failure) return BadRequest(result.Error);
            return Ok();
        }
    }

}

