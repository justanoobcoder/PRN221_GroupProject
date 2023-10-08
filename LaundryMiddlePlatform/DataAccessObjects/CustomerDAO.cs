﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class CustomerDAO
{
    private static CustomerDAO? _instance = null;
    private readonly static object _lock = new();

    private CustomerDAO() { }

    public static CustomerDAO Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new();
                return _instance;
            }
        }
    }

    public IQueryable<Customer> GetAll()
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Customers;
    }

    public Customer? GetById(int id)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Customers.Find(id);
    }

    public Customer? GetByPhone(string phone)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Customers.SingleOrDefault(c => c.Phone == phone);
    }

    public Customer? GetByEmail(string email)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Customers.SingleOrDefault(c => c.Email == email);
    }

    public Customer Create(Customer customer)
    {
        var context = new LaundryMiddlePlatformDbContext();
        context.Customers.Add(customer);
        context.SaveChanges();
        return customer;
    }

    public void Update(Customer customer)
    {
        var context = new LaundryMiddlePlatformDbContext();
        var c = context.Customers.Find(customer.Id);
        if (c != null)
        {
            context.Entry(c).CurrentValues.SetValues(customer);
        }
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var context = new LaundryMiddlePlatformDbContext();
        var c = context.Customers.Find(id);
        if (c != null)
        {
            c.DeletedAt = DateTime.Now;
        }
        context.SaveChanges();
    }
}