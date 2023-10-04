using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Xml;
using Microsoft.Extensions.Logging;
using System.Text;
using System.IO;

namespace IntegrationTest
{
    public class LoggingMessageInspector : IClientMessageInspector
    {
        public LoggingMessageInspector(ILogger<LoggingMessageInspector> logger)
        {
            Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public ILogger<LoggingMessageInspector> Logger { get; }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            using var buffer = reply.CreateBufferedCopy(int.MaxValue);
            var document = GetDocument(buffer.CreateMessage());
            Logger.LogWarning(document.OuterXml);

            string path = $@"c:\temp\gbdflResponse-{DateTime.Now.Ticks}.txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(@"c:\temp");
            }

            using FileStream fs = File.Create(path);
            byte[] info = Encoding.UTF8.GetBytes(document.OuterXml);
            // Add some information to the file.
            fs.Write(info, 0, info.Length);

            reply = buffer.CreateMessage();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            using var buffer = request.CreateBufferedCopy(int.MaxValue);
            var document = GetDocument(buffer.CreateMessage());
            Logger.LogWarning(document.OuterXml);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("VYPOLNYAETSYA ZAPROS...");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            string path = $@"c:\temp\gbdflRequest-{DateTime.Now.Ticks}.txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(@"c:\temp");
            }

            using FileStream fs = File.Create(path);
            byte[] info = Encoding.UTF8.GetBytes(document.OuterXml);
            // Add some information to the file.
            fs.Write(info, 0, info.Length);

            request = buffer.CreateMessage();
            return null;
        }

        private XmlDocument GetDocument(Message request)
        {
            XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = ("\t"),
                OmitXmlDeclaration = true
            };
            XmlDocument document = new();
            using MemoryStream memoryStream = new();

            using XmlWriter writer = XmlWriter.Create(memoryStream, settings);
            request.WriteMessage(writer);
            writer.Flush();
            memoryStream.Position = 0;

            // load memory stream into a document
            document.Load(memoryStream);

            return document;
        }
    }
}
