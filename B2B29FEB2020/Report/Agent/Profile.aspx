<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="Report_Agent_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section>
		<div class="db">
			<!--LEFT SECTION-->
			<div class="db-l">
				<div class="db-l-1">
					<ul>
						<li><img src="../../Images/icons/Men-Profile-Image-715x657.png" alt="">
						</li>
						<li><span>80%</span> profile compl</li>
						<li><span>18</span> Notifications</li>
					</ul>
				</div>
				<div class="db-l-2">
					<ul>
						<li>
							<a href="dashboard.html"><img src="images/icon/dbl1.png" alt=""> All Bookings</a>
						</li>
						<li>
							<a href="db-travel-booking.html"><img src="images/icon/dbl2.png" alt=""> Travel Bookings</a>
						</li>
						<li>
							<a href="db-hotel-booking.html"><img src="images/icon/dbl3.png" alt=""> Hotel Bookings</a>
						</li>
						<li>
							<a href="db-event-booking.html"><img src="images/icon/dbl4.png" alt=""> Event Bookings</a>
						</li>
						<li>
							<a href="db-my-profile.html"><img src="images/icon/dbl6.png" alt=""> My Profile</a>
						</li>
						<li>
							<a href="db-all-payment.html"><img src="images/icon/dbl9.png" alt=""> Payments</a>
						</li>
						<li>
							<a href="db-refund.html"><img src="images/icon/dbl7.png" alt=""> Claim &amp; Refund</a>
						</li>
					</ul>
				</div>
			</div>
			<!--CENTER SECTION-->
			<div class="db-2">
				<div class="db-2-com db-2-main">
					<h4>My Profile</h4>
					<div class="db-2-main-com db-2-main-com-table">
						<table class="responsive-table">
							<tbody>
								<tr>
									<td>User Name</td>
									<td>:</td>
									<td>Sam Anderson</td>
								</tr>
								<tr>
									<td>Password</td>
									<td>:</td>
									<td>mypasswordtour</td>
								</tr>
								<tr>
									<td>Eamil</td>
									<td>:</td>
									<td>sam_anderson@gmail.com</td>
								</tr>
								<tr>
									<td>Phone</td>
									<td>:</td>
									<td>+01 4561 3214</td>
								</tr>
								<tr>
									<td>Date of birth</td>
									<td>:</td>
									<td>03 Jun 1990</td>
								</tr>
								<tr>
									<td>Address</td>
									<td>:</td>
									<td>8800 Orchard Lake Road, Suite 180 Farmington Hills, U.S.A.</td>
								</tr>
								<tr>
									<td>Status</td>
									<td>:</td>
									<td><span class="db-done">Active</span>
									</td>
								</tr>
							</tbody>
						</table>
						<div class="db-mak-pay-bot">
							<p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters</p> <a href="db-my-profile-edit.html" class="waves-effect waves-light btn-large">Edit my profile</a> </div>
					</div>
				</div>
			</div>
			<!--RIGHT SECTION-->
			
		</div>
	</section>
</asp:Content>

