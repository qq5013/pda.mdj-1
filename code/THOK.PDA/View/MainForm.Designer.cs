namespace THOK.PDA.View
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnParamenter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnParamenter
            // 
            this.btnParamenter.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnParamenter.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnParamenter.Location = new System.Drawing.Point(4, 214);
            this.btnParamenter.Name = "btnParamenter";
            this.btnParamenter.Size = new System.Drawing.Size(112, 53);
            this.btnParamenter.TabIndex = 4;
            this.btnParamenter.Text = "参数设置";
            this.btnParamenter.Click += new System.EventHandler(this.btnParamenter_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExit.Location = new System.Drawing.Point(122, 214);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 53);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "关闭";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 32);
            this.label1.Text = "天海欧康科技信息(厦门)有限公司       版权所有        ©2010-2020";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.label2.Location = new System.Drawing.Point(4, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 30);
            this.label2.Text = "仓储引导作业系统";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnOut
            // 
            this.btnOut.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnOut.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnOut.Location = new System.Drawing.Point(4, 49);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(230, 159);
            this.btnOut.TabIndex = 0;
            this.btnOut.Text = "出库作业";
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(243, 320);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnParamenter);
            this.Controls.Add(this.btnOut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnParamenter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOut;

    }
}

