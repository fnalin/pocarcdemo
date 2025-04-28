using Fansoft.PocArc.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fansoft.PocArc.Front.Controllers;

[Authorize]
public class CustomersController(ApiService apiService) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? search = null)
    {
        var (customers, currentPage, currentPageSize, totalPages, totalItems) = await apiService.GetCustomersPagedAsync(page, pageSize, search);

        ViewData["CurrentPage"] = currentPage;
        ViewData["PageSize"] = currentPageSize;
        ViewData["TotalPages"] = totalPages;
        ViewData["TotalItems"] = totalItems;
        ViewData["Search"] = search ?? string.Empty;

        return View(customers);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerDto customer)
    {
        if (!ModelState.IsValid)
            return View(customer);

        await apiService.CreateCustomerAsync(customer);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        var customer = await apiService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, CustomerDto customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        await apiService.UpdateCustomerAsync(customer);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await apiService.DeleteCustomerAsync(id);
        return RedirectToAction(nameof(Index));
    }
    
}