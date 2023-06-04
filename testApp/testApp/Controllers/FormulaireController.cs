using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApp.Context;
using testApp.Models;
using testApp.PostModels;

namespace testApp.Controllers;

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
    public async Task<ResponceMessage> addForm(FormModule module)
    {
        ResponceMessage responce = new ResponceMessage();
        try
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(module.file.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
            if (module.file != null && module.file.Length > 0)
            {
                // Generate a unique filename or use the original filename

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await module.file.CopyToAsync(stream);
                }
            }
            Formulaire formulaire = new Formulaire(module.form);
            formulaire.path = filePath;
            responce.dataResult = await _context.AddAsync(formulaire);
            await _context.SaveChangesAsync();
            if (responce.dataResult != null)
            {
                responce.Message = "Success";
                responce.isSuccess = true;
            }
            else
            {
                responce.Message = "Faild";
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
