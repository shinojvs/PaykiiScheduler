<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOraFusion
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOraFusion))
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSourceID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDeviceID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSupplierDeviceEvent = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtReporterIDType = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtReporterID = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtRequestNumber = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtpRequestTimestamp = New System.Windows.Forms.DateTimePicker()
        Me.dtpEventDateTime = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSendTestData = New System.Windows.Forms.Button()
        Me.btnSendDataDB = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGVUsers = New System.Windows.Forms.DataGridView()
        Me.btnFetchDBData = New System.Windows.Forms.Button()
        Me.btnAuto = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.lblHeader.Text = "Suprema - Oracle"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel1.Controls.Add(Me.btnAuto)
        Me.Panel1.Controls.Add(Me.btnFetchDBData)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnSendDataDB)
        Me.Panel1.Controls.Add(Me.btnSendTestData)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 574)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1072, 58)
        Me.Panel1.TabIndex = 12
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtURL)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtValue)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtReporterIDType)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtSupplierDeviceEvent)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtDeviceID)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSourceID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 197)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Static Data"
        '
        'txtSourceID
        '
        Me.txtSourceID.Location = New System.Drawing.Point(141, 29)
        Me.txtSourceID.Name = "txtSourceID"
        Me.txtSourceID.Size = New System.Drawing.Size(212, 20)
        Me.txtSourceID.TabIndex = 3
        Me.txtSourceID.Text = "TEST_SUPPLIER_DEVICE"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Source ID"
        '
        'txtDeviceID
        '
        Me.txtDeviceID.Location = New System.Drawing.Point(141, 57)
        Me.txtDeviceID.Name = "txtDeviceID"
        Me.txtDeviceID.Size = New System.Drawing.Size(212, 20)
        Me.txtDeviceID.TabIndex = 5
        Me.txtDeviceID.Text = "100"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Device ID"
        '
        'txtSupplierDeviceEvent
        '
        Me.txtSupplierDeviceEvent.Location = New System.Drawing.Point(141, 85)
        Me.txtSupplierDeviceEvent.Name = "txtSupplierDeviceEvent"
        Me.txtSupplierDeviceEvent.Size = New System.Drawing.Size(212, 20)
        Me.txtSupplierDeviceEvent.TabIndex = 7
        Me.txtSupplierDeviceEvent.Tag = ""
        Me.txtSupplierDeviceEvent.Text = "TEST_SUPPLIER_DEVICE_IN"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Supplier Device Event"
        '
        'txtReporterIDType
        '
        Me.txtReporterIDType.Location = New System.Drawing.Point(141, 116)
        Me.txtReporterIDType.Name = "txtReporterIDType"
        Me.txtReporterIDType.Size = New System.Drawing.Size(212, 20)
        Me.txtReporterIDType.TabIndex = 9
        Me.txtReporterIDType.Text = "PERSON"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Reporter ID Type"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(510, 57)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(212, 20)
        Me.txtValue.TabIndex = 13
        Me.txtValue.Text = "HPH_UAE_Regular Hours Worked"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(390, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Value"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(510, 29)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(212, 20)
        Me.txtName.TabIndex = 11
        Me.txtName.Text = "PayrollTimeType"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(390, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Name"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(510, 116)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(212, 20)
        Me.txtPassword.TabIndex = 17
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(390, 119)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(510, 88)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(212, 20)
        Me.txtUserName.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(390, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "User Name"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(141, 148)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(581, 20)
        Me.txtURL.TabIndex = 19
        Me.txtURL.Text = "https://fa-ercf-dev2-saasfaprod1.fa.ocs.oraclecloud.com/hcmRestApi/resources/11.1" &
    "3.18.05/timeEventRequests"
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpEventDateTime)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.dtpRequestTimestamp)
        Me.GroupBox2.Controls.Add(Me.txtReporterID)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.txtRequestNumber)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 289)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(748, 104)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Variable Data"
        '
        'txtReporterID
        '
        Me.txtReporterID.Location = New System.Drawing.Point(510, 57)
        Me.txtReporterID.Name = "txtReporterID"
        Me.txtReporterID.Size = New System.Drawing.Size(212, 20)
        Me.txtReporterID.TabIndex = 25
        Me.txtReporterID.Text = "9028"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(390, 60)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 13)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Reporter ID"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(24, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(101, 13)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Request Timestamp"
        '
        'txtRequestNumber
        '
        Me.txtRequestNumber.Location = New System.Drawing.Point(144, 30)
        Me.txtRequestNumber.Name = "txtRequestNumber"
        Me.txtRequestNumber.Size = New System.Drawing.Size(212, 20)
        Me.txtRequestNumber.TabIndex = 19
        Me.txtRequestNumber.Text = "201"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(24, 33)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 13)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "Request Number"
        '
        'dtpRequestTimestamp
        '
        Me.dtpRequestTimestamp.CustomFormat = "dd-MMM-yyyy HH:mm:ss"
        Me.dtpRequestTimestamp.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRequestTimestamp.Location = New System.Drawing.Point(144, 60)
        Me.dtpRequestTimestamp.Name = "dtpRequestTimestamp"
        Me.dtpRequestTimestamp.Size = New System.Drawing.Size(212, 20)
        Me.dtpRequestTimestamp.TabIndex = 34
        '
        'dtpEventDateTime
        '
        Me.dtpEventDateTime.CustomFormat = "dd-MMM-yyyy HH:mm:ss"
        Me.dtpEventDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEventDateTime.Location = New System.Drawing.Point(510, 29)
        Me.dtpEventDateTime.Name = "dtpEventDateTime"
        Me.dtpEventDateTime.Size = New System.Drawing.Size(212, 20)
        Me.dtpEventDateTime.TabIndex = 36
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(390, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 13)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "Event Date Time"
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
        'btnSendTestData
        '
        Me.btnSendTestData.Location = New System.Drawing.Point(291, 13)
        Me.btnSendTestData.Name = "btnSendTestData"
        Me.btnSendTestData.Size = New System.Drawing.Size(108, 33)
        Me.btnSendTestData.TabIndex = 16
        Me.btnSendTestData.Text = "Send Test Data"
        Me.btnSendTestData.UseVisualStyleBackColor = True
        '
        'btnSendDataDB
        '
        Me.btnSendDataDB.Location = New System.Drawing.Point(547, 13)
        Me.btnSendDataDB.Name = "btnSendDataDB"
        Me.btnSendDataDB.Size = New System.Drawing.Size(108, 33)
        Me.btnSendDataDB.TabIndex = 17
        Me.btnSendDataDB.Text = "Send DB Data"
        Me.btnSendDataDB.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(46, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 33)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Test"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'DGVUsers
        '
        Me.DGVUsers.AllowUserToAddRows = False
        Me.DGVUsers.AllowUserToDeleteRows = False
        Me.DGVUsers.AllowUserToResizeRows = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVUsers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DGVUsers.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.DGVUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGVUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ActiveBorder
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVUsers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DGVUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVUsers.Location = New System.Drawing.Point(9, 399)
        Me.DGVUsers.Name = "DGVUsers"
        Me.DGVUsers.ReadOnly = True
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.ActiveBorder
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVUsers.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DGVUsers.RowHeadersVisible = False
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Transparent
        Me.DGVUsers.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.DGVUsers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DGVUsers.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVUsers.RowTemplate.ReadOnly = True
        Me.DGVUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVUsers.Size = New System.Drawing.Size(748, 157)
        Me.DGVUsers.TabIndex = 16
        Me.DGVUsers.TabStop = False
        '
        'btnFetchDBData
        '
        Me.btnFetchDBData.Location = New System.Drawing.Point(419, 13)
        Me.btnFetchDBData.Name = "btnFetchDBData"
        Me.btnFetchDBData.Size = New System.Drawing.Size(108, 33)
        Me.btnFetchDBData.TabIndex = 19
        Me.btnFetchDBData.Text = "Fetch DB Data"
        Me.btnFetchDBData.UseVisualStyleBackColor = True
        '
        'btnAuto
        '
        Me.btnAuto.Location = New System.Drawing.Point(675, 13)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(108, 33)
        Me.btnAuto.TabIndex = 20
        Me.btnAuto.Text = "Automatic Mode"
        Me.btnAuto.UseVisualStyleBackColor = True
        '
        'frmOraFusion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 632)
        Me.Controls.Add(Me.DGVUsers)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.txtLog)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmOraFusion"
        Me.Text = "Suprema - Oracle"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DGVUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLog As TextBox
    Friend WithEvents lblHeader As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtValue As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtReporterIDType As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtSupplierDeviceEvent As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDeviceID As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSourceID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtURL As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtReporterID As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents txtRequestNumber As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents dtpEventDateTime As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents dtpRequestTimestamp As DateTimePicker
    Friend WithEvents btnClear As Button
    Friend WithEvents btnSendDataDB As Button
    Friend WithEvents btnSendTestData As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnFetchDBData As Button
    Friend WithEvents DGVUsers As DataGridView
    Friend WithEvents btnAuto As Button
End Class
