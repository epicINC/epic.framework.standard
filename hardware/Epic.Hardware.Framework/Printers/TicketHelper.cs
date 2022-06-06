using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Hardware.Printers
{
    public class TicketHelper
    {
        static void Demo(PrintQueue queue, PrintTicket ticket)
        {
            var printCapabilites = queue.GetPrintCapabilities();

            // Modify PrintTicket
            if (printCapabilites.CollationCapability.Contains(Collation.Collated))
                ticket.Collation = Collation.Collated;

            if (printCapabilites.DuplexingCapability.Contains(Duplexing.TwoSidedLongEdge))
                ticket.Duplexing = Duplexing.TwoSidedLongEdge;

            if (printCapabilites.StaplingCapability.Contains(Stapling.StapleDualLeft))
                ticket.Stapling = Stapling.StapleDualLeft;

        }
    }
}
