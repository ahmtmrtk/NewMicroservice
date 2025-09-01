using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservice.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        public string Username => "Ahmet16";
    }
}
