namespace PresentationLayer.Forms {
    partial class AddUpdatePersonForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.addUpdateUC1 = new PresentationLayer.UCs.AddUpdateUC();
            this.SuspendLayout();
            // 
            // addUpdateUC1
            // 
            this.addUpdateUC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addUpdateUC1.Location = new System.Drawing.Point(14, 14);
            this.addUpdateUC1.Margin = new System.Windows.Forms.Padding(5);
            this.addUpdateUC1.Name = "addUpdateUC1";
            this.addUpdateUC1.Size = new System.Drawing.Size(1078, 527);
            this.addUpdateUC1.TabIndex = 0;
            // 
            // AddUpdatePersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 558);
            this.Controls.Add(this.addUpdateUC1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddUpdatePersonForm";
            this.Text = "Add New Person";
            this.ResumeLayout(false);

        }

        #endregion

        private UCs.AddUpdateUC addUpdateUC1;
    }
}