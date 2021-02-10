using PaginationDemo.Extensions;
using System.Collections.Generic;

namespace PaginationDemo.Interfaces
{
    public interface ILinkedResource
    {
        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
