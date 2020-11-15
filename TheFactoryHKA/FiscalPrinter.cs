/*************************************************************************************
 * Product: FiscalPrinter ERP & CRM Smart Business Solution                          *
 * This program is free software; you can redistribute it and/or modify it           *
 * under the terms version 2 or later of the GNU General Public License as published *
 * by the Free Software Foundation. This program is distributed in the hope          *
 * that it will be useful, but WITHOUT ANY WARRANTY; without even the implied        *
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                  *
 * See the GNU General Public License for more details.                              *
 * You should have received a copy of the GNU General Public License along           *
 * with this program; if not, write to the Free Software Foundation, Inc.,           *
 * 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA.                            *
 * For the text or an alternative of this public license, you may reach us           *
 * Copyright (C) 2012-2018 E.R.P. Consultores y Asociados, S.A. All Rights Reserved. *
 * Contributor(s): Yamel Senih www.erpya.com                                         *
 *************************************************************************************/
using System;
using TfhkaNet.IF;
using TfhkaNet.IF.VE;
namespace FiscalPrinter {
    class Printer {
        public static void Main(string[] args) {
            if(args == null
                || args.Length < 2) {
                throw new Exception("Arguments Must Be: [Printer(Port), File Path(Path)]");
            }
            Tfhka printer = new Tfhka();
            Console.WriteLine("Opening port: {0}", args[0]);
            printer.OpenFpCtrl(args[0]);
            Console.WriteLine("Port {0} status: {1}", args[0], printer.CheckFPrinter());
            Console.WriteLine("Sending file... {0}", args[1]);
            Console.WriteLine("Printer response: {0}", printer.SendFileCmd(args[1]));
            Console.WriteLine("File sent: {0}", args[1]);
            printer.CloseFpCtrl();
            Console.WriteLine("Port closed");
        }
    }
}
