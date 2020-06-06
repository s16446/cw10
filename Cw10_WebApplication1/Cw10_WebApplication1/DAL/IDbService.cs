using Cw10_WebApplication1.Models;
using System;
using System.Collections.Generic;


namespace WebApplication1.DAL
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();
        
        void AddStudent(Student student);

        void DeleteStudent(Student student);

        Student FindStudent(string index);

        IEnumerable<Student> GetStudent(string id);

        IEnumerable<Enrollment> GetEnrollments(string id, int semester);

    }
}
