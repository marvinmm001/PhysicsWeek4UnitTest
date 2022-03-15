using System;
using GXPEngine;
using System.Drawing;
using System.Threading;

public class MyGame : Game
{
	/// <summary>
	/// Returns true if a and b are within [errorMargin] of each other
	/// </summary>
	static bool Approximate(float a, float b, float errorMargin = 0.01f)
	{
		return (Mathf.Abs(a - b) < errorMargin);
	}

    static bool Approximate(Vec2 v1, Vec2 v2, float errorMargin = 0.01f)
    {
		//return (Mathf.Abs(v1.x - v2.x) < errorMargin && Mathf.Abs(v1.y - v2.y) < errorMargin);
		bool x = Approximate(v1.x, v2.x, errorMargin);
		bool y = Approximate(v1.y, v2.y, errorMargin);
		return (x && y);
    }

    static void DoTests()
	{
		// unit test for Length:
		Vec2 v1 = new Vec2(3, 4);

		Console.WriteLine("Length ok? {0}", v1.Length() == 5);
		Console.WriteLine("Length = {0} (should be 5)", v1.Length());

		Vec2 o = new Vec2(2, 5);
		Console.WriteLine("Length ok? {0}", Approximate(o.Length(), 5.3851f));
		Console.WriteLine("Length = {0} (should be 5.3851)", o.Length());

		// unit test for Normalized:
		Vec2 n = v1.Normalized();
		Console.WriteLine("Normalized ok? {0}", (n.x == 0.6f && n.y == 0.8f && v1.x == 3 && v1.y == 4));
		Console.WriteLine("n={0} (should be (0.6,0.8))  myVec={1}  (should be (3,4))", n, v1);

		//unit test for Normalize:
		Vec2 m = new Vec2(-3.5f, -4.5f);
		m.Normalize();
		Console.WriteLine("Normalize ok? {0}", Approximate(m.x, -0.614f) && Approximate(m.y, -0.789f));
		Console.WriteLine("m = {0} (should be (-0.614, -0.789))", m);

		Vec2 p = new Vec2(3.5f, 4.5f);
        Console.WriteLine(p.Length());
		p.Normalize();
		Console.WriteLine(p.Length());
		Console.WriteLine(p);
        Console.WriteLine("Normalize ok? {0}", Approximate(p.Length(), 1));

		// unit test for operators:
		Vec2 sum = v1 + n * 2;
		Console.WriteLine("Sum ok? {0}", (sum.x == 4.2f && sum.y == 5.6f));
		Console.WriteLine("Sum = {0} should be (4.2, 5.6)", sum);

		Vec2 sub = v1 - n;
		Console.WriteLine("Sub ok? {0}", (sub.x == 2.4f && sub.y == 3.2f));
		Console.WriteLine("Sub = {0} should be (2.4, 3.2)", sub);

		Vec2 scale = v1 * 2;
		Console.WriteLine("Scale ok? {0}", (scale.x == 6 && scale.y == 8));
		Console.WriteLine("Scale = {0} should be (6, 8)");

		// unit test for SetXY
		Vec2 newVec = new Vec2(2, 2);
		newVec.SetXY(4, 5);
		Console.WriteLine("SetXY ok? {0}", (newVec.x == 4 && newVec.y == 5));
		Console.WriteLine("SetXy = {0} should be (4,5)");
        Console.WriteLine("\n");

		//-------------------------------------Trigonometry_&_Rotation------------------------------------------//
		Console.WriteLine("is Deg2Rad ok? " + Approximate(Vec2.Deg2Rad(-324.67f), -5.67f));
		Console.WriteLine("is Rad2Deg ok? " + Approximate(Vec2.Rad2Deg(57.4f), 3288.78f));

		Vec2 GetUnitVectorRadAnsw = new Vec2(0.28f, -0.96f);
		Console.WriteLine("is GetUnitVectorRad ok? " + Approximate(Vec2.GetUnitVectorRad(5), GetUnitVectorRadAnsw));

		Vec2 GetUnitVectorDegAnsw = new Vec2(0.84f, -0.55f);
		Console.WriteLine("is GetUnitVectorDeg ok? " + Approximate(Vec2.GetUnitVectorDeg(-33.3f), GetUnitVectorDegAnsw));

		Console.WriteLine("is RandomUnitVector ok? " + Vec2.RandomUnitVector());
        Console.WriteLine("\n");

		Vec2 SetAngleRadAnsw = new Vec2(7,-8);
		SetAngleRadAnsw.SetAngleRadians(5.6f);
        Console.WriteLine("is SetAngleRadians ok? (Should be (8.2443,-6.7104): " + (Approximate(SetAngleRadAnsw.x, 8.24f) && Approximate(SetAngleRadAnsw.y,-6.71f)) ); //FALSE!!!

		Vec2 SetAngleDegAnsw = new Vec2(2, 3);
		SetAngleDegAnsw.SetAngleDegrees(66.8f);
        Console.WriteLine("is SetAngleDegrees ok? (should be (1.4203,3.3139)): " + (Approximate(SetAngleDegAnsw.x, 1.42f) && Approximate(SetAngleDegAnsw.y, 3.31f)) ); //FALSE!!!

		Vec2 GetAngleRadAnsw = new Vec2(3, 4);
        Console.WriteLine("is GetAngleRadians ok? should be (0.927295218): " + Approximate(GetAngleRadAnsw.GetAngleRadians(),0.93f) ); 

		Vec2 GetAngleDegAnsw = new Vec2(6.7f, -8.6f);
        Console.WriteLine("is GetAngleDegrees ok? should be (-52.0789216046): " + Approximate(GetAngleDegAnsw.GetAngleDegrees(), -52.08f) );
        Console.WriteLine("\n");

		Vec2 RotateRadAnsw = new Vec2(-2, 3);
		RotateRadAnsw.RotateRadians(Mathf.PI / 8);
		Console.WriteLine("is RotateRadians ok? (should be (-2.99580936212, 2.0062717328)): " + (Approximate(RotateRadAnsw.x, -2.99f) && Approximate(RotateRadAnsw.y, 2.01f)) );

		Vec2 RotateDegAnsw = new Vec2(-2, 3);
		RotateDegAnsw.RotateDegrees(225);
		Console.WriteLine("is RotateDegrees ok? (should be (3.53553390593, -0.70710678119)): " + (Approximate(RotateDegAnsw.x, 3.54f) && Approximate(RotateDegAnsw.y, -0.71f)));

		Vec2 RotateAroundRad = new Vec2(4, -7);
		Vec2 RotatePointRad = new Vec2(2, 1);
		RotateAroundRad.RotateAroundRadians(RotatePointRad, Mathf.PI / 2);
		Console.WriteLine("is RotateAroundRadian ok? should be (10,3): " + (Approximate(RotateAroundRad.x, 10) && Approximate(RotateAroundRad.y, 3)) );

		Vec2 RotateAroundDeg = new Vec2(4, -7);
		Vec2 RotatePointDeg = new Vec2(2, 1);
		RotateAroundDeg.RotateAroundDegrees(RotatePointDeg, 90);
		Console.WriteLine("is RotateAroundDegrees ok? should be (10,3): " + (Approximate(RotateAroundRad.x, 10) && Approximate(RotateAroundRad.y, 3)) );
	}

	static void Main() 
	{
		DoTests();
        new MyGame().Start();
    }

	Ball _ball;

	EasyDraw _text;

	public MyGame () : base(800, 600, false,false)
	{
	/*	_ball = new Ball (30, new Vec2 (width / 2, height / 2));
		AddChild (_ball);

		_text = new EasyDraw (200,25);
		_text.TextAlign (CenterMode.Min, CenterMode.Min);
		AddChild (_text);*/

		
	}

	
	void Update () 
	{
		/*_ball.Step ();

		_text.Clear (Color.Transparent);
		_text.Text("Velocity: "+_ball.velocity, 0, 0);*/

		
		//Thread.Sleep(500);
	}
}

