//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************

#pragma warning disable S125 // Sections of code should not be commented out

namespace River.OneMoreAddIn.Commands.Tables.Formulas
{
	using System;
	using System.Collections.Generic;
	using System.Linq;


	internal delegate double MathFunc(FunctionParameters args);


	internal class MathFunction
	{
		public string Name { get; private set; }

		public MathFunc Fn { get; private set; }

		public MathFunction(string name, MathFunc fn)
		{
			Name = name;
			Fn = fn;
		}
	}


	internal static class MathFunctions
	{
		private static readonly List<MathFunction> functions = new List<MathFunction>();
		private static readonly ParameterType D = ParameterType.Double;
		private static readonly ParameterType B = ParameterType.Boolean;
		private static readonly ParameterType S = ParameterType.String;

		static MathFunctions()
		{
			functions.Add(new MathFunction("abs", (p) => Math.Abs(p.Match(D)[0])));
			functions.Add(new MathFunction("acos", (p) => Math.Acos(p.Match(D)[0])));
			functions.Add(new MathFunction("asin", (p) => Math.Asin(p.Match(D)[0])));
			functions.Add(new MathFunction("atan", (p) => Math.Atan(p.Match(D)[0])));
			functions.Add(new MathFunction("atan2", (p) => Math.Atan2(p.Match(D, D)[0], p[1])));
			functions.Add(new MathFunction("average", (p) => Average(p.Match(D, D).ToDubleArray())));
			functions.Add(new MathFunction("ceiling", (p) => Math.Ceiling(p.Match(D, D)[0])));
			functions.Add(new MathFunction("cos", (p) => Math.Cos(p.Match(D)[0])));
			functions.Add(new MathFunction("cosh", (p) => Math.Cosh(p.Match(D)[0])));
			functions.Add(new MathFunction("countif", (p) => CountIf(p)));
			functions.Add(new MathFunction("exp", (p) => Math.Exp(p.Match(D)[0])));
			functions.Add(new MathFunction("floor", (p) => Math.Floor(p.Match(D)[0])));
			functions.Add(new MathFunction("log", (p) => Math.Log(p.Match(D)[0])));
			functions.Add(new MathFunction("log10", (p) => Math.Log10(p.Match(D)[0])));
			functions.Add(new MathFunction("max", (p) => Max(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("median", (p) => Median(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("min", (p) => Min(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("mode", (p) => Mode(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("pow", (p) => Math.Pow(p.Match(D, D)[0], p[1])));
			functions.Add(new MathFunction("range", (p) => Range(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("round", (p) => Math.Round(p.Match(D)[0])));
			functions.Add(new MathFunction("sign", (p) => Math.Sign(p.Match(D)[0])));
			functions.Add(new MathFunction("sin", (p) => Math.Sin(p.Match(D)[0])));
			functions.Add(new MathFunction("sinh", (p) => Math.Sinh(p.Match(D)[0])));
			functions.Add(new MathFunction("sqrt", (p) => Math.Sqrt(p.Match(D)[0])));
			functions.Add(new MathFunction("stdev", (p) => StandardDeviation(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("sum", (p) => Sum(p.Match(D).ToDubleArray())));
			functions.Add(new MathFunction("tan", (p) => Math.Tan(p.Match(D)[0])));
			functions.Add(new MathFunction("tanh", (p) => Math.Tanh(p.Match(D)[0])));
			functions.Add(new MathFunction("trunc", (p) => Math.Truncate(p.Match(D)[0])));
			functions.Add(new MathFunction("variance", (p) => Variance(p.Match(D).ToDubleArray())));
		}


		public static MathFunc Find(string name)
		{
			return functions.FirstOrDefault(f => f.Name == name)?.Fn;
		}


		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		private static double Average(double[] p)
		{
			if (p.Length == 0)
				return 0.0;

			return p.AsEnumerable().Average();
		}


		private static double CountIf(FunctionParameters p)
		{
			if (p.Count < 2)
				throw new FormulaException($"CountIf requires at least two parameters");

			var a = p.ToArray();
			var last = a[a.Length - 1];

			var op = ((string)last.Value)[0];

			var test = ("<>!".Contains(op)
				? ((string)last.Value).Substring(1)
				: (string)last.Value)
				.Cast<double>().First();

			if (last.Type == ParameterType.Boolean)
			{
				return a.Count(v => v.Type == ParameterType.Boolean && (bool)v.Value);
			}
			else if (last.Type == ParameterType.String)
			{
				switch (op)
				{
					case '<': return a.Count(v => v.Type == ParameterType.Double && (double)v.Value < test);
					case '>': return a.Count(v => v.Type == ParameterType.Double && (double)v.Value > test);
					case '!': return a.Count(v => v.Type == ParameterType.Double && (double)v.Value != test);
					default: return a.Count(v => v.Type == ParameterType.Double && (double)v.Value == test);
				}
			}

			throw new FormulaException($"CountIf parameters {a.Length - 1} is not a bool or string");
		}


		private static double Max(double[] p)
		{
			if (p.Length == 0)
				return 0.0;

			return p.AsEnumerable().Max();
		}


		private static double Median(double[] p)
		{
			if (p.Length == 0)
				return 0.0;

			int count = p.Count();
			if (count % 2 == 0)
			{
				return p.OrderBy(n => n).Skip((count / 2) - 1).Take(2).Average();
			}

			return p.OrderBy(n => n).ElementAt(count / 2);
		}


		private static double Min(double[] p)
		{
			if (p.Length == 0)
				return 0.0;

			return p.AsEnumerable().Min();
		}


		private static double Mode(double[] values)
		{
			return values
				.GroupBy(n => n)
				.OrderByDescending(g => g.Count())
				.Select(g => g.Key).FirstOrDefault();
		}


		private static double Range(double[] p)
		{
			if (p.Length == 0)
				return 0.0;

			return p.AsEnumerable().Max() - p.AsEnumerable().Min();
		}


		private static double Sum(double[] p)
		{
			if (p.Length == 0)
				return 0.0;

			return p.AsEnumerable().Sum();
		}


		private static double StandardDeviation(double[] values)
		{
			var variance = Variance(values);

			if (variance == 0.0)
				return 0.0;

			return Math.Sqrt(variance);
		}


		private static double Variance(double[] values)
		{
			var mean = 0.0;
			var sum = 0.0;
			var variance = 0.0;
			var n = 0;
			foreach (var value in values)
			{
				n++;
				var delta = value - mean;
				mean += delta / n;
				sum += delta * (value - mean);
			}

			if (n > 1)
			{
				// if (Population)
				variance = sum / (n - 1);

				// else if (Sample)
				//variance = sum / n;
			}

			return variance;
		}
	}
}
