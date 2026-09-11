using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentoController : Controller
    {
        private readonly string _caminhoRaiz = Path.Combine(Directory.GetCurrentDirectory(), "clienteArquivos");

        private static List<Models.documentoMETADADO> _documentosMetadados = new List<Models.documentoMETADADO>();

        private static int _nextId = 1;
        [HttpPost("upload/{CodCliente}")]
        public async Task<ActionResult> AnexarArquivo(int CodCliente, IFormFile arquivo);
    }
}
