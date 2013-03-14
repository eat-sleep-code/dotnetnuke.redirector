Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.ModuleSettingsBase
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Services.Exceptions
Imports System.Web.UI.WebControls


Namespace DONEIN_NET.Redirector

	Public Class Settings
		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable



		#Region " Declare: Shared Classes "
			
			Private database As New database(System.Configuration.ConfigurationSettings.AppSettings("SiteSqlServer"), "SqlClient") '// THIS WILL NEED TO CHANGE THIS FOR ASP.NET 2.0 COMPLIANCE
			
		#End Region



		#Region " Declare: Local Objects "
		
			Protected WithEvents btn_create As System.Web.UI.WebControls.LinkButton		
			Protected WithEvents btn_update As System.Web.UI.WebControls.LinkButton		
			Protected WithEvents btn_cancel As System.Web.UI.WebControls.LinkButton		
			
			Protected WithEvents dg_portal_alias As System.Web.UI.WebControls.DataGrid
			Protected WithEvents tbl_portal_alias_edit As System.Web.UI.HtmlControls.HtmlTable
				
			Protected pl_portal_alias As UI.UserControls.LabelControl
			Protected pl_portal_alias_target As UI.UserControls.LabelControl
			
			Protected WithEvents txt_ID As System.Web.UI.HtmlControls.HtmlInputHidden
			Protected WithEvents ddl_portal_alias As System.Web.UI.WebControls.DropDownList
			Protected WithEvents ddl_portal_alias_target As System.Web.UI.WebControls.DropDownList
			
			Private obj_user As New UserController
			Private obj_user_info As UserInfo  = obj_user.GetCurrentUserInfo
			
		#End Region
		
		

		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				Try
					If Not IsPostBack Then
						If ModuleId > 0 Then
							module_localize() '// LOCALIZE THE MODULE
							dg_portal_alias_bind() '// BIND THE DATAGRID
							ddl_portal_alias_target_bind() '// BIND THE LIST OF TABS
							ddl_portal_alias_bind() '// BIND THE LIST OF ALIASES
							tbl_portal_alias_edit.Visible = False
														
						End If
					End If
				Catch ex As Exception
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub

		#End Region



		#Region " Page: Localize "

 			Private Sub module_localize()
 				
 				btn_create.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_create.Text", LocalResourceFile)
				btn_update.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_update.Text", LocalResourceFile)
				btn_cancel.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_cancel.Text", LocalResourceFile)
				
				pl_portal_alias.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_portal_alias.Text", LocalResourceFile)
				pl_portal_alias_target.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_portal_alias_target.Text", LocalResourceFile)
				
			End Sub 

		#End Region



		#Region " Bind: DataGrid (dg_portal_alias) "

 			Private Sub dg_portal_alias_bind()
 				Services.Localization.Localization.LocalizeDataGrid(dg_portal_alias, Me.LocalResourceFile)
 				
				Dim dt_portal_alias As New DataTable
				database.CreateCommand("donein_redirector_R", CommandType.StoredProcedure)
				database.AddParameter("@int_ID", 0)
				database.AddParameter("@vch_portal_alias", "")
				database.AddParameter("@int_module", ModuleID)
				database.Execute(dt_portal_alias)
				If dt_portal_alias.Rows.Count > 0 Then
					dg_portal_alias.DataSource = dt_portal_alias
					dg_portal_alias.DataBind
					dg_portal_alias.Visible = True
				Else
					dg_portal_alias.Visible = False
				End If			
				btn_create.Visible = True
			End Sub 

		#End Region
		
		
		
		#Region " Handle: DataGrid ItemCommand (dg_portal_alias) "

 			Private Sub dg_portal_alias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_portal_alias.ItemCommand
 				If e.CommandName.ToLower.Trim = "delete" Then
 					database.CreateCommand("donein_redirector_CUD", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", (CType(e.CommandArgument,Integer) * -1))
					database.ExecuteNonQuery()
					dg_portal_alias_bind()
				Else
					Dim dt_portal_alias_edit As New DataTable
					database.CreateCommand("donein_redirector_R", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", CType(e.CommandArgument,Integer))
					database.AddParameter("@vch_portal_alias", "")
					database.AddParameter("@int_module", ModuleID)
					database.Execute(dt_portal_alias_edit)
					If dt_portal_alias_edit.Rows.Count > 0 Then
						txt_ID.Value = dt_portal_alias_edit.Rows(0).Item("ID").ToString
						Try 
							ddl_portal_alias.SelectedValue = dt_portal_alias_edit.Rows(0).Item("vch_portal_alias").ToString	
						Catch ex As Exception '// PORTAL ALIAS MOST LIKELY WAS CHANGED
							ddl_portal_alias.SelectedIndex = 0
						End Try
						Try 
							ddl_portal_alias_target.SelectedValue = dt_portal_alias_edit.Rows(0).Item("int_portal_alias_target").ToString		
						Catch ex As Exception '// PAGE WAS MOST LIKELY REMOVED
							ddl_portal_alias_target.SelectedIndex = 0
						End Try
						
						dg_portal_alias.Visible = False
						btn_create.Visible = False
						tbl_portal_alias_edit.Visible = True
					End If					
				End If 				
			End Sub 

		#End Region
		
		

		#Region " Handle: Update Button (btn_update) "

  			Private Sub btn_update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_update.Click	
				Try
					Dim dt_portal_alias_new As New DataTable
					database.CreateCommand("donein_redirector_CUD", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", CType(txt_ID.Value, Integer))
					database.AddParameter("@vch_portal_alias", ddl_portal_alias.SelectedValue)
					database.AddParameter("@int_portal_alias_target", CType(ddl_portal_alias_target.SelectedValue, Integer))
					database.AddParameter("@int_module", ModuleID)
					database.AddParameter("@int_author", CType(obj_user_info.UserID, Integer))
					database.Execute(dt_portal_alias_new)
					If dt_portal_alias_new.Rows.Count > 0 Then
						dg_portal_alias_bind()	
						tbl_portal_alias_edit.Visible = False
					Else
						tbl_portal_alias_edit.Visible = False
						Exit Sub													
					End If		
				Catch ex As Exception
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub 

		#End Region
		
		
		
		#Region " Handle: Create Button (btn_create) "

  			Private Sub btn_create_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_create.Click	
				Try
					dg_portal_alias.Visible = False
					btn_create.Visible = False
					tbl_portal_alias_edit.Visible = True
					txt_ID.Value = "0"
					ddl_portal_alias.SelectedIndex = 0
					ddl_portal_alias_target.SelectedIndex = 0
				Catch ex As Exception
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub 

		#End Region


		
		#Region " Handle: Cancel Button (btn_cancel) "

 			Private Sub btn_cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_cancel.Click	
				Try
					dg_portal_alias.Visible = True
					btn_create.Visible = True
					tbl_portal_alias_edit.Visible = False	
				Catch ex As Exception		  
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub 

		 #End Region


         
        #Region " Bind: Tab Dropdown List (ddl_portal_alias_target) "
		
			Private Sub ddl_portal_alias_target_bind()

				Dim obj_tab_collection As ArrayList = GetPortalTabs(PortalSettings.DesktopTabs, False, True, False, True)
				Dim obj_tab_info As TabInfo 
				For Each obj_tab_info In obj_tab_collection
					ddl_portal_alias_target.Items.Add(New ListItem(obj_tab_info.TabName, obj_tab_info.TabID.ToString))
				Next				

			End Sub			
			
		#End Region
		
		
		
		    
        #Region " Bind: Portal Alias Dropdown List (ddl_portal_alias) "
		
			Private Sub ddl_portal_alias_bind()

				Dim obj_portalalias As New PortalAliasController
				Dim obj_portalalias_collection As PortalAliasCollection = obj_portalalias.GetPortalAliases
				Dim obj_portalalias_info As DictionaryEntry
				For Each obj_portalalias_info In obj_portalalias_collection
					ddl_portal_alias.Items.Add(New ListItem(obj_portalalias_info.Key.ToString.Trim, obj_portalalias_info.Key.ToString.Trim)) 
				Next
								
			End Sub			
			
		#End Region
		
		
		
		    
            
		#Region " Web Form Designer Generated Code "

			'This call is required by the Web Form Designer.
			<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

			End Sub

			'NOTE: The following placeholder declaration is required by the Web Form Designer.
			'Do not delete or move it.
			Private designerPlaceholderDeclaration As System.Object

			Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
				'CODEGEN: This method call is required by the Web Form Designer
				'Do not modify it using the code editor.
				InitializeComponent()
			End Sub

		#End Region



		#Region "Optional Interfaces"

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						'Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_update.Text", LocalResourceFile), "", "", "", get_update_url("DONEIN_NET\Redirector"), False, DotNetNuke.Security.SecurityAccessLevel.Host, True, True) '// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
					Return Actions
				End Get
			End Property

			'// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
			'Private Function get_update_url(ByVal module_name As String) As String
			'	Dim obj_tab As DotNetNuke.Entities.Tabs.TabInfo
			'	With New DotNetNuke.Entities.Tabs.TabController 
			'		obj_tab = .GetTabByName("DNN Update", DotNetNuke.Common.Utilities.Null.NullInteger)
			'	End With

			'	If obj_tab Is Nothing Then
			'		Return "http://www.dnnupdate.com/module-intro.content?module=" + module_name
			'	Else
			'		Return obj_tab.Url + "?tabid=" + obj_tab.TabID.ToString + "&module=" + module_name
			'	End If
			'End Function

		#End Region


		
	End Class

End NameSpace
