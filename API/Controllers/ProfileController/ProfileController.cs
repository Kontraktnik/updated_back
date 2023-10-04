using System.Security.Claims;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.Survey;
using Application.Features.ProfileCQRS.Command.SendConfirmation;
using Application.Features.ProfileCQRS.Command.SendRequest;
using Application.Features.SurveyExecutorCQRS.Command.AddSurveyExecutor;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProfileController;

public class ProfileController : BaseApiController
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    
    

    

    

    
    
    
    
    
    
    
    
    
    
    
    
    
    


}