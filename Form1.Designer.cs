namespace TestEval
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnEvaluate = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtExpression = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 23);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "E&xpression:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 63);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Result:";
			// 
			// txtResult
			// 
			this.txtResult.Location = new System.Drawing.Point(118, 58);
			this.txtResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.Size = new System.Drawing.Size(412, 26);
			this.txtResult.TabIndex = 3;
			this.txtResult.TabStop = false;
			// 
			// btnEvaluate
			// 
			this.btnEvaluate.Location = new System.Drawing.Point(298, 112);
			this.btnEvaluate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnEvaluate.Name = "btnEvaluate";
			this.btnEvaluate.Size = new System.Drawing.Size(112, 35);
			this.btnEvaluate.TabIndex = 1;
			this.btnEvaluate.Text = "&Evaluate";
			this.btnEvaluate.UseVisualStyleBackColor = true;
			this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(420, 112);
			this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(112, 35);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtExpression
			// 
			this.txtExpression.Location = new System.Drawing.Point(118, 18);
			this.txtExpression.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtExpression.Name = "txtExpression";
			this.txtExpression.Size = new System.Drawing.Size(412, 26);
			this.txtExpression.TabIndex = 0;
			this.txtExpression.Text = "round(pi * (abs(pow(-3, 2)) + sqrt(147 * (14 + 27))))";
			// 
			// Form1
			// 
			this.AcceptButton = this.btnEvaluate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(550, 169);
			this.Controls.Add(this.txtExpression);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnEvaluate);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Test Expression Evaluator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnEvaluate;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtExpression;
	}
}

