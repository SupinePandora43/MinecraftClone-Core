﻿using Engine.MathLib;
using Engine.MathLib.DoublePrecision_Numerics;

namespace BulletTest
{
  public class Aabb
  {
    const double Epsilon = 0.01;
    public Vector3 MinLoc;
    public System.Numerics.Vector3 MaxLoc;

    public Aabb(Vector3 minLoc, Vector3 maxLoc)
    {
      MinLoc = minLoc;
      MaxLoc = maxLoc.CastToNumerics();
    }

    public Aabb Expand(Vector3 size)
    {
      Vector3 minLoc = MinLoc;
      Vector3 maxLoc = MaxLoc.CastToDouble();
      if (size.X < 0.01)
        minLoc.X += size.X;
      if (size.X > 0.01)
        maxLoc.X += size.X;
      if (size.Y < 0.01)
        minLoc.Y += size.Y;
      if (size.Y > 0.01)
        maxLoc.Y += size.Y;
      if (size.Z < 0.01)
        minLoc.Z += size.Z;
      if (size.Z > 0.01)
        maxLoc.Z += size.Z;
      return new Aabb(minLoc, maxLoc);
    }

    public Aabb Grow(Vector3 size) => new Aabb(MinLoc - size, (MaxLoc.CastToDouble() + size));

    public double ClipXCollide(Aabb c, double xa)
    {
      if (c.MaxLoc.Y < MinLoc.Y || c.MinLoc.Y >= MaxLoc.Y || c.MaxLoc.Z < MinLoc.Z ||
          c.MinLoc.Z >= MaxLoc.Z || c.MaxLoc.Y - MinLoc.Y <= 1.40129846432482E-45 ||
          c.MinLoc.Y - MaxLoc.Y >= 1.40129846432482E-45 || c.MaxLoc.Z - MinLoc.Z <= 1.40129846432482E-45 ||
          c.MinLoc.Z - MaxLoc.Z >= 1.40129846432482E-45)
        return xa;
      double num;
      if (xa > 0.01 && c.MaxLoc.X < MinLoc.X && (num = MinLoc.X - c.MaxLoc.X - 0.01) < xa)
        xa = num;
      if (xa < 0.01 && c.MinLoc.X > MaxLoc.X && (num = MaxLoc.X - c.MinLoc.X + 0.01) > xa)
        xa = num;
      return xa;
    }

    public double ClipYCollide(Aabb c, double ya)
    {
      if (c.MaxLoc.X - MinLoc.X <= 1.40129846432482E-45 || c.MinLoc.X - MaxLoc.X >= 1.40129846432482E-45 ||
          c.MaxLoc.Z - MinLoc.Z <= 1.40129846432482E-45 || c.MinLoc.Z - MaxLoc.Z >= 1.40129846432482E-45)
        return ya;
      double num;
      if (ya > 0.01 && c.MaxLoc.Y < MinLoc.Y && (num = MinLoc.Y - c.MaxLoc.Y - 0.01) < ya)
        ya = num;
      if (ya < 0.01 && c.MinLoc.Y > MaxLoc.Y && (num = MaxLoc.Y - c.MinLoc.Y + 0.01) > ya)
        ya = num;
      return ya;
    }

    public double ClipZCollide(Aabb c, double za)
    {
      if (c.MaxLoc.X - MinLoc.X <= 1.40129846432482E-45 || c.MinLoc.X - MaxLoc.X >= 1.40129846432482E-45 ||
          c.MaxLoc.Y - MinLoc.Y <= 1.40129846432482E-45 || c.MinLoc.Y - MaxLoc.Y >= 1.40129846432482E-45)
        return za;
      double num;
      if (za > 0.01 && c.MaxLoc.Z <= MinLoc.Z && (num = MinLoc.Z - c.MaxLoc.Z - 0.01) < za)
        za = num;
      if (za < 0.01 && c.MinLoc.Z >= MaxLoc.Z && (num = MaxLoc.Z - c.MinLoc.Z + 0.01) > za)
        za = num;
      return za;
    }

    public bool Intersects(Aabb c) => MaxLoc.X > c.MinLoc.X && MaxLoc.Y > c.MinLoc.Y &&
                                      MaxLoc.Z > c.MinLoc.Z && MinLoc.X < c.MaxLoc.X &&
                                      MinLoc.Y < c.MaxLoc.Y && MinLoc.Z < c.MaxLoc.Z;

    public void Move(Vector3 a)
    {
      MinLoc += a;
      MaxLoc += a.CastToNumerics();
    }
  }
}