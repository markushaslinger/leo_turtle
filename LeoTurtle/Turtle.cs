using Avalonia;
using SimpleDrawing;

namespace LeoTurtle;

/// <summary>
///     Represents a turtle that can move and turn
/// </summary>
public class Turtle
{
    private const double DefaultRotation = 0D;
    private const double MaxDegrees = 360D;
    private const double TurtleImageScaleFactor = 8D;
    private const double TurtleImageWidth = 166D / TurtleImageScaleFactor;
    private const double TurtleImageHeight = 256D / TurtleImageScaleFactor;
    private int _maxX;
    private int _maxY;
    
    /// <summary>
    ///     The current rotation of the turtle in degrees euler
    /// </summary>
    protected double Rotation { get; private set; } = DefaultRotation;
    
    /// <summary>
    ///     The current position of the turtle in pixels
    /// </summary>
    protected Point CurrentPosition { get; private set; }
    
    /// <summary>
    ///     The total distance the turtle has traveled in meters.
    ///     Does not include the distance traveled during teleportation.
    /// </summary>
    public double TravelDistanceMeters { get; private set; }

    /// <summary>
    ///     The path to the image representing the turtle
    /// </summary>
    protected virtual string TurtleImageFileName => "turtle.png";

    /// <summary>
    ///     Orders the turtle to move forward by a certain distance.
    ///     If the distance is greater than the remaining space, the turtle will move as far as possible and then stop.
    /// </summary>
    /// <param name="meters">The distance to travel</param>
    public void MoveForward(double meters)
    {
        var distance = Beach.MeterToPixel(meters);

        var start = CurrentPosition;
        var end = CalcMovementEndPoint(distance);

        if (start == end)
        {
            return;
        }

        CurrentPosition = end;

        LeoCanvas.DrawLine(start, end);

        // has to be calculated instead of taking the passed distance,
        // because movement might have been limited to the boundaries of the canvas
        TravelDistanceMeters += GetPointDistanceInMeters(start, end);
    }

    /// <summary>
    ///     Orders the turtle to turn by a certain number of degrees.
    ///     Positive values turn the turtle clockwise, negative values turn it counterclockwise.
    /// </summary>
    /// <param name="degrees">Euler angles to turn</param>
    public void Turn(double degrees)
    {
        degrees %= MaxDegrees;
        Rotation += degrees;
        switch (Rotation)
        {
            case >= MaxDegrees:
            {
                Rotation -= MaxDegrees;

                break;
            }
            case < 0:
            {
                Rotation += MaxDegrees;

                break;
            }
        }
    }

    /// <summary>
    ///     Teleports the turtle to a new position.
    ///     This movement does not count towards the total distance traveled.
    /// </summary>
    /// <param name="newHorizontalPosition">The new horizontal position in meters</param>
    /// <param name="newVerticalPosition">The new vertical position in meters</param>
    /// <param name="resetRotation">
    ///     If true, the rotation will be reset to 0 during the teleportation; if false, the previous rotation is maintained
    /// </param>
    public void Teleport(double newHorizontalPosition, double newVerticalPosition, bool resetRotation = true)
    {
        var pixelX = Beach.MeterToPixel(newHorizontalPosition);
        var pixelY = _maxY - Beach.MeterToPixel(newVerticalPosition);

        var newPoint = LimitToBoundaries(new Point(pixelX, pixelY));

        CurrentPosition = newPoint;
        if (resetRotation)
        {
            Rotation = DefaultRotation;
        }
    }

    internal void DrawSelf()
    {
        const double HalfWidth = TurtleImageWidth / 2D;
        const double HalfHeight = TurtleImageHeight / 2D;

        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", TurtleImageFileName);

        var (x, y) = CurrentPosition;
        var leftUpper = new Point(x - HalfWidth, y - HalfHeight);
        var rightLower = new Point(x + HalfWidth, y + HalfHeight);

        LeoCanvas.DrawImageAtLocation(imagePath, leftUpper, rightLower, Rotation);
    }

    internal void SetBoundaries(int x, int y)
    {
        _maxX = x;
        _maxY = y;
    }

    private static double GetPointDistanceInMeters(Point start, Point end)
    {
        var deltaX = end.X - start.X;
        var deltaY = end.Y - start.Y;
        var pixelDistance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

        return Beach.PixelToMeter(pixelDistance);
    }

    private Point CalcMovementEndPoint(double distance)
    {
        return Rotation switch
               {
                   <= 90  => CalcPoint(CurrentPosition, distance, -distance, 90 - Rotation),
                   <= 180 => CalcPoint(CurrentPosition, distance, distance, 90 - (180 - Rotation)),
                   <= 270 => CalcPoint(CurrentPosition, -distance, distance, 270 - Rotation),
                   _      => CalcPoint(CurrentPosition, -distance, -distance, Rotation - 270)
               };
    }

    private Point CalcPoint(Point origin, double distanceX, double distanceY, double angleDegrees)
    {
        var radians = angleDegrees * Math.PI / 180;
        var xDelta = distanceX * Math.Cos(radians);
        var yDelta = distanceY * Math.Sin(radians);
        var point = new Point(origin.X + xDelta, origin.Y + yDelta);

        return LimitToBoundaries(point);
    }

    private Point LimitToBoundaries(Point position)
    {
        const int Min = 0;

        if (position.X < Min)
        {
            position = new Point(Min, position.Y);
        }
        else if (position.X > _maxX)
        {
            position = new Point(_maxX, position.Y);
        }

        if (position.Y < Min)
        {
            position = new Point(position.X, Min);
        }
        else if (position.Y > _maxY)
        {
            position = new Point(position.X, _maxY);
        }

        return position;
    }
}
