using Microsoft.AspNetCore.Mvc;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Zapatilla> zapaillas = this.repo.GetZapatillas();
            return View(zapaillas);
        }

        public IActionResult Detalles(int id)
        {
            Zapatilla zapatilla = this.repo.GetZapatillasDetalles(id);
            return View(zapatilla);
        }

        public async Task<IActionResult>
    PaginarGrupoImagenes(int? posicion, int id)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int registros = this.repo.GetNumeroImagens();
            List<ImagenZapatilla> imagenes =
                await this.repo.GetGrupoImagenesAsync(posicion.Value, id);
            ViewData["REGISTROS"] = registros;
            return View(imagenes);
        }
    }
}
