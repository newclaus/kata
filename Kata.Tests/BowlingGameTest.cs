namespace Kata.Tests;

public class BowlingGameTest
{
    private readonly BowlingGame _bowlingGame;
    
    public BowlingGameTest()
    {
        _bowlingGame = new BowlingGame();
    }

    private void RollMany(int n, int pins)
    {
        for (int i = 0; i < n; i++)
        {
            _bowlingGame.Roll(pins);
        }
    }

    [Fact]
    public void TestGutterGame()
    {
        RollMany(20, 0);

        Assert.Equal(0, _bowlingGame.Score());
    }

    [Fact]
    public void TestAllOnes()
    {
        RollMany(20, 1);

        Assert.Equal(20, _bowlingGame.Score());
    }

    [Fact]
    public void TestOneSpare()
    {
        RollSpare();
        _bowlingGame.Roll(3);
        RollMany(17, 0);
        
        Assert.Equal(16, _bowlingGame.Score());
    }

    [Fact]
    public void TestOneStrike()
    {
        RollStrike();
        _bowlingGame.Roll(3);
        _bowlingGame.Roll(4);
        RollMany(16, 0);
        
        Assert.Equal(24, _bowlingGame.Score());
    }

    [Fact]
    public void TestAllStrikes()
    {
        RollMany(20, 5);
        _bowlingGame.Roll(10);
        
        Assert.Equal(155, _bowlingGame.Score());
    }

    [Fact]
    public void TestPerfectGame()
    {
        RollMany(12, 10);
        
        Assert.Equal(300, _bowlingGame.Score());
    }

    private void RollStrike()
    {
        _bowlingGame.Roll(10);
    }

    private void RollSpare()
    {
        _bowlingGame.Roll(5);
        _bowlingGame.Roll(5);
    }
}