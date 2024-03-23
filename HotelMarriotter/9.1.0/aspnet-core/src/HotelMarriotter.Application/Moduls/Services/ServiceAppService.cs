using Abp.Domain.Repositories;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.Rooms.Dto;
using HotelMarriotter.Moduls.Services.Dto;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Services
{
    public class ServiceAppService : HotelMarriotterAppServiceBase
    {
        private readonly IRepository<Service> _serviceRepository;
        public ServiceAppService(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> GetServiceById(int id)
        {
            try
            {
                var service = await _serviceRepository.GetAsync(id);

                if (service == null)
                {
                    throw new Exception($"Không tìm thấy dịch vụ với Id {id}");
                }

                var serviceDto = new ServiceDto
                {
                    Id = service.Id,
                    ServiceName = service.ServiceName,
                    Price = service.Price,
                    ImageUrl = service.ImageUrl,
                    IsActive = service.IsActive

                };

                return serviceDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ServiceDto>> GetAllServices()
        {
            try
            {
                var services = await _serviceRepository.GetAllListAsync();

                var dtoList = services.Select(service => new ServiceDto
                {
                    Id = service.Id,
                    ServiceName = service.ServiceName,
                    Price = service.Price,
                    ImageUrl = service.ImageUrl,
                    IsActive = service.IsActive

                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        /* public async Task AddService(ServiceDto input)
         {
             try
             {
                 // Kiểm tra xem người dùng đã chọn hình ảnh hay chưa
                 if (input.ImageFile != null && input.ImageFile.Length > 0)
                 {
                     // Đọc hình ảnh vào một MemoryStream
                     using (var memoryStream = new MemoryStream())
                     {
                         await input.ImageFile.CopyToAsync(memoryStream);

                         // Đặt vị trí của Stream về đầu để đọc lại từ đầu
                         memoryStream.Seek(0, SeekOrigin.Begin);

                         // Xử lý hình ảnh bằng ImageSharp
                         using (var image = Image.Load(memoryStream))
                         {
                             // Thực hiện các xử lý cần thiết trên hình ảnh ở đây, ví dụ:
                             // Chỉnh kích thước hình ảnh
                             image.Mutate(x => x.Resize(800, 600));

                             // Tạo đường dẫn cho tệp hình ảnh
                             var fileName = Guid.NewGuid().ToString() + ".jpg";
                             var filePath = Path.Combine(@"C:\Temp\image", fileName);

                             // Lưu hình ảnh xuống đĩa
                             using (var outputStream = new FileStream(filePath, FileMode.Create))
                             {
                                 image.SaveAsJpeg(outputStream);
                             }

                             // Lưu đường dẫn của hình ảnh vào đối tượng service
                             var service = new Service
                             {
                                 ServiceName = input.ServiceName,
                                 Price = input.Price,
                                 ImageUrl = filePath, // Lưu đường dẫn của hình ảnh
                                 IsActive = input.IsActive
                             };

                             await _serviceRepository.InsertAsync(service);
                         }
                     }
                 }
                 else
                 {
                     // Nếu người dùng không chọn hình ảnh, chỉ lưu thông tin phòng mà không có hình ảnh
                     var service = new Service
                     {
                         ServiceName = input.ServiceName,
                         Price = input.Price,
                         IsActive = input.IsActive
                     };

                     await _serviceRepository.InsertAsync(service);
                 }
             }
             catch (Exception ex)
             {
                 // Xử lý ngoại lệ ở đây, ví dụ: ghi log, hiển thị thông báo cho người dùng, vv.
                 Console.WriteLine($"An error occurred: {ex.Message}");
                 throw; // Ném lại ngoại lệ để bên gọi xử lý tiếp
             }
         }*/

        public async Task AddService(ServiceDto input)
        {
            try
            {
                // Lưu thông tin phòng vào cơ sở dữ liệu, bao gồm đường dẫn của hình ảnh
                var service = new Service
                {
                    ServiceName = input.ServiceName,
                    Price = input.Price,
                    ImageUrl = input.ImageUrl, // Sửa thành input.ImageUrl
                    IsActive = input.IsActive
                };

                await _serviceRepository.InsertAsync(service);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log, hiển thị thông báo cho người dùng, vv.
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Ném lại ngoại lệ để bên gọi xử lý tiếp
            }
        }


        public async Task<bool> DeleteService(int id)
        {
            try
            {
                var service = await _serviceRepository.FirstOrDefaultAsync(id);
                if (service == null)
                {
                    return false;
                }

                await _serviceRepository.DeleteAsync(service);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateService(ServiceDto input)
        {
            try
            {
                var existingService = await _serviceRepository.GetAsync(input.Id);

                if (existingService == null)
                {
                    return false;
                }
                existingService.ServiceName = input.ServiceName;
                existingService.Price = input.Price;
                existingService.ImageUrl = input.ImageUrl;


                await _serviceRepository.UpdateAsync(existingService);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<List<ServiceDto>> SearchServices(string keyword)
        {
            try
            {
                var services = await _serviceRepository.GetAllListAsync();
                var filteredServices = services
                    .Where(service =>
                        service.ServiceName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        service.Price.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var dtoList = filteredServices.Select(service => new ServiceDto
                {
                    Id = service.Id,
                    ServiceName = service.ServiceName,
                    Price = service.Price,
                    ImageUrl = service.ImageUrl,
                    IsActive = service.IsActive
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
