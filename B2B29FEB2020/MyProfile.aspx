<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="MyProfile.aspx.vb" Inherits="MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
        .hover:hover {
            background:#e5e5e5;
        }
    </style>

    <section class="tourb2-ab-p-4 com-colo-abou" style="padding: 17px 0px 70px 0px !important;background: linear-gradient(to right, rgb(173, 83, 137), rgb(60, 16, 83));">

        
        
		<div class="container">
            <h3 style="font-weight: 100 !important;color:#fff;">Your Account</h3>
			<div class="row tourb2-ab-p4">
				<div class="col-md-4 col-sm-6">
                    <a href="Report/Accounts/LedgerSingleOrderID.aspx">
					<div class="tourb2-ab-p4-1 tourb2-ab-p4-com hover"> <i><img src="Images/icons/unnamed%20(1).png" style="width:50px;"/></i>
						<div class="tourb2-ab-p4-text">
							<h4  style=" margin-top: 0px;margin-bottom: 0px;">Accounts</h4>
							<p style="font-size: 12px;">Manage account section by an User to define each class of items for which money or its equivalent is spent or received</p>
                          
						</div>
					</div>
                        </a>
				</div>

                <div class="col-md-4 col-sm-6">
                    <a href="/Report/TicketReport.aspx">
					<div class="tourb2-ab-p4-1 tourb2-ab-p4-com hover"> <i aria-hidden="true"><img src="Images/icons/261-2618707_plane-svg-toy-airplane-icon-png-color.png" style="width:50px;"/></i>
						<div class="tourb2-ab-p4-text">
							<h4  style=" margin-top: 0px;margin-bottom: 0px;">Flight</h4>
							<p style="font-size: 12px;">You will get an overview of all your bookings in your profile and you can manage these quickly and easily.</p>
						</div>
					</div>
                        </a>
				</div>

                	<div class="col-md-4 col-sm-6">
                    <a href="<%= ResolveUrl("~/Report/Accounts/AgentPanel.aspx")%>">
					<div class="tourb2-ab-p4-1 tourb2-ab-p4-com hover"> <i><img src="Images/icons/261778.png" style="width:50px;"/></i>
						<div class="tourb2-ab-p4-text">
							<h4  style=" margin-top: 0px;margin-bottom: 0px;">Upload</h4>
							<p style="font-size: 12px;">You can see all the transaction that has been transacted here, and you can also manage it.</p>
						</div>
					</div>
                        </a>
				</div>

				<div class="col-md-4 col-sm-6">
                    <a href="Report/Agent/Agent_markup.aspx">
					<div class="tourb2-ab-p4-1 tourb2-ab-p4-com hover"> <i><img src="Images/icons/1801676272_preview_Gear%209.png" style="width:50px;"/></i>
						<div class="tourb2-ab-p4-text">
							<h4  style=" margin-top: 0px;margin-bottom: 0px;">Settings</h4>
							<p style="font-size: 12px;">User can easily add markup through domestic airline markup option and international airline markup..</p>
                           
                            
						</div>
					</div>
                        </a>
				</div>
				<div class="col-md-4 col-sm-6">
                    <a href="#">
					<div class="tourb2-ab-p4-1 tourb2-ab-p4-com hover"> <i><img src="Images/icons/d11a452f5ce6ab534e083cdc11e8035e.png" style="width:50px;"/></i>
						<div class="tourb2-ab-p4-text">
							<h4  style=" margin-top: 0px;margin-bottom: 0px;">Profile</h4>
							<p style="font-size: 12px;">User can easily manage your profile in this section, manage your password, change user name and contact informations.</p>
						</div>
					</div>
                        </a>
				</div>
			
				
                <div class="col-md-4 col-sm-6">
                    <a href="#">
					<div class="tourb2-ab-p4-1 tourb2-ab-p4-com hover"> <i aria-hidden="true"><img src="Images/icons/1845287.png" style="width:50px;"/></i>
						<div class="tourb2-ab-p4-text">
							<h4  style=" margin-top: 0px;margin-bottom: 0px;">Booking</h4>
							<p style="font-size: 12px;">Manage My Booking service for agents, to view Flight Details, Fares, Taxes, to make changes to booked flights.</p>
						</div>
					</div>
                        </a>
				</div>
				
			</div>
		</div>

      
     
        

	</section>

   

   

</asp:Content>

