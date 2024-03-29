﻿//////////////////////////////////////////////////////////////////////////////
// This source code and all associated files and resources are copyrighted by
// the author(s). This source code and all associated files and resources may
// be used as long as they are used according to the terms and conditions set
// forth in The Code Project Open License (CPOL), which may be viewed at
// http://www.blackbeltcoder.com/Legal/Licenses/CPOL.
//
// Copyright (c) 2010 Jonathan Wood
//

#pragma warning disable IDE1006 // Naming Styles

using River.OneMoreAddIn.Commands.Tables.Formulas;
using System;
using System.Windows.Forms;

namespace CalculatorHarness
{
	internal partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			txtExpression.SelectedIndex = 0;
			txtExpression.Focus();
		}

		private void btnEvaluate_Click(object sender, EventArgs e)
		{
			try
			{
				// Evaluate the current expression
				var calculator = new Calculator();
				calculator.ProcessSymbol += ProcessSymbol;
				calculator.ProcessFunction += ProcessFunction;
				txtResult.Text = calculator.Execute(txtExpression.Text).ToString();
			}
			catch (FormulaException ex)
			{
				// Report expression error and move caret to error position
				MessageBox.Show(ex.Message);
				txtExpression.Select(ex.Column, 0);
				txtExpression.Select();
			}
			catch (Exception ex)
			{
				// Unknown error
				MessageBox.Show("Unexpected error : " + ex.Message);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		// Implement expression symbols
		protected void ProcessSymbol(object sender, SymbolEventArgs e)
		{
			switch (e.Name.ToLower())
			{
				case "a1":
					e.SetResult(7.0);
					break;

				case "a2":
					e.SetResult(17);
					break;

				case "a3":
					e.SetResult("abc");
					break;

				case "a4":
					e.SetResult(true);
					break;

				case "a5":
					e.SetResult(double.NaN);
					e.Status = SymbolStatus.None;
					break;

				default:
					e.Status = SymbolStatus.UndefinedSymbol;
					break;
			}
		}

		// Implement expression functions
		protected void ProcessFunction(object sender, FunctionEventArgs e)
		{
			switch (e.Name.ToLower())
			{
				case "sum":
					if (e.Parameters.Count == 2)
					{
						// this is a silly test
						e.Result = Math.Min(e.Parameters[0], e.Parameters[1]);
					}
					else
					{
						e.Status = FunctionStatus.WrongParameterCount;
					}
					break;

				default:
					e.Status = FunctionStatus.UndefinedFunction;
					break;
			}
		}
	}
}
