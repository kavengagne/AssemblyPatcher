namespace Main
{
    partial class TestForm
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.operand1 = new System.Windows.Forms.TextBox();
            this.operand2 = new System.Windows.Forms.TextBox();
            this.result = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnInject = new System.Windows.Forms.Button();
            this.btnAdd2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(162, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSub
            // 
            this.btnSub.Location = new System.Drawing.Point(162, 81);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(75, 23);
            this.btnSub.TabIndex = 1;
            this.btnSub.Text = "Sub";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // operand1
            // 
            this.operand1.Location = new System.Drawing.Point(25, 25);
            this.operand1.Name = "operand1";
            this.operand1.Size = new System.Drawing.Size(55, 20);
            this.operand1.TabIndex = 2;
            this.operand1.Text = "2";
            // 
            // operand2
            // 
            this.operand2.Location = new System.Drawing.Point(86, 25);
            this.operand2.Name = "operand2";
            this.operand2.Size = new System.Drawing.Size(55, 20);
            this.operand2.TabIndex = 3;
            this.operand2.Text = "4";
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(255, 25);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(55, 20);
            this.result.TabIndex = 4;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(25, 121);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(204, 20);
            this.txtPath.TabIndex = 5;
            this.txtPath.Text = "E:\\Dropbox\\Projects\\AssemblyPatcher\\Main\\bin\\Debug\\CalculatorLib.dll";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(235, 119);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // btnInject
            // 
            this.btnInject.Location = new System.Drawing.Point(25, 156);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(75, 23);
            this.btnInject.TabIndex = 7;
            this.btnInject.Text = "Inject";
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // btnAdd2
            // 
            this.btnAdd2.Location = new System.Drawing.Point(162, 52);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(75, 23);
            this.btnAdd2.TabIndex = 8;
            this.btnAdd2.Text = "Add2";
            this.btnAdd2.UseVisualStyleBackColor = true;
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 351);
            this.Controls.Add(this.btnAdd2);
            this.Controls.Add(this.btnInject);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.result);
            this.Controls.Add(this.operand2);
            this.Controls.Add(this.operand1);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnAdd);
            this.Name = "TestForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.TextBox operand1;
        private System.Windows.Forms.TextBox operand2;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.Button btnAdd2;
    }
}

