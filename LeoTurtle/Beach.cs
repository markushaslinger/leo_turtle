using SimpleDrawing;

namespace LeoTurtle;

/// <summary>
///     A nice beach of white sand and without obstacles upon which the turtle walks
/// </summary>
public static class Beach
{
    private const double MeterPixelFactor = 10D;
    
    /// <summary>
    ///     Prepares the beach for the turtle to walk on
    /// </summary>
    /// <param name="turtlePath">Method that describes the movement of the turtle</param>
    /// <param name="lengthMeters">The horizontal lenght of the beach</param>
    /// <param name="widthMeters">The vertical width of the beach</param>
    /// <typeparam name="T">Either a <see cref="Turtle"/> or <see cref="SmartTurtle"/> depending on assignment</typeparam>
    public static void Prepare<T>(TurtlePath<T> turtlePath, int lengthMeters, int widthMeters) where T : Turtle, new()
    {
        var width = ToPixel(lengthMeters);
        var height = ToPixel(widthMeters);
        
        var turtle = new T();
        turtle.SetBoundaries(width, height);

        LeoCanvas.Init(Execute, width, height, windowTitle: "Turtle Beach");

        return;

        void Execute()
        {
            turtlePath(turtle);
            turtle.DrawSelf();
            
            LeoCanvas.Render();
        }

        static int ToPixel(double meters) => (int) Math.Ceiling(MeterToPixel(meters));
    }

    internal static double MeterToPixel(double meters) => meters * MeterPixelFactor;
    internal static double PixelToMeter(double pixel) => pixel * (1D / MeterPixelFactor);
}

/// <summary>
///     Describes the path the provided turtle will walk on
/// </summary>
/// <typeparam name="T"></typeparam>
public delegate void TurtlePath<in T>(T turtle) where T : Turtle;
