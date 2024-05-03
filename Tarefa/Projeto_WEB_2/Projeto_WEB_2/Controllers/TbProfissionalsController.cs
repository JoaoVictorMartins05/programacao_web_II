using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_WEB_2.Models;

namespace Projeto_WEB_2.Controllers
{
    [Authorize]
    public class TbProfissionalsController : Controller
    {
        private readonly db_IF_WEB_IIContext _context;

        public TbProfissionalsController(db_IF_WEB_IIContext context)
        {
            _context = context;
        }

        public enum Plano
        {
            MedicoTotal = 1,
            MedicoParcial = 2,
            Nutricionista = 3
        }

        // GET: TbProfissionals
        [Authorize(Roles = "Medico,Nutricionista,GerenteGeral,GerenteMedico,GerenteNutricionista")]
        public IActionResult Index()
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (User.IsInRole("Medico"))
            {
                var db_IF_WEB_IIContext = _context.TbProfissional
                    //.Where(t => (Plano)t.IdContratoNavigation.IdPlano == Plano.MedicoTotal)
                    .Where(t => t.IdUser == userId)
                    .Select(pro => new ProfissionalResumido
                    {
                        Nome = pro.Nome,
                        NomeCidade = pro.IdCidadeNavigation.Nome,
                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                        IdProfissional = pro.IdProfissional,
                        Cpf = pro.Cpf,
                        CrmCrn = pro.CrmCrn,
                        Especialidade = pro.Especialidade,
                        Logradouro = pro.Logradouro,
                        Numero = pro.Numero,
                        Bairro = pro.Bairro,
                        Cep = pro.Cep,
                        Ddd1 = pro.Ddd1,
                        Ddd2 = pro.Ddd2,
                        Telefone1 = pro.Telefone1,
                        Telefone2 = pro.Telefone2,
                        Salario = pro.Salario
                    });



                return View(db_IF_WEB_IIContext);

            }
            else
            {
                if (User.IsInRole("Nutricionista"))
                {
                    var db_IF_WEB_IIContext = _context.TbProfissional
                        //.Where(t => (Plano)t.IdContratoNavigation.IdPlano == Plano.Nutricionista)
                        .Where(t => t.IdUser == userId)
                        .Select(pro => new ProfissionalResumido
                        {
                            Nome = pro.Nome,
                            NomeCidade = pro.IdCidadeNavigation.Nome,
                            NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                            IdProfissional = pro.IdProfissional,
                            Cpf = pro.Cpf,
                            CrmCrn = pro.CrmCrn,
                            Especialidade = pro.Especialidade,
                            Logradouro = pro.Logradouro,
                            Numero = pro.Numero,
                            Bairro = pro.Bairro,
                            Cep = pro.Cep,
                            Ddd1 = pro.Ddd1,
                            Ddd2 = pro.Ddd2,
                            Telefone1 = pro.Telefone1,
                            Telefone2 = pro.Telefone2,
                            Salario = pro.Salario
                        });
                    return View(db_IF_WEB_IIContext);

                }
                else
                {
                    if (User.IsInRole("GerenteNutricionista"))
                    {
                        var db_IF_WEB_IIContext = _context.TbProfissional
                        .Where(t => (Plano)t.IdContratoNavigation.IdPlano == Plano.Nutricionista)
                        .Select(pro => new ProfissionalResumido
                        {
                            Nome = pro.Nome,
                            NomeCidade = pro.IdCidadeNavigation.Nome,
                            NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                            IdProfissional = pro.IdProfissional,
                            Cpf = pro.Cpf,
                            CrmCrn = pro.CrmCrn,
                            Especialidade = pro.Especialidade,
                            Logradouro = pro.Logradouro,
                            Numero = pro.Numero,
                            Bairro = pro.Bairro,
                            Cep = pro.Cep,
                            Ddd1 = pro.Ddd1,
                            Ddd2 = pro.Ddd2,
                            Telefone1 = pro.Telefone1,
                            Telefone2 = pro.Telefone2,
                            Salario = pro.Salario
                        });
                        return View(db_IF_WEB_IIContext);
                    }
                    else
                    {
                        if (User.IsInRole("GerenteMedico"))
                        {
                            var db_IF_WEB_IIContext = _context.TbProfissional
                            .Where(t => (Plano)t.IdContratoNavigation.IdPlano == Plano.MedicoTotal || (Plano)t.IdContratoNavigation.IdPlano == Plano.MedicoParcial)
                            .Select(pro => new ProfissionalResumido
                            {
                                Nome = pro.Nome,
                                NomeCidade = pro.IdCidadeNavigation.Nome,
                                NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                IdProfissional = pro.IdProfissional,
                                Cpf = pro.Cpf,
                                CrmCrn = pro.CrmCrn,
                                Especialidade = pro.Especialidade,
                                Logradouro = pro.Logradouro,
                                Numero = pro.Numero,
                                Bairro = pro.Bairro,
                                Cep = pro.Cep,
                                Ddd1 = pro.Ddd1,
                                Ddd2 = pro.Ddd2,
                                Telefone1 = pro.Telefone1,
                                Telefone2 = pro.Telefone2,
                                Salario = pro.Salario
                            });
                            return View(db_IF_WEB_IIContext);
                        }
                        else
                        {
                            if (User.IsInRole("GerenteGeral"))
                            {
                                var db_IF_WEB_IIContext = _context.TbProfissional
                                .Select(pro => new ProfissionalResumido
                                {
                                    Nome = pro.Nome,
                                    NomeCidade = pro.IdCidadeNavigation.Nome,
                                    NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                    IdProfissional = pro.IdProfissional,
                                    Cpf = pro.Cpf,
                                    CrmCrn = pro.CrmCrn,
                                    Especialidade = pro.Especialidade,
                                    Logradouro = pro.Logradouro,
                                    Numero = pro.Numero,
                                    Bairro = pro.Bairro,
                                    Cep = pro.Cep,
                                    Ddd1 = pro.Ddd1,
                                    Ddd2 = pro.Ddd2,
                                    Telefone1 = pro.Telefone1,
                                    Telefone2 = pro.Telefone2,
                                    Salario = pro.Salario
                                });
                                return View(db_IF_WEB_IIContext);
                            }
                        }
                    }
                }
            }
            return View(null);
        }

