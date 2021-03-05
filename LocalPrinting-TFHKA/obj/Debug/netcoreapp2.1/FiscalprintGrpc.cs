// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/fiscalprint.proto
// </auto-generated>
// Original file comments:
// ***********************************************************************************
// Copyright (C) 2012-2018 E.R.P. Consultores y Asociados, C.A.                     *
// Contributor(s): Yamel Senih ysenih@erpya.com                                     *
// This program is free software: you can redistribute it and/or modify             *
// it under the terms of the GNU General Public License as published by             *
// the Free Software Foundation, either version 2 of the License, or                *
// (at your option) any later version.                                              *
// This program is distributed in the hope that it will be useful,                  *
// but WITHOUT ANY WARRANTY; without even the implied warranty of                   *
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.	See the                     *
// GNU General Public License for more details.                                     *
// You should have received a copy of the GNU General Public License                *
// along with this program.	If not, see <https://www.gnu.org/licenses/>.            *
// **********************************************************************************
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcFiscalPrint {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class FiscalPrintService
  {
    static readonly string __ServiceName = "FiscalPrint.FiscalPrintService";

    static readonly grpc::Marshaller<global::GrpcFiscalPrint.Subscription> __Marshaller_FiscalPrint_Subscription = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.Subscription.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.Document> __Marshaller_FiscalPrint_Document = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.Document.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.PrinterResponse> __Marshaller_FiscalPrint_PrinterResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.PrinterResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.ResponseStatus> __Marshaller_FiscalPrint_ResponseStatus = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.ResponseStatus.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.ServerStatusRequest> __Marshaller_FiscalPrint_ServerStatusRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.ServerStatusRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.ServerStatus> __Marshaller_FiscalPrint_ServerStatus = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.ServerStatus.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.PrinterStatusRequest> __Marshaller_FiscalPrint_PrinterStatusRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.PrinterStatusRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcFiscalPrint.PrinterStatus> __Marshaller_FiscalPrint_PrinterStatus = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcFiscalPrint.PrinterStatus.Parser.ParseFrom);

    static readonly grpc::Method<global::GrpcFiscalPrint.Subscription, global::GrpcFiscalPrint.Document> __Method_subscribePrinter = new grpc::Method<global::GrpcFiscalPrint.Subscription, global::GrpcFiscalPrint.Document>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "subscribePrinter",
        __Marshaller_FiscalPrint_Subscription,
        __Marshaller_FiscalPrint_Document);

    static readonly grpc::Method<global::GrpcFiscalPrint.Subscription, global::GrpcFiscalPrint.PrinterResponse> __Method_subscribeSender = new grpc::Method<global::GrpcFiscalPrint.Subscription, global::GrpcFiscalPrint.PrinterResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "subscribeSender",
        __Marshaller_FiscalPrint_Subscription,
        __Marshaller_FiscalPrint_PrinterResponse);

    static readonly grpc::Method<global::GrpcFiscalPrint.Subscription, global::GrpcFiscalPrint.PrinterResponse> __Method_subscribeListener = new grpc::Method<global::GrpcFiscalPrint.Subscription, global::GrpcFiscalPrint.PrinterResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "subscribeListener",
        __Marshaller_FiscalPrint_Subscription,
        __Marshaller_FiscalPrint_PrinterResponse);

    static readonly grpc::Method<global::GrpcFiscalPrint.Document, global::GrpcFiscalPrint.ResponseStatus> __Method_printDocument = new grpc::Method<global::GrpcFiscalPrint.Document, global::GrpcFiscalPrint.ResponseStatus>(
        grpc::MethodType.Unary,
        __ServiceName,
        "printDocument",
        __Marshaller_FiscalPrint_Document,
        __Marshaller_FiscalPrint_ResponseStatus);

    static readonly grpc::Method<global::GrpcFiscalPrint.PrinterResponse, global::GrpcFiscalPrint.ResponseStatus> __Method_sendPrintResponse = new grpc::Method<global::GrpcFiscalPrint.PrinterResponse, global::GrpcFiscalPrint.ResponseStatus>(
        grpc::MethodType.Unary,
        __ServiceName,
        "sendPrintResponse",
        __Marshaller_FiscalPrint_PrinterResponse,
        __Marshaller_FiscalPrint_ResponseStatus);

    static readonly grpc::Method<global::GrpcFiscalPrint.ServerStatusRequest, global::GrpcFiscalPrint.ServerStatus> __Method_getServerStatus = new grpc::Method<global::GrpcFiscalPrint.ServerStatusRequest, global::GrpcFiscalPrint.ServerStatus>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getServerStatus",
        __Marshaller_FiscalPrint_ServerStatusRequest,
        __Marshaller_FiscalPrint_ServerStatus);

    static readonly grpc::Method<global::GrpcFiscalPrint.PrinterStatusRequest, global::GrpcFiscalPrint.PrinterStatus> __Method_getPrinterStatus = new grpc::Method<global::GrpcFiscalPrint.PrinterStatusRequest, global::GrpcFiscalPrint.PrinterStatus>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getPrinterStatus",
        __Marshaller_FiscalPrint_PrinterStatusRequest,
        __Marshaller_FiscalPrint_PrinterStatus);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcFiscalPrint.FiscalprintReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for FiscalPrintService</summary>
    public partial class FiscalPrintServiceClient : grpc::ClientBase<FiscalPrintServiceClient>
    {
      /// <summary>Creates a new client for FiscalPrintService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public FiscalPrintServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for FiscalPrintService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public FiscalPrintServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected FiscalPrintServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected FiscalPrintServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Subscribe Printer
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::GrpcFiscalPrint.Document> subscribePrinter(global::GrpcFiscalPrint.Subscription request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return subscribePrinter(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Subscribe Printer
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::GrpcFiscalPrint.Document> subscribePrinter(global::GrpcFiscalPrint.Subscription request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_subscribePrinter, null, options, request);
      }
      /// <summary>
      /// Subscribe Sender
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::GrpcFiscalPrint.PrinterResponse> subscribeSender(global::GrpcFiscalPrint.Subscription request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return subscribeSender(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Subscribe Sender
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::GrpcFiscalPrint.PrinterResponse> subscribeSender(global::GrpcFiscalPrint.Subscription request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_subscribeSender, null, options, request);
      }
      /// <summary>
      /// Subscribe Listener
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::GrpcFiscalPrint.PrinterResponse> subscribeListener(global::GrpcFiscalPrint.Subscription request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return subscribeListener(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Subscribe Listener
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::GrpcFiscalPrint.PrinterResponse> subscribeListener(global::GrpcFiscalPrint.Subscription request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_subscribeListener, null, options, request);
      }
      /// <summary>
      /// Print a Document
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.ResponseStatus printDocument(global::GrpcFiscalPrint.Document request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return printDocument(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Print a Document
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.ResponseStatus printDocument(global::GrpcFiscalPrint.Document request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_printDocument, null, options, request);
      }
      /// <summary>
      /// Print a Document
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.ResponseStatus> printDocumentAsync(global::GrpcFiscalPrint.Document request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return printDocumentAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Print a Document
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.ResponseStatus> printDocumentAsync(global::GrpcFiscalPrint.Document request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_printDocument, null, options, request);
      }
      /// <summary>
      /// Response from Printer
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.ResponseStatus sendPrintResponse(global::GrpcFiscalPrint.PrinterResponse request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return sendPrintResponse(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Response from Printer
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.ResponseStatus sendPrintResponse(global::GrpcFiscalPrint.PrinterResponse request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_sendPrintResponse, null, options, request);
      }
      /// <summary>
      /// Response from Printer
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.ResponseStatus> sendPrintResponseAsync(global::GrpcFiscalPrint.PrinterResponse request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return sendPrintResponseAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Response from Printer
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.ResponseStatus> sendPrintResponseAsync(global::GrpcFiscalPrint.PrinterResponse request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_sendPrintResponse, null, options, request);
      }
      /// <summary>
      ///	Handle Monitor
      /// Response from Printer Server
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.ServerStatus getServerStatus(global::GrpcFiscalPrint.ServerStatusRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getServerStatus(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///	Handle Monitor
      /// Response from Printer Server
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.ServerStatus getServerStatus(global::GrpcFiscalPrint.ServerStatusRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getServerStatus, null, options, request);
      }
      /// <summary>
      ///	Handle Monitor
      /// Response from Printer Server
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.ServerStatus> getServerStatusAsync(global::GrpcFiscalPrint.ServerStatusRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getServerStatusAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///	Handle Monitor
      /// Response from Printer Server
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.ServerStatus> getServerStatusAsync(global::GrpcFiscalPrint.ServerStatusRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getServerStatus, null, options, request);
      }
      /// <summary>
      ///	Response for printers detail
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.PrinterStatus getPrinterStatus(global::GrpcFiscalPrint.PrinterStatusRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getPrinterStatus(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///	Response for printers detail
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcFiscalPrint.PrinterStatus getPrinterStatus(global::GrpcFiscalPrint.PrinterStatusRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getPrinterStatus, null, options, request);
      }
      /// <summary>
      ///	Response for printers detail
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.PrinterStatus> getPrinterStatusAsync(global::GrpcFiscalPrint.PrinterStatusRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getPrinterStatusAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///	Response for printers detail
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcFiscalPrint.PrinterStatus> getPrinterStatusAsync(global::GrpcFiscalPrint.PrinterStatusRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getPrinterStatus, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override FiscalPrintServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new FiscalPrintServiceClient(configuration);
      }
    }

  }
}
#endregion