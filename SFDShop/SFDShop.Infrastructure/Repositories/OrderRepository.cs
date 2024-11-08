﻿using SFDShop.Infrastructure;
using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SFDShopDbContext _context;
        public OrderRepository(SFDShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public int UpdateItemCount(int itemId, int minusCount)
        {
            var item = _context.Items.FirstOrDefault(c => c.Id == itemId);
            if(item.Count >= minusCount)
            {
                item.Count -= minusCount;
                _context.Items.Attach(item);
                _context.Entry(item).Property("Count").IsModified = true;
            }
            else
            {
                return 0;
            }

            if(item.Count <= 0)
            {
                item.IsAvailable = false;
                _context.Entry(item).Property("IsAvailable").IsModified= true;
            }
            _context.SaveChanges();

            return 1;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order GetOrderByCustomerId (int customerId)
        {
            return _context.Orders.FirstOrDefault(o => o.CustomerId == customerId);
        }

        public bool CheckOrderStatus(int id)
        {
            var order = GetOrderById(id);
            return order.isDone;
        }   

        public int AddNewOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges(); 
            return order.Id;
        }

        public int CopletedOrded(int id)
        {
            var order = GetOrderById(id);
            if (order != null)
            {
                order.isDone = true;
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
