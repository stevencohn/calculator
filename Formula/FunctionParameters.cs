﻿//************************************************************************************************
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

	internal enum ParameterOperator
	{
		GreaterThan,
		LessThan,
		NotEqual
	}

	/// <summary>
	/// Boxy collection of things
	/// </summary>
	internal class FunctionParameter
	{
		public ParameterType Type { get; private set; }
		public ParameterOperator Operator { get; private set; }
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

		public int Compare(FunctionParameter template)
		{
			if (template.Type == Type)
			{
				switch (template.Type)
				{
					case ParameterType.Boolean:
						return (bool)template.Value == (bool)Value ? 0 : -1;

					case ParameterType.String:
						return (string)template.Value == (string)Value ? 0 : -1;

					default:
						{
							var t = (double)template.Value;
							var v = (double)Value;
							if (t < v) return -1;
							if (t > v) return 1;
							return 0;
						}
				}
			}

			return -1;
		}
	}


	/// <summary>
	/// Custom parameter: double, bool, or string
	/// </summary>
	internal class FunctionParameters
	{
		protected readonly List<FunctionParameter> values;

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

		public FunctionParameter ItemAt(int index)
		{
			if (index >= 0 && index < values.Count)
			{
				return values[index];
			}

			throw new FormulaException("ItemAt index is out of range");
		}

		public FunctionParameters Match(params ParameterType[] types)
		{
			// values should contain at least the required types
			if (types.Length <= values.Count)
			{
				for (int i = 0; i < types.Length; i++)
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

		public double[] ToDoubleArray()
		{
			return values.Select(v => (double)v.Value).ToArray();
		}
	}
}