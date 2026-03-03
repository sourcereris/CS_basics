// This program will calculate bezier curve from given points into list

using System.Numerics;

List<Vector2> bezierPoints = new();

Vector2[] init = 
{
    new Vector2(1, 1),
    new Vector2(2, 3),
    new Vector2(4, 2),
    new Vector2(5, 3)
};

for (int i = 0; i <= 80; i++) 
{
    float dt = i / 80f;
    Bezier(dt);
}

Console.WriteLine("Bezier Points:");
foreach (var point in bezierPoints)
{
    Console.WriteLine($"{point.X:F2}, {point.Y:F2}");
}

void Bezier(float t)
{
    float tmp = 1 - t;

    float p1 = tmp * tmp * tmp;
    float p2 = 3* tmp * tmp * t;
    float p3 = 3 * tmp * t * t;
    float p4 = t * t * t;

    Vector2 point = init[0] * p1 + init[1] * p2 + init[2] * p3 + init[3] * p4;
    bezierPoints.Add(point);
}