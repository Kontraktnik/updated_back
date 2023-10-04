// See https://aka.ms/new-console-template for more information
using Integration;
using IntegrationTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShepAsyncHttp;
using System.Reflection.Metadata;
using System.ServiceModel;
using System.Text;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<LoggingEndpointBehaviour>();
        services.AddTransient<LoggingMessageInspector>();
    })
    .Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

Console.WriteLine("VVEDITE IIN:");

//XmlWriter? writer = null;

//try
//{
//    XmlWriterSettings settings = new()
//    {
//        Indent = true,
//        IndentChars = ("\t"),
//        OmitXmlDeclaration = true
//    };

//    // Create the XmlWriter object and write some content.
//    writer = XmlWriter.Create("data.xml", settings);
//    writer.WriteStartElement("book");
//    writer.WriteElementString("item", "tesing");
//    writer.WriteEndElement();

//    writer.Flush();
//}
//finally
//{
//    if (writer != null)
//        writer.Close();
//}

await MakeRequest(host.Services, config);

async Task MakeRequest(IServiceProvider hostProvider, IConfiguration conf)
{
    try
    {
        AsyncChannelClient shepClient = new();
        SetShepCredentials(shepClient, conf);
        //await shepClient.OpenAsync();

        using IServiceScope serviceScope = hostProvider.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;

        shepClient.Endpoint.EndpointBehaviors.Add(provider.GetRequiredService<LoggingEndpointBehaviour>());

        string? iin = Console.ReadLine();

        var message = new sendMessage()
        {
            request = new AsyncSendMessageRequest()
            {
                messageInfo = new AsyncMessageInfo()
                {
                    messageDate = DateTime.Now,
                    messageId = Guid.NewGuid().ToString(),
                    correlationId = Guid.NewGuid().ToString(),
                    messageType = AsyncMessageInfoMessageType.REQUEST,
                    sender = new SenderInfo()
                    {
                        senderId = conf.GetSection("SenderId").Value,
                        password = conf.GetSection("SenderSecret").Value
                    }
                },
                messageData = new MessageData()
                {
                    data = new GbdflMessageData()
                    {
                        MessageId = Guid.NewGuid().ToString(),
                        MessageDate = DateTime.Now,
                        SenderCode = "",
                        Iin = iin,
                        Surname = "",
                        Name = "",
                        Patronymic = "",
                        //birthDate = DateTime.Now,
                        //DocumentNumber = ""
                    }
                }
            }
        };

        Console.WriteLine(JsonConvert.SerializeObject(message));

        var result = await shepClient.sendMessageAsync(message);

        string path = $@"c:\temp\gbdflSuccessResult-{DateTime.Now.Ticks}.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(@"c:\temp");
        }

        using FileStream fs = File.Create(path);
        byte[] info = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
        // Add some information to the file.
        fs.Write(info, 0, info.Length);

        Console.WriteLine(JsonConvert.SerializeObject(result));

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("NAZHMITE NA ENTER CHTOBY ZAKRYT'");
        Console.ReadLine();
    }
    catch(Exception ex)
    {
        string path = $@"c:\temp\gbdflErrorResult-{DateTime.Now.Ticks}.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(@"c:\temp");
        }

        using FileStream fs = File.Create(path);
        byte[] info = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ex));
        // Add some information to the file.
        fs.Write(info, 0, info.Length);

        Console.WriteLine("ERROR:");
        Console.WriteLine(JsonConvert.SerializeObject(ex));

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("NAZHMITE NA ENTER CHTOBY ZAKRYT'");
        Console.ReadLine();
    }
}

static void SetShepCredentials(AsyncChannelClient client, IConfiguration conf)
{
    if (client.Endpoint.Binding is BasicHttpsBinding httpsBinding)
    {
        httpsBinding.Security.Mode = BasicHttpsSecurityMode.Transport;
        httpsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
    }
    else if (client.Endpoint.Binding is BasicHttpBinding httpBinding)
    {
        httpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
        httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
    }

    client.ClientCredentials.UserName.UserName = conf.GetSection("ShepUserName").Value;
    client.ClientCredentials.UserName.Password = conf.GetSection("ShepUserSecret").Value;
}