using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Dtos
{
    public class RefreshTockenDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public bool Revoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CompanyUserId { get; set; }
    }
}
