﻿
namespace QLNhaHang
{
    partial class Add_DM
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
            this.txt_danhmuc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_huybo = new System.Windows.Forms.Button();
            this.btn_themdm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "THÊM DANH MỤC";
            // 
            // txt_danhmuc
            // 
            this.txt_danhmuc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_danhmuc.Location = new System.Drawing.Point(171, 43);
            this.txt_danhmuc.Name = "txt_danhmuc";
            this.txt_danhmuc.Size = new System.Drawing.Size(181, 25);
            this.txt_danhmuc.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tên Danh Mục";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_danhmuc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 99);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin";
            // 
            // btn_huybo
            // 
            this.btn_huybo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_huybo.Location = new System.Drawing.Point(246, 179);
            this.btn_huybo.Name = "btn_huybo";
            this.btn_huybo.Size = new System.Drawing.Size(98, 42);
            this.btn_huybo.TabIndex = 25;
            this.btn_huybo.Text = "HỦY BỎ";
            this.btn_huybo.UseVisualStyleBackColor = true;
            this.btn_huybo.Click += new System.EventHandler(this.btn_huybo_Click);
            // 
            // btn_themdm
            // 
            this.btn_themdm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themdm.Location = new System.Drawing.Point(49, 179);
            this.btn_themdm.Name = "btn_themdm";
            this.btn_themdm.Size = new System.Drawing.Size(98, 42);
            this.btn_themdm.TabIndex = 24;
            this.btn_themdm.Text = "THÊM";
            this.btn_themdm.UseVisualStyleBackColor = true;
            this.btn_themdm.Click += new System.EventHandler(this.btn_themdm_Click);
            // 
            // Add_DM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 234);
            this.Controls.Add(this.btn_huybo);
            this.Controls.Add(this.btn_themdm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Add_DM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add_DM";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_danhmuc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_huybo;
        private System.Windows.Forms.Button btn_themdm;
    }
}