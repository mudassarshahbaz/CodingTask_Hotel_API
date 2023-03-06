using AutoMapper;
using Common.Helpers;
using DTO.ViewModel.Hotel;
using Logger.Interfaces;
using Newtonsoft.Json;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IRepositoryUnit _repository;
        private readonly IEventLogger eventLogger;
        private readonly IMapper mapper;

        public HotelService(IRepositoryUnit repository, IEventLogger eventLogger, IMapper mapper)
        {
            _repository = repository;
            this.eventLogger = eventLogger;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<Hotels>> GetHotelData(Int32 hotelId, DateTime arrivalDate)
        {
            string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName;
            var FinalPath = Path.Combine(path, "Common\\Assets", "hotelRatesList.json");
            var jsonContent = ReadFileContent(FinalPath);

            //Validate json
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                await eventLogger.LogEvent("", "User", "Getting Json ERROR", "Invalid Json Format");
                return ServiceResults.Errors.Invalid<Hotels>("Json Format", null);
            }

            try
            {
                //Filter List By Id
                var result = JsonConvert.DeserializeObject<IEnumerable<Hotels>>(jsonContent).FirstOrDefault(c => c.hotel.hotelID == hotelId);

                if (result == null)
                {
                    await eventLogger.LogEvent("", "User", "Result not found", "Result not found");
                    return ServiceResults.Errors.NotFound<Hotels>("Result", null);

                }

                //Filter List by arivalDate
                //if (arrivalDate.HasValue)
                //{
                    result.hotelRates = result
                        .hotelRates
                        .Where(c => DateTime.Compare(c.targetDay.Date, arrivalDate.Date) == 0)
                        .ToList();
                //}

                return ServiceResults.GetSuccessfully<Hotels>(result);
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (JsonSerializationException)
            {
                throw new UserFriendlyException(HotelResourceKeys.InvalidJsonContent);
            }
            catch
            {
                throw new UserFriendlyException(MessageHelper.SystemUnhandledException);
            }


        }

        public string ReadFileContent(string filePath)
        {
            try
            {
                return File.Exists(filePath)
                    ? File.ReadAllText(filePath)
                    : throw new UserFriendlyException(MessageHelper.FileNotFound);
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (IOException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (Exception)
            {
                //ToDo: Log Exception
                throw new UserFriendlyException(MessageHelper.SystemUnhandledException);
            }
        }

        /// <summary>
        /// Read file content as byte[] from a path.
        /// </summary>
        /// <param name="filePath">file path based on os</param>
        /// <returns>file content as byte[]</returns>
        public byte[] ReadFileBytes(string filePath)
        {
            try
            {
                return File.Exists(filePath)
                    ? File.ReadAllBytes(filePath)
                    : throw new UserFriendlyException(MessageHelper.FileNotFound);
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (IOException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (Exception)
            {
                //ToDo: Log Exception
                throw new UserFriendlyException(MessageHelper.SystemUnhandledException);
            }
        }
    }
}
