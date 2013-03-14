<%@ Control Language="vb" AutoEventWireup="false" Codebehind="settings.ascx.vb" Inherits="DONEIN_NET.Redirector.Settings" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<DIV STYLE="text-align: left;">
	<BR />
	<ASP:LINKBUTTON RUNAT="server" ID="btn_create" />
	<BR />
	<BR />
	<ASP:DATAGRID RUNAT="server" ID="dg_portal_alias" WIDTH="640" BORDERWIDTH="0" CELLPADDING="2" CELLSPACING="1" AUTOGENERATECOLUMNS="False" ALLOWPAGING="False" ALLOWSORTING="False" >
		<HEADERSTYLE CSSCLASS="HEADER" FONT-BOLD="True" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
		<ITEMSTYLE BACKCOLOR="#FEFEFE" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
		<ALTERNATINGITEMSTYLE BACKCOLOR="#EEEEEE" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
		<COLUMNS>
			<ASP:BOUNDCOLUMN DATAFIELD="ID" READONLY="True" HEADERTEXT="" ITEMSTYLE-WIDTH="30" ITEMSTYLE-HORIZONTALALIGN="Right" VISIBLE="False" />
			<ASP:BOUNDCOLUMN DATAFIELD="vch_portal_alias" HEADERTEXT="" />
			<ASP:TEMPLATECOLUMN ITEMSTYLE-WIDTH="120" > 
				<ITEMTEMPLATE> 
					<ASP:LINKBUTTON RUNAT="server" ID="btn_dg_edit" COMMANDNAME="edit" COMMANDARGUMENT='<%# DataBinder.Eval(Container, "DataItem.ID") %>' RESOURCEKEY="pl_portal_alias_edit" />
					<SPAN STYLE="font-weight: bold;">
						&nbsp;&nbsp;|&nbsp;&nbsp;
					</SPAN>
					<ASP:LINKBUTTON RUNAT="server" ID="btn_dg_delete" COMMANDNAME="delete" COMMANDARGUMENT='<%# DataBinder.Eval(Container, "DataItem.ID") %>' RESOURCEKEY="pl_portal_alias_delete" /> 
				</ITEMTEMPLATE> 
			</ASP:TEMPLATECOLUMN>
		</COLUMNS>
	</ASP:DATAGRID>

	<TABLE RUNAT="server" ID="tbl_portal_alias_edit" BORDER="0" CELLPADDING="3" CELLSPACING="1">
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<DNN:LABEL RUNAT="server" ID="pl_portal_alias" CONTROLNAME="ddl_portal_alias" SUFFIX=":" />
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_portal_alias" CSSCLASS="NormalText" WIDTH="360" />							
			</TD>        
		</TR>
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<DNN:LABEL RUNAT="server" ID="pl_portal_alias_target" CONTROLNAME="ddl_portal_alias_target" SUFFIX=":" />
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_portal_alias_target" CSSCLASS="NormalText" WIDTH="360" />							
			</TD>        
		</TR>
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<INPUT TYPE="hidden" RUNAT="server" ID="txt_ID" NAME="txt_ID"/>
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:LINKBUTTON RUNAT="server" ID="btn_update" />
				&nbsp;&nbsp;	
				<ASP:LINKBUTTON RUNAT="server" ID="btn_cancel" />
			</TD>        
		</TR>
	</TABLE>
</DIV>

