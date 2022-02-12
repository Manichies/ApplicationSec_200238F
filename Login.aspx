<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ApplicationSec_200238F.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href ="asset/css/style.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet"/>
</head>
<body>
    <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

              <div class="d-flex justify-content-center py-4">
                <a href="index.html" class="logo d-flex align-items-center w-auto">
                  <span class="d-none d-lg-block">Application Security</span>
                </a>
              </div><!-- End Logo -->

              <div class="card mb-3">

                <div class="card-body">

                  <div class="pt-4 pb-2">
                    <h5 class="card-title text-center pb-0 fs-4">Login to Your Account</h5>
                    <p class="text-center small">Enter your email & password to login</p>
                  </div>

                  <form id="form1" runat="server">
                    <div class="col-12">
                      <div class="input-group has-validation">
                         <asp:TextBox ID="userEmail" Class="form-control" placeholder="Email" runat="server"></asp:TextBox>
                        <div class="invalid-feedback">Please enter your username.</div>
                      </div>
                    </div>
                      <br />
                    <div class="col-12">
                      <asp:TextBox ID="userPassword" TextMode="Password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
                      <div class="invalid-feedback">Please enter your password!</div>
                    </div>
                      <br />
                      <div class="col-12">
                      <div class="input-group has-validation">
                         <asp:TextBox ID="pinNumber" Class="form-control" placeholder="Authentication Code" runat="server"></asp:TextBox>
                        <div class="invalid-feedback">Please enter your code .</div>
                      </div>
                    </div>
                      <br \ />
                    <div class="col-12">
                        <div>
                            <asp:CheckBox id="rememberMe" runat="server"/>
                        <label for="rememberMe">Remember me</label>
                      </div>
                    </div>
                      <br />
                    <div class="col">
                        <asp:Label ID="error" runat="server"></asp:Label>

                    </div>
                    <div class="col-12">
                        <asp:Button class="btn btn-primary w-100" type="submit" onclick="Button_Click" Text="Login" runat="server"/>
                    </div>
                    <div class="col-12">
                      <p class="small mb-0">Don't have account? <a href="Registration.aspx">Register an account</a></p>
                    </div>
                  </form>

                </div>
              </div>
                </div>
              </div>
            </div>
      </section>
</body>
</html>
