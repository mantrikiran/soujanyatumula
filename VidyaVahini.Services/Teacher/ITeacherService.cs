using VidyaVahini.Entities.Teacher;

namespace VidyaVahini.Services.Teacher
{
    public interface ITeacherService
    {
        FindTeacherModel FindTeacherProfile(string username);

        string RegisterTeacher(RegisterTeacherCommand registerTeacherCommand);
    }
}
