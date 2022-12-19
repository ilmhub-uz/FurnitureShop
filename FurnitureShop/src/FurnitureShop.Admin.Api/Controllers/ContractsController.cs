using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _service;
        private readonly IUnitOfWork _unitOfWork;
        public ContractsController(IContractsService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts([FromQuery] ContractsFilterDto filter)
        {
            var contracts = await _service.GetContracts(filter);
            return Ok(contracts);
        }

        [HttpGet("{contractId:Guid}")]
        public async Task<IActionResult> GetContract(Guid contractId)
        {
            var contract = await _service.GetContract(contractId);
            return Ok(contract);
        }
        [HttpPut("{contractId:Guid}")]
        public async Task<IActionResult> UpdateContract(Guid contractId, [FromBody] UpdateContractDto contractDto)
        {
            await _service.UpdateContract(contractId, contractDto);
            return Ok();
        }
        [HttpDelete("{contractId:Guid}")]
        public async Task<IActionResult> DeleteContract(Guid contractId)
        {
            await _service.DeleteContract(contractId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalSales([FromQuery] OrganizationFilterDto filter)
        {
            var contracts = _unitOfWork.Contracts.GetAll();

            if (filter.OrganizationId is not null)
            {
                var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(o => o.Id == filter.OrganizationId);
                if (organization is null)
                    throw new NotFoundException<Organization>();
                contracts = contracts.Where(c => c.Product!.OrganizationId == filter.OrganizationId);
            }

            var totalSale = await contracts.Select(c => c.TotalPrice).SumAsync();
            return Ok(totalSale);
        }
    }
}
