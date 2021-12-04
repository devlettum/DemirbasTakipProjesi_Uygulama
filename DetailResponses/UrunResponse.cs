using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemirbasTakipProjesi_Uygulama.DetailResponses
{
    public class UrunResponse
    {
        public int Id { get; set; }
        public string UrunIsim { get; set; }
        public int SeriNo { get; set; }
        public int BirimFiyati { get; set; }
        public InsanResponse Insan { get; set; }



    }
}
