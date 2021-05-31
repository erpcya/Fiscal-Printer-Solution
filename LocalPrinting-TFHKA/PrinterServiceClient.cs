using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcFiscalPrint; 
 
 namespace FiscalPrintTest
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

            public Task SendResponseServer(PrinterResponse request, Document document)
            {
                if (document.DocumentType.Equals(Document.Types.DocumentType.Setup))
                {
                    if (document.SetupType.Equals(Document.Types.SetupType.GetStatus))
                    {
                        /*                         TfhkaNet.IF.PrinterStatus status = fiscalPrinter.FiscalPrinterGetStatus();
                                                request.Result = "Código de error: " + status.PrinterErrorCode + "\r\n" +
                                                "Mensaje de error: " + status.PrinterErrorCode + "\r\n" +
                                                "Código de Status: " + status.PrinterStatusCode + "\r\n" +
                                                "Descripción del estatus: " + status.PrinterStatusCode; */
                        request.Result = "OK Status";
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);


                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.OpenDrawer))
                    {
                        /*                         Boolean drawer = fiscalPrinter.FiscalPrinterCheckDrawer();

                                                if (drawer)
                                                {

                                                    Boolean isprinted = fiscalPrinter.FiscalPrinterSendCmd("0");
                                                    if (isprinted)
                                                    {
                                                        request.Result = "GAVETA CONECTADA \r\n COMANDO APLICADO" ;
                                                    }
                                                    else
                                                    {
                                                        request.Result = "GAVETA CONECTADA \r\n COMANDO NO APLICADO" ;
                                                    }
                                                }
                                                else
                                                {
                                                    request.Result = "GAVETA NO CONECTADA";
                                                } */

                        request.Result = "OK Open Drawer";
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.CutPaper))
                    {
                        request.Result = "OK CutPaper";
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.MemoryStatus))
                    {
                        request.Result = "OK MemoryStatus";
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.FirmwareInformation))
                    {
                        request.Result = "OK FirmwareInformation";
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);
                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.ResetPrinter))
                    {
                        request.Result = "OK ResetPrinter";
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);
                    }
                }
                else
                {

                    if (document.DocumentType.Equals(Document.Types.DocumentType.Invoice))
                    {
                        request.Result = "OK Invoice";
                        client.sendPrintResponseAsync(request);
                    }
                    else if (document.DocumentType.Equals(Document.Types.DocumentType.CreditMemo))
                    {
                        request.Result = "OK CreditMemo";
                        client.sendPrintResponseAsync(request);
                    }
                }

                return Task.CompletedTask;
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
}