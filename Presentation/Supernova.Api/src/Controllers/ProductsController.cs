using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supernova.Application.Abstraction;

namespace Supernova.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
	private readonly IProductService productService;
	public ProductsController(IProductService productService)
	{
		this.productService = productService;
	}

	[HttpGet]
	public IActionResult GetProducts()
	{
		var products = productService.GetProducts();
		return Ok(products);
	}
}
