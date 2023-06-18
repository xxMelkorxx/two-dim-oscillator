namespace two_dim_oscillator;

public class RungeKutta
{
    /// <summary>
    /// Коэффициент жётскости пружинок.
    /// </summary>
    public double K { get; set; }

    /// <summary>
    /// Масса тела.
    /// </summary>
    public double M { get; set; }

    /// <summary>
    /// Длина пружинки.
    /// </summary>
    public double L { get; set; }
    
    /// <summary>
    /// Система дифференциальных уравнений системы.
    /// </summary>
    /// <param name="p">Координата</param>
    /// <param name="i"></param>
    /// <returns></returns>
    private double DiffFunc(PointD p, int i)
    {
        var l1 = double.Sqrt((L - p.X) * (L - p.X) + p.Y * p.Y);
        var l2 = double.Sqrt((L + p.X) * (L + p.X) + p.Y * p.Y);
        var cosA = (L - p.X) / l1;
        var cosB = (L + p.X) / l2;
        var sinA = p.Y / l1;
        var sinB = p.Y / l2;
        var f1 = K * (l1 - L);
        var f2 = K * (l2 - L);

        return i switch
        {
            0 => (1 / M) * (f1 * cosA - f2 * cosB),
            1 => (1 / M) * (-f1 * sinA - f2 * sinB),
            _ => 0
        };
    }

    /// <summary>
    /// Решение системы из 2-х дифференциальных уравнений методом Рунге-Кутты 4-ого порядка.
    /// </summary>
    /// <param name="currPos">Текущее координата тела.</param>
    /// <param name="currVel">Текущая скорость тела.</param>
    /// <param name="dt">Шаг по времени.</param>
    /// <param name="nextPos">Новая координата тела.</param>
    /// <param name="nextVel">Новая скорость тела.</param>
    public void NextStep(PointD currPos, PointD currVel, double dt, out PointD nextPos, out PointD nextVel)
    {
        var kx1 = DiffFunc(currPos, 0) * dt;
        var ky1 = DiffFunc(currPos, 1) * dt;
        var kx2 = DiffFunc(new PointD(currPos.X + currVel.X * dt * 0.5, currPos.Y + currVel.Y * dt * 0.5), 0) * dt;
        var ky2 = DiffFunc(new PointD(currPos.X + currVel.X * dt * 0.5, currPos.Y + currVel.Y * dt * 0.5), 1) * dt;
        var kx3 = DiffFunc(new PointD(currPos.X + currVel.X * dt * 0.5 + kx1 * dt * 0.25, currPos.Y + currVel.Y * dt * 0.5 + ky1 * dt * 0.25), 0) * dt;
        var ky3 = DiffFunc(new PointD(currPos.X + currVel.X * dt * 0.5 + kx1 * dt * 0.25, currPos.Y + currVel.Y * dt * 0.5 + ky1 * dt * 0.25), 1) * dt;
        var kx4 = DiffFunc(new PointD(currPos.X + currVel.X * dt * 0.5 + kx2 * 0.5, currPos.Y + currVel.Y * dt * 0.5 + ky2 * 0.5), 0) * dt;
        var ky4 = DiffFunc(new PointD(currPos.X + currVel.X * dt * 0.5 + kx2 * 0.5, currPos.Y + currVel.Y * dt * 0.5 + ky2 * 0.5), 1) * dt;

        nextPos = new PointD(currPos.X + currVel.X * dt + dt * (kx1 + kx2 + kx3) / 6, currPos.Y + currVel.Y * dt + dt * (ky1 + ky2 + ky3) / 6);
        nextVel = new PointD(currVel.X + (kx1 + 2 * kx2 + 2 * kx3 + kx4) / 6, currVel.Y + (ky1 + 2 * ky2 + 2 * ky3 + ky4) / 6);
    }
    
    /// <summary>
    /// Расчёт энергий системы.
    /// </summary>
    /// <param name="pos">Координата тела.</param>
    /// <param name="vel">Скорость тела.</param>
    /// <param name="pe1">Потенциальная энергия 1-ой пружины.</param>
    /// <param name="pe2">Потенциальная энергия 2-ой пружины.</param>
    /// <param name="ke">Кинетическая энергия тела.</param>
    /// <param name="fe">Полная энергия системы.</param>
    public void GetEnergy(PointD pos, PointD vel, out double pe1, out double pe2, out double ke, out double fe)
    {
        var l1 = double.Sqrt((L - pos.X) * (L - pos.X) + pos.Y * pos.Y);
        var l2 = double.Sqrt((L + pos.X) * (L + pos.X) + pos.Y * pos.Y);
        
        pe1 = 0.5 * K * (l1 - L) * (l1 - L);
        pe2 = 0.5 * K * (l2 - L) * (l2 - L);
        ke = 0.5 * M * (vel.X * vel.X + vel.Y * vel.Y);
        fe = pe1 + pe2 + ke;
    }
}
