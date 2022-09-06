﻿//************************************************************************************************
// Copyright © 2022 Steven M Cohn.  All rights reserved.
//************************************************************************************************

namespace River.OneMoreAddIn.Commands.Tables.Formulas
{
	using System.Collections.Generic;
	using System.Linq;


	/// <summary>
	/// Accepted types of values in a formula
	/// </summary>
	internal enum FormulaValueType
	{
		Boolean,
		Double,
		String
	}


	/// <summary>
	/// Allowed operators in first character of a countif comparison
	/// </summary>
	internal enum CountIfOperator
	{
		GreaterThan,
		LessThan,
		NotEqual
	}


	/// <summary>
	/// Boxy formula value representing a double, string, or boolean value.
	/// </summary>
	internal class FormulaValue
	{
		public FormulaValueType Type { get; private set; }
		public CountIfOperator Operator { get; private set; }
		public object Value { get; private set; }
		public double DoubleValue { get => (double)Value; }
		public FormulaValue(object value)
		{
			Value = value;
			if (value is double)
			{
				Type = FormulaValueType.Double;
				return;
			}
			if (value is string)
			{
				Type = FormulaValueType.String;
				return;
			}
			if (value is bool)
			{
				Type = FormulaValueType.Boolean;
				return;
			}

			throw new FormulaException($"{value} ({value.GetType()}) Must be bool, double, or string");
		}

		public int CompareTo(FormulaValue template)
		{
			if (template.Type == Type)
			{
				switch (template.Type)
				{
					case FormulaValueType.Boolean:
						return ((bool)Value).CompareTo((bool)template.Value);

					case FormulaValueType.String:
						return ((string)Value).CompareTo((string)template.Value);

					default:
						return ((double)Value).CompareTo(template.DoubleValue);
				}
			}

			return -1;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}


	/// <summary>
	/// Collection of formula values
	/// </summary>
	internal class FormulaValues
	{
		protected readonly List<FormulaValue> values;

		public FormulaValues()
		{
			values = new List<FormulaValue>();
		}

		public void Add(bool value)
		{
			values.Add(new FormulaValue(value));
		}

		public void Add(double value)
		{
			values.Add(new FormulaValue(value));
		}

		public void Add(double[] valueList)
		{
			foreach (var v in valueList)
			{
				Add(v);
			}
		}

		public void Add(FormulaValue other)
		{
			values.Add(other);
		}

		public void Add(string value)
		{
			values.Add(new FormulaValue(value));
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

		public FormulaValue ItemAt(int index)
		{
			if (index >= 0 && index < values.Count)
			{
				return values[index];
			}

			throw new FormulaException("ItemAt index is out of range");
		}

		public FormulaValues Match(params FormulaValueType[] types)
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

		public FormulaValue[] ToArray()
		{
			return values.ToArray();
		}

		public double[] ToDoubleArray()
		{
			return values.Select(v => (double)v.Value).ToArray();
		}
	}
}