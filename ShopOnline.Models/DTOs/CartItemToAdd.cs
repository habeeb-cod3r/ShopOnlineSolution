﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Models.DTOs
{
    public class CartItemToAdd
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  
        public int Qty { get; set; }
    }
}
