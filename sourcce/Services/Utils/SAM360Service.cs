using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SAM360Service : ISAM360Service
    {
        private readonly ISAM360Provider sam360Provider;

        public SAM360Service(ISAM360Provider pSam360Provider) {
            sam360Provider = pSam360Provider;
        }

        public string GetSAM360On()
        {
            return sam360Provider.GetSAM360On();
        }
    }
}
