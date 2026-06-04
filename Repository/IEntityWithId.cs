namespace EMC.BuildingBlocks.Repository
{
    public interface IEntityWithId<T>
    {
        T Id { get; set; }
    }
}
