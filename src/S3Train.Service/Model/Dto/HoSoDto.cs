using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Model.Dto
{
    public class HoSoDto : EntityBase
    {
        public string PhongLuuTru { get; set; }

        public HopDto Hop { get; set; }
    }
}
