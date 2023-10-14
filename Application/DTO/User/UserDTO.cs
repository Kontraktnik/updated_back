using Application.DTO.System;
using Application.Enums;

namespace Application.DTO.User;

public class UserDTO
{
    public long Id { get; set; }

    public long RoleId { get; set; }
    public virtual  RoleDTO Role { get; set; }
    //Area
    public long? AreaId { get; set; }
    public virtual  AreaDTO? Area { get; set; }
    //Common Fields
    public string? ImageUrl { get; set; }
    public string IIN { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public bool Verified { get; set; }
    
    public bool Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public DateOnly BirthDay  {
        get
        {
            try
            {
                // Extract the year, month, and day from the IIN string
                int year = int.Parse(IIN.Substring(0, 2));
                int month = int.Parse(IIN.Substring(2, 2));
                int day = int.Parse(IIN.Substring(4, 2));

                // Determine the century based on the first digit of the year
                int century = (year < 23) ? 20 : 19; // Adjust the threshold if needed

                // Create a DateOnly object representing the birthdate
                return new DateOnly(century * 100 + year, month, day);
            }
            catch
            {
                return new DateOnly();
            }
            
        }
    }

    public GenderEnum Gender
    {
        get
        {
            if (IIN == null || IIN.Length < 6)
            {
                return GenderEnum.Unknown;
            }

            var genderDigit = int.Parse(IIN.Substring(6, 1));

            switch (genderDigit)
            {
                case 1:
                case 3:
                case 5:
                    return GenderEnum.Male;
                case 2:
                case 4:
                case 6:
                    return GenderEnum.Female;
                default:
                    return GenderEnum.Unknown;
            }
        }
    }
    
}