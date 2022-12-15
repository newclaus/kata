using System.Drawing;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace Kata.Tests;

public class HashiGameTest
{
    [Theory]
    [DataSet]
    public void TestGutterGame(bool expectedResult, Puzzle puzzle)
    {
        var game = new HashiGame(puzzle);

        Assert.Equal(expectedResult, game.Check());
    }

    private class DataSetAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return CreateSuccessfulSet(new PuzzleBuilder()
                .AddIsland(At(0, 0), 0));

            yield return CreateSuccessfulSet(new PuzzleBuilder()
                .AddIsland(At(0, 0), 1)
                .AddIsland(At(0, 1), 1)
                .AddBridge(0, 1));

            yield return CreateSuccessfulSet(new PuzzleBuilder()
                .AddIsland(At(0, 0), 2) // 0
                .AddIsland(At(0, 1), 3) // 1
                .AddIsland(At(0, 3), 4) // 2
                .AddIsland(At(0, 5), 2) // 3

                .AddIsland(At(1, 6), 2) // 4

                .AddIsland(At(2, 0), 1) // 5
                .AddIsland(At(2, 1), 1) // 6
                .AddIsland(At(2, 4), 1) // 7
                .AddIsland(At(2, 5), 3) // 8
                .AddIsland(At(2, 6), 3) // 9

                .AddIsland(At(3, 0), 2) // 10
                .AddIsland(At(3, 3), 8) // 11
                .AddIsland(At(3, 5), 5) // 12
                .AddIsland(At(3, 6), 2) // 13

                .AddIsland(At(4, 0), 3) // 14
                .AddIsland(At(4, 2), 3) // 15
                .AddIsland(At(4, 6), 1) // 16

                .AddIsland(At(5, 2), 2) // 17
                .AddIsland(At(5, 5), 3) // 18
                .AddIsland(At(5, 6), 4) // 19

                .AddIsland(At(6, 0), 3) // 20
                .AddIsland(At(6, 3), 3) // 21
                .AddIsland(At(6, 4), 1) // 22
                .AddIsland(At(6, 6), 2) // 23

                .AddBridge(0, 1).AddBridge(0, 5)
                .AddBridge(1, 2).AddBridge(1, 6)
                .AddBridge(2, 3).AddBridge(2, 11).AddBridge(2, 11)
                .AddBridge(3, 8)

                .AddBridge(4, 9).AddBridge(4, 9)

                .AddBridge(7, 8)
                .AddBridge(8, 12)
                .AddBridge(9, 13)

                .AddBridge(10, 11).AddBridge(10, 11)
                .AddBridge(11, 12).AddBridge(11, 12).AddBridge(11, 21).AddBridge(11, 21)
                .AddBridge(12, 13).AddBridge(12, 18)

                .AddBridge(14, 15).AddBridge(14, 20).AddBridge(14, 20)
                .AddBridge(15, 17).AddBridge(15, 17)
                .AddBridge(16, 19)

                .AddBridge(18, 19).AddBridge(18, 19)
                .AddBridge(19, 23)

                .AddBridge(20, 21)
                .AddBridge(22, 23)
            );
            

            yield return CreateUnsuccessfulSet(new PuzzleBuilder()
                .AddIsland(At(0, 0), 1));

            yield return CreateUnsuccessfulSet(new PuzzleBuilder()
                .AddIsland(At(2, 0), 1)
                .AddIsland(At(2, 4), 1)
                .AddBridge(0, 1)
                .AddIsland(At(0, 2), 1)
                .AddIsland(At(4, 2), 1)
                .AddBridge(2, 3));
        }

        private static object[] CreateSuccessfulSet(PuzzleBuilder puzzleBuilder)
        {
            return CreateSet(true, puzzleBuilder);
        }

        private static object[] CreateUnsuccessfulSet(PuzzleBuilder puzzleBuilder)
        {
            return CreateSet(false, puzzleBuilder);
        }

        private static object[] CreateSet(bool expectedResult, PuzzleBuilder puzzleBuilder)
        {
            return new object[] { expectedResult, puzzleBuilder.BuildPuzzle() };
        }

        private static Point At(int x, int y)
        {
            return new Point(x, y);
        }

        private class PuzzleBuilder
        {
            private readonly Puzzle _puzzle = new TestPuzzle();
            private readonly List<Island> _islands = new();
            
            public PuzzleBuilder AddIsland(Point position, int bridgeCount)
            {
                var island = new Island(position, bridgeCount);
                
                _puzzle.AddVertex(island);
                _islands.Add(island);

                return this;
            }
            
            public PuzzleBuilder AddBridge(int sourceIslandId, int targetIslandId)
            {
                var bridge = new Bridge(_islands[sourceIslandId], _islands[targetIslandId]);
                
                _puzzle.AddEdge(bridge);

                return this;
            }
            
            public Puzzle BuildPuzzle()
            {
                return _puzzle;
            }
        }
    }

    private class TestPuzzle : Puzzle
    {
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('[');
        
            bool first = true;

            foreach (var vertex in Vertices)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    stringBuilder.Append(", ");
                }
            
                stringBuilder.Append(vertex.BridgeCount);
            }

            stringBuilder.Append(']');
        
            return stringBuilder.ToString();
        }
    }
}