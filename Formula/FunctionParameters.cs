//************************************************************************************************
// Copyright © 2022 Steven M Cohn.  All rights reserved.
//************************************************************************************************

namespace River.OneMoreAddIn.Commands.Tables.Formulas
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal enum ParameterType
	{
		Boolean,
		Double,
		String
	}

	/// <summary>
	/// Boxy collection of things
	/// </summary>
	internal class FunctionParameter
	{
		public ParameterType Type { get; private set; }
		public object Value { get; private set; }
		public FunctionParameter(object value)
		{
			Value = value;
			if (value is double)
			{
				Type = ParameterType.Double;
				return;
			}
			if (value is string)
			{
				Type = ParameterType.String;
				return;
			}
			if (value is bool)
			{
				Type = ParameterType.Boolean;
				return;
			}

			throw new FormulaException($"{value} ({value.GetType()}) Must be bool, double, or string");
		}		
	}


	/// <summary>
	/// Custom parameter: double, bool, or string
	/// </summary>
	internal class FunctionParameters
	{
		private readonly List<FunctionParameter> values;

		public FunctionParameters()
		{
			values = new List<FunctionParameter>();
		}

		public void Add(bool value)
		{
			values.Add(new FunctionParameter(value));
		}

		public void Add(double value)
		{
			values.Add(new FunctionParameter(value));
		}

		public void Add(double[] valueList)
		{
			foreach (var v in valueList)
			{
				Add(v);
			}
		}

		public void Add(string value)
		{
			values.Add(new FunctionParameter(value));
		}

		public int Count
		{
			get => values.Count;
		}

		public double this[int i]
		{
			get => (double)values[i].Value;
		}

		public bool GetBool(int i)
		{
			return (bool)values[i].Value;
		}

		public string GetString(int i)
		{
			return (string)values[i].Value;
		}

		public FunctionParameters Match(params ParameterType[] types)
		{
			// values should contain at least the required types
			if (types.Length <= values.Count)
			{
				for (int i=0; i < types.Length; i++)
				{
					// does each value match the required type in sequence
					if (types[i] != values[i].Type)
					{
						throw new FormulaException($"parameter {i} is not of type {types[i]}");
					}
				}
			}

			return this;
		}

		public FunctionParameter[] ToArray()
		{
			return values.ToArray();
		}

		public double[] ToDubleArray()
		{
			return values.Select(v => (double)v.Value).ToArray();
		}
	}
}