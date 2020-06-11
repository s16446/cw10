using Cw10_WebApplication1.Models;
using System;
using System.Collections.Generic;


namespace WebApplication1.DAL
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();
        
        Boolean AddStudent(Student student);

        Boolean UpdateStudent(String id);

        Boolean DeleteStudent(Student student);

        IEnumerable<Student> GetStudent(string id);

        IEnumerable<Enrollment> GetEnrollments(string id, int semester);

	}
}
