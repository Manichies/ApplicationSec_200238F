<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ApplicationSec_200238F.Registration" ValidateRequest="false" %>

<script src="//code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
 <script type="text/javascript">
     function ImagePreview(input) {
         if (input.files && input.files[0]) {
             var reader = new FileReader();
             reader.onload = function (e) {
                 $('#<%=Img1.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0])
            }
        }
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;

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
     function fnameValidation() {
         var fname = document.getElementById('<%=firstname.ClientID%>').value;
         if (fname.search(/^[a-zA-Z\s]+$/)) {
             document.getElementById("lbl_fname").innerHTML = "Invalid Firstname"
             document.getElementById("lbl_fname").style.color = "Red";
             return ("invalid_firstname")
         }
         else {
             document.getElementById("lbl_fname").innerHTML = "Valid";
             document.getElementById("lbl_fname").style.color ="Green";
         }
     }
     function lnameValidation() {
         var lname = document.getElementById('<%=lastname.ClientID%>').value;
         if (lname.search(/^[a-zA-Z\s]+$/)) {
             document.getElementById("lbl_lname").innerHTML = "Invalid Lastname"
             document.getElementById("lbl_lname").style.color = "Red";
             return ("invalid_lastname")         }
         else {
             document.getElementById("lbl_lname").innerHTML = "Valid";
             document.getElementById("lbl_lname").style.color = "Green";
         }
     }
     function creditcardValidation() {
         var creditcard = document.getElementById('<%=creditcard.ClientID%>').value;
         if (creditcard.search(/^\d+$/)) {
             document.getElementById("lbl_creditcard").innerHTML = "Invalid Credit Card";
             document.getElementById("lbl_creditcard").style.color = "Red";
             return ("too_short");
         }
         else if (creditcard.length > 16) {
             document.getElementById("lbl_creditcard").innerHTML = "Invalid Credit Card";
             document.getElementById("lbl_creditcard").style.color = "Red";
             return ("invalid_creditcard")
         }
         else if (creditcard.length == 16) {
             document.getElementById("lbl_creditcard").innerHTML = "Valid";
             document.getElementById("lbl_creditcard").style.color = "Green";
         }
         else if (creditcard.length < 16) {
             document.getElementById("lbl_creditcard").innerHTML = "Credit Card must be 16 character";
             document.getElementById("lbl_creditcard").style.color = "Red";
             return ("invalid_creditcard")
         }
         else {
             document.getElementById("lbl_creditcard").innerHTML = "Valid";
             document.getElementById("lbl_creditcard").style.color = "Green";
         }
     }
     function emailValidation() {
         var email = document.getElementById('<%=email.ClientID%>').value;
         if (email.search(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)) {
             document.getElementById("lbl_email").innerHTML = "Invalid Email";
             document.getElementById("lbl_email").style.color = "Red";
             return ("Invalid_email")
         }
         document.getElementById("lbl_email").innerHTML = "Valid";
         document.getElementById("lbl_email").style.color = "Green";
     }
      
 </script>  
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml%22%3E>
<head runat="server">
    <title>Registration</title>
    <link href ="asset/css/style.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet"/>
    <script src="https://www.google.com/recaptcha/api.js?render=6LcMz1keAAAAAEeQkOMBmTnMdTbfJ2i6S40LTuMS"></script>
  
</head>
<body>

    <div class="container">

      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

              <div class="card mb-3">

                <div class="card-body">

                  <div class="pt-4 pb-2">
                    <h5 class="card-title text-center pb-0 fs-4">Register an Account</h5>
                    <p class="text-center small">Enter your personal details to create account</p>
                  </div>

                  <form id="form1" runat="server">
                    <div class="col-12">
                        <br />
                        <asp:TextBox required="true" ID="firstname" Class="form-control" placeholder="First Name" runat="server" onkeyup="javascript:fnameValidation()"></asp:TextBox>
                        <asp:Label ID="lbl_fname" runat="server" ></asp:Label>
                
                    </div>

                    <div class="col-12">
                        <br />
                        <asp:TextBox required="true" ID="lastname" Class="form-control" placeholder="Last Name" runat="server" onkeyup="javascript:lnameValidation()"></asp:TextBox>
                        <asp:Label ID="lbl_lname" runat="server" ></asp:Label>
           
                    </div>

                    <div class="col-12">
                        <br />
                        <asp:TextBox required="true" ID="creditcard" MaxLength="16" Class="form-control" placeholder="Credit Card Info" runat="server" onkeyup="javascript:creditcardValidation()"></asp:TextBox>
                        <asp:Label ID="lbl_creditcard" runat="server" ></asp:Label>
                    </div>

                    <div class="col-12">
                        <br /> 
                        <asp:TextBox required="true" ID="email" TextMode="Email" CssClass="form-control" placeholder="Email address" runat="server" onkeyup="javascript:emailValidation()"></asp:TextBox>
                        <asp:Label ID="lbl_email" runat="server" ></asp:Label>
                    </div>

                    <div class="col-12">
                        <br />
                       <asp:TextBox required="true" ID="tb_password" runat="server" class="form-control" placeholder="Password" TextMode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                       <asp:Label ID="lbl_pwdchecker" runat="server" ></asp:Label>
                    </div>
                    <div class="col-12">
                        <br />
                       <asp:TextBox required="true" ID="DOB" TextMode="Date" Class="form-control" placeholder="Date of Birth" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <br />
                       <asp:Image ID="Img1" Height="150px" Width="240px" Class="form-control" runat="server"></asp:Image>
                        <br />
                       <asp:FileUpload ID="FileUpload1" runat="server" onchange="ImagePreview(this);" />
                    </div>
                      <div class="col-12">
                          <asp:Label ID="uploadChecker" runat="server"></asp:Label>
                      </div>
                    <div class="col-12">     
                        <br />
                        <div type="hidden"  id="g-recaptcha-response" name="g-recaptcha-response"></div>
                        <%--<asp:Label ID="lblCaptchaMessage" runat="server" EnableViewState="false">Check if you're a robot</asp:Label>--%>
                    </div>
                    <div class="col-12">
                      
   
                      <%--<asp:Button ID="Btn1" OnClick="btn1_click" CssClass="btn btn-primary w-100" runat="server" Text="Create Account" />--%>

                          <asp:Button OnClick="btn1_click" CssClass="btn btn-primary w-100" runat="server" Text="Create Account" />

                    </div>
                    <div class="col-12">
                      <p class="small mb-0">Already have an account? <a href="Login.aspx">Log in</a></p>
                    </div>
                  </form>
                    <script>
                        grecaptcha.ready(function () {
                            grecaptcha.execute('6LcMz1keAAAAAEeQkOMBmTnMdTbfJ2i6S40LTuMS', { action: 'Registeration' }).then(function (token) {
                                document.getElementById("g-recaptcha-response").value = token;
                            });
                        });
                    </script>   

                </div>
              </div>

            </div>
          </div>
        </div>

      </section>

    </div>
</body>

  
</html>
