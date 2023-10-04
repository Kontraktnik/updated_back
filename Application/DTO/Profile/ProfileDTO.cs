using Application.DTO.Step;
using Application.DTO.Survey;
using Application.DTO.System;
using Application.DTO.User;

namespace Application.DTO.Profile;

public class ProfileDTO
{
    public long Id { get; set; }
    public long StepGroupId { get; set; }
    public StepGroupDTO StepGroup { get; set; }

    public long StepId { get; set; }
    public StepDTO Step { get; set; }

    public long SurveyId { get; set; }
    public virtual SurveyDTO Survey { get; set; }

    public long AreaId { get; set; }
    public virtual AreaDTO Area { get; set; }

    public long RequestedUserId { get; set; }
    public virtual UserDTO RequestedUser { get; set; }

    public string RequestedUserIIN { get; set; }
    public int RequestedStatus { get; set; }
    public string RequestedSIGN { get; set; }

    public long? ConfirmedUserId { get; set; }
    public virtual UserDTO? ConfirmedUser { get; set; }

    public string? ConfirmedUserIIN { get; set; }
    public int? ConfirmedStatus { get; set; }
    public string? ConfirmedSIGN { get; set; }

    public int Status { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProfileFileDTO> ProfileFiles { get; set; }

}