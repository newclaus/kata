namespace Kata.Tests;

public class TennisGameTest
{
    private readonly TennisGame _game;

    private readonly Player _firstPlayer;
    private readonly Player _secondPlayer;

    public TennisGameTest()
    {
        _game = new TennisGame();

        _firstPlayer = _game.FirstPlayer;
        _secondPlayer = _game.SecondPlayer;
    }

    [Fact]
    public void TestGutterGame()
    {
        _game.ScoreBall(_firstPlayer);

        Assert.Null(_game.Winner);
    }

    [Fact]
    public void TestFlawlessWin()
    {
        ScoreManyBalls(_firstPlayer, 4);

        Assert.Equal(_firstPlayer, _game.Winner);
    }

    [Fact]
    public void TestDeuce()
    {
        Deuce();

        Assert.Equal(Score.Deuce, _game.GetScore());
    }

    [Fact]
    public void TestForty()
    {
        ScoreManyBalls(_firstPlayer, 3);

        Assert.Equal(Score.Forty, _game.GetScore());
    }

    [Fact]
    public void TestThirty()
    {
        ScoreManyBalls(_firstPlayer, 2);

        Assert.Equal(Score.Thirty, _game.GetScore());
    }

    [Fact]
    public void TestFifteen()
    {
        _game.ScoreBall(_firstPlayer);

        Assert.Equal(Score.Fifteen, _game.GetScore());
    }

    [Fact]
    public void TestLove()
    {
        Assert.Equal(Score.Love, _game.GetScore());
    }

    [Fact]
    public void TestAdvantage()
    {
        Deuce();
        _game.ScoreBall(_firstPlayer);

        Assert.True(_firstPlayer.Advantage);
    }

    [Fact]
    public void TestWinAfterAdvantage()
    {
        Deuce();
        ScoreManyBalls(_firstPlayer, 2);

        Assert.Equal(_firstPlayer, _game.Winner);
    }

    [Fact]
    public void TestDeuceAfterAdvantage()
    {
        Deuce();
        _game.ScoreBall(_firstPlayer);
        _game.ScoreBall(_secondPlayer);

        Assert.False(_firstPlayer.Advantage);
    }

    private void Deuce()
    {
        ScoreManyBalls(_firstPlayer, 3);
        ScoreManyBalls(_secondPlayer, 3);
    }

    private void ScoreManyBalls(Player player, int count)
    {
        for (int i = 0; i < count; i++)
        {
            _game.ScoreBall(player);
        }
    }
}