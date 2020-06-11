using Cw10_WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.DAL
{
    public class MockDbService : IDbService
    {
        private readonly s16446Context _context;

        public MockDbService(s16446Context context)
        {
           _context = context;
        }

        public IEnumerable<Student> GetStudents()
        {
            var _students = _context.Student.Select(p => new Student() 
            { 
                IndexNumber = p.IndexNumber.ToString()
                , FirstName  = p.FirstName
                , LastName = p.LastName
                , BirthDate = p.BirthDate
                , IdEnrollment = p.IdEnrollment
            }).ToList();

            return _students;
        }

        public IEnumerable<Student> GetStudent(string id) 
        {
            var _students = _context.Student.Where(p => p.IndexNumber == id).Select(p => new Student() 
            { 
                IndexNumber = p.IndexNumber.ToString()
                , FirstName  = p.FirstName
                , LastName = p.LastName
                , BirthDate = p.BirthDate
                , IdEnrollment = p.IdEnrollment
            }).ToList();
            
            return _students;
        }

        public Boolean AddStudent(Student student)
        {
            if (!_context.Student.Contains(student)) {
                _context.Student.Add(student);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Boolean UpdateStudent(String id)
        {
            var student = _context.Student.Where(s => s.IndexNumber == id).FirstOrDefault();
            if (_context.Student.Contains(student)) {
                _context.Student.Update(student);
                _context.SaveChanges();
                return true;
             }
             else
                return false;
        }

        public Boolean DeleteStudent(Student student)
        {
            if (_context.Student.Contains(student)) {
                _context.Student.Remove(student);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public IEnumerable<Enrollment> GetEnrollments(string id, int semester) {
            var _wpisy = _context.Enrollment.Join(_context.Student,
                        a => a.IdEnrollment,
                        b => b.IdEnrollment,
                        (a,b) => a
                        /*new {
                              b.IndexNumber
                            , b.FirstName
                            , b.LastName
                            , a.Semester
                            , a.StartDate}*/
                    ).ToList();
            return _wpisy;
        }

    }
}
