using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl;

public class CustomerRepository : ICustomerRepository
{
    public IQueryable<Customer> GetAll() => CustomerDAO.Instance.GetAll();

    public Customer? GetById(int id) => CustomerDAO.Instance.GetById(id);

    public Customer? GetByPhone(string phone) => CustomerDAO.Instance.GetByPhone(phone);

    public Customer? GetByPhoneAndPassword(string phone, string password) 
        => CustomerDAO.Instance.GetByPhoneAndPassword(phone, password);

    public Customer? GetByEmail(string email) => CustomerDAO.Instance.GetByEmail(email);

    public Customer Create(Customer customer) => CustomerDAO.Instance.Create(customer);

    public void Update(Customer customer) => CustomerDAO.Instance.Update(customer);

    public void Delete(int id) => CustomerDAO.Instance.Delete(id);
}
