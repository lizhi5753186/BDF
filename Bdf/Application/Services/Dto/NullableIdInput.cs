namespace Bdf.Application.Services.Dto
{
    /// <summary>
    /// implements <see cref="IInputDto"/> 
    /// </summary>
    /// <typeparam name="TId">Type of Id</typeparam>
    public class NullableIdInput<TId>: IInputDto
        where TId : struct 
    {
        public TId? Id { get; set; }

        public NullableIdInput()
        {
        }

        public NullableIdInput(TId? id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="NullableIdInput{TPrimaryKey}"/> with <see cref="int"/>
    /// </summary>
    public class NullableIdInput : NullableIdInput<int>
    {
        public NullableIdInput()
        {
        }
        public NullableIdInput(int? id)
            : base(id)
        {

        }
    }
}