using Metronik.Data;
using Metronik.DTOs;
using Metronik.Enteties;
using Metronik.Entities;
using Metronik.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Metronik.Controllers;


[ApiController]
[Route("api/v2/")]

public class OrderController : ControllerBase
{


    private DataContext _context;
    private readonly TokenService _tokenService;

    public OrderController(DataContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;

    }

    [HttpPost("orders")]
    public async Task<ActionResult<EmissionResponseDto>> CreateOrder([FromHeader(Name = "clientToken")] string token,
        EmissionRequestDto emissionRequestDto)
    {
        var omsIdToken = _tokenService.ValidateToken(token);
        var omsObject = _context.Oms.FirstOrDefault(x => x.omsId == omsIdToken);
        if (omsObject == null)
        {
            return Unauthorized("Unauthorized access.");
        }

        var uuid = Guid.NewGuid().ToString();

        var newOrder = new AppOrder
        {
            omsId = emissionRequestDto.omsId,
            expectedCompletionTime = 8954353,
            orderId = uuid
        };



        var prodList = new List<AppOrderProduct>();
        foreach (var product in emissionRequestDto.ProductsDto)
        {
            var serialList = new List<AppSerialNumbers>();
            foreach (var serial in product.SerialNumbersDto)
            {
                var ser = new AppSerialNumbers
                {
                    SerialNumber = serial.SerialNumber,
                };

                serialList.Add(ser);
            }

            var pr = new AppOrderProduct
            {
                Gtin = product.Gtin,
                Quantity = product.Quantity,
                SerialNumberType = product.SerialNumberType,
                TemplateId = product.TemplateId,
                AppOrder  = newOrder,
                SerialNumbers = serialList
            };

            prodList.Add(pr);
        }

        var orderDetails = new AppOrderDetails
        {
            FactoryId = emissionRequestDto.OrderDetailsDto.FactoryId,
            FactortyName = emissionRequestDto.OrderDetailsDto.FactoryName,
            FactoryAddress = emissionRequestDto.OrderDetailsDto.FactoryAddress,
            FactortyCountry = emissionRequestDto.OrderDetailsDto.FactoryCountry,
            ProductionLineId = emissionRequestDto.OrderDetailsDto.ProductionLineId,
            ProductCode = emissionRequestDto.OrderDetailsDto.ProductCode,
            ProductDescription = emissionRequestDto.OrderDetailsDto.ProductDescription,
            PoNumber = emissionRequestDto.OrderDetailsDto.PoNumber,
            ExpectedStartDate = emissionRequestDto.OrderDetailsDto.ExpectedStartDate,
            AppOrder = newOrder
        };


        newOrder.AppOrderDetails = orderDetails;
        newOrder.AppOrderProducts = prodList;



        _context.Orders.Add(newOrder);


        await _context.SaveChangesAsync();


        return new EmissionResponseDto
        {
            OmsId = emissionRequestDto.omsId,
            OrderId = uuid,
            ExpectedCompletionTime = newOrder.expectedCompletionTime
        };

    }

    [HttpDelete("orders/{orderId}")]
    public async Task<ActionResult> DeleteOrder([FromHeader(Name = "clientToken")] string token, string orderId)
    {
        var omsIdToken = _tokenService.ValidateToken(token);
        var omsObject = _context.Oms.FirstOrDefault(x => x.omsId == omsIdToken);
        if (omsObject == null)
        {
            return Unauthorized("Unauthorized access.");
        }

        var res = await _context.Orders.FindAsync(Convert.ToInt32(orderId));
        if (res == null)
        {
            return BadRequest("Bad request");
        }

        _context.Orders.Remove(res);
        await _context.SaveChangesAsync();



        return StatusCode(200);
    }




