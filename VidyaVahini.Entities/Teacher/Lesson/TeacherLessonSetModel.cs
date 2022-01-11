using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher.Lesson
{
    public class TeacherLessonSetModel
    {
        public string LessonSetId { get; set; }
        public int LessonSetNumber { get; set; }        
        public List<TeacherLessonModel> Lessons { get; set; }
    }
}
