﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class ObrasPublicasResponse
    {
        public List<ObrasPublicasResponseItem> Items { get; set; }
        public int pages { get; set; }
    }
}
