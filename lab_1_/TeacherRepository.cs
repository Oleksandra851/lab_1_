using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab_2_
{
    public class TeacherRepository
    {
        public List<TeacherOverviewDto> GetTeachersOverview()
        {
            using (var context = new UniversityDbContext())
            {
                return context.TeacherSubjects
                    .Include(ts => ts.Teacher)
                        .ThenInclude(t => t.Position)
                    .Include(ts => ts.Subject)
                    .Select(ts => new TeacherOverviewDto
                    {
                        ID = ts.TeacherID,
                        ПІБ_Викладача = ts.Teacher.LastName + " " + ts.Teacher.FirstName + " " + (ts.Teacher.MiddleName ?? ""),
                        Телефон = ts.Teacher.Phone,
                        Місце_роботи = ts.Teacher.Workplace,
                        Посада = ts.Teacher.Position.PositionName,
                        Погодинна_ставка = ts.Teacher.Position.HourlyRate,
                        Предмет = ts.Subject.SubjectName,
                        Прочитані_години = ts.HoursRead,
                        Домашня_адреса = ts.Teacher.HomeAddress,
                        Характеристика = ts.Teacher.Characteristic
                    })
                    .OrderBy(t => t.ПІБ_Викладача)
                    .ToList();
            }
        }
    }
}

