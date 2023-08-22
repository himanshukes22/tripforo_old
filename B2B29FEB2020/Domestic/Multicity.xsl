<?xml version="1.0" encoding="iso-8859-1"?>
<!DOCTYPE xsl:stylesheet  [
	<!ENTITY nbsp   "&#160;">
	<!ENTITY copy   "&#169;">
	<!ENTITY reg    "&#174;">
	<!ENTITY trade  "&#8482;">
	<!ENTITY mdash  "&#8212;">
	<!ENTITY ldquo  "&#8220;">
	<!ENTITY rdquo  "&#8221;">
	<!ENTITY pound  "&#163;">
	<!ENTITY yen    "&#165;">
	<!ENTITY euro   "&#8364;">
]>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>
	<xsl:template match="/">



		<xsl:for-each select="NewDataSet/Table1">
			<!--<div class="flightsearchresult">-->
			<xsl:variable name="Airlogo">
				<xsl:value-of select="MarketingCarrier"/>
			</xsl:variable>
			<xsl:variable name="Ln">
				<xsl:value-of select="LineItemNumber"/>
			</xsl:variable>
			<xsl:variable name="AD">
				<xsl:value-of select="Adult"/>
			</xsl:variable>
			<xsl:variable name="CH">
				<xsl:value-of select="Child"/>
			</xsl:variable>
			<xsl:variable name="IN">
				<xsl:value-of select="Infant"/>
			</xsl:variable>
			<xsl:variable name="TP">
				<xsl:value-of select="TotPax"/>
			</xsl:variable>
			<xsl:variable name="ADTBF">
				<xsl:value-of select="AdtBfare"/>
			</xsl:variable>
			<xsl:variable name="CHDBF">
				<xsl:value-of select="ChdBfare"/>
			</xsl:variable>
			<xsl:variable name="INFBF">
				<xsl:value-of select="infBfare"/>
			</xsl:variable>
			<xsl:variable name="ADTTX">
				<xsl:value-of select="AdtTax"/>
			</xsl:variable>
			<xsl:variable name="CHDTX">
				<xsl:value-of select="ChdTax"/>
			</xsl:variable>
			<xsl:variable name="INFTX">
				<xsl:value-of select="InfTax"/>
			</xsl:variable>
			<xsl:variable name="ADTF">
				<xsl:value-of select="AdtFare"/>
			</xsl:variable>
			<xsl:variable name="CHDF">
				<xsl:value-of select="ChdFare"/>
			</xsl:variable>
			<xsl:variable name="INFF">
				<xsl:value-of select="InfFare"/>
			</xsl:variable>
			<xsl:variable name="TF">
				<xsl:value-of select="TotalFare"/>
			</xsl:variable>
			<xsl:variable name="Trip">
				<xsl:value-of select="TripType"/>
			</xsl:variable>
			<xsl:variable name="FltType">
				<xsl:value-of select="FType"/>
			</xsl:variable>
			<xsl:variable name="TrackID">
				<xsl:value-of select="Track_id"/>
			</xsl:variable>
			<xsl:variable name="DepTm">
				<xsl:value-of select="DepartureTime"/>
			</xsl:variable>
			<xsl:variable name="ArrTm">
				<xsl:value-of select="ArrivalTime"/>
			</xsl:variable>

			<xsl:if test="not(LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber)">
				<xsl:if test="preceding-sibling::node()">
					<div class="clear_1">
					</div>
				</xsl:if>

				<div class="flightsearchresult_top">
					<div style="float:left; width:250px;">

						<div style="float:left;">
							INR <xsl:value-of select="format-number(TotalFare, '##,###')"/> | <span style="font-size:10px;color:#000000;">Excl. Service Tax</span>
						</div>
						<div style="float:right;">
							<a style="cursor:pointer;color:#223C66;" onclick="DomFareDetails('{$Ln}','{$FltType}')">


								<img src="../images/breakup.gif" style="margin:0 5px; height:16px; vertical-align:middle;" alt="Click Here" />
							</a>
						</div>
					</div>
					<xsl:choose>
						<xsl:when test="(MarketingCarrier='SG') and (FlightIdentification='9888')">
							<span style="font-weight:bold; color:#000000; font-size:11px">Non-Refundable</span>
						</xsl:when>
						<xsl:when test="(CS='GOAIRSPECIAL')">
							<span style="font-weight:bold; color:#ff0000; font-size:11px">Spl. Fare, No Commission</span>
						</xsl:when>
						<xsl:when test="(CS='INDIGOSPECIAL')">
							<span style="font-weight:bold; color:#ff0000; font-size:11px">Spl. Fare, No Commission</span>
						</xsl:when>
						<xsl:when test="(CS='SPICEJETSPECIAL')">
							<span style="font-weight:bold; color:#ff0000; font-size:11px">Spl. Fare, No Commission</span>
						</xsl:when>
					</xsl:choose>
					<!--<span>
            Duration : <xsl:value-of select="Tot_Dur"/>
          </span>-->
					<span class="intlbtn">
						<xsl:choose>
							<xsl:when test ="TripCnt='2'">
								<a href="FltResultR.aspx?Linenumber={$Ln}&amp;FT={$FltType}&amp;TID={$TrackID}">Book Now</a>
							</xsl:when>
							<xsl:otherwise>
								<a href="CustomerInfo.aspx?Linenumber={$Ln}&amp;FT={$FltType}&amp;TID={$TrackID}">Book Now</a>
							</xsl:otherwise>
						</xsl:choose>
					</span>
				</div>
			</xsl:if>


			<xsl:if test="not(LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber) or not(TripType=preceding-sibling::Table1[1]/TripType)">
				<div class="flightsearchresult_body">
					<div class="flightsearchresult_logo">
						<div class="fsr_bt" style="width:70px;">
							<img src="../airlogo/sm{$Airlogo}.gif" alt="" />
							<p>
								<xsl:value-of select="MarketingCarrier"/>-
								<xsl:value-of select="FlightIdentification"/>
							</p>
						</div>
					</div>
					<div class="flightsearchresult_content">
						<div class="fsr_bt"  style="width:110px;">
							<div>
								<span>Depart :</span>
							</div>
							<div>

								<xsl:value-of select="DepartureCityName"/>
							</div>
						</div>
						<div class="fsr_bt">
							<div>
								<span>Time :</span>
							</div>
							<div>
								<xsl:choose>
									<xsl:when test="MarketingCarrier='6E'">
										<xsl:value-of select = "substring($DepTm,1,5)" /> Hrs
									</xsl:when>
									<xsl:when test="MarketingCarrier='SG'">
										<xsl:value-of select = "substring($DepTm,1,5)" /> Hrs
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="DepartureTime"/> Hrs
									</xsl:otherwise>
								</xsl:choose>

							</div>
						</div>
						<div class="fsr_bt" style="width:110px;">
							<div>
								<span>Arrive :</span>
							</div>
							<div>
								<xsl:value-of select="ArrivalCityName"/>
							</div>
						</div>
						<div class="fsr_bt">
							<div>
								<span>Time :</span>
							</div>
							<div>
								<xsl:choose>
									<xsl:when test="MarketingCarrier='6E'">
										<xsl:value-of select = "substring($ArrTm,1,5)" /> Hrs
									</xsl:when>
									<xsl:when test="MarketingCarrier='SG'">
										<xsl:value-of select = "substring($ArrTm,1,5)" /> Hrs.
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="ArrivalTime"/> Hrs
									</xsl:otherwise>
								</xsl:choose>

							</div>
						</div>
						<div class="fsr_bt">
							<div>
								<span>Date :</span>
							</div>
							<div>
								<xsl:value-of select="Departure_Date"/>
							</div>
						</div>

						<div class="fsr_bt">
							<div>
								<span>Aircraft :</span>
							</div>

							<div>
								<xsl:value-of select="EQ"/>
							</div>
						</div>
						<div class="fsr_bt">
							<div>
								<span>Class :</span>
							</div>

							<div>
								<xsl:value-of select="RBD"/>
							</div>
						</div>

					</div>
				</div>
			</xsl:if>
			<!--<xsl:if test="not(Leg=preceding-sibling::Table1[1]/Leg) and (Flight=preceding-sibling::Table1[1]/Flight) and (LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber)">
				<div class="Connection_pnt" >
					<span>
						&gt;&gt;..............O...........O.........
					</span>Change <img src="http://www.ITZ.com/images/change_p.png" /> in <xsl:value-of select="DepartureCityName"/><span>
						........O.......O.................&lt;&lt;
					</span>
				</div>
			</xsl:if>-->
			<xsl:if test="not(Leg=preceding-sibling::Table1[1]/Leg) and (Flight=preceding-sibling::Table1[1]/Flight) and (LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber)">
				<div class="flightsearchresult_body">
					<div class="flightsearchresult_logo">
						<div class="fsr_bt" style="width:70px;">
							<img src="../airlogo/sm{$Airlogo}.gif" alt="" />
							<p>
								<xsl:value-of select="MarketingCarrier"/>-
								<xsl:value-of select="FlightIdentification"/>
							</p>
						</div>
					</div>
					<div class="flightsearchresult_content">
						<xsl:choose>
							<xsl:when test="(LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber) and not(Leg=preceding-sibling::Table1[1]/Leg) and (Flight=preceding-sibling::Table1[1]/Flight) and not(LineItemNumber=following-sibling::Table1[1]/LineItemNumber)">
								<div class="fsr_bt"  style="width:110px;">
									<div>
										<span>Depart :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:choose>
											<xsl:when test="MarketingCarrier='6E'">
												<xsl:value-of select = "substring($DepTm,1,5)" /> Hrs
											</xsl:when>
											<xsl:when test="MarketingCarrier='SG'">
												<xsl:value-of select = "substring($DepTm,1,5)" /> Hrs
											</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="DepartureTime"/> Hrs
											</xsl:otherwise>
										</xsl:choose>

									</div>
								</div>
								<div class="fsr_bt" style="width:110px;">
									<div>
										<span>Arrive :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:choose>
											<xsl:when test="MarketingCarrier='6E'">
												<xsl:value-of select = "substring($ArrTm,1,5)" /> Hrs
											</xsl:when>
											<xsl:when test="MarketingCarrier='SG'">
												<xsl:value-of select = "substring($ArrTm,1,5)" /> Hrs.
											</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="ArrivalTime"/> Hrs
											</xsl:otherwise>
										</xsl:choose>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Date :</span>
									</div>
									<div>
										<xsl:value-of select="Departure_Date"/>
									</div>
								</div>

								<div class="fsr_bt">
									<div>
										<span>Aircraft :</span>
									</div>

									<div>
										<xsl:value-of select="EQ"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Class :</span>
									</div>

									<div>
										<xsl:value-of select="RBD"/>
									</div>
								</div>
								<br/>
							</xsl:when>
							<xsl:when test="(LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber) and not(Leg=preceding-sibling::Table1[1]/Leg) and (Flight=preceding-sibling::Table1[1]/Flight)and not(Flight=following-sibling::Table1[1]/Flight)">
								<div class="fsr_bt"  style="width:110px;">
									<div>
										<span>Depart :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt" style="width:110px;">
									<div>
										<span>Arrive :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Date :</span>
									</div>
									<div>
										<xsl:value-of select="Departure_Date"/>
									</div>
								</div>

								<div class="fsr_bt">
									<div>
										<span>Aircraft :</span>
									</div>

									<div>
										<xsl:value-of select="EQ"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Class :</span>
									</div>

									<div>
										<xsl:value-of select="RBD"/>
									</div>
								</div>
								<br/>
							</xsl:when>
							<xsl:when test="not(LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber) and not(LineItemNumber=following-sibling::Table1[1]/LineItemNumber)">
								<div class="fsr_bt"  style="width:110px;">
									<div>
										<span>Depart :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt" style="width:110px;">
									<div>
										<span>Arrive :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Date :</span>
									</div>
									<div>
										<xsl:value-of select="Departure_Date"/>
									</div>
								</div>

								<div class="fsr_bt">
									<div>
										<span>Aircraft :</span>
									</div>

									<div>
										<xsl:value-of select="EQ"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Class :</span>
									</div>

									<div>
										<xsl:value-of select="RBD"/>
									</div>
								</div>
								<br/>
							</xsl:when>
							<xsl:when test="(LineItemNumber=preceding-sibling::Table1[1]/LineItemNumber) and not(LineItemNumber=following-sibling::Table1[1]/LineItemNumber)">
								<div class="fsr_bt"  style="width:110px;">
									<div>
										<span>Depart :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt" style="width:110px;">
									<div>
										<span>Arrive :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Date :</span>
									</div>
									<div>
										<xsl:value-of select="Departure_Date"/>
									</div>
								</div>

								<div class="fsr_bt">
									<div>
										<span>Aircraft :</span>
									</div>

									<div>
										<xsl:value-of select="EQ"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Class :</span>
									</div>

									<div>
										<xsl:value-of select="RBD"/>
									</div>
								</div>
								<br/>
							</xsl:when>
							<xsl:when test="(LineItemNumber=following-sibling::Table1[1]/LineItemNumber) and (Leg=following-sibling::Table1[1]/Leg) and not(Flight=following-sibling::Table1[1]/Flight)">
								<div class="fsr_bt"  style="width:110px;">
									<div>
										<span>Depart :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt" style="width:110px;">
									<div>
										<span>Arrive :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Date :</span>
									</div>
									<div>
										<xsl:value-of select="Departure_Date"/>
									</div>
								</div>
								<div class="fsr_bt" style="width:100px;">
									<div>
										<span>Duration :</span>
									</div>

									<div>
										<xsl:value-of select="Tot_Dur"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Aircraft :</span>
									</div>

									<div>
										<xsl:value-of select="EQ"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Class :</span>
									</div>

									<div>
										<xsl:value-of select="RBD"/>
									</div>
								</div>
								<br/>
							</xsl:when>
							<xsl:otherwise>
								<div class="fsr_bt"  style="width:110px;">
									<div>
										<span>Depart :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="DepartureTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt" style="width:110px;">
									<div>
										<span>Arrive :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalCityName"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Time :</span>
									</div>
									<div>
										<xsl:value-of select="ArrivalTime"/> Hrs
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Date :</span>
									</div>
									<div>
										<xsl:value-of select="Departure_Date"/>
									</div>
								</div>

								<div class="fsr_bt">
									<div>
										<span>Aircraft :</span>
									</div>

									<div>
										<xsl:value-of select="EQ"/>
									</div>
								</div>
								<div class="fsr_bt">
									<div>
										<span>Class :</span>
									</div>

									<div>
										<xsl:value-of select="RBD"/>
									</div>
								</div>
								<br/>
							</xsl:otherwise>
						</xsl:choose>
					</div>
				</div>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>

