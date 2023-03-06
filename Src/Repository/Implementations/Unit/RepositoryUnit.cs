using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using Repository.Implementations;
using Repository.Interfaces.Unit;

namespace Repository.Implementations.Unit
{
    internal class RepositoryUnit : IRepositoryUnit
    {
        private IHotelRepository _hotel;
        
        public RepositoryUnit()
        {
        }

        public IHotelRepository Hotel =>
           _hotel ??= new HotelRepository();
       
       
    }
}
