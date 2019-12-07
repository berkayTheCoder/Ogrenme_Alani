﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
   public class Order
    {
        
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }

        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public string ConservationId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }

    public enum EnumPaymentTypes
    {
        waiting = 0,
        Unpadid = 1,
        Completed = 2
    }

    public enum EnumOrderState
    {
        CreditCart = 0,
        Eft = 1
    }
}