<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayProfile.aspx.cs" Inherits="ApplicationSec_200238F.DisplayProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href ="asset/css/style.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="asset/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet"/>
</head>
<body>
    <form id="form" runat="server">
    <section class="section profile">
      <div class="row">
        <div class="col-xl-4">
          <div class="card">
            <div class="card-body profile-card pt-4 d-flex flex-column align-items-center">
              <asp:Image ID="uprofilepicture" Height="100" Width="100" runat="server" CssClass="rounded-circle" />
              <div class="social-links mt-2">
                <a href="#" class="twitter"><i class="bi bi-twitter"></i></a>
                <a href="#" class="facebook"><i class="bi bi-facebook"></i></a>
                <a href="#" class="instagram"><i class="bi bi-instagram"></i></a>
                <a href="#" class="linkedin"><i class="bi bi-linkedin"></i></a>
              </div>
            </div>
          </div>

            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
            <asp:Button ID="btnLogOut" OnClick="Logout" CssClass="btn btn-primary w-100" runat="server" Text="Log Out" />
        </div>

        <div class="col-xl-8">

          <div class="card">
            <div class="card-body pt-3">
              <!-- Bordered Tabs -->
              <ul class="nav nav-tabs nav-tabs-bordered">

                <li class="nav-item">
                  <button class="nav-link active" data-bs-toggle="tab" data-bs-target="#profile-overview">Overview</button>

                </li>
                  <li class="nav-item">
                  <button class="nav-link" data-bs-toggle="tab" data-bs-target="#profile-change-password">Change Password</button>
                </li>

              </ul>
              <div class="tab-content pt-2">

                <div class="tab-pane fade show active profile-overview" id="profile-overview">
                  <h5 class="card-title">About</h5>
                  <p class="small fst-italic">Sunt est soluta temporibus accusantium neque nam maiores cumque temporibus. Tempora libero non est unde veniam est qui dolor. Ut sunt iure rerum quae quisquam autem eveniet perspiciatis odit. Fuga sequi sed ea saepe at unde.</p>

                  <h5 class="card-title">Profile Details</h5>

                  <div class="row">
                    <div class="col-lg-3 col-md-4 label ">First Name</div>
                    <asp:Label ID="ufirstname" runat="server" Text="Label"></asp:Label>
                  </div>

                  <div class="row">
                    <div class="col-lg-3 col-md-4 label">Last Name</div>
                    <asp:Label ID="ulastname" runat="server" Text="Label"></asp:Label>
                  </div>

                  <div class="row">
                    <div class="col-lg-3 col-md-4 label">Credit Card Info</div>
                      <asp:Label ID="ucreditcard" runat="server" Text="Label"></asp:Label>
                  </div>

                   <div class="row">
                    <div class="col-lg-3 col-md-4 label">Email</div>
                      <asp:Label ID="uemail" runat="server" Text="Label"></asp:Label>
                  </div>

                  <div class="row">
                    <div class="col-lg-3 col-md-4 label">Date of Birth</div>
                      <asp:Label ID="uDOB" runat="server" Text="Label"></asp:Label>
                  </div>

                    <div class=" d-flex mt-2 row">
                        <a href="NewPassword.aspx" type="button" class="btn btn-primary" height="50px" width="100px">Change Password</a>
          
                    </div>
                </div>
              </div><!-- End Bordered Tabs -->

            </div>
          </div>
        </div>
      </div>
    </section>
    </form>
</body>
</html>
