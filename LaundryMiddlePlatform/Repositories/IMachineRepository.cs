using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMachineRepository
    {
        public IEnumerable<Machine> GetAllByStoreId(int? id);
        public Machine Create(Machine machine);
        public void Update(Machine machine);
        public Machine? GetById(int? id);
        public Machine? GetByName(string? name);
    }
}
