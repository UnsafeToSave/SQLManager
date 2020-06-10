﻿namespace SqlManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GridContent = new System.Windows.Forms.DataGridView();
            this.TreeViewExplorer = new System.Windows.Forms.TreeView();
            this.ExplorerDBContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CreateTableTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameDBTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteDBTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExplorerTableContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RenameTableTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteTableTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddDB = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnDeleteDB = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.GridRowContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FindValueTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteRowTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridContent)).BeginInit();
            this.ExplorerDBContext.SuspendLayout();
            this.ExplorerTableContext.SuspendLayout();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.GridRowContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridContent
            // 
            this.GridContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridContent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.GridContent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridContent.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridContent.EnableHeadersVisualStyles = false;
            this.GridContent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(51)))));
            this.GridContent.Location = new System.Drawing.Point(181, 36);
            this.GridContent.MultiSelect = false;
            this.GridContent.Name = "GridContent";
            this.GridContent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridContent.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.GridContent.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GridContent.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.GridContent.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.GridContent.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.GridContent.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.GridContent.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridContent.Size = new System.Drawing.Size(712, 589);
            this.GridContent.TabIndex = 1;
            // 
            // TreeViewExplorer
            // 
            this.TreeViewExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeViewExplorer.Location = new System.Drawing.Point(12, 36);
            this.TreeViewExplorer.Name = "TreeViewExplorer";
            this.TreeViewExplorer.ShowPlusMinus = false;
            this.TreeViewExplorer.Size = new System.Drawing.Size(151, 553);
            this.TreeViewExplorer.TabIndex = 5;
            // 
            // ExplorerDBContext
            // 
            this.ExplorerDBContext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.ExplorerDBContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateTableTSMItem,
            this.RenameDBTSMItem,
            this.DeleteDBTSMItem});
            this.ExplorerDBContext.Name = "DBСontextMenu";
            this.ExplorerDBContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ExplorerDBContext.Size = new System.Drawing.Size(189, 70);
            // 
            // CreateTableTSMItem
            // 
            this.CreateTableTSMItem.ForeColor = System.Drawing.Color.White;
            this.CreateTableTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("CreateTableTSMItem.Image")));
            this.CreateTableTSMItem.Name = "CreateTableTSMItem";
            this.CreateTableTSMItem.Size = new System.Drawing.Size(188, 22);
            this.CreateTableTSMItem.Text = "Создать таблицу";
            // 
            // RenameDBTSMItem
            // 
            this.RenameDBTSMItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RenameDBTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("RenameDBTSMItem.Image")));
            this.RenameDBTSMItem.Name = "RenameDBTSMItem";
            this.RenameDBTSMItem.Size = new System.Drawing.Size(188, 22);
            this.RenameDBTSMItem.Text = "Переименовать базу";
            // 
            // DeleteDBTSMItem
            // 
            this.DeleteDBTSMItem.ForeColor = System.Drawing.Color.White;
            this.DeleteDBTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteDBTSMItem.Image")));
            this.DeleteDBTSMItem.Name = "DeleteDBTSMItem";
            this.DeleteDBTSMItem.Size = new System.Drawing.Size(188, 22);
            this.DeleteDBTSMItem.Text = "Удалить базу";
            // 
            // ExplorerTableContext
            // 
            this.ExplorerTableContext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.ExplorerTableContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RenameTableTSMItem,
            this.DeleteTableTSMItem});
            this.ExplorerTableContext.Name = "TableContextMenu";
            this.ExplorerTableContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ExplorerTableContext.Size = new System.Drawing.Size(210, 48);
            // 
            // RenameTableTSMItem
            // 
            this.RenameTableTSMItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.RenameTableTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("RenameTableTSMItem.Image")));
            this.RenameTableTSMItem.Name = "RenameTableTSMItem";
            this.RenameTableTSMItem.Size = new System.Drawing.Size(209, 22);
            this.RenameTableTSMItem.Text = "Переименовать таблицу";
            // 
            // DeleteTableTSMItem
            // 
            this.DeleteTableTSMItem.ForeColor = System.Drawing.Color.White;
            this.DeleteTableTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteTableTSMItem.Image")));
            this.DeleteTableTSMItem.Name = "DeleteTableTSMItem";
            this.DeleteTableTSMItem.Size = new System.Drawing.Size(209, 22);
            this.DeleteTableTSMItem.Text = "Удалить таблицу";
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
            this.MenuPanel.Size = new System.Drawing.Size(905, 30);
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
            this.btnMinimize.Location = new System.Drawing.Point(841, 0);
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
            this.btnClose.Location = new System.Drawing.Point(873, 0);
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.panel1.Controls.Add(this.btnDisconnect);
            this.panel1.Controls.Add(this.btnDeleteDB);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnAddDB);
            this.panel1.Location = new System.Drawing.Point(12, 595);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 30);
            this.panel1.TabIndex = 9;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDisconnect.BackgroundImage")));
            this.btnDisconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDisconnect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDisconnect.FlatAppearance.BorderSize = 0;
            this.btnDisconnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))));
            this.btnDisconnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(209)))), ((int)(((byte)(77)))));
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.Location = new System.Drawing.Point(90, 0);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(30, 30);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.UseVisualStyleBackColor = true;
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
            // GridRowContext
            // 
            this.GridRowContext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.GridRowContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindValueTSMItem,
            this.FilterTSMItem,
            this.DeleteRowTSMItem});
            this.GridRowContext.Name = "RowContextMenu";
            this.GridRowContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.GridRowContext.Size = new System.Drawing.Size(159, 70);
            // 
            // FindValueTSMItem
            // 
            this.FindValueTSMItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FindValueTSMItem.Name = "FindValueTSMItem";
            this.FindValueTSMItem.Size = new System.Drawing.Size(158, 22);
            this.FindValueTSMItem.Text = "Поиск значния";
            // 
            // FilterTSMItem
            // 
            this.FilterTSMItem.ForeColor = System.Drawing.Color.White;
            this.FilterTSMItem.Name = "FilterTSMItem";
            this.FilterTSMItem.Size = new System.Drawing.Size(158, 22);
            this.FilterTSMItem.Text = "Фильтр";
            // 
            // DeleteRowTSMItem
            // 
            this.DeleteRowTSMItem.ForeColor = System.Drawing.Color.White;
            this.DeleteRowTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRowTSMItem.Image")));
            this.DeleteRowTSMItem.Name = "DeleteRowTSMItem";
            this.DeleteRowTSMItem.Size = new System.Drawing.Size(158, 22);
            this.DeleteRowTSMItem.Text = "Удалить строку";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(64)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(905, 637);
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
            this.ExplorerDBContext.ResumeLayout(false);
            this.ExplorerTableContext.ResumeLayout(false);
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.GridRowContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView GridContent;
        private System.Windows.Forms.TreeView TreeViewExplorer;
        private System.Windows.Forms.ContextMenuStrip ExplorerDBContext;
        private System.Windows.Forms.ToolStripMenuItem CreateTableTSMItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteDBTSMItem;
        private System.Windows.Forms.ContextMenuStrip ExplorerTableContext;
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
        private System.Windows.Forms.ContextMenuStrip GridRowContext;
        private System.Windows.Forms.ToolStripMenuItem DeleteRowTSMItem;
        private System.Windows.Forms.ToolStripMenuItem RenameDBTSMItem;
        private System.Windows.Forms.ToolStripMenuItem RenameTableTSMItem;
        private System.Windows.Forms.ToolStripMenuItem FindValueTSMItem;
        private System.Windows.Forms.ToolStripMenuItem FilterTSMItem;
        private System.Windows.Forms.Button btnDisconnect;
    }
}

