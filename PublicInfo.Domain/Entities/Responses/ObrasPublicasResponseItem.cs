﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class ObrasPublicasResponseItem
    {
        public ObrasPublicasHeaderResponse Header { get; set; }

        public ObrasPublicasDetailResponse Detail { get; set; }
    }
}
