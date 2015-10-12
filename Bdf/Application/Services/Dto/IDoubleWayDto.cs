namespace Bdf.Application.Services.Dto
{
    /// <summary>
    /// Used to mark a DTO as both of <see cref="IInputDto"/> and <see cref="IOutputDto"/>.
    /// </summary>
    public interface IDoubleWayDto : IInputDto, IOutputDto
    {
    }
}