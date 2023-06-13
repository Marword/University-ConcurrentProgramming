using BallSimulator.Data.API;

namespace BallSimulator.Data;

internal static class Collisions
{
    private static readonly int threads = Environment.ProcessorCount;
    private static readonly HashSet<(IBall, IBall)> ballCollisions = new(threads);
    private static readonly List<(IBall, Vector2, CollisionAxis)> gameCollisions = new(threads);

    public static List<(IBall, Vector2, CollisionAxis)> GetBoardCollisions(IEnumerable<IBall> balls, Game game)
    {
        gameCollisions.Clear();

        var (boundryXx, boundryXy) = game.boundX;
        var (boundryYx, boundryYy) = game.boundY;

        foreach (var ball in balls)
        {
            var (posX, posY) = ball.Coordinates;
            int radius = ball.Radius;

            if (!posX.Inside(boundryXx, boundryXy, radius))
            {
                gameCollisions.Add((ball, game.boundX, CollisionAxis.X));
            }
            if (!posY.Inside(boundryYx, boundryYy, radius))
            {
                gameCollisions.Add((ball, game.boundY, CollisionAxis.Y));
            }
        }

        return gameCollisions;
    }

    public static HashSet<(IBall, IBall)> GetBallsCollisions(IEnumerable<IBall> balls)
    {
        ballCollisions.Clear();

        foreach (var ball1 in balls)
        {
            foreach (var ball2 in balls)
            {
                if (ball1 == ball2) continue;
                if (ball1.Touches(ball2)) ballCollisions.Add((ball1, ball2));
            }
        }

        return ballCollisions;
    }

    public static Vector2 CalculateTempo(IBall ball, Vector2 boundry, CollisionAxis collisionAxis)
    {
        Vector2 position = ball.Coordinates;
        Vector2 speed = ball.Tempo;
        int radius = ball.Radius;

        var (newTempoX, newTempoY) = speed;

        switch (collisionAxis)
        {
            case CollisionAxis.X:
                if (position.X <= boundry.X + radius) newTempoX = MathF.Abs(newTempoX);
                else newTempoX = -MathF.Abs(newTempoX);
                break;
            case CollisionAxis.Y:
                if (position.Y <= boundry.X + radius) newTempoY = MathF.Abs(newTempoY);
                else newTempoY = -MathF.Abs(newTempoY);
                break;
            default:
                throw new ArgumentException("Collision Point not recognized", nameof(collisionAxis));
        }

        return new Vector2(newTempoX, newTempoY);
    }

    public static (Vector2 speedOne, Vector2 speedTwo, bool speedChanged) CalculateTempos(IBall ball1, IBall ball2)
    {
        float distance = Vector2.Distance(ball1.Coordinates, ball2.Coordinates);

        Vector2 normal = new((ball2.Coordinates.X - ball1.Coordinates.X) / distance, (ball2.Coordinates.Y - ball1.Coordinates.Y) / distance);
        Vector2 tangent = new(-normal.Y, normal.X);

        Vector2 ball1Tempo = ball1.Tempo;
        Vector2 ball2Tempo = ball2.Tempo;
        if (Vector2.Scalar(ball1Tempo, normal) < 0f) return (ball1Tempo, ball2Tempo, false);

        float ball1Radius = ball1.Radius;
        float ball2Radius = ball2.Radius;
        float ball1Weight = ball1Radius * ball1Radius;
        float ball2Weight = ball2Radius * ball2Radius;

        float dpNorm1 = ball1Tempo.X * normal.X + ball1Tempo.Y * normal.Y;
        float dpNorm2 = ball2Tempo.X * normal.X + ball2Tempo.Y * normal.Y;

        float dpTan1 = ball1Tempo.X * tangent.X + ball1Tempo.Y * tangent.Y;
        float dpTan2 = ball2Tempo.X * tangent.X + ball2Tempo.Y * tangent.Y;

        float momentum1 = (dpNorm1 * (ball1Weight - ball2Weight) + 2.0f * ball2Weight * dpNorm2) / (ball1Weight + ball2Weight);
        float momentum2 = (dpNorm2 * (ball2Weight - ball1Weight) + 2.0f * ball1Weight * dpNorm1) / (ball2Weight + ball1Weight);

        Vector2 newVelocity1 = new(tangent.X * dpTan1 + normal.X * momentum1, tangent.Y * dpTan1 + normal.Y * momentum1);
        Vector2 newVelocity2 = new(tangent.X * dpTan2 + normal.X * momentum2, tangent.Y * dpTan2 + normal.Y * momentum2);

        return (newVelocity1, newVelocity2, true);
    }

    internal enum CollisionAxis
    {
        X,
        Y
    }
}