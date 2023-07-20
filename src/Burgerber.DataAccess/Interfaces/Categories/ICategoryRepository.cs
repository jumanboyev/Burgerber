using Burgerber.DataAccess.Common.Interfaces;
using Burgerber.Domain.Entities.Catagories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.DataAccess.Interfaces.Categories;

public interface ICategoryRepository:IRepository<Category,Category>,
    IGetAll<Category>
{

}
