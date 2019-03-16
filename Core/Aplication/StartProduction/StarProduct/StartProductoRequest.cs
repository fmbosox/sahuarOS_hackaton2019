using System;

namespace Core.Aplication.StartProduction.StarProduct
{
    public class StartProductoRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime Now{ get; set; }
    }
}