using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApp.Context;
using testApp.Models;

namespace testApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulaireController : ControllerBase
    {
        private TestContext _context;
        public FormulaireController(TestContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<ResponceMessage> GetFormulaireList()
        {
            ResponceMessage responce = new ResponceMessage();
            try
            {
                responce.dataResult = (await _context.Set<Formulaire>().ToListAsync());
                responce.isSuccess = true;
                responce.Message = "Success";
            }
            catch (Exception ex)
            {
                responce.isSuccess = false;
                responce.Message = ex.Message;
            }
            return responce;
        }

        [HttpPost("insertFormulaire")]
        public async Task<ResponceMessage> addForm(string nom, string prenom, string mail, string tele, string niveau, int exp, string path, string der)
        {
            ResponceMessage responce = new ResponceMessage();
            try
            {
                Formulaire formulaire = new Formulaire();
                formulaire.nom = nom;
                formulaire.prenom = prenom;
                formulaire.mail = mail;
                formulaire.telephone = tele;
                formulaire.niveau = niveau;
                formulaire.experience = exp;
                formulaire.path = path;
                formulaire.dernierEmployeur = der;
                await _context.AddAsync(formulaire);
                await _context.SaveChangesAsync();
                if (responce.dataResult != null)
                {
                    responce.Message = "Success";
                    responce.isSuccess = true;
                }
                else
                {
                    responce.Message = "faild";
                    responce.isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                responce.Message = ex.Message;
                responce.isSuccess = false;
            }
            return responce;
        }
    }
}
