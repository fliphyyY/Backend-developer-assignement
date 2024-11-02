﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.ProductContext
{
    public interface IProductContext
    {
        Task<List<Product>> GetProducts();
    }
}
