using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Integration
{
    [GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [XmlType(AnonymousType = true, Namespace = "", TypeName = "")]
    public class GbdflMessageData
    {
        [XmlElement("messageId", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false, Order = 0)]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();

        [XmlElement("messageDate", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false, Order = 1)]
        public DateTime MessageDate { get; set; } = DateTime.Now;

        [XmlElement("senderCode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false, Order = 2)]
        public string SenderCode { get; set; } = "";

        [XmlElement("iin", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 3)]
        public string Iin { get; set; } = "";

        [XmlElement("surname", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 4)]
        public string Surname { get; set; } = "";

        [XmlElement("name", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 5)]
        public string Name { get; set; } = "";

        [XmlElement("patronymic", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 6)]
        public string Patronymic { get; set; } = "";

        //[XmlElement("birthDate", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 7)]
        //public DateOnly? BirthDate { get; set; } = null;

        //[XmlElement("documentNumber", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 8)]
        //public string DocumentNumber { get; set; } = "";
    }
}
