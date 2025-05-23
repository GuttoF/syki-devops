namespace Syki.Back.Features.Teacher.GetTeacherClassLessons;

[ApiController, AuthTeacher]
[EnableRateLimiting("Medium")]
[Consumes("application/json"), Produces("application/json")]
public class GetTeacherClassLessonsController(GetTeacherClassLessonsService service) : ControllerBase
{
    /// <summary>
    /// Aulas da turma
    /// </summary>
    /// <remarks>
    /// Retorna as aulas da turma informada.
    /// </remarks>
    [HttpGet("teacher/classes/{classId}/lessons")]
    public async Task<IActionResult> Get([FromRoute] Guid classId)
    {
        var result = await service.Get(User.Id(), classId);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
