namespace Data
{
    public interface IBallDto : IObservable<IBallDto>
    {
        int Diameter { get; init; }
        float TempoX { get; }
        float TempoY { get; }
        float CoordinatesX { get; }
        float CoordinatesY { get; }

        Task SetTempo(float tempoX, float tempoY);
        Task Move(float moveX, float moveY);
    }
}
