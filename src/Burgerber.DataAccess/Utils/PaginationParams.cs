using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.DataAccess.Utils;

public class PaginationParams
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int SkipCount {get
        {
            return (PageNumber-1)*PageSize;
        } 
    }

    public PaginationParams(int pageNumber,int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }


}
