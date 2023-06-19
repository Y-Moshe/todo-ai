using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/subtasks")]
[Authorize]
public class SubTaskController : BaseApiController
{
    private readonly ISubTaskService _subTaskService;
    private readonly IMapper _mapper;

    public SubTaskController(ISubTaskService subTaskService, IMapper mapper)
    {
        _mapper = mapper;
        _subTaskService = subTaskService;
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IReadOnlyList<SubTask>>>
        GetCompletedSubTasksAsync()
    {
        var list = await _subTaskService.ListCompletedSubTasksAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult<SubTask>> CreateSubTask(SubTaskDto taskDto)
    {
        var subTask = _mapper.Map<SubTask>(taskDto);
        await _subTaskService.CreateSubTaskAsync(subTask);
        return Ok(subTask);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SubTask>> UpdateSubTask(
        int id, [FromBody] SubTaskDto taskDto)
    {
        taskDto.Id = id;
        var subTask = _mapper.Map<SubTask>(taskDto);
        var result = await _subTaskService.UpdateSubTaskAsync(subTask);

        if (result == null) return NotFound(
            new ApiErrorResponse(404, "SubTask not found!"));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task DeleteSubTask(int id)
    {
        await _subTaskService.DeleteSubTaskAsync(id);
    }
}
