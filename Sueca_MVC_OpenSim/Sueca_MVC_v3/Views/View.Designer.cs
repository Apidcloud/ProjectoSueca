namespace Sueca_MVC_v3
{
    partial class View
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
            this.groupBoxCredenciais = new System.Windows.Forms.GroupBox();
            this.buttonSair = new System.Windows.Forms.Button();
            this.checkBoxGuardar = new System.Windows.Forms.CheckBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxSobrenome = new System.Windows.Forms.TextBox();
            this.textBoxNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.groupBoxLogs = new System.Windows.Forms.GroupBox();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.labelIM = new System.Windows.Forms.Label();
            this.labelLocal = new System.Windows.Forms.Label();
            this.labelBot = new System.Windows.Forms.Label();
            this.groupBoxCredenciais.SuspendLayout();
            this.groupBoxLogs.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCredenciais
            // 
            this.groupBoxCredenciais.Controls.Add(this.buttonSair);
            this.groupBoxCredenciais.Controls.Add(this.checkBoxGuardar);
            this.groupBoxCredenciais.Controls.Add(this.textBoxPassword);
            this.groupBoxCredenciais.Controls.Add(this.textBoxSobrenome);
            this.groupBoxCredenciais.Controls.Add(this.textBoxNome);
            this.groupBoxCredenciais.Controls.Add(this.label3);
            this.groupBoxCredenciais.Controls.Add(this.label2);
            this.groupBoxCredenciais.Controls.Add(this.label1);
            this.groupBoxCredenciais.Controls.Add(this.buttonLogout);
            this.groupBoxCredenciais.Controls.Add(this.buttonLogin);
            this.groupBoxCredenciais.Location = new System.Drawing.Point(13, 13);
            this.groupBoxCredenciais.Name = "groupBoxCredenciais";
            this.groupBoxCredenciais.Size = new System.Drawing.Size(283, 121);
            this.groupBoxCredenciais.TabIndex = 0;
            this.groupBoxCredenciais.TabStop = false;
            this.groupBoxCredenciais.Text = "Autenticação";
            // 
            // buttonSair
            // 
            this.buttonSair.Location = new System.Drawing.Point(198, 92);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(75, 23);
            this.buttonSair.TabIndex = 9;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = true;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);
            // 
            // checkBoxGuardar
            // 
            this.checkBoxGuardar.AutoSize = true;
            this.checkBoxGuardar.Location = new System.Drawing.Point(73, 93);
            this.checkBoxGuardar.Name = "checkBoxGuardar";
            this.checkBoxGuardar.Size = new System.Drawing.Size(64, 17);
            this.checkBoxGuardar.TabIndex = 8;
            this.checkBoxGuardar.Text = "Guardar";
            this.checkBoxGuardar.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(73, 66);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxSobrenome
            // 
            this.textBoxSobrenome.Location = new System.Drawing.Point(73, 39);
            this.textBoxSobrenome.Name = "textBoxSobrenome";
            this.textBoxSobrenome.Size = new System.Drawing.Size(100, 20);
            this.textBoxSobrenome.TabIndex = 6;
            // 
            // textBoxNome
            // 
            this.textBoxNome.Location = new System.Drawing.Point(73, 13);
            this.textBoxNome.Name = "textBoxNome";
            this.textBoxNome.Size = new System.Drawing.Size(100, 20);
            this.textBoxNome.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sobrenome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome";
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(198, 42);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(75, 23);
            this.buttonLogout.TabIndex = 1;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(198, 13);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // groupBoxLogs
            // 
            this.groupBoxLogs.Controls.Add(this.textBoxLogs);
            this.groupBoxLogs.Location = new System.Drawing.Point(13, 141);
            this.groupBoxLogs.Name = "groupBoxLogs";
            this.groupBoxLogs.Size = new System.Drawing.Size(644, 228);
            this.groupBoxLogs.TabIndex = 1;
            this.groupBoxLogs.TabStop = false;
            this.groupBoxLogs.Text = "Logs";
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Location = new System.Drawing.Point(7, 20);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogs.Size = new System.Drawing.Size(631, 199);
            this.textBoxLogs.TabIndex = 0;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.labelIM);
            this.groupBoxInfo.Controls.Add(this.labelLocal);
            this.groupBoxInfo.Controls.Add(this.labelBot);
            this.groupBoxInfo.Location = new System.Drawing.Point(302, 13);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(355, 121);
            this.groupBoxInfo.TabIndex = 2;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // labelIM
            // 
            this.labelIM.AutoSize = true;
            this.labelIM.Location = new System.Drawing.Point(6, 69);
            this.labelIM.Name = "labelIM";
            this.labelIM.Size = new System.Drawing.Size(66, 13);
            this.labelIM.TabIndex = 2;
            this.labelIM.Text = "IM recebida:";
            // 
            // labelLocal
            // 
            this.labelLocal.AutoSize = true;
            this.labelLocal.Location = new System.Drawing.Point(7, 45);
            this.labelLocal.Name = "labelLocal";
            this.labelLocal.Size = new System.Drawing.Size(44, 13);
            this.labelLocal.TabIndex = 1;
            this.labelLocal.Text = "Região:";
            // 
            // labelBot
            // 
            this.labelBot.AutoSize = true;
            this.labelBot.Location = new System.Drawing.Point(7, 20);
            this.labelBot.Name = "labelBot";
            this.labelBot.Size = new System.Drawing.Size(26, 13);
            this.labelBot.TabIndex = 0;
            this.labelBot.Text = "Bot:";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 375);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.groupBoxLogs);
            this.Controls.Add(this.groupBoxCredenciais);
            this.Name = "View";
            this.Text = "side";
            this.Load += new System.EventHandler(this.View_Load);
            this.groupBoxCredenciais.ResumeLayout(false);
            this.groupBoxCredenciais.PerformLayout();
            this.groupBoxLogs.ResumeLayout(false);
            this.groupBoxLogs.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCredenciais;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxSobrenome;
        private System.Windows.Forms.TextBox textBoxNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.CheckBox checkBoxGuardar;
        private System.Windows.Forms.GroupBox groupBoxLogs;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label labelLocal;
        private System.Windows.Forms.Label labelBot;
        private System.Windows.Forms.Label labelIM;
    }
}

