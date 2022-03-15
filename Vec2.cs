using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}
	public override string ToString()
	{
		return String.Format("({0},{1})", x, y);
	}

	public float Length() 
	{
		//Console.WriteLine("this.x = {0}, this.y = {1}", this.x, this.y);
		return Mathf.Sqrt(Mathf.Pow(this.x, 2) + Mathf.Pow(this.y, 2));
	}

	/// <summary>
	/// Normalizes the current vector
	/// </summary>
	public void Normalize()
	{ 
		float length = Length();

		if (length == 0) return;
		this.x /= length;
		this.y /= length;
	}

	/// <summary>
	/// Returns a normalized version of this vector, without modifying it
	/// </summary>
	/// <returns></returns>
	public Vec2 Normalized()
	{
		float length = Length();

		if (length == 0) return new Vec2(0, 0);
		return new Vec2(this.x/length, this.y/length);
	}

	public void SetXY(float givenX, float givenY)
    {
		this.x = givenX;
		this.y = givenY;
    }
	/*public void SetXY(float pPos)
	{
		SetXY(pPos, pPos);
	}
	public void SetXY(Vec2 pPos)
	{
		SetXY(pPos.x, pPos.y);
	}*/

	public void SetLength(float scale)
	{
		this.Normalize();
		this.x *= scale;
		this.y *= scale;
	}

	public static Vec2 operator+ (Vec2 left, Vec2 right) 
	{
		return new Vec2(left.x+right.x, left.y+right.y);
		//will add the x and y of the left vector with the right one
	}

	public static Vec2 operator- (Vec2 left, Vec2 right)
	{
		return new Vec2(left.x-right.x, left.y-right.y);
	}

	public static Vec2 operator* (Vec2 myVec, float multiplyBy)
	{
		return new Vec2(myVec.x * multiplyBy, myVec.y * multiplyBy);
	}

	/* Trigonometry_&_Rotation */
	public static float Deg2Rad(float degrees) 
	{
		return degrees * Mathf.PI / 180;
	}
	public static float Rad2Deg(float radians)
    {
		return radians * 180 / Mathf.PI;
    }

	/// <summary>
	/// returns a new vector pointing in the given direction in radians/degrees
	/// </summary>
	public static Vec2 GetUnitVectorRad(float rads)
	{
		return new Vec2(Mathf.Cos(rads), Mathf.Sin(rads));
	}
	public static Vec2 GetUnitVectorDeg(float degs)
    {
		return GetUnitVectorRad(Deg2Rad(degs));
    }
	public static Vec2 RandomUnitVector()
	{ 
		float random = Utils.Random(0, Mathf.PI * 2);
		return GetUnitVectorRad(random);
	}

	/// <summary>
	/// set vector angle to the given direction in radians/degrees (length doesn’t change)
	/// </summary>
	public void SetAngleRadians(float rads)
	{
        Vec2 angle = GetUnitVectorRad(rads);
        angle *= this.Length();
        this.SetXY(angle.x, angle.y);

        /*float length = this.Length();
		this = GetUnitVectorRad(rads); //returns a new Vec2 but a unit vector
		this *= length;*/
    }
    public void SetAngleDegrees(float degs)
	{
		SetAngleRadians(Deg2Rad(degs));
	}

	public float GetAngleRadians()
	{
		return Mathf.Atan2(this.y, this.x);
	}
	public float GetAngleDegrees()
	{
		return Rad2Deg(GetAngleRadians()); //yesterday's mistake was Deg2Rad
	}

	public void RotateRadians(float rads)
	{
		this.SetXY(Mathf.Cos(rads) * this.x - Mathf.Sin(rads) * this.y, Mathf.Cos(rads) * this.y + Mathf.Sin(rads) * this.x);
	}
	public void RotateDegrees(float degs)
	{
		RotateRadians(Deg2Rad(degs));	
	}
	public void RotateAroundRadians(Vec2 vec, float rads)
	{
		this -= vec;
		this.RotateRadians(rads);
		this += vec;
	}
	public void RotateAroundDegrees(Vec2 vec, float degs)
	{
		RotateAroundRadians(vec, Deg2Rad(degs));
	}
}

