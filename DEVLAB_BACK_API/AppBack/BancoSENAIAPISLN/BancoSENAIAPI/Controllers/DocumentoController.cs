using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentoController : Controller
    {
        private readonly string _caminhoRaiz = Path.Combine(Directory.GetCurrentDirectory(), "clienteArquivos");
        private static List<documentoMETADADO> _documentosMetadados = new List<documentoMETADADO>();
        private static int _nextId = 1;

        [HttpPost("upload/{CodCliente}")]
        public async Task<IActionResult> AnexarArquivo(int CodCliente, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            string pastaCliente = Path.Combine(_caminhoRaiz, CodCliente.ToString());

            if (!Directory.Exists(pastaCliente))
            {
                Directory.CreateDirectory(pastaCliente);
            }

            string extensao = Path.GetExtension(arquivo.FileName);
            string nomeOriginal = Path.GetFileNameWithoutExtension(arquivo.FileName);
            string novoNome = $"{CodCliente}_{nomeOriginal}_{Guid.NewGuid()}{extensao}";
            string caminhoFinal = Path.Combine(pastaCliente, novoNome);

            using (var stream = new FileStream(caminhoFinal, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var documentoMetadados = new documentoMETADADO
            {
                Id = _nextId++,
                nome = nomeOriginal,
                extensao = extensao,
                caminho = caminhoFinal,
                CodCliente = CodCliente
            };

            _documentosMetadados.Add(documentoMetadados);
            return Ok(new { mensagem = "Documento anexado com sucesso", arquivoSalvo = novoNome });
        }

        [HttpGet("listar/{codigoCliente}")]
        public IActionResult ListarDocumentos(int codigoCliente)
        {
            var documentos = _documentosMetadados.Where(d => d.CodCliente == codigoCliente).ToList();
            return Ok(documentos);
        }

        [HttpGet("download/{id}")]
        public IActionResult Download(int id)
        {
            var doc = _documentosMetadados.FirstOrDefault(d => d.Id == id);
            if (doc == null)
            {
                return NotFound("Documento não encontrado nos registros.");
            }

            if (!System.IO.File.Exists(doc.caminho))
            {
                return NotFound("Arquivo não encontrado no servidor.");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(doc.caminho);
            string nomeDownload = $"{doc.nome}{doc.extensao}";
            return File(fileBytes, "application/octet-stream", nomeDownload);
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Deletar(int id)
        {
            var doc = _documentosMetadados.FirstOrDefault(d => d.Id == id);
            if (doc == null)
            {
                return NotFound("Documento não encontrado.");
            }

            _documentosMetadados.Remove(doc);
            return Ok("Arquivo deletado com sucesso.");
        }
    }
}