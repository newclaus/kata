using System.Reflection;
using Xunit.Sdk;

namespace Kata.Tests;

public class TicTacToeGameTest
{
    [Theory]
    [BoardData]
    public void TestGutterGame(int exprectedResult, int[,] board)
    {
        var game = new TicTacToeGame();

        Assert.Equal(exprectedResult, game.IsSolved(board));
    }

    private class BoardDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[]
            {
                -1, 
                new[,]
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 0 }
                }
            };
            
            yield return new object[]
            {
                1, 
                new[,]
                {
                    { 2, 1, 0 },
                    { 2, 1, 0 },
                    { 0, 1, 0 }
                }
            };
            
            yield return new object[]
            {
                1, 
                new[,]
                {
                    { 2, 0, 1 },
                    { 2, 0, 1 },
                    { 0, 0, 1 }
                }
            };
            
            yield return new object[]
            {
                1, 
                new[,]
                {
                    { 2, 2, 0 },
                    { 1, 1, 1 },
                    { 0, 0, 0 }
                }
            };
            
            yield return new object[]
            {
                1, 
                new[,]
                {
                    { 1, 2, 0 },
                    { 2, 1, 0 },
                    { 0, 0, 1 }
                }
            };
            
            yield return new object[]
            {
                2, 
                new[,]
                {
                    { 1, 0, 2 },
                    { 1, 2, 0 },
                    { 2, 1, 1 }
                }
            };
            
            yield return new object[]
            {
                0, 
                new[,]
                {
                    { 1, 2, 1 },
                    { 2, 1, 1 },
                    { 2, 1, 2 }
                }
            };
        }
    }
}