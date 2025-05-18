using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController(InvoiceService invoiceService) : ControllerBase
{
    private readonly InvoiceService _invoiceService = invoiceService;

    [HttpPost]
    public async Task<IActionResult> Create(AddInvoiceFormData project)
    {
        if (!ModelState.IsValid)
            return BadRequest(project);

        var result = await _invoiceService.AddInvoiceAsync(project);
        return result ? Ok() : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync(orderByDescending: true);
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        return invoice == null ? NotFound() : Ok(invoice);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateInvoiceFormData invoice)
    {
        if (!ModelState.IsValid)
            return BadRequest(invoice);

        var result = await _invoiceService.UpdateInvoiceAsync(invoice);
        return result ? Ok() : NotFound();
    }

    //Just for testing purposes

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        var result = await _invoiceService.DeleteInvoiceAsync(id);
        return result ? Ok() : NotFound();
    }
}
