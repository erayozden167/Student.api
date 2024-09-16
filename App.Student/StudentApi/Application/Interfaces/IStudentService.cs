using StudentApi.Models.DTOs;
using StudentApi.Models.Entity;
using StudentApi.Models.ViewModel;

namespace StudentApi.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentListViewModel>> GetAllAsync();
        Task<StudentViewModel?> GetByIdAsync(int id);
        Task AddAsync(StudentDto student);
        Task UpdateAsync(StudentDto student);
        Task DeleteAsync(int id);
    }
}
