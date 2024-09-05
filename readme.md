# LeoTurtle

Allows students to control a turtle, which can primarily only move forward and turn.
This turtle leaves track in the sand, and by looking at this track they can draw simple shapes.

![Screenshot](https://raw.githubusercontent.com/markushaslinger/leo_turtle/master/pics/screenshot.png)

## Turtle

The `Turtle` has the following capabilities:

- Can move forward a certain distance in meters
  - While moving forward, a line is drawn
- Can turn in degrees euler 
  - Positive values turn right
  - Negative values turn left
- Can teleport itself to a specific position
  - No line is drawn when teleporting
- Knows how many meters it has traveled
- Will stay within the boundaries of the 'beach'

### Smart Turtle

The `SmartTurtle` went to HTL Leonding and thus became the smarter cousin of `Turtle` with the following _additional_ capabilities:

- Knows where it is and can report its current position
- Can _look at_ a certain point on the beach
  - => turns to face that point automatically

## Sample Usage

```csharp
using LeoTurtle;

const int Length = 100;
const int Width = 80;
const string Basic = "1";
const string Smart = "2";

Console.Write($"Run Basic ({Basic}) or Smart ({Smart}) demo? ");
var choice = Console.ReadLine();
switch (choice)
{
    case Basic:
    {
        Beach.Prepare<Turtle>(WalkPath, Length, Width);
        break;
    }
    case Smart:
    {
        Beach.Prepare<SmartTurtle>(WalkSmartPath, Length, Width);
        break;
    }
    default:
    {
        Console.WriteLine("Unknown option");
        break;
    }
}

return;

static void WalkPath(Turtle turtle)
{
    turtle.Teleport(10, 5);
    turtle.MoveForward(30);
    turtle.Turn(180);
    turtle.MoveForward(15);
    turtle.Turn(-90);
    turtle.MoveForward(15);
    turtle.Turn(-90);
    turtle.MoveForward(15);
    turtle.Turn(180);
    turtle.MoveForward(30);
    
    turtle.Teleport(30, 35);
    turtle.Turn(90);
    turtle.MoveForward(15);
    turtle.Turn(180);
    turtle.MoveForward(15/2D);
    turtle.Turn(-90);
    turtle.MoveForward(30);
    
    turtle.Teleport(50, 35, false);
    turtle.MoveForward(30);
    turtle.Turn(-90);
    turtle.MoveForward(15);
}

static void WalkSmartPath(SmartTurtle turtle)
{
    const double Distance = 23.41;
    var center = new Point(Length / 2D, Width / 2D);
    var starPoint1 = GetPointByOffset(center, 0, 30);
    var starPoint2 = GetPointByOffset(center, 8, 8);
    var starPoint3 = GetPointByOffset(center, 30, 0);
    var starPoint4 = GetPointByOffset(center, 8, -8);
    var starPoint5 = GetPointByOffset(center, 0, -30);
    var starPoint6 = GetPointByOffset(center, -8, -8);
    var starPoint7 = GetPointByOffset(center, -30, 0);
    var starPoint8 = GetPointByOffset(center, -8, 8);
    
    turtle.Teleport(starPoint1.X, starPoint1.Y, resetRotation: true);
    turtle.LookAt(starPoint2.X, starPoint2.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint3.X, starPoint3.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint4.X, starPoint4.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint5.X, starPoint5.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint6.X, starPoint6.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint7.X, starPoint7.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint8.X, starPoint8.Y);
    turtle.MoveForward(Distance);
    turtle.LookAt(starPoint1.X, starPoint1.Y);
    turtle.MoveForward(Distance);
    
    Console.WriteLine($"Turtle has moved: {turtle.TravelDistanceMeters:F1} meters");

    static Point GetPointByOffset(Point origin, double xOffset, double yOffset) 
        => new(origin.X + xOffset, origin.Y + yOffset);
}

readonly record struct Point(double X, double Y);
```

> I recommend providing the `Beach.Prepare<T>` call for the students and only letting them work within a method similar to `WalkPath` (from which they may call other methods, of course).

## License

- The source code is licensed under the MIT License.
- The turtle images are licensed under CC BY-SA 3.0 and CC BY-SA 4.0.

See [LICENSE](./LICENSE) for more information.