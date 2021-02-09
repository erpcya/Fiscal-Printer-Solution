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
            // Create Grpc Channel
            Channel channel = new Channel("20.12.0.2:50043", ChannelCredentials.Insecure);
            // Create Service
            var client = new FiscalPrintService.FiscalPrintServiceClient(channel);

            // Create Printer
            Subscription printer = new Subscription { PrinterName = "PRUEBA01", Description = "Impresora de Pruebas", Type = 0, ListenerName = "TestListener" };

            // Create Subscribe Printer
            try
            {
                Console.WriteLine("Try Subscribe printer");
                client.subscribePrinter(printer);
            }
            catch (RpcException e)
            {
                Console.WriteLine("RPC failed", e);
                throw;
            }

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}
