using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortMan.Data;

namespace ResortMan.MvcApp.Controllers;

public class AccomodationPackagePicturesController : Controller
{
    private readonly ApplicationDbContext context;

    public AccomodationPackagePicturesController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // GET: AccomodationPackagePictures/Details/5
    public async Task<IActionResult> Details(int id)
    {
        return View(id);
    }
    public async Task<IActionResult> Index(int id)
    {
        var picture = await context.AccomodationPackagePictures.FindAsync(id);
        if (picture == null)
            return NotFound(); 

        return File(picture.Data, picture.ContentType);
    }

    // GET: AccomodationPackagePictures/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: AccomodationPackagePictures/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: AccomodationPackagePictures/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: AccomodationPackagePictures/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: AccomodationPackagePictures/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: AccomodationPackagePictures/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
