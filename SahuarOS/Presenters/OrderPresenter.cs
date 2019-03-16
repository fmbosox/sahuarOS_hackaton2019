﻿using System;
using Core.Domain.Order;

namespace SahuarOS.Presenters
{
    public class OrderPresenter
    {
        public static string PresenetStatus(Order.OrderStatus status)
        {
            switch(status){
                case Order.OrderStatus.Received:
                    return "Recibida";
                case Order.OrderStatus.Started:
                    return "Empezada";
                case Order.OrderStatus.Finished:
                    return "Terminda";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
        
        
    }
}