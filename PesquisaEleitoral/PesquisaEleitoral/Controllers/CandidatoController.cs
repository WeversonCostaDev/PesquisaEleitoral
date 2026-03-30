using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral.Data;
using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private AppDbContext _context;

        public CandidatoController(AppDbContext context)
        {
            _context = context;
        }
    }
}
