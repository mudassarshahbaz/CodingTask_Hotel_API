using DTO.ViewModel.Hotel;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IHotelService
    {
        Task<ServiceResult<Hotels>> GetHotelData(Int32 hotelId, DateTime arrivalDate);
    }
}
