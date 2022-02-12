<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPassword.aspx.cs" Inherits="ApplicationSec_200238F.NewPassword" %>
<script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_Npassword.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password Length Must be at Least 8 Characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too_short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number")
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 uppercase character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_uppercase")
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 lowercase character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_lowercase")
            }
            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 special character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_specialchar")
            }
            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!"
            document.getElementById("lbl_pwdchecker").style.color = "Blue";
        }
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href ="asset/css/style.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div">
                  <!-- Change Password Form -->

                    <div class="row mb-3">          
                         <asp:Label ID="currentPassword" class="col-md-4 col-lg-3 col-form-label" runat="server" Text="Current Password"></asp:Label>
                      <div class="col-md-8 col-lg-9">
                        <asp:TextBox ID="tb_Cpassword" runat="server" TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cpassword" ErrorMessage="Please enter Current password"></asp:RequiredFieldValidator>
                      </div>
                    </div>

                    <div class="row mb-3">
                        <asp:Label ID="newPassword" class="col-md-4 col-lg-3 col-form-label" runat="server" Text="Enter New Password"></asp:Label>
                      <div class="col-md-8 col-lg-9">
                        <asp:TextBox ID="tb_Npassword" runat="server" TextMode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_Npassword" ErrorMessage="Please enter New Password"></asp:RequiredFieldValidator>
                          <asp:Label ID="lbl_pwdchecker" runat="server" EnableViewState="false"></asp:Label>
                      </div>
                    </div>

                    <div class="row mb-3">
                        <asp:Label ID="ConfirmPass" class="col-md-4 col-lg-3 col-form-label" runat="server" Text="Re-enter New Password"></asp:Label>
                      <div class="col-md-8 col-lg-9">
                        <asp:TextBox ID="tb_N2Password" runat="server" TextMode="Password"></asp:TextBox>
                          <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tb_Npassword" ControlToValidate="txt_ccpassword" ErrorMessage="Password Mismatch"></asp:CompareValidator>
                      </div>
                    </div>

                    <div class="text-center">
                        <asp:Button ID="btn_change" runat="server" Font-Bold="True" BackColor="#CCFF99" OnClick="btn_change_click" Text="Change Password" />
                    </div>
                  <!-- End Change Password Form -->
                </div>
    </form>
     <script>
         grecaptcha.ready(function () {
             grecaptcha.execute('6LcMz1keAAAAAEeQkOMBmTnMdTbfJ2i6S40LTuMS', { action: 'Registeration' }).then(function (token) {
                 document.getElementById("g-recaptcha-response").value = token;
             });
         });
     </script>
</body>
</html>
