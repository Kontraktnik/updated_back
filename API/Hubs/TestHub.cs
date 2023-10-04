using System.Security.Claims;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class TestHub : Hub
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TestHub(IMediator _mediator,IHttpContextAccessor contextAccessor)
    {
        this._mediator = _mediator;
        _httpContextAccessor = contextAccessor;
    }
    public async Task sendTestMessage(string GroupName)
    {
        await Clients.All.SendAsync("TestMessage", "Hello!");
        await Clients.Group("directorArea" + 1.ToString()).SendAsync("TestMessage", "Hello my friend director");
    }
    
    public async override Task OnConnectedAsync()
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        if (user.Data != null)
        {
            if (user.Data.RoleId.Equals(AppConstant.DirectorRoleId))
            {
                AddToGroup("directorArea" + user.Data.AreaId.ToString());
            }
        }
        
        Clients.All.SendAsync("TestMessage", _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
         
    }
    
    
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}