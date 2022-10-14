using org.matheval;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace sistemi // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static void Tabella((int Left, int Top) tab)
        {

            Console.SetCursorPosition(3 + tab.Left, 8);
            var p = Console.GetCursorPosition();
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(p.Left, 0 + i + 8);
                Console.WriteLine("|");
            }
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(p.Left + 5, 0 + i + 8);
                Console.WriteLine(" ");
            }
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(p.Left + 8, 0 + i + 8);
                Console.WriteLine("|");
            }
        }
        private static void NumTab(int left, int top, float[] Dx, float[] Dy, float[] D)
        {

            Console.SetCursorPosition(4, top - 2);
            Console.WriteLine(Dx[0]);
            Console.SetCursorPosition(left, top);
            Console.WriteLine(Dx[3]);
            Console.SetCursorPosition(left - 4, top);
            Console.WriteLine(Dx[2]);
            Console.SetCursorPosition(left, top - 2);
            Console.WriteLine(Dx[1]);

            Console.SetCursorPosition(left * 2 + 1, top - 2);
            Console.WriteLine(Dy[0]);
            Console.SetCursorPosition(left * 2 + 1, top);
            Console.WriteLine(Dy[2]);
            Console.SetCursorPosition(left * 2 + 5, top);
            Console.WriteLine(Dy[3]);
            Console.SetCursorPosition(left * 2 + 5, top - 2);
            Console.WriteLine(Dy[1]);


            Console.SetCursorPosition(left * 2 + 13, top - 2);
            Console.WriteLine(D[0]);
            Console.SetCursorPosition(left * 2 + 13, top);
            Console.WriteLine(D[2]);
            Console.SetCursorPosition(left * 2 + 17, top);
            Console.WriteLine(D[3]);
            Console.SetCursorPosition(left * 2 + 17, top - 2);
            Console.WriteLine(D[1]);


        }
        private static string tt(string[] split, int bb, int scelta = 0)
        {
            var x = "*0";
            var y = "*0";
            int aa = 0;
            var ocultay = ".0";
            if (bb == 1)
            {
                aa = 1;
            }
            if (scelta == 0)
            {
                var ocultax = split[aa].Replace("x", x);
                ocultay = ocultax.Replace("y", y);

            }
            if (scelta == 1)
            {
                var ocultax = split[aa].Replace("x", "*1");
                ocultay = ocultax.Replace("y", y);

            }
            if (scelta == 2)
            {
                var ocultax = split[aa].Replace("x", x);
                ocultay = ocultax.Replace("y", "*1");


            }

            return ocultay;


        }
        private static async Task<double> yy(string sis11)
        {

            //  double tt = await CSharpScript.EvaluateAsync<double>(sis11,ScriptOptions.Default.WithImports("System.Math")); 
            Expression expression = new Expression(sis11);
            double tt = Convert.ToDouble(expression.Eval());

            return tt;

        }
        private static double[] tot(string[] split, int s)
        {
            var oo = tt(split, s, 0);
            var totN = yy(oo);
            var oo1 = tt(split, s, 1);
            var totX = yy(oo1);
            var oo2 = tt(split, s, 2);
            var totY = yy(oo2);
            double[] toTtot = new[] { Convert.ToDouble(totN.Result), Convert.ToDouble(totX.Result), Convert.ToDouble(totY.Result) };
            return toTtot;
        }

        static void Main(string[] args)
        {
            bool ff = false;
            do
            {
                Console.WriteLine("benvenuto nella app che risolve i sistemi con il metodo di Cramel e le equazioni di 2 grado.");
                Console.WriteLine("c per cramel");
                Console.WriteLine("e per le equazioni");
                var scelta = Console.ReadLine();
                while (scelta == "c")
                {
                    Console.Clear();
                    Console.WriteLine("inserisi il primo sistema in forma semplice->");
                    var sis1 = Console.ReadLine()!;
                    string pattern = @"(?<![\.[0-9])(?<Int>[0-9]+)(?![\.0-9])";
                    string substitution = @"${Int}.0";
                    RegexOptions options = RegexOptions.Multiline;
                    Regex regex = new Regex(pattern, options);
                    string sis1_1 = regex.Replace(sis1, substitution);
                    Console.WriteLine("inserisi il secondo sistema in forma semplice->");
                    var sis2 = Console.ReadLine()!;
                    string sis2_1 = regex.Replace(sis2, substitution);
                    var split = sis1_1.Split("=");
                    var split2 = sis2_1.Split("=");
                    var eq1 = tot(split, 0);
                    double n = eq1[0] * -1;
                    double x = eq1[1] - eq1[0];
                    double y = eq1[2] - eq1[0];

                    var eq1_2 = tot(split, 1);
                    double n0_1 = eq1_2[0] + n;
                    double x0_1 = (eq1_2[1] - eq1_2[0]) * -1 + x;
                    double y0_1 = (eq1_2[2] - eq1_2[0]) * -1 + y;
                    Console.WriteLine($"tot {x0_1}x{y0_1}y={n0_1}");


                    var eq2 = tot(split2, 0);
                    double n1 = eq2[0] * -1;
                    double x1 = eq2[1] - eq2[0];
                    double y1 = eq2[2] - eq2[0];

                    var eq2_1 = tot(split2, 1);
                    double n1_1 = eq2_1[0] + n1;
                    double x1_1 = (eq2_1[1] - eq2_1[0]) * -1 + x1;
                    double y1_1 = (eq2_1[2] - eq2_1[0]) * -1 + y1;
                    Console.WriteLine($"tot {x1_1}x{y1_1}y={n1_1}");


                    Matrix Dx = new Matrix((float)n0_1, (float)y0_1, (float)n1_1, (float)y1_1, 0, 0);
                    Matrix Dy = new Matrix((float)x0_1, (float)n0_1, (float)x1_1, (float)n1_1, 0, 0);
                    Matrix D = new Matrix((float)x0_1, (float)y0_1, (float)x1_1, (float)y1_1, 0, 0);

                    float X = (Dx.Elements[0] * Dx.Elements[3]) - (Dx.Elements[1] * Dx.Elements[2]);
                    float Y = (Dy.Elements[0] * Dy.Elements[3]) - (Dy.Elements[1] * Dy.Elements[2]);
                    float N = (D.Elements[0] * D.Elements[3]) - (D.Elements[1] * D.Elements[2]);
                    float px = X / N;
                    float py = Y / N;



                    Console.SetCursorPosition(0, 2 + 8);
                    var TabX = Console.GetCursorPosition();
                    Console.WriteLine("DX=");
                    Tabella(TabX);

                    Console.SetCursorPosition(13, 2 + 8);
                    var TabY = Console.GetCursorPosition();
                    Console.WriteLine("DY=");
                    Tabella(TabY);

                    Console.SetCursorPosition(26, 2 + 8);
                    var Tab = Console.GetCursorPosition();
                    Console.WriteLine("D=");
                    Tab.Left = Tab.Left - 1;
                    Tabella(Tab);
                    int left = 8;
                    int top = 10;
                    NumTab(left, top, Dx.Elements, Dy.Elements, D.Elements);
                    Console.SetCursorPosition(0, left + 4);
                    Console.WriteLine($"P({px}; {py})");
                    break;
                }
                while (scelta == "e")
                {
                    Console.Clear();
                    Console.WriteLine("inserisi l'equazione di secondo grado le potenze n^n e la radice qadrate si fa Sqrt(n)");
                    var sis1 = Console.ReadLine();
                    var split = sis1.Split("=");
                    var totEqaz = eqaz(split, 0);
                    var totEqaz1 = eqaz(split, 1);
                    double c = totEqaz[0] + (totEqaz1[0] * -1);
                    double totc0_1 = (totEqaz[1] + (totEqaz1[1] * -1)) - (totEqaz[0] + (totEqaz1[0] * -1));
                    double totc0_2 = (totEqaz[2] + (totEqaz1[2] * -1)) - (totEqaz[0] + (totEqaz1[0] * -1));
                    double a1 = 1 + 1;
                    double tot = totc0_1 + totc0_2;
                    double a = tot / a1;
                    double b = totc0_1 - a;
                    double delta = Math.Pow(b, 2) - 4 * a * c;
                    double x1 = (-(b) + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-(b) - Math.Sqrt(delta)) / (2 * a);
                    Console.WriteLine($"x1 = {x1}");
                    Console.WriteLine($"x2 = {x2}");
                    break;

                }
                Console.SetCursorPosition(0, 15);

                Console.WriteLine("se vuoi uscire prmi n se non vuoi uscire premi s");
                var scelta2 = Console.ReadLine();
                if (scelta2 == "s")
                {
                    ff = true;
                    Console.Clear();
                }
            } while (ff == true);

        }
        private static double[] eqaz(string[] sis1, int n)
        {
            var rr = sis1[n].Replace("x", "*0");
            var c = yy(rr);
            var rrPos = sis1[n].Replace("x", "*1");
            var totPs = yy(rrPos);
            var rrNeg = sis1[n].Replace("x", "*(-1)");
            var totNe = yy(rrNeg);
            double[] equazioni = new[] { Convert.ToDouble(c.Result), Convert.ToDouble(totPs.Result), Convert.ToDouble(totNe.Result) };
            return equazioni;
        }

    }
}
