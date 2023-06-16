﻿using System;

namespace two_dim_oscillator;

public struct PointD
{
    public double X, Y;
    
    private const double Eps = 1e-10;

    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }

    public PointD ToPoint() => new(X, Y);

    public PointD Rotate(double angle) => new(X * Math.Cos(angle) - Y * Math.Sin(angle), X * Math.Sin(angle) + Y * Math.Cos(angle));

    public override bool Equals(object obj) => obj is PointD d && this == d;

    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

    public static bool operator ==(PointD a, PointD b) => a.X - Eps <= b.X && a.Y + Eps >= b.Y;

    public static bool operator !=(PointD a, PointD b) => !(a == b);
}