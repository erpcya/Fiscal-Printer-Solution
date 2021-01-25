// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace LocalPrinting
{
  class Program
  {

    public static void Main(string[] args)
    {
      Channel channel = new Channel("20.12.0.2:50043", ChannelCredentials.Insecure);

      var client = new FiscalPrintService.FiscalPrintServiceClient(channel);

      //   using (var call = client.subscribePrinter(new Subscription { PrinterName = "CAJA01" }))
      // {
      // foreach (var responseStream in responseStreams)
      //{
      //await NewMethod(call, responseStream);
      //}
      //await call.RequestStream.CompleteAsync();

      //RouteSummary summary = await call.ResponseAsync;
      //}
      try
      {

        var request = client.subscribePrinter(new Subscription { PrinterName = "CAJA01" });

        //Feature feature = client.GetFeature(request);
      }
      catch (RpcException e)
      {
        Console.WriteLine("RPC failed " + e);
        throw;
      }



      //public async Task Documents(Document)
      //{
      //  try
      //  {


      //    Document request = new Document;

      //    using (var call = client.subscribePrinter((Subscription)request))
      //    {
      //      var responseStream = call.ResponseStream;
      //      System.Text.StringBuilder responseLog = new System.Text.StringBuilder("Result: ");

      //      while (await responseStream.MoveNext())
      //      {
      //        var response = responseStream.Current;
      //        responseLog.Append(response.ToString());
      //      }
      //      Console.WriteLine(responseLog.ToString());
      //    }
      //  }
      //  catch (RpcException e)
      //  {
      //    Console.WriteLine("RPC failed " + e);
      //    throw;
      //  }
      //}


      //var reply =  client.subscribePrinter(new );
      //Console.WriteLine("Greeting: " + request.ResponseStream);

      channel.ShutdownAsync().Wait();
      //Console.WriteLine("Press any key to exit...");
      // Console.ReadKey();
    }

  }
}
