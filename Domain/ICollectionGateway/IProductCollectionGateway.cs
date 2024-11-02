using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.ICollectionGateway
{
    public interface IProductCollectionGateway
    {
        Task<List<Product>> FetchProducts();
    }
}
