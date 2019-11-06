<%@ Page Title="یادآوری رمز عبور" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
            <div class="overlay">
                <section id="imageLoading">
                    <%--<asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/ajax-loader.gif" ImageAlign="AbsMiddle" ToolTip="Loading..." />--%>
                    <%--Loading...Please wait!--%>
                </section>

            </div>
             <div  class="alert alert-warning" id="divMenuTop" onload="divMenuTop_Load" runat="server">
        <asp:Literal  runat="server" ID="LitMenuTop" />
    </div>
        <asp:ValidationSummary ID="ForgotValidationSummery" runat="server" CssClass="validationSummery alert alert-danger"
                EnableClientScript="true" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="false"
                HeaderText="<%$Resources:Messages,FixBelowErrors%>"  />
            <fieldset>
                <legend><asp:Literal ID="litLegend" runat="server" Text="<%$Resources:Legends,ForgotPassword%>" />
                </legend>
                <div class="field">
                    <div class="caption">
                        <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:Captions,Email%>" CssClass="label" />
                    </div>
                    <div class="controls">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textBox" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            CssClass="validator" Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                            Text="*" ForeColor="Red" ErrorMessage="<%$Resources:Messages,EmailRequired%>"  InitialValue=""/>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            CssClass="validator" Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                            Text="*" ErrorMessage="<%$Resources:Messages,EmailIsNotValid%>" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  />
                    </div>

                    <br/>

                    <div class="buttons">
                        <asp:Button ID="btnForgot" runat="server" Text="<%$Resources:Buttons,SendPassword%>" AccessKey="S"  OnClick="btnForgot_Click"  />
                    </div>
                </div>
            </fieldset>
</asp:Content>

