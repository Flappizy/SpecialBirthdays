using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Repository;

namespace RemindMeOfSpecialBirthdays.Client.Features
{
    public class PagingResponse<T>
    {
        public List<T> PersonsBirthdays { get; set; }
        public MetaData PageMetadata { get; set; }
    }
}
