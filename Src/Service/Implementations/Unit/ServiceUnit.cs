using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger.Interfaces;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Implementations;
using Service.Interfaces.Unit;

namespace Service.Implementations.Unit
{
    internal class ServiceUnit : IServiceUnit
    {
        private readonly IRepositoryUnit _repository;
        private readonly IMapper _mapper;
        private readonly IEventLogger _eventLogger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IHotelService _hotel;

        public ServiceUnit(IRepositoryUnit repository, IMapper mapper, IEventLogger eventLogger, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _eventLogger = eventLogger;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public IHotelService Hotel =>
             _hotel ??= new HotelService(_repository, _eventLogger, _mapper);

    }
}
