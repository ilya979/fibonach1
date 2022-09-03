using System.Diagnostics;

ulong N, res;
Stopwatch stopWatch;

Matrix22 M1 = new Matrix22();



Console.WriteLine("Fib(N);");
Console.Write("for N = ");

N = Convert.ToUInt64(Console.ReadLine());
stopWatch = new Stopwatch();


Console.Write("Умножение матриц: ");

stopWatch.Restart();
res = FibM(N);
stopWatch.Stop();

Console.WriteLine("Fib(" + N + ") = " + res + " (" + stopWatch.ElapsedMilliseconds + " msec.)");



Console.Write("Итерационный    : ");
stopWatch.Restart();
res = FibI(N);
stopWatch.Stop();

Console.WriteLine("Fib(" + N + ") = " + res + " (" + stopWatch.ElapsedMilliseconds + " msec.)");


Console.Write("Золотое сечение : ");
stopWatch.Restart();
try
{
    res = FibF(N);
    stopWatch.Stop();
    Console.WriteLine("Fib(" + N + ") = " + res + " (" + stopWatch.ElapsedMilliseconds + " msec.)");
}
catch
{
    stopWatch.Stop();
    Console.WriteLine("Ошибка переполнения");
}

Console.Write("Рекурсия        : ");
stopWatch = new Stopwatch();
stopWatch.Start();
res = FibR(N);
stopWatch.Stop();

Console.WriteLine("Fib(" + N + ") = " + res + " (" + stopWatch.ElapsedMilliseconds + " msec.)");



ulong FibR(ulong N)
{
    if (N == 0) return 0;
    if (N == 1) return 1;
    return FibR(N-1)+FibR(N-2);
}

ulong FibI(ulong N)
{
    ulong F0, F1, F2;
    if (N == 0) return 0;
    if (N == 1) return 1;

    F0 = 0;
    F1 = 1;
    for(ulong i=2; i<N+1; i++)
    {
        F2 = F1 + F0;
        F0 = F1;
        F1 = F2;
    }
    return F1;
}

ulong FibF(ulong N)
{
    double sq5 = Math.Sqrt(5);

    return Convert.ToUInt64(Math.Truncate(Pow2((sq5 + 1)/2, N)/ sq5 + 0.5));

}

ulong FibM(ulong N)
{
    Matrix22 M0 = new Matrix22();

    M0 = MatrixPow2(M0, N);

    return M0.a11;
}



double Pow2(double a, ulong p)
{
    ulong N = p;
    double d = a;
    double res = 1;
    while (N > 1)
    {
        N /= 2;
        d *= d;

        if (N % 2 == 1)
            res *= d;
    }
    if (p % 2 == 1) res *= a;
    return res;
}

Matrix22 MatrixPow2(Matrix22 a, ulong p)
{
    ulong N = p;
    Matrix22 d = a;
    Matrix22 res = new Matrix22();
    while (N > 1)
    {
        N /= 2;
        d = multiM(d, d);

        if (N % 2 == 1)
            res = multiM(res, d);
    }
    if (p % 2 == 1) res = multiM(res, a);
    return res;
}

Matrix22 multiM(Matrix22 M1, Matrix22 M2)
{
    Matrix22 result = new Matrix22();

    result.a00 = M1.a00 * M2.a00 + M1.a01 * M2.a10;
    result.a01 = M1.a00 * M2.a10 + M1.a01 * M2.a11;
    result.a10 = M1.a10 * M2.a00 + M1.a11 * M2.a10;
    result.a11 = M1.a10 * M2.a01 + M1.a11 * M2.a11;


    return result;
}




public class Matrix22
{
    public ulong a00, a01, a10, a11;

    public Matrix22()
    {
        a00 = 1;
        a01 = 1;
        a10 = 1;
        a11 = 0;
    }
}