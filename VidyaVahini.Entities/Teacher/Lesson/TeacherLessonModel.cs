using System;
using System.Collections.Generic;
using System.Text;
using VidyaVahini.Entities.Teacher.LessonSection;

namespace VidyaVahini.Entities.Teacher.Lesson
{
    public class TeacherLessonModel
    {
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonCode { get; set; }
        public List<TeacherLessonSectionModel> LessonSections { get; set; }
    }
}
