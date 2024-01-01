using LAB6.Data;
using LAB6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LAB6.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        public EmployeeController(EmployeeContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Layout = "_Lab2Layout";
            var employees = _context.Employees.Include(e => e.Company);
            return View(await employees.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            ViewBag.Layout = "_Lab2Layout";

            var employee = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", employee.CompanyId);
            return View(employee);
        }
        [HttpPost("[controller]/[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthDate,Position,Image,CompanyId")] Employee updatedEmployee, IFormFile image)
        {
            if (id != updatedEmployee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _context.Entry(updatedEmployee);
                    employee.State = EntityState.Modified;
                    if (image != null && image.Length > 0)
                    {

                        // Generate a unique filename
                        string uniqueFileName = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + image.FileName;
                        // Combine the filename with the wwwroot path where to store the images
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);
                        try
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }

                            // Update the image path only if a new image is provided
                            // Delete the old image file if it exists

                            var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", updatedEmployee.Image.Replace("/images/", ""));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                            updatedEmployee.Image = "/images/" + uniqueFileName;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            ModelState.AddModelError("", "Something went wrong while saving the new image");
                            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", updatedEmployee.CompanyId);
                            return View(updatedEmployee);
                        }
                    }
                    else
                    {
                        employee.Property(e => e.Image).IsModified = false;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(updatedEmployee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Something went wrong");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", updatedEmployee.CompanyId);
            return View(updatedEmployee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View();

        }
        [HttpPost("[action]/[controller]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,BirthDate,Position,Image,CompanyId")] Employee employee, IFormFile image)

        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    // Generate a unique filename
                    string uniqueFileName = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + image.FileName;
                    // Combine the filename with the wwwroot path where to store the images
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);

                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        ModelState.AddModelError("", "Something went wrong");
                    }
                    employee.Image = "/images/" + uniqueFileName;
                }
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Something went wrong");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", employee.CompanyId);
            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'EmployeeContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(employee.Image))
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", employee.Image.Replace("/images/", ""));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
