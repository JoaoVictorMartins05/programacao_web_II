using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_WEB_2.Models;

namespace Projeto_WEB_2.Controllers
{
    [Authorize(Roles = "Medico,Nutricionista")]
    public class TbPacientesController : Controller
    {
        private readonly db_IF_WEB_IIContext _context;

        public TbPacientesController(db_IF_WEB_IIContext context)
        {
            _context = context;
        }

        // GET: TbPacientes
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Medico") || User.IsInRole("Nutricionista"))
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var db_IF_WEB_IIContext = _context.TbPaciente
                    .Include(t => t.IdCidadeNavigation)
                    .Include(t => t.TbMedicoPaciente)
                    .ThenInclude(s => s.IdProfissionalNavigation)
                    .Where(t => t.TbMedicoPaciente.Any(mp => mp.IdProfissionalNavigation.IdUser == userId));
                return View(await db_IF_WEB_IIContext.ToListAsync());
                
            }
            return View(null);
        }

        // GET: TbPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPaciente = await _context.TbPaciente
                .Include(t => t.IdCidadeNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (tbPaciente == null)
            {
                return NotFound();
            }

            return View(tbPaciente);
        }

        // GET: TbPacientes/Create
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome");
            return View();
        }

        // POST: TbPacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Rg,Cpf,DataNascimento,NomeResponsavel,Sexo,Etnia,Endereco,Bairro,IdCidade,TelResidencial,TelComercial,TelCelular,Profissao,FlgAtleta,FlgGestante")] TbPaciente tbPaciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                    _context.Add(tbPaciente);
                    await _context.SaveChangesAsync();

                    // Crie uma instância de TbMedicoPaciente e defina os valores
                    TbMedicoPaciente tbMedicoPaciente = new TbMedicoPaciente
                    {
                        IdPaciente = tbPaciente.IdPaciente, // Use o Id do paciente recém-criado
                        IdProfissional = GetProfissionalId(userId), // Defina o Id do profissional usando sua lógica atual
                        InformacaoResumida = "Informações resumidas aqui" // Defina as informações resumidas conforme necessário
                    };

                    // Adicione a instância de TbMedicoPaciente ao contexto
                    _context.Add(tbMedicoPaciente);

                    // Salve as mudanças no contexto
                    await _context.SaveChangesAsync();



                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "IdCidade", tbPaciente.IdCidade);
                return View(tbPaciente);
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "IdCidade", tbPaciente.IdCidade);
            return View(tbPaciente);
        }

        // Método para obter o Id do profissional com base no userId
        private int GetProfissionalId(string userId)
        {
            var profissional = _context.TbProfissional.FirstOrDefault(p => p.IdUser == userId);
            return profissional != null ? profissional.IdProfissional : 0;
        }

        // GET: TbPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPaciente = await _context.TbPaciente.FindAsync(id);
            if (tbPaciente == null)
            {
                return NotFound();
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            return View(tbPaciente);
        }

        // POST: TbPacientes/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var pacientToUpdate = await _context.TbPaciente.FirstOrDefaultAsync(s => s.IdPaciente == id);

            if(pacientToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<TbPaciente>(
                pacientToUpdate,
                "",
                s => s.Nome, s => s.Rg, s => s.Cpf, s => s.DataNascimento, s => s.NomeResponsavel, s => s.Sexo, s => s.Etnia, s => s.Endereco, s => s.Bairro, s => s.IdCidade,
                s => s.TelResidencial, s => s.TelComercial, s => s.TelCelular, s => s.Profissao, s => s.FlgAtleta, s => s.FlgGestante))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }


            return View(pacientToUpdate);
        }

        // GET: TbPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPaciente = await _context.TbPaciente
                .Include(t => t.IdCidadeNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (tbPaciente == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tbPaciente);
        }

        // POST: TbPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPaciente = await _context.TbPaciente.FindAsync(id);

            if (tbPaciente == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.TbPaciente.Remove(tbPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }

        private bool TbPacienteExists(int id)
        {
            return _context.TbPaciente.Any(e => e.IdPaciente == id);
        }
    }
}
