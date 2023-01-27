<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDingIntegrator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDingIntegrator))
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnGetPromotionsDesc = New System.Windows.Forms.Button()
        Me.btnGetProductDescriptions = New System.Windows.Forms.Button()
        Me.btnGetProducts = New System.Windows.Forms.Button()
        Me.btnGetProviders = New System.Windows.Forms.Button()
        Me.btnGetAllRegions = New System.Windows.Forms.Button()
        Me.btnGetPromotions = New System.Windows.Forms.Button()
        Me.btnGetRegionsByCountry = New System.Windows.Forms.Button()
        Me.btnGetCurrencies = New System.Windows.Forms.Button()
        Me.btnAuto = New System.Windows.Forms.Button()
        Me.btnFetchDBData = New System.Windows.Forms.Button()
        Me.btnGetCountries = New System.Windows.Forms.Button()
        Me.btnGetProviderStatus = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.DGVUsers = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGVUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(763, 78)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(299, 459)
        Me.txtLog.TabIndex = 7
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(1072, 63)
        Me.lblHeader.TabIndex = 11
        Me.lblHeader.Text = "Ding Integrator"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnGetPromotionsDesc)
        Me.Panel1.Controls.Add(Me.btnGetProductDescriptions)
        Me.Panel1.Controls.Add(Me.btnGetProducts)
        Me.Panel1.Controls.Add(Me.btnGetProviders)
        Me.Panel1.Controls.Add(Me.btnGetAllRegions)
        Me.Panel1.Controls.Add(Me.btnGetPromotions)
        Me.Panel1.Controls.Add(Me.btnGetRegionsByCountry)
        Me.Panel1.Controls.Add(Me.btnGetCurrencies)
        Me.Panel1.Controls.Add(Me.btnAuto)
        Me.Panel1.Controls.Add(Me.btnFetchDBData)
        Me.Panel1.Controls.Add(Me.btnGetCountries)
        Me.Panel1.Controls.Add(Me.btnGetProviderStatus)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 573)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1072, 106)
        Me.Panel1.TabIndex = 12
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(839, 61)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(230, 33)
        Me.Button1.TabIndex = 29
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnGetPromotionsDesc
        '
        Me.btnGetPromotionsDesc.Location = New System.Drawing.Point(509, 52)
        Me.btnGetPromotionsDesc.Name = "btnGetPromotionsDesc"
        Me.btnGetPromotionsDesc.Size = New System.Drawing.Size(126, 33)
        Me.btnGetPromotionsDesc.TabIndex = 28
        Me.btnGetPromotionsDesc.Text = "Get Promotions Desc"
        Me.btnGetPromotionsDesc.UseVisualStyleBackColor = True
        '
        'btnGetProductDescriptions
        '
        Me.btnGetProductDescriptions.Location = New System.Drawing.Point(354, 52)
        Me.btnGetProductDescriptions.Name = "btnGetProductDescriptions"
        Me.btnGetProductDescriptions.Size = New System.Drawing.Size(149, 33)
        Me.btnGetProductDescriptions.TabIndex = 27
        Me.btnGetProductDescriptions.Text = "Get Product Descriptions"
        Me.btnGetProductDescriptions.UseVisualStyleBackColor = True
        '
        'btnGetProducts
        '
        Me.btnGetProducts.Location = New System.Drawing.Point(240, 52)
        Me.btnGetProducts.Name = "btnGetProducts"
        Me.btnGetProducts.Size = New System.Drawing.Size(108, 33)
        Me.btnGetProducts.TabIndex = 26
        Me.btnGetProducts.Text = "Get Products"
        Me.btnGetProducts.UseVisualStyleBackColor = True
        '
        'btnGetProviders
        '
        Me.btnGetProviders.Location = New System.Drawing.Point(12, 52)
        Me.btnGetProviders.Name = "btnGetProviders"
        Me.btnGetProviders.Size = New System.Drawing.Size(108, 33)
        Me.btnGetProviders.TabIndex = 25
        Me.btnGetProviders.Text = "Get Providers"
        Me.btnGetProviders.UseVisualStyleBackColor = True
        '
        'btnGetAllRegions
        '
        Me.btnGetAllRegions.Location = New System.Drawing.Point(383, 13)
        Me.btnGetAllRegions.Name = "btnGetAllRegions"
        Me.btnGetAllRegions.Size = New System.Drawing.Size(138, 33)
        Me.btnGetAllRegions.TabIndex = 24
        Me.btnGetAllRegions.Text = "Get All Regions"
        Me.btnGetAllRegions.UseVisualStyleBackColor = True
        '
        'btnGetPromotions
        '
        Me.btnGetPromotions.Location = New System.Drawing.Point(527, 13)
        Me.btnGetPromotions.Name = "btnGetPromotions"
        Me.btnGetPromotions.Size = New System.Drawing.Size(108, 33)
        Me.btnGetPromotions.TabIndex = 23
        Me.btnGetPromotions.Text = "Get Promotions"
        Me.btnGetPromotions.UseVisualStyleBackColor = True
        '
        'btnGetRegionsByCountry
        '
        Me.btnGetRegionsByCountry.Location = New System.Drawing.Point(239, 13)
        Me.btnGetRegionsByCountry.Name = "btnGetRegionsByCountry"
        Me.btnGetRegionsByCountry.Size = New System.Drawing.Size(138, 33)
        Me.btnGetRegionsByCountry.TabIndex = 22
        Me.btnGetRegionsByCountry.Text = "Get Regions By Country"
        Me.btnGetRegionsByCountry.UseVisualStyleBackColor = True
        '
        'btnGetCurrencies
        '
        Me.btnGetCurrencies.Location = New System.Drawing.Point(125, 13)
        Me.btnGetCurrencies.Name = "btnGetCurrencies"
        Me.btnGetCurrencies.Size = New System.Drawing.Size(108, 33)
        Me.btnGetCurrencies.TabIndex = 21
        Me.btnGetCurrencies.Text = "Get Currencies"
        Me.btnGetCurrencies.UseVisualStyleBackColor = True
        '
        'btnAuto
        '
        Me.btnAuto.Location = New System.Drawing.Point(952, 13)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(108, 33)
        Me.btnAuto.TabIndex = 20
        Me.btnAuto.Text = "Automatic Mode"
        Me.btnAuto.UseVisualStyleBackColor = True
        '
        'btnFetchDBData
        '
        Me.btnFetchDBData.Location = New System.Drawing.Point(839, 13)
        Me.btnFetchDBData.Name = "btnFetchDBData"
        Me.btnFetchDBData.Size = New System.Drawing.Size(108, 33)
        Me.btnFetchDBData.TabIndex = 19
        Me.btnFetchDBData.Text = "Fetch DB Data"
        Me.btnFetchDBData.UseVisualStyleBackColor = True
        Me.btnFetchDBData.Visible = False
        '
        'btnGetCountries
        '
        Me.btnGetCountries.Location = New System.Drawing.Point(11, 13)
        Me.btnGetCountries.Name = "btnGetCountries"
        Me.btnGetCountries.Size = New System.Drawing.Size(108, 33)
        Me.btnGetCountries.TabIndex = 18
        Me.btnGetCountries.Text = "Get Countries"
        Me.btnGetCountries.UseVisualStyleBackColor = True
        Me.btnGetCountries.Visible = False
        '
        'btnGetProviderStatus
        '
        Me.btnGetProviderStatus.Location = New System.Drawing.Point(126, 52)
        Me.btnGetProviderStatus.Name = "btnGetProviderStatus"
        Me.btnGetProviderStatus.Size = New System.Drawing.Size(108, 33)
        Me.btnGetProviderStatus.TabIndex = 16
        Me.btnGetProviderStatus.Text = "Get Provider Status"
        Me.btnGetProviderStatus.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtURL)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 197)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Static Data"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(141, 148)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(581, 20)
        Me.txtURL.TabIndex = 19
        Me.txtURL.Text = "https://edts.ezedistributor.com/api/EdtsV3/"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 151)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "URL"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(141, 58)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(212, 20)
        Me.txtPassword.TabIndex = 17
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(21, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(141, 30)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(212, 20)
        Me.txtUserName.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(21, 33)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "User Name"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(9, 289)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(748, 104)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Variable Data"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(952, 544)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(108, 23)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'DGVUsers
        '
        Me.DGVUsers.AllowUserToAddRows = False
        Me.DGVUsers.AllowUserToDeleteRows = False
        Me.DGVUsers.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVUsers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVUsers.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.DGVUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGVUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveBorder
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVUsers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVUsers.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVUsers.Location = New System.Drawing.Point(9, 399)
        Me.DGVUsers.Name = "DGVUsers"
        Me.DGVUsers.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveBorder
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVUsers.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVUsers.RowHeadersVisible = False
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Transparent
        Me.DGVUsers.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DGVUsers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DGVUsers.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVUsers.RowTemplate.ReadOnly = True
        Me.DGVUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVUsers.Size = New System.Drawing.Size(748, 157)
        Me.DGVUsers.TabIndex = 16
        Me.DGVUsers.TabStop = False
        '
        'frmDingIntegrator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 679)
        Me.Controls.Add(Me.DGVUsers)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.txtLog)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDingIntegrator"
        Me.Text = "Ding Integrator"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DGVUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLog As TextBox
    Friend WithEvents lblHeader As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtURL As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnClear As Button
    Friend WithEvents btnGetProviderStatus As Button
    Friend WithEvents btnGetCountries As Button
    Friend WithEvents btnFetchDBData As Button
    Friend WithEvents DGVUsers As DataGridView
    Friend WithEvents btnAuto As Button
    Friend WithEvents btnGetPromotions As Button
    Friend WithEvents btnGetRegionsByCountry As Button
    Friend WithEvents btnGetCurrencies As Button
    Friend WithEvents btnGetAllRegions As Button
    Friend WithEvents btnGetProviders As Button
    Friend WithEvents btnGetProductDescriptions As Button
    Friend WithEvents btnGetProducts As Button
    Friend WithEvents btnGetPromotionsDesc As Button
    Friend WithEvents Button1 As Button
End Class
