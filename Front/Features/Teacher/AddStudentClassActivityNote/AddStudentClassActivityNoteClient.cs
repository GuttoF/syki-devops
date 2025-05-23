namespace Syki.Front.Features.Teacher.AddStudentClassActivityNote;

public class AddStudentClassActivityNoteClient(HttpClient http) : ITeacherClient
{
    public async Task<OneOf<SuccessOut, ErrorOut>> Add(Guid classActivityId, Guid studentId, decimal value)
    {
        var data = new AddStudentClassActivityNoteIn(studentId, value);

        var response = await http.PostAsJsonAsync($"teacher/class-activities/{classActivityId}", data);

        return await response.Resolve<SuccessOut>();
    }
}
