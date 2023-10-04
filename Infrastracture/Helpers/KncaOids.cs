using Kz.GammaTech.Asn1;

namespace Infrastracture.Helpers;

public class KncaOids
{
    /// <summary>
    /// Тип пользователя
    /// </summary>
    public static readonly DerObjectIdentifier UserTypeOid = new("1.2.398.3.3.4.1");

    /// <summary>
    /// Физическое лицо
    /// </summary>
    public static readonly DerObjectIdentifier IndividualOid = new("1.2.398.3.3.4.1.1");

    /// <summary>
    /// Информационная система физического лица
    /// </summary>
    public static readonly DerObjectIdentifier IndividualIsOid = new("1.2.398.3.3.4.1.1.1");

    /// <summary>
    /// Юридическое лицо/индивидуальный предприниматель 
    /// осуществляющий деятельность 
    /// в виде совместного предпринимательства
    /// </summary>
    public static readonly DerObjectIdentifier LegalEntityOid = new("1.2.398.3.3.4.1.2");

    /// <summary>
    /// Первый руководитель юридического лица/ индивидуальный предприниматель 
    /// осуществляющий деятельность в виде совместного предпринимательства
    /// </summary>
    public static readonly DerObjectIdentifier FirstHeadOid = new("1.2.398.3.3.4.1.2.1");

    /// <summary>
    /// Лицо, наделенное правом подписи
    /// </summary>
    public static readonly DerObjectIdentifier SignatureRightOid = new("1.2.398.3.3.4.1.2.2");

    /// <summary>
    /// Лицо, наделенное правом подписи финансовых документов
    /// </summary>
    public static readonly DerObjectIdentifier FinSignatureRightOid = new("1.2.398.3.3.4.1.2.3");

    /// <summary>
    /// Сотрудник отдела кадров
    /// </summary>
    public static readonly DerObjectIdentifier HrOid = new("1.2.398.3.3.4.1.2.4");

    /// <summary>
    /// Сотрудник организации
    /// </summary>
    public static readonly DerObjectIdentifier EmployeeOid = new("1.2.398.3.3.4.1.2.5");

    /// <summary>
    /// Информационная система юридического лица
    /// </summary>
    public static readonly DerObjectIdentifier LegalEntityIsOid = new("1.2.398.3.3.4.1.2.6");

    public static readonly Dictionary<DerObjectIdentifier, string> UserTypes;

    static KncaOids()
    {
        UserTypes = new Dictionary<DerObjectIdentifier, string>
        {
            [IndividualOid] = "Физическое лицо",
            [IndividualIsOid] = "Информационная система физического лица",
            [FirstHeadOid] = "Первый руководитель",
            [SignatureRightOid] = "Лицо, наделенное правом подписи",
            [FinSignatureRightOid] = "Лицо, наделенное правом подписи финансовых документов",
            [HrOid] = "Сотрудник отдела кадров",
            [EmployeeOid] = "Сотрудник организации",
            [LegalEntityIsOid] = "Информационная система юридического лица"
        };
    }
}
