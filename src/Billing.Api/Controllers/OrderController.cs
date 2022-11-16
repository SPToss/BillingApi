using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Request;
using XYZ.Billing.Core.Models.Response;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReceiptResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> ProcessOrder([FromBody] OrderRequest order)
        {

            var result = await _orderService.ProcessOrder(_mapper.Map<OrderModel>(order));

            return Ok(_mapper.Map<ReceiptResponse>(result));
        }
    }
}
