namespace Kata;

public class TennisGame
{
    public Player? Winner { get; private set; }

    public Player FirstPlayer { get; } = new Player();

    public Player SecondPlayer { get; } = new Player();

    public void ScoreBall(Player player)
    {
        if (player.Advantage)
        {
            Winner = player;
            return;
        }

        Player opponent = player == FirstPlayer ? SecondPlayer : FirstPlayer;

        if (player.Points == Points.Forty && opponent.Advantage)
        {
            opponent.RemovePoint();
            return;
        }

        if (player.Points == Points.Forty && opponent.Points < Points.Forty)
        {
            Winner = player;
            return;
        }

        player.AddPoint();
    }

    public Score GetScore()
    {
        return (FirstPlayer.Points, SecondPlayer.Points) switch
        {
            (Points.Forty, Points.Forty) => Score.Deuce,
            (Points.Forty, _) or (_, Points.Forty) => Score.Forty,
            (Points.Thirty, _) or (_, Points.Thirty) => Score.Thirty,
            (Points.Fifteen, _) or (_, Points.Fifteen) => Score.Fifteen,
            _ => Score.Love
        };
    }
}

public class Player
{
    public Points Points { get; private set; } = Points.Zero;

    public bool Advantage { get; private set; }

    public void AddPoint()
    {
        if (Points == Points.Forty)
        {
            Advantage = true;
            return;
        }

        Points++;
    }

    public void RemovePoint()
    {
        if (Advantage)
        {
            Advantage = false;
        }
    }
}

public enum Points
{
    Zero = 0,
    Fifteen = 1,
    Thirty = 2,
    Forty = 3
}

public enum Score
{
    Love,
    Fifteen,
    Thirty,
    Forty,
    Deuce
}