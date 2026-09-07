using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace lab_1_
{
    public class TeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["UniversityDb"].ConnectionString;
        }

        public DataTable GetTeachersOverview()
        {
            string query = @"
                SELECT 
                    T.TeacherID AS [ID],
                    T.LastName + ' ' + T.FirstName + ' ' + ISNULL(T.MiddleName, '') AS [ПІБ Викладача],
                    T.Phone AS [Телефон],
                    T.Workplace AS [Місце роботи],
                    P.PositionName AS [Посада],
                    P.HourlyRate AS [Погодинна ставка],
                    S.SubjectName AS [Предмет],
                    TS.HoursRead AS [Прочитані години],
                    T.HomeAddress AS [Домашня адреса],
                    T.Characteristic AS [Характеристика]
                FROM Teachers T
                INNER JOIN Positions P ON T.PositionID = P.PositionID
                INNER JOIN TeacherSubjects TS ON T.TeacherID = TS.TeacherID
                INNER JOIN Subjects S ON TS.SubjectID = S.SubjectID
                ORDER BY T.LastName;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
