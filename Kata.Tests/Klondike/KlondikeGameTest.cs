using Kata.Klondike;

namespace Kata.Tests.Klondike;

public class KlondikeGameTest
{
    private readonly KlondikeGame _game;

    public KlondikeGameTest()
    {
        _game = new KlondikeGame();
    }

    [Fact]
    public void TestGutterGame()
    {
        Assert.Equal(Status.New, _game.Status);
    }

    [Fact]
    public void TestStartNew()
    {
        _game.StartNew();

        Assert.Equal(Status.Playing, _game.Status);
    }

    [Fact]
    public void TestHasMoves()
    {
        _game.StartNew();

        Assert.True(_game.HasMoves());
    }

    /*[Fact]
    public void TestNoMoves()
    {
        _game.StartNew(new Board());
        
        Assert.False(_game.HasMoves());
    }*/
}