using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class HinhVanBan : EntityBase
    {
        public string TenHinh { get; set; }

        [ForeignKey("TaiLieuVanBan")]
        public string TaiLieuVanBanId { get; set; }

        public virtual TaiLieuVanBan TaiLieuVanBan { get; set; }
    }
}
