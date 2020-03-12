namespace SqlManager
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GridContent = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.TreeViewExplorer = new System.Windows.Forms.TreeView();
            this.CheckLabel = new System.Windows.Forms.Label();
            this.ExplorerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.создатьDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridContent)).BeginInit();
            this.ExplorerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridContent
            // 
            this.GridContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridContent.BackgroundColor = System.Drawing.Color.MintCream;
            this.GridContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridContent.Location = new System.Drawing.Point(189, 12);
            this.GridContent.MultiSelect = false;
            this.GridContent.Name = "GridContent";
            this.GridContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridContent.Size = new System.Drawing.Size(634, 525);
            this.GridContent.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(747, 560);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.Location = new System.Drawing.Point(666, 560);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnDelete.Location = new System.Drawing.Point(585, 560);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // TreeViewExplorer
            // 
            this.TreeViewExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeViewExplorer.Location = new System.Drawing.Point(12, 12);
            this.TreeViewExplorer.Name = "TreeViewExplorer";
            this.TreeViewExplorer.Size = new System.Drawing.Size(171, 525);
            this.TreeViewExplorer.TabIndex = 5;
            // 
            // CheckLabel
            // 
            this.CheckLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckLabel.AutoSize = true;
            this.CheckLabel.Location = new System.Drawing.Point(9, 560);
            this.CheckLabel.Name = "CheckLabel";
            this.CheckLabel.Size = new System.Drawing.Size(35, 13);
            this.CheckLabel.TabIndex = 6;
            this.CheckLabel.Text = "label1";
            // 
            // ExplorerContextMenu
            // 
            this.ExplorerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьDBToolStripMenuItem,
            this.создатьTableToolStripMenuItem});
            this.ExplorerContextMenu.Name = "ExplorerContextMenu";
            this.ExplorerContextMenu.Size = new System.Drawing.Size(150, 48);
            // 
            // создатьDBToolStripMenuItem
            // 
            this.создатьDBToolStripMenuItem.Name = "создатьDBToolStripMenuItem";
            this.создатьDBToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.создатьDBToolStripMenuItem.Text = "Создать DB";
            // 
            // создатьTableToolStripMenuItem
            // 
            this.создатьTableToolStripMenuItem.Name = "создатьTableToolStripMenuItem";
            this.создатьTableToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.создатьTableToolStripMenuItem.Text = "Создать Table";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 596);
            this.Controls.Add(this.CheckLabel);
            this.Controls.Add(this.TreeViewExplorer);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GridContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SQL Manager";
            ((System.ComponentModel.ISupportInitialize)(this.GridContent)).EndInit();
            this.ExplorerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView GridContent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TreeView TreeViewExplorer;
        private System.Windows.Forms.Label CheckLabel;
        private System.Windows.Forms.ContextMenuStrip ExplorerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem создатьDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьTableToolStripMenuItem;
    }
}

