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
                        Log(request.Result);
                        client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.OpenDrawer))
                    {
                        request.Result = "OK Open Drawer";
                        Log(request.Result);
                        client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.CutPaper))
                    {
                        request.Result = "OK CutPaper";
                        Log(request.Result);
                        client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.MemoryStatus))
                    {
                        request.Result = "OK MemoryStatus";
                        Log(request.Result);
                        client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.FirmwareInformation))
                    {
                        request.Result = "OK FirmwareInformation";
                        Log(request.Result);
                        client.sendPrintResponseAsync(request);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.ResetPrinter))
                    {
                        request.Result = "OK ResetPrinter";
                        Log(request.Result);
                        client.sendPrintResponseAsync(request);
                    }
                }
                else
                {

                    if (document.DocumentType.Equals(Document.Types.DocumentType.Invoice))
                    {
                        request.Result = "OK Invoice";
                        Log(request.Result);
                        await client.sendPrintResponseAsync(request);
                    }
                    else if (document.DocumentType.Equals(Document.Types.DocumentType.CreditMemo))
                    {
                        request.Result = "OK CreditMemo";
                        Log(request.Result);
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
            var channel = GrpcChannel.ForAddress("http://20.12.0.3:50043", new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure,
            });
            // Set Printer parameters
            Subscription printer = new Subscription { PrinterName = "TESTPRINTER01", Description = "Impresora de Pruebas", Type = 0, ListenerName = "TestListener" };

            Document receivedDocument = new Document { };
            var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(50));
            var client = new PrinterServiceClient(new FiscalPrintService.FiscalPrintServiceClient(channel));
            //await client.ReadResponses(printer, channel);
            //await client.SendResponseServer();

                    await client.SubscribeToStream(printer, channel);                  

        }
    }
}
