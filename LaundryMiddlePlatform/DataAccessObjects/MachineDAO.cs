using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class MachineDAO
    {
        private static MachineDAO? _instance = null;
        private static readonly object _lock = new();
        private MachineDAO() { }
        public static MachineDAO Instance
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

        public IEnumerable<Machine> GetAllByStoreId(int? id)
        {
            var context = new LaundryMiddlePlatformDbContext();
            return context.Machines.Where(s => s.StoreId.Equals(id)).ToList();
        }
        public Machine? GetById(int? id)
        {
            var context = new LaundryMiddlePlatformDbContext();
            return context.Machines.Find(id);
        }
        public Machine? GetByName(string? name)
        {
            var context = new LaundryMiddlePlatformDbContext();
            return context.Machines.SingleOrDefault(m => m.Name.Equals(name));
        }


        public Machine Create(Machine machine)
        {
            var context = new LaundryMiddlePlatformDbContext();
            context.Machines.Add(machine);
            context.SaveChanges();
            return machine;
        }

        public void Update(Machine machine)
        {
            var context = new LaundryMiddlePlatformDbContext();
            var _machine = context.Machines.Find(machine.Id);
            if (_machine != null)
            {
                context.Entry(_machine).CurrentValues.SetValues(machine);
            }
            context.SaveChanges();
        }


    }



}
