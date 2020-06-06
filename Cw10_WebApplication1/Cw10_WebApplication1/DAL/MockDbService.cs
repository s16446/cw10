using Cw10_WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.DAL
{
    public class MockDbService : IDbService
    {
        private static List<Student> _students;

        private const string CONN_STR = "Data Source=db-mssql;Initial Catalog=s16446;Integrated Security=True";

        static MockDbService() 
        {
            _students = new List<Student>();
            var db = new s16446Context();
           _students = db.Student.ToList();

        }

        public IEnumerable<Student> GetStudents()
        {
        foreach(var r in _students)
            Console.WriteLine(r.FirstName);
            return _students;
        }

        public IEnumerable<Student> GetStudent(string id) 
        {
            List <Student> n = new List<Student>();
            //string index_no = id;

            //using (var client = new SqlConnection(CONN_STR))
            //using (var com = new SqlCommand())
            //{
            //    com.Connection = client;
            //    com.CommandText = "select IndexNumber, FirstName, LastName, BirthDate from dbo.Student WHERE IndexNumber = @index_no";
            //    com.Parameters.AddWithValue("index_no", index_no);
            //    client.Open();
                
            //    var dr = com.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var st = new Student
            //        {
            //            IndexNumber = dr["IndexNumber"].ToString(),
            //            FirstName = dr["FirstName"].ToString(),
            //            LastName = dr["LastName"].ToString(),
            //            BirthDate = dr["BirthDate"].ToString()
            //        };
            //        n.Add(st);
            //    }
            //}
            return n;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            if (_students.Contains(student))
                _students.Remove(student);
        }

        public Student FindStudent(string index)
        {
            for(int i = 0; i < _students.Count; i++) {
                if (_students[i].IndexNumber.Equals(index)){
                    return _students[i];
                }
            }
            return null;
        }


        public IEnumerable<Enrollment> GetEnrollments(string id, int semester) {
            List<Enrollment> wpisy = null;
            
            //using (var client = new SqlConnection(CONN_STR))
            //using (var com = new SqlCommand())
            //{
            //    com.Connection = client;
            //    com.CommandText = "" +
            //    "SELECT " +
            //      "  st.IndexNumber " +
	           //   ", st.FirstName " +
	           //   ", st.LastName " +
            //      ", e.Semester " +
            //      ", e.StartDate " +
	           //   ", s.[Name] " +
            //    "FROM dbo.Student st " +
            //    "LEFT JOIN dbo.Enrollment e ON st.IdEnrollment = e.IdEnrollment " +
            //    "LEFT JOIN dbo.Studies s ON e.IdStudy = s.IdStudy " +
            //    "WHERE IndexNumber = '" + id + "' AND e.Semester = '" + semester + "'";
  
            //    client.Open();
            //    wpisy = new List<Enrollment>();

            //    var dr = com.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var e = new Enrollment();
            //        e.Semester = dr["Semester"].ToString();
            //        e.StartDate = DateTime.Parse(dr["StartDate"].ToString()).ToShortDateString();
            //        e.StudiesName = dr["Name"].ToString();
            //        wpisy.Add(e);
            //    }
            //}
            return wpisy;
        }

        }
}
