using Syki.Back.Features.Academic.CreateClass;
using Syki.Back.Features.Academic.CreateStudent;

namespace Syki.Tests.Features.Academic.StartClass;

public class StartClassUnitTests
{
    [Test]
    public void Should_start_class_with_2_students()
    {
        // Arrange
        var studentA = new SykiStudent(Guid.NewGuid(), Guid.NewGuid(), "Zaqueu", Guid.NewGuid());
        var studentB = new SykiStudent(Guid.NewGuid(), Guid.NewGuid(), "Maria", Guid.NewGuid());
        var @class = Class.New(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "2023.2", 40, []).GetSuccess();
        @class.Students = [studentA, studentB];

        // Act
        @class.Start();

        // Assert
        @class.Status.Should().Be(ClassStatus.Started);
    }
}
