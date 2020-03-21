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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GridContent = new System.Windows.Forms.DataGridView();
            this.TreeViewExplorer = new System.Windows.Forms.TreeView();
            this.DBContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CreateTableTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteDBTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameDBTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteTableTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameTableTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddDB = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteDB = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.RowContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteRowTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridContent)).BeginInit();
            this.DBContextMenu.SuspendLayout();
            this.TableContextMenu.SuspendLayout();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.RowContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridContent
            // 
            this.GridContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridContent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.GridContent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.GridContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridContent.DefaultCellStyle = dataGridViewCellStyle6;
            this.GridContent.EnableHeadersVisualStyles = false;
            this.GridContent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            this.GridContent.Location = new System.Drawing.Point(189, 53);
            this.GridContent.MultiSelect = false;
            this.GridContent.Name = "GridContent";
            this.GridContent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridContent.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            this.GridContent.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.GridContent.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.GridContent.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.GridContent.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.GridContent.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.GridContent.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridContent.Size = new System.Drawing.Size(674, 550);
            this.GridContent.TabIndex = 1;
            // 
            // TreeViewExplorer
            // 
            this.TreeViewExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeViewExplorer.Location = new System.Drawing.Point(12, 53);
            this.TreeViewExplorer.Name = "TreeViewExplorer";
            this.TreeViewExplorer.ShowPlusMinus = false;
            this.TreeViewExplorer.Size = new System.Drawing.Size(171, 514);
            this.TreeViewExplorer.TabIndex = 5;
            // 
            // DBContextMenu
            // 
            this.DBContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.DBContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateTableTSMItem,
            this.DeleteDBTSMItem,
            this.RenameDBTSMItem});
            this.DBContextMenu.Name = "DBСontextMenu";
            this.DBContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.DBContextMenu.Size = new System.Drawing.Size(189, 70);
            // 
            // CreateTableTSMItem
            // 
            this.CreateTableTSMItem.ForeColor = System.Drawing.Color.White;
            this.CreateTableTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("CreateTableTSMItem.Image")));
            this.CreateTableTSMItem.Name = "CreateTableTSMItem";
            this.CreateTableTSMItem.Size = new System.Drawing.Size(188, 22);
            this.CreateTableTSMItem.Text = "Создать таблицу";
            // 
            // DeleteDBTSMItem
            // 
            this.DeleteDBTSMItem.ForeColor = System.Drawing.Color.White;
            this.DeleteDBTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteDBTSMItem.Image")));
            this.DeleteDBTSMItem.Name = "DeleteDBTSMItem";
            this.DeleteDBTSMItem.Size = new System.Drawing.Size(188, 22);
            this.DeleteDBTSMItem.Text = "Удалить базу";
            // 
            // RenameDBTSMItem
            // 
            this.RenameDBTSMItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RenameDBTSMItem.Name = "RenameDBTSMItem";
            this.RenameDBTSMItem.Size = new System.Drawing.Size(188, 22);
            this.RenameDBTSMItem.Text = "Переименовать базу";
            // 
            // TableContextMenu
            // 
            this.TableContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.TableContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteTableTSMItem,
            this.RenameTableTSMItem});
            this.TableContextMenu.Name = "TableContextMenu";
            this.TableContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TableContextMenu.Size = new System.Drawing.Size(210, 48);
            // 
            // DeleteTableTSMItem
            // 
            this.DeleteTableTSMItem.ForeColor = System.Drawing.Color.White;
            this.DeleteTableTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteTableTSMItem.Image")));
            this.DeleteTableTSMItem.Name = "DeleteTableTSMItem";
            this.DeleteTableTSMItem.Size = new System.Drawing.Size(209, 22);
            this.DeleteTableTSMItem.Text = "Удалить таблицу";
            // 
            // RenameTableTSMItem
            // 
            this.RenameTableTSMItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.RenameTableTSMItem.Name = "RenameTableTSMItem";
            this.RenameTableTSMItem.Size = new System.Drawing.Size(209, 22);
            this.RenameTableTSMItem.Text = "Переименовать таблицу";
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            this.MenuPanel.Controls.Add(this.pictureBox1);
            this.MenuPanel.Controls.Add(this.label1);
            this.MenuPanel.Controls.Add(this.btnMinimize);
            this.MenuPanel.Controls.Add(this.btnClose);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(875, 30);
            this.MenuPanel.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "SQLManger";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))));
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnMinimize.Location = new System.Drawing.Point(811, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(32, 30);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "__";
            this.btnMinimize.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Location = new System.Drawing.Point(843, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnAddDB
            // 
            this.btnAddDB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDB.BackgroundImage")));
            this.btnAddDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddDB.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddDB.FlatAppearance.BorderSize = 0;
            this.btnAddDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))));
            this.btnAddDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.btnAddDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDB.Location = new System.Drawing.Point(0, 0);
            this.btnAddDB.Name = "btnAddDB";
            this.btnAddDB.Size = new System.Drawing.Size(30, 30);
            this.btnAddDB.TabIndex = 8;
            this.btnAddDB.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.panel1.Controls.Add(this.btnDeleteDB);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnAddDB);
            this.panel1.Location = new System.Drawing.Point(12, 573);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 30);
            this.panel1.TabIndex = 9;
            // 
            // btnDeleteDB
            // 
            this.btnDeleteDB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDB.BackgroundImage")));
            this.btnDeleteDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDeleteDB.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteDB.FlatAppearance.BorderSize = 0;
            this.btnDeleteDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))));
            this.btnDeleteDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.btnDeleteDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDB.Location = new System.Drawing.Point(60, 0);
            this.btnDeleteDB.Name = "btnDeleteDB";
            this.btnDeleteDB.Size = new System.Drawing.Size(30, 30);
            this.btnDeleteDB.TabIndex = 10;
            this.btnDeleteDB.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(30, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // RowContextMenu
            // 
            this.RowContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.RowContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteRowTSMItem});
            this.RowContextMenu.Name = "RowContextMenu";
            this.RowContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.RowContextMenu.Size = new System.Drawing.Size(159, 26);
            // 
            // DeleteRowTSMItem
            // 
            this.DeleteRowTSMItem.ForeColor = System.Drawing.Color.White;
            this.DeleteRowTSMItem.Name = "DeleteRowTSMItem";
            this.DeleteRowTSMItem.Size = new System.Drawing.Size(158, 22);
            this.DeleteRowTSMItem.Text = "Удалить строку";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(875, 615);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MenuPanel);
            this.Controls.Add(this.TreeViewExplorer);
            this.Controls.Add(this.GridContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(875, 615);
            this.Name = "MainForm";
            this.Text = "SQL Manager";
            ((System.ComponentModel.ISupportInitialize)(this.GridContent)).EndInit();
            this.DBContextMenu.ResumeLayout(false);
            this.TableContextMenu.ResumeLayout(false);
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.RowContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView GridContent;
        private System.Windows.Forms.TreeView TreeViewExplorer;
        private System.Windows.Forms.ContextMenuStrip DBContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CreateTableTSMItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteDBTSMItem;
        private System.Windows.Forms.ContextMenuStrip TableContextMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteTableTSMItem;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnAddDB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDeleteDB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip RowContextMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteRowTSMItem;
        private System.Windows.Forms.ToolStripMenuItem RenameDBTSMItem;
        private System.Windows.Forms.ToolStripMenuItem RenameTableTSMItem;
    }
}

