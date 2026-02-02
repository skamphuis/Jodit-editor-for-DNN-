<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JoditEditorOptions.ascx.cs" Inherits="DNNConnect.CKE.Controls.JoditEditorOptions" %>

<div class="dnnForm dnnJoditOptions dnnClear">
    <h2 class="dnnFormSectionHead">
        <a href="" class="dnnSectionExpanded">Jodit Editor Settings</a>
    </h2>
    <fieldset>
        <div class="dnnFormItem">
            <asp:Label ID="lblEditorMode" runat="server" ResourceKey="EditorMode" ControlName="ddlEditorMode" />
            <asp:DropDownList ID="ddlEditorMode" runat="server">
                <asp:ListItem Value="wysiwyg" ResourceKey="ModeWysiwyg">WYSIWYG</asp:ListItem>
                <asp:ListItem Value="source" ResourceKey="ModeSource">Source Code</asp:ListItem>
                <asp:ListItem Value="split" ResourceKey="ModeSplit">Split View</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblDefaultHeight" runat="server" ResourceKey="DefaultHeight" ControlName="txtDefaultHeight" />
            <asp:TextBox ID="txtDefaultHeight" runat="server" Text="400" />
            <asp:Label ID="lblHeightUnit" runat="server" Text="px" CssClass="dnnFormLabel" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblToolbarSticky" runat="server" ResourceKey="ToolbarSticky" ControlName="chkToolbarSticky" />
            <asp:CheckBox ID="chkToolbarSticky" runat="server" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblShowCharsCounter" runat="server" ResourceKey="ShowCharsCounter" ControlName="chkShowCharsCounter" />
            <asp:CheckBox ID="chkShowCharsCounter" runat="server" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblShowWordsCounter" runat="server" ResourceKey="ShowWordsCounter" ControlName="chkShowWordsCounter" />
            <asp:CheckBox ID="chkShowWordsCounter" runat="server" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblShowXPathInStatusbar" runat="server" ResourceKey="ShowXPathInStatusbar" ControlName="chkShowXPathInStatusbar" />
            <asp:CheckBox ID="chkShowXPathInStatusbar" runat="server" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblAllowResizeY" runat="server" ResourceKey="AllowResizeY" ControlName="chkAllowResizeY" />
            <asp:CheckBox ID="chkAllowResizeY" runat="server" Checked="true" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblSpellcheck" runat="server" ResourceKey="Spellcheck" ControlName="chkSpellcheck" />
            <asp:CheckBox ID="chkSpellcheck" runat="server" Checked="true" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblTheme" runat="server" ResourceKey="Theme" ControlName="ddlTheme" />
            <asp:DropDownList ID="ddlTheme" runat="server">
                <asp:ListItem Value="default" ResourceKey="ThemeDefault">Default</asp:ListItem>
                <asp:ListItem Value="dark" ResourceKey="ThemeDark">Dark</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblImageUploadEnabled" runat="server" ResourceKey="ImageUploadEnabled" ControlName="chkImageUploadEnabled" />
            <asp:CheckBox ID="chkImageUploadEnabled" runat="server" Checked="true" />
        </div>
        
        <div class="dnnFormItem">
            <asp:Label ID="lblMaxImageFileSize" runat="server" ResourceKey="MaxImageFileSize" ControlName="txtMaxImageFileSize" />
            <asp:TextBox ID="txtMaxImageFileSize" runat="server" Text="5" />
            <asp:Label ID="lblFileSizeUnit" runat="server" Text="MB" CssClass="dnnFormLabel" />
        </div>
    </fieldset>
    
    <h2 class="dnnFormSectionHead">
        <a href="" class="dnnSectionExpanded">Advanced Settings</a>
    </h2>
    <fieldset>
        <div class="dnnFormItem">
            <asp:Label ID="lblCustomConfig" runat="server" ResourceKey="CustomConfig" ControlName="txtCustomConfig" />
            <asp:TextBox ID="txtCustomConfig" runat="server" TextMode="MultiLine" Rows="10" CssClass="dnnFormInput" />
            <asp:Label ID="lblCustomConfigHelp" runat="server" ResourceKey="CustomConfigHelp" CssClass="dnnFormMessage dnnFormInfo" />
        </div>
    </fieldset>
</div>