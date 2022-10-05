using Sovtech.Infrastructure.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sovtech.Utilities
{
    public class General
    {
        public IEnumerable<string> GetColumns()
        {
            return typeof(Result)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
