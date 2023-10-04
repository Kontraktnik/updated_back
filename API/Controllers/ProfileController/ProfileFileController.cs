using System.Security.Claims;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.Features.ProfileFileCQRS.Command;
using Application.Features.ProfileFileCQRS.Command.DeleteProfileFile;
using Application.Features.ProfileFileCQRS.Query.GetProfileFileByIdAsync;
using Application.Features.ProfileFileCQRS.Query.ListProfileFileWithSpecAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Infrastracture.Contracts.Specifications.ProfileFileSpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProfileController;

public class ProfileFileController : BaseApiController
{
    private readonly IMediator _mediator;

    public ProfileFileController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [Authorize]
    [HttpPost]
    
    public async Task<ActionResult<ResponseDTO<ProfileFileDTO>>> AddProfileFile([FromForm] IFormFile file, [FromQuery] ProfileFileCUDTO model)
    {
        var user =   await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));

        //Check if file exists
        if (file != null)
        {
            var result = await _mediator.Send(new AddProfileFileCommand(
                Path.GetFileNameWithoutExtension(file.FileName),
                Path.GetExtension(file.FileName),
                model,
                user.Data));

            if (result.Success)
            {
                await using (var stream = new FileStream(result.Data.File, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return StatusCode(result.StatusCode, result);
        }
        else
        {
            return  BadRequest();
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<ResponseDTO<bool>>> DeleteProfileFile([FromQuery] long Id)
    {
        var user =   await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new GetProfileFileByIdAsyncQuery(Id));
        if (result.Success)
        {
            var filePath = result.Data.File;
            var resultDeleted = await _mediator.Send(new DeleteProfileFileCommand(Id,user.Data));
            if (resultDeleted.Success)
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            return StatusCode(resultDeleted.StatusCode, resultDeleted);
        }
        return  BadRequest();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ResponseDTO<ICollection<ProfileFileDTO>>>> GetAll([FromQuery] long ProfileId)
    {
        var user =   await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var specification = new ProfileFileManagementSpecification(ProfileId, user.Data.Id);
        return await _mediator.Send(new ListProfileFileWithSpecAsyncQuery(specification));
    }


    

}