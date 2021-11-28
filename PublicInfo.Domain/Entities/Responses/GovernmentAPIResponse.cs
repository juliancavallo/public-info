using System;
using System.Collections.Generic;

namespace PublicInfo.Domain.Entities.Responses
{
    public class GovernmentAPIResponse
    {
        public class Root
        {
            public bool success { get; set; }
            public Result result { get; set; }
        }

        public class Result
        {
            public string id { get; set; }
            public List<ResourceItem> resources { get; set; }

        }

        public class ResourceItem
        {
            public string url { get; set; }
        }


    }
}
