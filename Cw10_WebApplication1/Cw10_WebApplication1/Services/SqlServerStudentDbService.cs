using Cw10_WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;

namespace WebApplication1.Services
{
	public class SqlServerStudentDbService : IStudentDbService
    {
        private readonly s16446Context _context;

        public SqlServerStudentDbService(s16446Context context)
        {
           _context = context;
        }
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var response = new EnrollStudentResponse();
            response.setStatus(400, "Unknown Error"); // domyślnie - błąd

            var _studies = _context.Studies.Where(p => p.Name == request.Studies).FirstOrDefault();
            if (_studies == null) {
               response.setStatus(400, "ERROR: Nie istnieją studia przekazane przez klienta");
               return response;
            }

            var _enrollment = _context.Enrollment.Where(e => e.IdStudy ==_studies.IdStudy && e.Semester == 1).FirstOrDefault();
            if (_enrollment == null) {
                _enrollment = new Enrollment() {
                        IdEnrollment = _context.Enrollment.Max(p => p.IdEnrollment) + 1,
                        Semester = 1, 
                        IdStudy = _studies.IdStudy,
                        StartDate = DateTime.Now.Date
                 };
                _context.Enrollment.Add(_enrollment);
                _context.SaveChanges();
             }

             var _student = _context.Student.Where(p => p.IndexNumber == request.IndexNumber).FirstOrDefault();
             if (_student == null) {
                DateTime dateValue;
		        DateTime.TryParseExact(request.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);

			    _student = new Student {
			        IndexNumber = request.IndexNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = dateValue,
                    IdEnrollment = _enrollment.IdEnrollment
			    };
                _context.Student.Add(_student);
                _context.SaveChanges();
                response.setStatus(201, "Student został poprawnie zapisany na semestr"); // student został poprawnie zapisany na semestr                                                
             }
             else {
                response.setStatus(400, "Taki student już istnieje");
             }
             response.LastName = _student.LastName;
             response.Semester = _enrollment.Semester;
             response.StartDate = _enrollment.StartDate.ToString();
             response.Studies = request.Studies;

             return response;
        }

        public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        {
            var response = new PromoteStudentsResponse();
            response.setStatus(400, "Unknown Error"); // domyślnie - błąd

            var _studies = _context.Studies.Where( s => s.Name == request.Studies).FirstOrDefault();
            if (_studies == null) {
                response.setStatus(404, "ERROR: nie znaleziono studiów");
                return response;
            };

            var _enrollment = _context.Enrollment
                .Where(a => a.IdStudy == _studies.IdStudy && a.Semester == request.Semester)
                .OrderByDescending(s => s.StartDate).FirstOrDefault();

            if (_enrollment == null) {
                response.setStatus(404, "ERROR: nie znaleziono semestru i studiów");
                return response;
            };
            Console.WriteLine("tutaj:" + _enrollment.IdEnrollment);
            var _enrollment_next = _context.Enrollment.Where(p => p.Semester == _enrollment.Semester + 1).FirstOrDefault();
            if  (_enrollment_next == null) {
                _enrollment_next = new Enrollment(){
                        IdEnrollment = _context.Enrollment.Max(p => p.IdEnrollment) + 1,
                        Semester = _enrollment.Semester + 1, 
                        IdStudy = _enrollment.IdStudy,
                        StartDate = DateTime.Now.Date
                };
                _context.Add(_enrollment_next);
                _context.SaveChanges();
            }

            var _students = _context.Student.Where(s => s.IdEnrollment == _enrollment.IdEnrollment);
           
            foreach (var s in _students) {
                s.IdEnrollment = _enrollment_next.IdEnrollment;
            }
            _context.SaveChanges();
            response.Semester = _enrollment_next.Semester;
            response.StartDate = _enrollment_next.StartDate.ToString();
            response.StudiesName = _studies.Name;
            response.setStatus(201, "Studenci zostali promowani na następny semestr");
            return response;
        }


    }
}
