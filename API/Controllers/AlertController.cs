using API.Dtos;
using API.Helpers;
using API.Helpers.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class AlertController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AlertController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<Alert>>> GetAll([FromQuery] Params alertParams)
    {
        var result = await _unitOfWork.Alert.GetAllWithPaginationAsync(alertParams.PageIndex, alertParams.PageSize, alertParams.Search);

        var alerts = _mapper.Map<List<Alert>>(result.registers);

        return new Pager<Alert>(alerts, result.totallyRegister, alertParams.PageIndex, alertParams.PageSize, alertParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Alert>> Post(AddAlertDto alertDto)
    {
        var product = _unitOfWork.Products.Find(u => u.Id == alertDto.ProductId).FirstOrDefault();

        if (product == null)
            return BadRequest(new ApiResponse(400, $"Product don't exists"));

        var store = _unitOfWork.Stores.Find(u => u.Id == alertDto.StoreId).FirstOrDefault();

        if (store == null)
            return BadRequest(new ApiResponse(400, $"Store don't exists"));


        var alert = _mapper.Map<Alert>(alertDto);


        _unitOfWork.Alert.Add(alert);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(Post), alert);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var alert = await _unitOfWork.Alert.GetByIdAsync(id);

        if (alert == null)
            return NotFound(new ApiResponse(404));

        _unitOfWork.Alert.Remove(alert);
        await _unitOfWork.SaveAsync();

        return Ok(new
        {
            Id = id,
        });
    }
}
