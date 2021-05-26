using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcFiscalPrint;
using System.IO.Ports;
using TfhkaNet.IF;
using TfhkaNet.IF.VE;

namespace FiscalPrintTest
{
    class Program
    {

        public class TfhkaPrinter
        {
            public double _add;
            private Tfhka Printer;
            private bool Response;
            private ReportData Report;
            private ReportData[] Reports;
            private TfhkaNet.IF.PrinterStatus StatusError;
            private S1PrinterData StatusS1;
            private S2PrinterData StatusS2;
            private S3PrinterData StatusS3;
            private S4PrinterData StatusS4;
            private S5PrinterData StatusS5;
            private S6PrinterData StatusS6;
            private SVPrinterData CountryModel;
            public bool PrinterConnected = false;

            public TfhkaPrinter()
            {
                this.Printer = new Tfhka();
            }

            #region Communications Functions
            public void FiscalPrinterOpenPort(String port)
            {
                try
                {
                    //localizador = "abrir";
                    Response = Printer.OpenFpCtrl(port);
                    if (Response)
                    {
                        Response = Printer.CheckFPrinter();
                        if (Response)
                        {

                            PrinterConnected = Response;
                            Console.WriteLine("PUERTO ABIERTO The Factory HKA - Venezuela");
                        }
                        else
                        {
                            Console.WriteLine("Error The Factory HKA - Venezuela");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error The Factory HKA - Venezuela");
                    }
                }//try
                catch (InvalidOperationException e)
                {
                    //excp = true;
                    Console.WriteLine("Conecte Printer The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                } //catch
                catch (ArgumentNullException e)
                {
                    //excp = true;
                    Console.WriteLine("Conecte Printer The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                } //catch
                catch (Exception e)
                {
                    //excp = true;
                    Console.WriteLine("Conecte Printer The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                } //catch
            }

            public void FiscalPrinterClosePort()
            {
                //localizador = "cerrar";
                Printer.CloseFpCtrl();
                if (PrinterConnected == false)
                    PrinterConnected = false;
                Console.WriteLine("PUERTO CERRADO The Factory HKA - Venezuela");                     //<GB>            
            }

            public void FiscalPrinterCheckConection()
            {
                //localizador = "conexion";
                Response = Printer.CheckFPrinter();
                if (Response)
                {
                    Console.WriteLine("Printer CONECTADA The Factory HKA - Venezuela");
                }
                else
                {
                    Console.WriteLine("Printer NO CONECTADA The Factory HKA - Venezuela");
                }
            }

            public Boolean FiscalPrinterCheckDrawer()
            {
                //localizador = "DRAWER";
                Response = Printer.CheckDrawer();
                if (Response)
                {
                    Console.WriteLine("GAVETA CONECTADA", "The Factory HKA - Venezuela");
                }
                else
                {
                    Console.WriteLine("GAVETA NO CONECTADA", "The Factory HKA - Venezuela");
                }
                return Response;
            }

            public TfhkaNet.IF.PrinterStatus FiscalPrinterGetStatus()
            {
                try
                {
                    //localizador = "status";
                    StatusError = Printer.GetPrinterStatus();
                    //TxtInformacion.Text = "";
                    Console.WriteLine(
                    "Código de error: " + StatusError.PrinterErrorCode + "\r\n" +
                    "Mensaje de error: " + StatusError.PrinterErrorCode + "\r\n" +
                    "Código de Status: " + StatusError.PrinterStatusCode + "\r\n" +
                    "Descripción del estatus: " + StatusError.PrinterStatusCode);

                    /*                 if (btnAbrir.Enabled == false)
                                    {
                                    label3.BackColor = Color.Lime;
                                    label3.ForeColor = Color.Black;
                                    label3.Text = Resources.texto.status;
                                    }
                                    else
                                    {
                                        label3.BackColor = Color.Red;
                                        label3.ForeColor = Color.White;
                                        label3.Text = Resources.texto.status;
                                    } */
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    /*                 label3.BackColor = Color.Red;
                                    label3.ForeColor = Color.White;
                                    label3.Text = Resources.texto.status;
                 */
                }
                return StatusError;
            }
            #endregion

            #region Send Commands

            public bool FiscalPrinterSendCmd(String Command)
            {
                //localizador = "send";
                if (PrinterConnected == true)
                {
                    Response = Printer.SendCmd(Command);
                }
                else
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                }
                return Response;
            }

            public void FiscalPrinterSendFile(String filePath)
            {
                //localizador = "send_file";
                int n;
                n = Printer.SendFileCmd(filePath);
            }

            #endregion

            #region Reports Commands
            public void FiscalPrinterGetLastReportZ()
            {
                try                                             //Bloque try-catch para manejar excepciones
                {
                    //localizador = "Z";
                    Report = Printer.GetZReport();           //se guarda en una variable de tipo ReportData <GB>
                                                             //TxtInformacion.Text = "";                   //se muestra la información ordenada y legible <GB>
                    Console.WriteLine("Ultimo Reporte Z:" + Report.NumberOfLastZReport + "\r\n" +
                    "Fecha de Ultimo Reporte Z:" + Report.ZReportDate + "\r\n" +
                    "Ventas Libres de Impuesto:" + Report.FreeSalesTax + "\r\n" +
                    "BI: " + Report.GeneralRate1Sale + "," + " IVA: " + Report.GeneralRate1Tax + "\r\n" +
                    "BI: " + Report.ReducedRate2Sale + "," + " IVA: " + Report.ReducedRate2Tax + "\r\n" +
                    "BI: " + Report.AdditionalRate3Sale + "," + " IVA: " + Report.AdditionalRate3Tax + "\r\n" +
                    "Ventas Totales (BI): " + add(Report.FreeSalesTax, Report.GeneralRate1Sale, Report.ReducedRate2Sale, Report.AdditionalRate3Sale) + "\r\n" +
                    "Acumulados en Nota de Crédito: " + add(Report.AdditionalRateDevolution, Report.FreeTaxDevolution, Report.GeneralRateDevolution, Report.ReducedRateDevolution) + "\r\n" +
                    "Acumulados en Nota de Débito: " + add(Report.AdditionalRateDebit, Report.FreeTaxDebit, Report.GeneralRateDebit, Report.ReducedRateDebit) + "\r\n" +
                    "Número de la última factura: " + Report.NumberOfLastInvoice + "\r\n" +
                    "Fecha y la hora de la última factura: " + Report.LastInvoiceDate + "\r\n" +
                    "Número de la última Nota de Credito: " + Report.NumberOfLastCreditNote + "\r\n" +
                    "Número de la última Nota de Débito: " + Report.NumberOfLastDebitNote + "\r\n" +
                    "Número del último Documento No Fiscal: " + Report.NumberOfLastNonFiscal);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }

            public void FiscalPrinterGetStatus(String Statustype)    //función que solicita los diferentes status
            {
                //localizador = "S";
                try
                {
                    if (Statustype.Equals("S1"))
                    {
                        //localizador = "S1";
                        StatusS1 = Printer.GetS1PrinterData();
                        Console.WriteLine("Número de contador de reporte de auditoría: " + StatusS1.AuditReportsCounter + "\r\n" +
                        "Número del cajero activo: " + StatusS1.CashierNumber + "\r\n" +
                        "Hora y Fecha actual de la Printer: " + StatusS1.CurrentPrinterDateTime + "\r\n" +
                        "Número de contador de cierre del día: " + StatusS1.DailyClosureCounter + "\r\n" +
                        "Número de la última Nota de Crédito: " + StatusS1.LastCreditNoteNumber + "\r\n" +
                        "Número de la última Nota de Débito: " + StatusS1.LastDebitNoteNumber + "\r\n" +
                        "Número de la última factura: " + StatusS1.LastInvoiceNumber + "\r\n" +
                        "Número del último documento No Fiscal: " + StatusS1.LastNonFiscalDocNumber + "\r\n" +
                        "Cantidad de documentos no fiscales: " + StatusS1.QuantityNonFiscalDocuments + "\r\n" +
                        "Cantidad de notas de crédito emitidas en el día: " + StatusS1.QuantityOfCreditNotesToday + "\r\n" +
                        "Cantidad de notas de débito emitidas en el día: " + StatusS1.QuantityOfDebitNotesToday + "\r\n" +
                        "Cantidad de facturas emitidas en el día: " + StatusS1.QuantityOfInvoicesToday + "\r\n" +
                        "Número de registro de la Printer fiscal: " + StatusS1.RegisteredMachineNumber + "\r\n" +
                        "Número de RIF: " + StatusS1.RIF + "\r\n" +
                        "Monto total de las ventas diarias: " + StatusS1.TotalDailySales);
                    }

                    if (Statustype.Equals("S2"))
                    {
                        //localizador = "S2";
                        StatusS2 = Printer.GetS2PrinterData();
                        Console.WriteLine(
                        "Monto Por Pagar:  " + StatusS2.AmountPayable + "\r\n" +
                        "Cantidad de Pagos Realizados: " + StatusS2.NumberPaymentsMade + "\r\n" +
                        "Cantidad de Artículos: " + StatusS2.QuantityArticles + "\r\n" +
                        "Subtotal Bases Imponibles: " + StatusS2.SubTotalBases + "\r\n" +
                        "Subtotal de Impuesto (IVA): " + StatusS2.SubTotalTax + "\r\n" +
                        "Tipo de Documento: " + StatusS2.TypeDocument
                        );

                    }

                    if (Statustype.Equals("S3"))
                    {
                        //localizador = "S3";
                        StatusS3 = Printer.GetS3PrinterData();
                        Console.WriteLine(
                        "Valor de la tasa 1 (%):  " + StatusS3.Tax1 + "\r\n" +
                        "Valor de la tasa 2 (%):  " + StatusS3.Tax2 + "\r\n" +
                        "Valor de la tasa 3 (%):  " + StatusS3.Tax3 + "\r\n" +
                        "Tipo de tasa 1 (Modo Incluido = 1, Modo Excluido = 2):  " + StatusS3.TypeTax1 + "\r\n" +
                        "Tipo de tasa 2 (Modo Incluido = 1, Modo Excluido = 2):  " + StatusS3.TypeTax2 + "\r\n" +
                        "Tipo de tasa 3 (Modo Incluido = 1, Modo Excluido = 2):  " + StatusS3.TypeTax3 + "\r\n\r\n" +
                        "Lista de Banderas: " + "\r\n");

                        for (int i = 0; i < StatusS3.AllSystemFlags.Length; i++)
                        {
                            String flags = null;
                            flags += "[" + i + "]= " + StatusS3.AllSystemFlags[i] + "\r\n";
                            Console.WriteLine(flags);
                        }
                    }

                    if (Statustype.Equals("S4"))
                    {
                        //localizador = "S4";
                        String text = null;
                        StatusS4 = Printer.GetS4PrinterData();
                        text += "Lista de los montos acumulados en todos los medios de pagos: \r\n" + "\r\n";

                        for (int i = 0; i < StatusS4.AccumulatedMountsAllMeansOfPayment.Length; i++)
                        {
                            text += "Medio de Pago" + "[" + (i + 1) + "]: " + StatusS4.AccumulatedMountsAllMeansOfPayment[i] + " BsF." + "\r\n";
                        }
                        Console.WriteLine(text);
                    }

                    if (Statustype.Equals("S5"))
                    {
                        //localizador = "S5";
                        StatusS5 = Printer.GetS5PrinterData();
                        Console.WriteLine(
                        "Espacio libre en la memoria de auditoria (MB): " + StatusS5.AuditMemoryFreeCapacity + "\r\n" +
                        "Numero de la memoria de auditoría: " + StatusS5.AuditMemoryNumber + "\r\n" +
                        "Capacidad total de la memoria de auditoria (MB): " + StatusS5.AuditMemoryTotalCapacity + "\r\n" +
                        "Cantidad de documentos almecenados: " + StatusS5.NumberRegisteredDocuments + "\r\n" +
                        "Número de registro de la impresora fiscal: " + StatusS5.RegisteredMachineNumber + "\r\n" +
                        "Número de RIF: " + StatusS5.RIF);
                    }

                    if (Statustype.Equals("S6"))
                    {
                        //localizador = "S6";
                        StatusS6 = Printer.GetS6PrinterData();
                        Console.WriteLine(
                        "Modo Facturación: " + StatusS6.Bit_Facturacion + "\r\n" +
                        "Modo Slip: " + StatusS6.Bit_Slip + "\r\n" +
                        "Modo Validación: " + StatusS6.Bit_Validacion);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }

            #endregion

            #region Extract Reports
            public void FiscalPrinterReadXReport()
            {
                //localizador = "X";
                try
                {
                    Report = Printer.GetXReport();

                    Console.WriteLine(
                    "Número de Reporte: " + Report.NumberOfLastZReport + "\r\n" +
                    "Fecha de Reporte: " + Report.ZReportDate + "\r\n" +
                    "Monto total Exento: " + Report.FreeSalesTax + "\r\n" +
                    "Monto total Tasa 1: " + "BI: " + Report.GeneralRate1Sale + "," + " IVA: " + Report.GeneralRate1Tax + "\r\n" +
                    "Monto total Tasa 2: " + "BI: " + Report.ReducedRate2Sale + "," + " IVA: " + Report.ReducedRate2Tax + "\r\n" +
                    "Monto total Tasa 3: " + "BI: " + Report.AdditionalRate3Sale + "," + " IVA: " + Report.AdditionalRate3Tax + "\r\n" +
                    "Ventas Totales (BI): " + add(Report.FreeSalesTax, Report.GeneralRate1Sale, Report.ReducedRate2Sale, Report.AdditionalRate3Sale) + "\r\n" +
                    "Acumulados en Nota de Crédito: " + add(Report.AdditionalRateDevolution, Report.FreeTaxDevolution, Report.GeneralRateDevolution, Report.ReducedRateDevolution) + "\r\n" +
                    "Acumulados en Nota de Débito: " + add(Report.AdditionalRateDebit, Report.FreeTaxDebit, Report.GeneralRateDebit, Report.ReducedRateDebit) + "\r\n" +
                    "Número de la última factura: " + Report.NumberOfLastInvoice + "\r\n" +
                    "Fecha y la hora de la última factura: " + Report.LastInvoiceDate + "\r\n" +
                    "Número de la última Nota de Credito: " + Report.NumberOfLastCreditNote + "\r\n" +
                    "Número de la última Nota de Débito: " + Report.NumberOfLastDebitNote + "\r\n" +
                    "Número del último Documento No Fiscal: " + Report.NumberOfLastNonFiscal);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }

            public void FiscalPrinterReadZReportbyRange(int ZInicio, int ZFin) //FUNCIÓN LLAMADA POR LOS EVENTOS DE REPORTE Z FECHAS Y POR RANGO
            {
                //localizador = "ZR";
                Reports = Printer.GetZReport(ZInicio, ZFin);
                //MessageBox.Show(Reports.Length.ToString() + _idioma.salida_idioma_principal(56));
                try
                {

                    for (int x = 0; x < Reports.Length; x++)
                    {
                        Console.WriteLine(
                        "\t\t-----" + "Reporte N°" + (x + 1) + "-----" + "\r\n\r\n" +
                        "Número de Reporte: " + Reports[x].NumberOfLastZReport + "\r\n" +
                        "Fecha de Reporte: " + Reports[x].ZReportDate + "\r\n" +
                        "Monto total Exento: " + Reports[x].FreeSalesTax + "\r\n" +
                        "Monto total Tasa 1: " + "BI: " + Reports[x].GeneralRate1Sale + "," + " IVA: " + Reports[x].GeneralRate1Tax + "\r\n" +
                        "Monto total Tasa 2: " + "BI: " + Reports[x].ReducedRate2Sale + "," + " IVA: " + Reports[x].ReducedRate2Tax + "\r\n" +
                        "Monto total Tasa 3: " + "BI: " + Reports[x].AdditionalRate3Sale + "," + " IVA: " + Reports[x].AdditionalRate3Tax + "\r\n" +
                        "Ventas Totales (BI): " + add(Reports[x].FreeSalesTax, Reports[x].GeneralRate1Sale, Reports[x].ReducedRate2Sale, Reports[x].AdditionalRate3Sale) + "\r\n" +
                        "Acumulados en Nota de Crédito: " + add(Reports[x].AdditionalRateDevolution, Reports[x].FreeTaxDevolution, Reports[x].GeneralRateDevolution, Reports[x].ReducedRateDevolution) + "\r\n" +
                        "Acumulados en Nota de Débito: " + add(Reports[x].AdditionalRateDebit, Reports[x].FreeTaxDebit, Reports[x].GeneralRateDebit, Reports[x].ReducedRateDebit) + "\r\n" +
                        "Número de la última factura: " + Reports[x].NumberOfLastInvoice + "\r\n" +
                        "Fecha y la hora de la última factura: " + Reports[x].LastInvoiceDate + "\r\n" +
                        "Número de la última Nota de Credito: " + Reports[x].NumberOfLastCreditNote + "\r\n" +
                        "Número de la última Nota de Débito: " + Reports[x].NumberOfLastDebitNote + "\r\n" +
                        "Número del último Documento No Fiscal: " + Reports[x].NumberOfLastNonFiscal + "\r\n\r\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }


            public void FiscalPrinterReadZReportbyDate(DateTime DtpFechaI, DateTime DtpFecha2)
            {
                //localizador = "ZF";
                Reports = Printer.GetZReport(DtpFechaI, DtpFecha2);
                //MessageBox.Show(Reports.Length.ToString() + _idioma.salida_idioma_principal(57) + DtpFechaI.Value + _idioma.salida_idioma_principal(58) + DtpFecha2.Value);
                try
                {
                    for (int x = 0; x < Reports.Length; x++)
                    {
                        Console.WriteLine(
                            "\t\t-----" + "Reporte N°" + (x + 1) + "-----" + "\r\n\r\n" +
                            "Número de Reporte: " + Reports[x].NumberOfLastZReport + "\r\n" +
                            "Fecha de Reporte: " + Reports[x].ZReportDate + "\r\n" +
                            "Monto total Exento: " + Reports[x].FreeSalesTax + "\r\n" +
                            "Monto total Tasa 1: " + "BI: " + Reports[x].GeneralRate1Sale + "," + " IVA: " + Reports[x].GeneralRate1Tax + "\r\n" +
                            "Monto total Tasa 2: " + "BI: " + Reports[x].ReducedRate2Sale + "," + " IVA: " + Reports[x].ReducedRate2Tax + "\r\n" +
                            "Monto total Tasa 3: " + "BI: " + Reports[x].AdditionalRate3Sale + "," + " IVA: " + Reports[x].AdditionalRate3Tax + "\r\n" +
                            "Ventas Totales (BI): " + add(Reports[x].FreeSalesTax, Reports[x].GeneralRate1Sale, Reports[x].ReducedRate2Sale, Reports[x].AdditionalRate3Sale) + "\r\n" +
                            "Acumulados en Nota de Crédito: " + add(Reports[x].AdditionalRateDevolution, Reports[x].FreeTaxDevolution, Reports[x].GeneralRateDevolution, Reports[x].ReducedRateDevolution) + "\r\n" +
                            "Acumulados en Nota de Débito: " + add(Reports[x].AdditionalRateDebit, Reports[x].FreeTaxDebit, Reports[x].GeneralRateDebit, Reports[x].ReducedRateDebit) + "\r\n" +
                            "Número de la última factura: " + Reports[x].NumberOfLastInvoice + "\r\n" +
                            "Fecha y la hora de la última factura: " + Reports[x].LastInvoiceDate + "\r\n" +
                            "Número de la última Nota de Credito: " + Reports[x].NumberOfLastCreditNote + "\r\n" +
                            "Número de la última Nota de Débito: " + Reports[x].NumberOfLastDebitNote + "\r\n" +
                            "Número del último Documento No Fiscal: " + Reports[x].NumberOfLastNonFiscal + "\r\n\r\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }
            #endregion

            #region Print Reports

            public void FiscalPrinterPrintZReport()
            {
                //localizador = "Zp";
                try
                {
                    Printer.PrintZReport();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }

            public void FiscalPrinterPrintZReportbyRange(int ZInicio, int ZFin)
            {
                //localizador = "ZR";
                try
                {
                    Printer.PrintZReport(ZInicio, ZFin);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }

            public void FiscalPrinterPrintZReportbyDate(DateTime DtpFechaI, DateTime DtpFecha2)
            {
                //localizador = "ZR";
                try
                {
                    Printer.GetZReport(DtpFechaI, DtpFecha2);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }


            public void FiscalPrinterPrintXReport()
            {
                //localizador = "Xp";
                try
                {
                    Printer.PrintXReport();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Verifique la conexion The Factory HKA - Venezuela");
                    Console.WriteLine(e);
                }
            }

            #endregion
            double add(double a, double b, double c, double d)
            {
                _add = a + b + c + d;
                return _add;
            }
        }




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

            public async Task SubscribeToStream(Subscription printer, GrpcChannel channel, TfhkaPrinter fiscalPrinter)
            {
                var clientService = new FiscalPrintService.FiscalPrintServiceClient(channel);
                var streamReader = clientService.subscribePrinter(new Subscription(printer)).ResponseStream;
                Document document = new GrpcFiscalPrint.Document { };
                PrinterResponse request = new PrinterResponse { Result = "OK", IsError = false, PrinterName = "TESTPRINTER01" };
                try
                {
                    while (true)
                    {
                        document = streamReader.Current;
                        Console.WriteLine($"Received: {document}");
                        await SendResponseServer(request, document, fiscalPrinter);
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

            public async Task SendResponseServer(PrinterResponse request, Document document, TfhkaPrinter fiscalPrinter)
            {
                if (document.DocumentType.Equals(Document.Types.DocumentType.Setup))
                {
                    if (document.SetupType.Equals(Document.Types.SetupType.GetStatus))
                    {
                        TfhkaNet.IF.PrinterStatus status = fiscalPrinter.FiscalPrinterGetStatus();
                        request.Result = "Código de error: " + status.PrinterErrorCode + "\r\n" +
                        "Mensaje de error: " + status.PrinterErrorCode + "\r\n" +
                        "Código de Status: " + status.PrinterStatusCode + "\r\n" +
                        "Descripción del estatus: " + status.PrinterStatusCode;
                        client.sendPrintResponseAsync(request);
                        Log(request.Result);


                    }
                    else if (document.SetupType.Equals(Document.Types.SetupType.OpenDrawer))
                    {
                        Boolean drawer = fiscalPrinter.FiscalPrinterCheckDrawer();

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
                        }

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
            // Enable Fiscal Printer Printer
            var fiscalPrinter = new TfhkaPrinter();
            fiscalPrinter.FiscalPrinterOpenPort("1");

            // Enable support for unencrypted HTTP2  
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            // Set Grpc Channel
            var channel = GrpcChannel.ForAddress("http://20.12.0.2:50043", new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            });
            // Set Printer parameters
            Subscription grpcPrinter = new Subscription { PrinterName = "TESTPRINTER01", Description = "Printer de Pruebas", Type = 0, ListenerName = "TestListener" };

            // Document receivedDocument = new Document { };
            // var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(5));
            var client = new PrinterServiceClient(new FiscalPrintService.FiscalPrintServiceClient(channel));

            await client.SubscribeToStream(grpcPrinter, channel, fiscalPrinter);

            fiscalPrinter.FiscalPrinterClosePort();
        }
    }
}