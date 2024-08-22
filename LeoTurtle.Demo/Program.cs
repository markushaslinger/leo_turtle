using LeoTurtle;

const int Length = 80;
const int Width = 40;
const string Basic = "1";
const string Smart = "2";

Console.Write($"Run Basic ({Basic}) or Smart ({Smart}) demo? ");
var choice = Console.ReadLine();
switch (choice)
{
    case Basic:
    {
        Beach.Prepare<Turtle>(WalkPath, 80, 40);
        break;
    }
    case Smart:
    {
        Beach.Prepare<SmartTurtle>(WalkSmartPath, 80, 40);
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
    turtle.Teleport(Length / 2D, Width / 2D);
    
    turtle.LookAt(50, 30);
    turtle.MoveForward(15);
    turtle.Turn(45);
    turtle.MoveForward(5);
    turtle.LookAt(0,0);
    turtle.MoveForward(30);
    
    Console.WriteLine($"Turtle has moved: {turtle.TravelDistanceMeters:F1} meters");
}