    [HttpGet("orders")]
    public async Task<ActionResult<List<OrderDto>>> GetOrders([FromHeader(Name = "clientToken")] string token,
        string omsId)
    {
        var omsIdToken = _tokenService.ValidateToken(token);
        var omsObject = _context.Oms.FirstOrDefault(x => x.omsId == omsIdToken);
        if (omsObject == null)
        {
            return Unauthorized("Unauthorized access.");
        }

        var resOrders = await _context.Orders.Include(x => x.AppOrderDetails).Include(x => x.AppOrderProducts)
            .ThenInclude(x => x.SerialNumbers).Where(x => x.omsId == omsId).ToListAsync();
        var orderList = new List<OrderDto>();

        foreach (var resOrder in resOrders)
        {

            var orderDetails = new OrderDetailsDto
            {
                Id = resOrder.AppOrderDetails.Id.ToString(),
                FactoryId = resOrder.AppOrderDetails.FactoryId,
                FactoryName = resOrder.AppOrderDetails.FactortyName,
                OmsId = resOrder.omsId,
                FactoryAddress = resOrder.AppOrderDetails.FactoryAddress,
                FactoryCountry = resOrder.AppOrderDetails.FactortyCountry,
                ProductionLineId = resOrder.AppOrderDetails.ProductionLineId,
                ProductCode = resOrder.AppOrderDetails.ProductCode,
                ProductDescription = resOrder.AppOrderDetails.ProductDescription,
                PoNumber = resOrder.AppOrderDetails.PoNumber,
                ExpectedStartDate = resOrder.AppOrderDetails.ExpectedStartDate
            };

            var productsList = new List<OrderProductDto>();


            foreach (var appOrderProduct in resOrder.AppOrderProducts)
            {
                var serialList = new List<SerialNumberDto>();

                foreach (var appSerialNumbers in appOrderProduct.SerialNumbers)
                {
                    serialList.Add(new SerialNumberDto
                    {
                        Id = appSerialNumbers.Id,
                        AppOrderProductId = appSerialNumbers.AppOrderProductId,
                        SerialNumber = appSerialNumbers.SerialNumber
                    });
                }

                productsList.Add(new OrderProductDto
                {
                    Id = appOrderProduct.Id,
                    Gtin = appOrderProduct.Gtin,
                    Quantity = appOrderProduct.Quantity,
                    SerialNumberType = appOrderProduct.SerialNumberType,
                    SerialNumbersDto = serialList,
                    TemplateId = appOrderProduct.TemplateId
                });
            }

            var req = new EmissionRequestDto
            {
                omsId = omsId,
                ProductsDto = productsList,
                OrderDetailsDto = orderDetails,
            };

            orderList.Add(new OrderDto
            {
                Id = resOrder.Id,
                omsId = resOrder.omsId,
                orderId = resOrder.orderId,
                expectedCompletionTime = resOrder.expectedCompletionTime,
                EmissionRequestDto = req
            });
        }

        return orderList;
    }





    [HttpPut("orderDetails")]
    public async Task<ActionResult> UpdateOrderDetails(OrderDetailsDto orderDetailsDto)
    {

        var res = await _context.OrderDetails.FindAsync(Convert.ToInt32(orderDetailsDto.Id));
        if (res == null)
        {
            return BadRequest("No OrderDetails of that Id");
        }

        res.FactortyCountry = orderDetailsDto.FactoryCountry;
        res.ExpectedStartDate = orderDetailsDto.ExpectedStartDate;
        res.FactortyName = orderDetailsDto.FactoryName;
        res.FactoryId = orderDetailsDto.FactoryId;
        res.PoNumber = orderDetailsDto.PoNumber;
        res.ProductCode = orderDetailsDto.ProductCode;
        res.ProductionLineId = orderDetailsDto.ProductionLineId;
        res.ProductDescription= orderDetailsDto.ProductDescription;
        res.FactoryAddress = orderDetailsDto.FactoryAddress;

        _context.Entry(res).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok("Update successful");
    }



    [HttpPut("orderProduct")]
    public async Task<ActionResult> UpdateOrderProduct([FromHeader(Name = "clientToken")] string token, OrderProductDto orderProductDto)
    {

        var omsIdToken = _tokenService.ValidateToken(token);
        var omsObject = _context.Oms.FirstOrDefault(x => x.omsId == omsIdToken);
        if (omsObject == null)
        {
            return Unauthorized("Unauthorized access.");
        }


        var res = await _context.OrderProduct.FindAsync(orderProductDto.Id);
        if (res == null)
        {
            return BadRequest("No OrderDetails of that Id");
        }


        foreach (var serialNumberDto in orderProductDto.SerialNumbersDto.ToArray())
        {
            var serial = await _context.SerialNumbers.FirstOrDefaultAsync(x => x.Id == serialNumberDto.Id);
            if (serial == null)
            {
                _context.SerialNumbers.Add(new AppSerialNumbers
                {
                    AppOrderProduct = res,
                    AppOrderProductId = serialNumberDto.AppOrderProductId,
                    SerialNumber = serialNumberDto.SerialNumber

                });

            }
            else
            {
                serial.SerialNumber = serialNumberDto.SerialNumber;
                _context.Entry(serial).State = EntityState.Modified;

            }

        }


        res.Gtin = orderProductDto.Gtin;
        res.Quantity = Convert.ToUInt64(orderProductDto.Quantity);
        // res.SerialNumbers = newSerialNumbers;
        res.SerialNumberType = orderProductDto.SerialNumberType;
        res.TemplateId = orderProductDto.TemplateId;

        _context.Entry(res).State = EntityState.Modified;

        await _context.SaveChangesAsync();


        return StatusCode(200);
    }

    [HttpDelete("orderProduct/{prodId}")]
    public async Task<ActionResult> DeleteOrderProduct( [FromHeader(Name = "clientToken")] string token, string prodId)
    {

        var omsIdToken = _tokenService.ValidateToken(token);
        var omsObject = _context.Oms.FirstOrDefault(x => x.omsId == omsIdToken);
        if (omsObject == null)
        {
            return Unauthorized("Unauthorized access.");
        }


        var res = await _context.OrderProduct.FindAsync(Convert.ToInt32(prodId));
        if (res == null)
        {
            return BadRequest("No OrderDetails of that Id");
        }

        _context.OrderProduct.Remove(res);

        await _context.SaveChangesAsync();


        return StatusCode(200);
    }


}