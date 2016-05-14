namespace Sueca_MVC_v3.Views
{
    partial class MainForm
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
            this.buttonSair = new System.Windows.Forms.Button();
            this.buttonCarregar = new System.Windows.Forms.Button();
            this.buttonNovo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSair
            // 
            this.buttonSair.Location = new System.Drawing.Point(70, 95);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(107, 23);
            this.buttonSair.TabIndex = 5;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = true;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);
            // 
            // buttonCarregar
            // 
            this.buttonCarregar.Location = new System.Drawing.Point(70, 66);
            this.buttonCarregar.Name = "buttonCarregar";
            this.buttonCarregar.Size = new System.Drawing.Size(107, 23);
            this.buttonCarregar.TabIndex = 4;
            this.buttonCarregar.Text = "Carregar Jogo";
            this.buttonCarregar.UseVisualStyleBackColor = true;
            // 
            // buttonNovo
            // 
            this.buttonNovo.Location = new System.Drawing.Point(70, 37);
            this.buttonNovo.Name = "buttonNovo";
            this.buttonNovo.Size = new System.Drawing.Size(107, 23);
            this.buttonNovo.TabIndex = 3;
            this.buttonNovo.Text = "Novo Jogo";
            this.buttonNovo.UseVisualStyleBackColor = true;
            this.buttonNovo.Click += new System.EventHandler(this.buttonNovo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 166);
            this.Controls.Add(this.buttonSair);
            this.Controls.Add(this.buttonCarregar);
            this.Controls.Add(this.buttonNovo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sueca v0.3 (Beta)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.Button buttonCarregar;
        private System.Windows.Forms.Button buttonNovo;
    }
}