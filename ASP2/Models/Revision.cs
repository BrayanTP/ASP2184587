using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP2.Models
{
    public class Revision
    {
        public String nombreCliente { get; set; }
        public String documentoCliente { get; set; }
        public String emailCliente { get; set; }
        public DateTime? fechaCompra { get; set; }
        public int? totalCompra { get; set; }
    }
}