        // GET: TbProfissionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TbProfissional? tbProfissional = await _context.TbProfissional
                .Include(t => t.IdCidadeNavigation)
                .Include(t => t.IdTipoAcessoNavigation)
                .Include(t => t.IdContratoNavigation)
                .ThenInclude(s => s.IdPlanoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdProfissional == id);
            if (tbProfissional == null)
            {
                return NotFound();
            }

            return View(tbProfissional);
        }

        // GET: TbProfissionals/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "IdCidade");
            ViewData["IdPlano"] = new SelectList(_context.TbPlano, "IdPlano", "IdPlano");
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome");
            return View();
        }

        // POST: TbProfissionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoProfissional,IdTipoAcesso,IdCidade,Nome,Cpf,CrmCrn,Especialidade,Logradouro,Numero,Bairro,Cep,Cidade,Estado,Ddd1,Ddd2,Telefone1,Telefone2,Salario")] TbProfissional tbProfissional, [Bind("IdPlano")] TbContrato IdContratoNavigation)
        {
            ModelState.Remove("IdUser");
            ModelState.Remove("IdContrato");

            try
            {
                if (ModelState.IsValid)
                {
                    IdContratoNavigation.DataInicio = DateTime.UtcNow;
                    IdContratoNavigation.DataFim = IdContratoNavigation.DataInicio.Value.AddMonths(1);
                    _context.Add(IdContratoNavigation);
                    await _context.SaveChangesAsync();


                    var userManager = HttpContext.RequestServices.GetService<UserManager<IdentityUser>>();
                    if (userManager != null)
                    {
                        var email = User.Identity?.Name;
                        if (email != null)
                        {
                            var user = await userManager.FindByEmailAsync(email);
                            if (user != null)
                            {
                                tbProfissional.IdUser = user.Id;
                            }
                            else
                            {
                                return NotFound();

                            }
                        }
                        else
                        {
                            return NotFound();

                        }
                    }
                    else
                    {
                        return NotFound();
                    }

                    tbProfissional.IdContrato = IdContratoNavigation.IdContrato;
                    _context.Add(tbProfissional);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar as mudanças.");
            }

            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdPlano"] = new SelectList(_context.TbContrato, "IdPlano", "Nome", IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional.FindAsync(id);
            if (tbProfissional == null)
            {
                return NotFound();
            }

            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdContrato"] = new SelectList(_context.TbPlano, "IdPlano", "Nome");// tbProfissional.IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // POST: TbProfissionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional.FirstOrDefaultAsync(s => s.IdProfissional == id);

            if (tbProfissional == null)
            {
                return NotFound();
            }

            //Validação backend para Médico e Nutricionista não atualizarem o CPF.
            if (!User.IsInRole("GerenteGeral,GerenteMedico,GerenteNutricionista"))
            {
                if (await TryUpdateModelAsync<TbProfissional>(
                     tbProfissional,
                     "",
                     s => s.IdTipoProfissional, s => s.IdTipoAcesso, s => s.IdCidade, s => s.Nome, s => s.CrmCrn, s => s.Especialidade,
                     s => s.Logradouro, s => s.Numero, s => s.Bairro,
                     s => s.Cep, s => s.Ddd1, s => s.Ddd2, s => s.Telefone1
                     , s => s.Telefone2, s => s.Salario, s => s.IdContrato))
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
            }
            else
            {
                if (await TryUpdateModelAsync<TbProfissional>(
                     tbProfissional,
                     "",
                     s => s.IdTipoProfissional, s => s.IdTipoAcesso, s => s.Cpf, s => s.IdCidade, s => s.Nome, s => s.CrmCrn, s => s.Especialidade,
                     s => s.Logradouro, s => s.Numero, s => s.Bairro,
                     s => s.Cep, s => s.Ddd1, s => s.Ddd2, s => s.Telefone1
                     , s => s.Telefone2, s => s.Salario, s => s.IdContrato))
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
            }

            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdContrato"] = new SelectList(_context.TbContrato, "IdPlano", "Nome", tbProfissional.IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Delete/5
        [Authorize(Roles = "GerenteGeral,GerenteMedico,GerenteNutricionista")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional
                .Include(t => t.IdCidadeNavigation)
                .Include(t => t.IdTipoAcessoNavigation)
                .Include(t => t.IdContratoNavigation)
                .ThenInclude(s => s.IdPlanoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdProfissional == id);
            if (tbProfissional == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tbProfissional);
        }

        // POST: TbProfissionals/Delete/5
        [HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Gerente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProfissional = await _context.TbProfissional.FindAsync(id);

            if (tbProfissional == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.TbProfissional.Remove(tbProfissional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool TbProfissionalExists(int id)
        {
            return _context.TbProfissional.Any(e => e.IdProfissional == id);
        }
    }
}
