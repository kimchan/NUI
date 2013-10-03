namespace DiskOfDemise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.phraseLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rightLegShape = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.leftLegShape = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.rightArmShape = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.leftArmShape = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.bodyShape = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.headShape = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.wheelImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.wheelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // phraseLabel
            // 
            resources.ApplyResources(this.phraseLabel, "phraseLabel");
            this.phraseLabel.Name = "phraseLabel";
            // 
            // nameLabel
            // 
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Name = "nameLabel";
            // 
            // shapeContainer1
            // 
            resources.ApplyResources(this.shapeContainer1, "shapeContainer1");
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rightLegShape,
            this.leftLegShape,
            this.rightArmShape,
            this.leftArmShape,
            this.bodyShape,
            this.headShape,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.TabStop = false;
            // 
            // rightLegShape
            // 
            resources.ApplyResources(this.rightLegShape, "rightLegShape");
            this.rightLegShape.Name = "lineShape3";
            // 
            // leftLegShape
            // 
            resources.ApplyResources(this.leftLegShape, "leftLegShape");
            this.leftLegShape.Name = "lineShape3";
            // 
            // rightArmShape
            // 
            resources.ApplyResources(this.rightArmShape, "rightArmShape");
            this.rightArmShape.Name = "lineShape3";
            // 
            // leftArmShape
            // 
            resources.ApplyResources(this.leftArmShape, "leftArmShape");
            this.leftArmShape.Name = "lineShape3";
            // 
            // bodyShape
            // 
            resources.ApplyResources(this.bodyShape, "bodyShape");
            this.bodyShape.Name = "lineShape3";
            // 
            // headShape
            // 
            resources.ApplyResources(this.headShape, "headShape");
            this.headShape.Name = "ovalShape1";
            // 
            // lineShape2
            // 
            resources.ApplyResources(this.lineShape2, "lineShape2");
            this.lineShape2.Name = "lineShape2";
            // 
            // lineShape1
            // 
            resources.ApplyResources(this.lineShape1, "lineShape1");
            this.lineShape1.Name = "lineShape1";
            // 
            // wheelImage
            // 
            resources.ApplyResources(this.wheelImage, "wheelImage");
            this.wheelImage.Name = "wheelImage";
            this.wheelImage.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wheelImage);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.phraseLabel);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wheelImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label phraseLabel;
        private System.Windows.Forms.Label nameLabel;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape headShape;
        private Microsoft.VisualBasic.PowerPacks.LineShape bodyShape;
        private Microsoft.VisualBasic.PowerPacks.LineShape leftArmShape;
        private Microsoft.VisualBasic.PowerPacks.LineShape rightArmShape;
        private Microsoft.VisualBasic.PowerPacks.LineShape leftLegShape;
        private Microsoft.VisualBasic.PowerPacks.LineShape rightLegShape;
        private System.Windows.Forms.PictureBox wheelImage;
        
    }
}

