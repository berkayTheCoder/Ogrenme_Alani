﻿using System;
using System.Collections.Generic;
using System.Text;
using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
    }
}
