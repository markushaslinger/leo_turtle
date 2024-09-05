namespace LeoTurtle;

/// <summary>
///     A smarter turtle that can look at specific points
/// </summary>
public sealed class SmartTurtle : Turtle
{
    /// <inheritdoc/>
    protected override string TurtleImageFileName => "smart_turtle.png";

    /// <summary>
    ///     Gets the current position of the turtle in meters
    /// </summary>
    /// <returns>An array containing first the horizontal and second the vertical position</returns>
    public double[] GetPosition()
    {
        var (x, y) = CurrentPosition;
        
        var horizontal = Beach.PixelToMeter(x);
        var vertical = Beach.PixelToMeter(InvertY(y));

        return [horizontal, vertical];
    }

    /// <summary>
    ///     Tells the turtle to look at a specific point
    /// </summary>
    /// <param name="horizontalPosition">X coordinate of the point to look at in meters</param>
    /// <param name="verticalPosition">Y coordinate of the point to look at in meters</param>
    public void LookAt(double horizontalPosition, double verticalPosition)
    {
        var x = Beach.MeterToPixel(horizontalPosition);
        var y = InvertY(Beach.MeterToPixel(verticalPosition));
        var (currentX, currentY) = CurrentPosition;

        var deltaX = x - currentX;
        var deltaY = y - currentY;
        var angleRad = Math.Atan2(deltaY, deltaX);

        var reqRot = 90D + angleRad / Math.PI * 180D;
        var deltaRot = reqRot - Rotation;

        Turn(deltaRot);
    }
}
