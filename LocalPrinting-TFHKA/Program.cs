using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcFiscalPrint;

namespace FiscalPrintTest
{
    class Program
    {

        public class PrinterServiceClient
        {
            readonly GrpcFiscalPrint.FiscalPrintService.FiscalPrintServiceClient client;
            public PrinterServiceClient(GrpcFiscalPrint.FiscalPrintService.FiscalPrintServiceClient client)
            {
                this.client = client;
            }

            public async Task ReadResponses(Subscription printer, GrpcChannel channel)
            {
                var client = new FiscalPrintService.FiscalPrintServiceClient(channel);
                Console.WriteLine("Subscribe Printer");
                using var call = client.subscribePrinter(new Subscription(printer));
                Console.WriteLine(call.ResponseHeadersAsync.Status + " Status");
                StringBuilder responses = new StringBuilder("Result: ");
                try
                {
                    var readTask = Task.Run(async () =>
                    {
                        var StreamingResponse = call.ResponseStream;
                        while (await StreamingResponse.MoveNext())
                        {
                            Console.WriteLine("Received Message");
                            Document receivedDocument = StreamingResponse.Current;
                            if (receivedDocument.SetupType.Equals(Document.Types.SetupType.GetStatus))
                            {
                                Console.WriteLine("Send Response");
                                await client.sendPrintResponseAsync(new PrinterResponse { Result = "OK", IsError = false, PrinterName = "TESTPRINTER01" });
                            }
                            responses.Append(receivedDocument.ToString());
                        }
                    });
                    await readTask;
                    Log(responses.ToString());
                }
                catch (RpcException e)
                {
                    Console.WriteLine("RPC failed", e);
                    throw;
                }
            }

            public async Task SubscribeToStream(Subscription printer, GrpcChannel channel)
            {
                var clientService = new FiscalPrintService.FiscalPrintServiceClient(channel);
                var streamReader = clientService.subscribePrinter(new Subscription(printer)).ResponseStream;
                Document document = new GrpcFiscalPrint.Document { };
                PrinterResponse request = new PrinterResponse { Result = "OK", IsError = false, PrinterName = "TESTPRINTER01" };
                try
                {
                    while (await streamReader.MoveNext())
                    {
                        document = streamReader.Current;
                        Console.WriteLine($"Received: {document}");
                        await SendResponseServer(request, document);
                    }
                }
                catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
                {
                    Console.WriteLine("Stream cancelled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading response: " + ex);
                }
            }


            public async Task SendResponseServer(PrinterResponse request, Document document)
            {
                if (document.DocumentType.Equals(Document.Types.DocumentType.Setup))
                {
                    if (document.SetupType.Equals(Document.Types.SetupType.GetStatus))
                    {
                        request.Result = "OK Status";
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.OpenDrawer))
                    {
                        request.Result = "OK Open Drawer";
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.CutPaper))
                    {
                        request.Result = "OK CutPaper";
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.MemoryStatus))
                    {
                        request.Result = "OK MemoryStatus";
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.FirmwareInformation))
                    {
                        request.Result = "OK FirmwareInformation";
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.ResetPrinter))
                    {
                        request.Result = "OK ResetPrinter";
                        await client.sendPrintResponseAsync(request);
                    }
                }
                else
                {

                    if (document.DocumentType.Equals(Document.Types.DocumentType.Invoice))
                    {
                        request.Result = "OK Invoice";
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.DocumentType.Equals(Document.Types.DocumentType.CreditMemo))
                    {
                        request.Result = "OK CreditMemo";
                        await client.sendPrintResponseAsync(request);
                    }
                }
                // switch(document.SetupType){
                //     case Document.Types.SetupType.GetStatus:

                // }
            }

            private void Log(string s, params object[] args)
            {
                Console.WriteLine(string.Format(s, args));
            }

            private void Log(string s)
            {
                Console.WriteLine(s);
            }
        }
        static async Task Main(string[] args)
        {
            // Enable support for unencrypted HTTP2  
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);


            // Set Grpc Channel
            var channel = GrpcChannel.ForAddress("http://20.12.0.2:50043", new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            });
            // Set Printer parameters
            Subscription printer = new Subscription { PrinterName = "TESTPRINTER01", Description = "Impresora de Pruebas", Type = 0, ListenerName = "TestListener" };

            Document receivedDocument = new Document { };
            var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(5));
            var client = new PrinterServiceClient(new FiscalPrintService.FiscalPrintServiceClient(channel));
            //await client.ReadResponses(printer, channel);
            //await client.SendResponseServer();


            await client.SubscribeToStream(printer, channel);

            //Console.WriteLine(response.SetupType.ToString());
            // Console.WriteLine("Press any key to exit...");
            // Console.ReadKey();
            // channel.ShutdownAsync().Wait();

            // // Funciona
            // try
            // {
            //     var client1 = new FiscalPrintService.FiscalPrintServiceClient(channel);
            // //     // Subscribe printer
            //     using var call = client1.subscribePrinter(new Subscription(printer));
            //     // Send Document to Server
            //     // ResponseStatus printDocument = client1.printDocument(new Document { SetupType = Document.Types.SetupType.GetStatus });
            //     // Get Server Status
            //     ServerStatus statusServer = await client1.getServerStatusAsync(new ServerStatusRequest { });

            //     Console.WriteLine(statusServer.ActivePrinters + "<-- Active Printers");

            //     // Obtain Client Service status
            //     Console.WriteLine(call.ResponseHeadersAsync.Status + " Status");

            //     // Read Server Messages
            //     Console.WriteLine("Starting background task to receive messages");

            //      var readTask = Task.Run(async () =>
            //      {
            //          await foreach (var response in call.ResponseStream.ReadAllAsync())
            //          {                        
            //              //Console.WriteLine(response.SetupType);
            // //             //Console.WriteLine(response.SetupType.ToString());
            //               if (response.SetupType.Equals(Document.Types.SetupType.GetStatus)){
            //                   Console.WriteLine("Send Response");
            //               await client1.sendPrintResponseAsync(new PrinterResponse { Result = "OK" , IsError = false, PrinterName = "TESTPRINTER01" });
            //               }
            //          }
            //      });
            //      await readTask;

            // }
            // catch (RpcException e)
            // {
            //     Console.WriteLine("RPC failed", e);
            //     channel.ShutdownAsync().Wait();
            // }
            // //  // Funciona
        }
    }
}
