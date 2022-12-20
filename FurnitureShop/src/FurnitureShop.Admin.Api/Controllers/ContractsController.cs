using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {

        private IContractService _contractService;
        private readonly IUnitOfWork _unitOfWork;
        
        public ContractsController(IUnitOfWork unitOfWork , IContractService contractService)
        {
            _unitOfWork = unitOfWork;
            _contractService = contractService;
        }

    

    }
}
