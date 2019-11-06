<%@ Page Title="اطلاعات تکمیلی تامین کننده" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register_Supplier_CompeleteInfo.aspx.cs" Inherits="Account_Register_Supplier_CompeleteInfo" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function showpreview(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgAvatar.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }

            }
            //$("#lnkAttachSOW").click(function () { $("#fuSOW").click(); });
    </script>

    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <script type="text/javascript">
        function nocopy() {
            alert("لطفا کپی نکنید");
            return false;
        }
    </script>
    <div class="form-horizontal">
        <h4>یک حساب جدید ایجاد کنید</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" EnableClientScript="true" DisplayMode="BulletList" ShowSummary="true" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FullName" CssClass="col-md-2 control-label">نام و نام خانوادگی</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" Enabled="false" ID="FullName" CssClass="form-control" />


            </div>
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">نام کاربری</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" Enabled="false" ID="UserName" CssClass="form-control" />

            </div>

            <asp:Label runat="server" AssociatedControlID="EcologeName" CssClass="col-md-2 control-label">نام اکولوژ</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="EcologeName" CssClass="form-control" />
            </div>

            <asp:Label runat="server" AssociatedControlID="Slogan" CssClass="col-md-2 control-label">شعار</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Slogan" CssClass="form-control" />

            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Kilometer" CssClass="col-md-2 control-label">کیلومتر</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" TextMode="Number" Text="0" Width="80px" ID="Kilometer" CssClass="form-control" />
            </div>

            <asp:Label runat="server" AssociatedControlID="Road" CssClass="col-md-2 control-label">جاده</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Road" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-9">


                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="DeliverEnabled" CssClass="col-md-4 control-label">امکان ارسال</asp:Label>
                    <asp:CheckBox runat="server" ID="DeliverEnabled" CssClass="col-md-2" />

                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="chkActive" CssClass="col-md-2 control-label">فعال</asp:Label>
                    <asp:CheckBox runat="server" Text="" ID="chkActive" CssClass="col-md-2 " />

                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="field">
                <div>
                    <asp:Literal ID="lblHeader" runat="server" Text="<%$Resources:Captions,UploadPhoto%>" />
                </div>
                <div class="caption">
                    <asp:Label ID="lblPicture" runat="server" Text="<%$Resources:Captions,Picture%>" CssClass="label" />
                </div>
                <div class="controls">
                    <asp:FileUpload ID="myFileUpload" runat="server" onchange="showpreview(this);" />
                    <br />
                    <asp:Image ID="imgAvatar" Height="200" Width="200" runat="server" />
                    <br />
                    <br />
                    <asp:Button ID="btnUpload" runat="server" Text="<%$Resources:Buttons,Upload%>" AccessKey="U" OnClick="btnUpload_Click" />
                    <br />

                    <%--<asp:Button ID="btnSkip" runat="server" Text="<%$Resources:Buttons,Skip%>" AccessKey="S" OnClick="btnSkip_Click"  />--%>
                </div>
                 <asp:Label runat="server" AssociatedControlID="TelNumber" CssClass="col-md-2 control-label">شماره تلفن</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TelNumber" CssClass="form-control" MaxLength="11"  ForeColor="Gray" ToolTip="مثال: 02100000000"/>
                  <asp:RequiredFieldValidator EnableClientScript="true" runat="server" ControlToValidate="TelNumber"
                    CssClass="text-danger" ErrorMessage="شماره تلفن مورد نیاز است" Text="*"  />
                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="TelNumber"
                     Text="*" ErrorMessage="شماره تلفن باید عددی باشد" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>

            </div>
            <asp:Label runat="server" AssociatedControlID="Address" CssClass="col-md-2 control-label">آدرس</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Address" CssClass="form-control" />
                  <asp:RequiredFieldValidator EnableClientScript="true" runat="server" ControlToValidate="Address"
                    CssClass="text-danger" ErrorMessage="آدرس مورد نیاز است" Text="*"  />
                
            </div>
                <table>
                    <tr>

                        <td>شماره</td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server" Width="211px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>نام</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="211px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>موضوع</td>
                        <td>
                            <asp:TextBox ID="txtSubject" runat="server" Width="211px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>شرح</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="211px"></asp:TextBox></td>
                    </tr>
                               <tr>  
           <td colspan="2"><asp:Button ID="btnSubmit" OnClientClick="yourfunction(); return false;"  runat="server" Text="ذخیره" OnClick="btnSubmit_Click"/></td>  
	            </tr>  

                </table>
                <asp:FileUpload ID="fileuploadEmpImage" runat="server" Width="180px" />
               
                 <asp:GridView ID="grdSupplierGallery" runat="server" AutoGenerateColumns="false" AllowPaging="true"  DataKeyNames="Id" OnRowDataBound="grdSupplierGallery_RowDataBound" OnRowDeleting="grdSupplierGallery_RowDeleting">
                    <Columns>
                       
                        <asp:BoundField HeaderText="موضوع" DataField="imgSubject" />
                        <asp:BoundField HeaderText="توضیحات" DataField="imgDescription" />
                        <asp:BoundField HeaderText="عکس" DataField="Image" Visible="false" />
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                             
                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("Id") %>'>ردیف</asp:Label>  
                                
                                 <asp:CheckBox ID="cbImage" runat="server" />
                                <asp:Image ID="imgSupplier" runat="server" ImageUrl='<%# "SupplierImageHandler.ashx?Id="+ Eval("Id") %>'
                                    Height="150px" Width="150px" />
                                <asp:Button Text="حذف" ID="btnDelete" runat="server" OnClick="btnDelete_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>
        </div>

        <div class="col-md-offset-2 col-md-10">
            <asp:Literal runat="server" ID="litError" />
        </div>
        <div id="divPageMessage" runat="server" style="background-color: yellow; color: red;">
            <asp:Literal ID="litPage" runat="server" />
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="ثبت " CssClass="btn btn-default" />
            </div>
        </div>
</asp:Content>

