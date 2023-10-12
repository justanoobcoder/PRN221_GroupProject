using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICustomerRepository
{
    IQueryable<Customer> GetAll();
    Customer? GetById(int id);
    Customer? GetByPhone(string phone);
    Customer? GetByPhoneAndPassword(string phone, string password);
    Customer? GetByEmail(string email);
    Customer Create(Customer customer);
    void Update(Customer customer);
    void Delete(int id);
}
