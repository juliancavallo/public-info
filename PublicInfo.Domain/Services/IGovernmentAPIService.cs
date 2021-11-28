using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Services
{
    public interface IGovernmentAPIService
    {
        string GetDatasetCsvURL(string datasetName);
    }
}
