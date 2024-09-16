using StudentApi.Application.Interfaces;
using StudentApi.Infrastructure.Interfaces;
using StudentApi.Models.DTOs;
using StudentApi.Models.Entity;
using StudentApi.Models.ViewModel;
using System.ComponentModel.DataAnnotations;
public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentListViewModel>> GetAllAsync()
    {
        List<Student> students = await _studentRepository.GetAllAsync();
        List<StudentListViewModel> view = students.Select(student => ConvertToListViewModel(student)).ToList();
        return view;
    }

    public async Task<StudentViewModel?> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return null;
        }
        return ConvertToViewModel(student);
    }

    public async Task AddAsync(StudentDto studentDto)
    {
        if (!ValidateDto(studentDto)) throw new ArgumentException("Invalid student data");

        var student = ConvertToEntity(studentDto);
        await _studentRepository.AddAsync(student);
    }

    public async Task UpdateAsync(StudentDto studentDto)
    {
        if (!ValidateDto(studentDto)) throw new ArgumentException("Invalid student data");

        var existingStudent = await _studentRepository.GetByIdAsync(studentDto.Id);
        if (existingStudent == null)
        {
            throw new KeyNotFoundException("Student not found");
        }

        var student = ConvertToEntity(studentDto);
        await _studentRepository.UpdateAsync(student);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }
        await _studentRepository.DeleteAsync(id);
    }

    private StudentListViewModel ConvertToListViewModel(Student student)
    {
        var age = DateTime.Now.Year - student.DateOfBirth.Year;

        return new StudentListViewModel
        {
            Id = student.Id,
            FullName = $"{student.FirstName} {student.LastName}",
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            Age = age
        };
    }
    private StudentViewModel ConvertToViewModel(Student student)
    {
        var age = DateTime.Now.Year - student.DateOfBirth.Year;

        return new StudentViewModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Phone = student.PhoneNumber,
            Age = age,
            Address = student.Address,
            Email = student.Email
        };
    }

    private Student ConvertToEntity(StudentDto studentDto)
    {
        return new Student
        {
            Id = studentDto.Id,
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            DateOfBirth = studentDto.DateOfBirth,
            Email = studentDto.Email,
            PhoneNumber = studentDto.PhoneNumber,
            Address = studentDto.Address
        };
    }

    private bool ValidateDto(StudentDto studentDto)
    {
        var context = new ValidationContext(studentDto, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(studentDto, context, results, validateAllProperties: true);
    }
}

