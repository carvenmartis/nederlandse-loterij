using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Commands;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Domain.Exceptions;

namespace NederlandseLoterij.Application.Tests;

public class ScratchRecordCommandHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IScratchableAreaRepository _scratchableAreaRepository;
    private readonly ScratchRecordCommandHandler _handler;
    private readonly IFixture _fixture = new Fixture();

    public ScratchRecordCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _scratchableAreaRepository = Substitute.For<IScratchableAreaRepository>();
        _handler = new ScratchRecordCommandHandler(_userRepository, _scratchableAreaRepository);
    }

    [Fact]
    public async Task Handle_WhenUserAlreadyScratchedSquare_ThrowsValidationException()
    {
        // Arrange
        var scratchableArea = _fixture.Create<ScratchableRecordDto>();
        var command = _fixture.Create<ScratchRecordCommand>();
        var user = _fixture.Build<UserDto>()
            .With(u => u.HasScratched, true)
            .With(u => u.Id, command.UserId)
            .Create();

        _userRepository.GetUserAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _scratchableAreaRepository.GetRecordByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(scratchableArea);

        // Act
        Func<Task> actual = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>().WithMessage("User has already scratched a record.");
    }

    [Fact]
    public async Task Handle_RecordNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = _fixture.Create<ScratchRecordCommand>();

        _scratchableAreaRepository.GetRecordByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns((ScratchableRecordDto?)null);

        // Act
        Func<Task> actual = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_ValidRequest_UpdatesUserAndRecord()
    {
        // Arrange
        var command = _fixture.Create<ScratchRecordCommand>();
        var user = _fixture.Build<UserDto>()
                .With(u => u.HasScratched, false)
                .With(u => u.Id, command.UserId)
                .Create();

        var scratchableArea = _fixture.Build<ScratchableRecordDto>()
            .With(r => r.IsScratched, false)
            .With(r => r.Id, command.Id)
            .With(r => r.Prize, "Prize")
            .Create();

        _userRepository.GetUserAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _scratchableAreaRepository.GetRecordByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(scratchableArea);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        actual.Should().NotBeNull();
        actual.IsScratched.Should().BeTrue();
        await _userRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _scratchableAreaRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}