using System.Drawing;
using QuikGraph;

namespace Kata;

public class HashiGame
{
    private readonly Puzzle _puzzle;

    public HashiGame(Puzzle puzzle)
    {
        _puzzle = puzzle;
    }

    public bool Check()
    {
        foreach (var island in _puzzle.Vertices)
        {
            if(_puzzle.AdjacentEdges(island).Count() != island.BridgeCount)
            {
                return false;
            }
        }
        
        foreach (var edge1 in _puzzle.Edges)
        foreach (var edge2 in _puzzle.Edges)
        {
            var a = edge1.Source.Position;
            var b = edge1.Target.Position;
            
            var p = edge2.Source.Position;
            var q = edge2.Target.Position;
            
            if(a.Y < p.Y && a.Y < q.Y &&
               b.Y > p.Y && b.Y > q.Y &&
               a.X > p.X && a.X < q.X &&
               b.X > p.X && b.X < q.X)
            {
                return false;
            }
        }

        return true;
    }
}

public class Island
{
    public Island(Point position, int bridgeCount)
    {
        Position = position;
        BridgeCount = bridgeCount;
    }

    public Point Position { get; }
    
    public int BridgeCount { get; }
}

public class Bridge : IEdge<Island>
{
    public Bridge(Island source, Island target)
    {
        Source = source;
        Target = target;
    }

    public Island Source { get; }

    public Island Target { get; }
}

public class Puzzle : UndirectedGraph<Island, Bridge>
{
    //
}