using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using STD.BAL;
using System.Collections;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string PaxName=" ";
            SqlTransaction objDA= new SqlTransaction();
            DataSet PaxDs = objDA.GetPaxDetails("FW9950fec9vWEdi154");
            if (PaxDs != null && PaxDs.Tables.Count > 0 && PaxDs.Tables[0].Rows.Count>0)
            {

                for (int p = 0; p < PaxDs.Tables[0].Rows.Count; p++)
                {
                    PaxName += Convert.ToString(PaxDs.Tables[0].Rows[p]["LName"]) + "/" + Convert.ToString(PaxDs.Tables[0].Rows[p]["FName"]);
                }                    
            }
       
        string PNBF2Res = @"<?xml version='1.0' encoding='UTF-8'?><soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>  <soapenv:Body>    <SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'> <SubmitXmlOnSessionResult>
        <PNRBFManagement_13 xmlns=''>
          <PNRBFRetrieve>
            <Control>
              <KLRCnt>27</KLRCnt>
              <KlrAry>
                <Klr>
                  <ID>BP10</ID>
                  <NumOccur>3</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP12</ID>
                  <NumOccur>3</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP13</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>IT01</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP21</ID>
                  <NumOccur>4</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP22</ID>
                  <NumOccur>4</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP20</ID>
                  <NumOccur>0</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP19</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP16</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP52</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>DPP0</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP32</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP26</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP48</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP28</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP27</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP08</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP40</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
              </KlrAry>
            </Control>
            <LNameInfo>
              <LNameNum>1</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType/>
              <LName>OBEROI</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>1</AbsNameNum>
              <FName>VISHIMR</FName>
            </FNameInfo>
            <LNameInfo>
              <LNameNum>2</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType/>
              <LName>MALHOTRA</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>2</AbsNameNum>
              <FName>CHHAVIMS</FName>
            </FNameInfo>
            <LNameInfo>
              <LNameNum>3</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType>I</NameType>
              <LName>OBEROI</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>3</AbsNameNum>
              <FName>PRAYANSH</FName>
            </FNameInfo>
            <NameRmkInfo>
              <LNameNum>3</LNameNum>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>3</AbsNameNum>
              <NameRmk>21SEP17</NameRmk>
            </NameRmkInfo>
            <AirSeg>
              <SegNum>1</SegNum>
              <Status>HK</Status>
              <Dt>20180318</Dt>
              <DayChg>00</DayChg>
              <AirV>AI</AirV>
              <NumPsgrs>2</NumPsgrs>
              <StartAirp>DEL</StartAirp>
              <EndAirp>BOM</EndAirp>
              <StartTm>900</StartTm>
              <EndTm>1120</EndTm>
              <BIC>T</BIC>
              <FltNum>678</FltNum>
              <OpSuf/>
              <COG>N</COG>
              <TklessInd>Y</TklessInd>
              <ConxInd>N</ConxInd>
              <FltFlownInd>N</FltFlownInd>
              <MarriageNum/>
              <SellType>O</SellType>
              <StopoverIgnoreInd/>
              <TDSValidateInd>N</TDSValidateInd>
              <NonBillingInd/>
              <PrevStatusCode>NN</PrevStatusCode>
              <ScheduleValidationInd/>
            </AirSeg>
            <ProgramaticSSR>
              <GFAXNum>1</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>1</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>1</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>0985314633235C1</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>2</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>2</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>2</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>0985314633236C1</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>3</GFAXNum>
              <SSRCode>INFT</SSRCode>
              <Status>KK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>1</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>1</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>OBEROI/PRAYANSH 21SEP17</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>4</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>3</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>3</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>0985314633237C1</Text>
            </ProgramaticSSRText>
            <OSI>
              <GFAXNum>1</GFAXNum>
              <OSIV>YY</OSIV>
              <OSIMsg>MOBILE-9818788055</OSIMsg>
            </OSI>
            <PhoneInfo>
              <PhoneFldNum>1</PhoneFldNum>
              <Pt>DEL</Pt>
              <Type>A</Type>
              <Phone>011 47677777</Phone>
            </PhoneInfo>
            <CreditCardFOP>
              <ID>6</ID>
              <Type>1</Type>
              <Currency/>
              <Amt/>
              <ExpDt>1219</ExpDt>
              <TransType/>
              <ApprovalInd/>
              <AcceptOverride/>
              <ValidationBypassReq/>
              <Vnd>VI</Vnd>
              <Acct><![CDATA[4000000000009002    ]]></Acct>
              <AdditionalInfoAry></AdditionalInfoAry>
            </CreditCardFOP>
            <TkArrangement>
              <Text>BOM 14FEB1044Z WS AG</Text>
            </TkArrangement>
            <GenRmkInfo>
              <GenRmkNum>1</GenRmkNum>
              <CreationDt>20180214</CreationDt>
              <CreationTm>1044</CreationTm>
              <GenlRmkQual/>
              <GenRmk>ON-LINE BOOKING - 9210325540</GenRmk>
            </GenRmkInfo>
            <VndRmk>
              <RmkNum>1</RmkNum>
              <TmStamp>1044</TmStamp>
              <DtStamp>20180214</DtStamp>
              <RmkType>I</RmkType>
              <VType>A</VType>
              <Vnd>AI</Vnd>
              <Rmk>AUTO XX IF SSR TKNA/E/M/C NOT RCVD BY AI BY 2014/14FEB/DE L LT</Rmk>
            </VndRmk>
            <GenPNRInfo>
              <FileAddr>E4DB01D6</FileAddr>
              <CodeCheck>67</CodeCheck>
              <RecLoc>7ZJ73O</RecLoc>
              <Ver>5</Ver>
              <OwningCRS>1G</OwningCRS>
              <OwningAgncyName>RWT COM</OwningAgncyName>
              <OwningAgncyPCC>000V</OwningAgncyPCC>
              <CreationDt>20180214</CreationDt>
              <CreatingAgntSignOn>000VGWS</CreatingAgntSignOn>
              <CreatingAgntDuty>AG</CreatingAgntDuty>
              <CreatingAgncyIATANum>14319126</CreatingAgncyIATANum>
              <OrigBkLocn>BOMOU</OrigBkLocn>
              <SATONum/>
              <PTAInd>N</PTAInd>
              <InUseInd>N</InUseInd>
              <SimultaneousUpdInd/>
              <BorrowedInd>N</BorrowedInd>
              <GlobInd>N</GlobInd>
              <ReadOnlyInd>N</ReadOnlyInd>
              <FareDataExistsInd>Y</FareDataExistsInd>
              <PastDtQuickInd>N</PastDtQuickInd>
              <CurAgncyPCC>5F86</CurAgncyPCC>
              <QInd>Y</QInd>
              <TkNumExistInd>Y</TkNumExistInd>
              <IMUdataexists>N</IMUdataexists>
              <ETkDataExistInd>Y</ETkDataExistInd>
              <CurDtStamp>20180214</CurDtStamp>
              <CurTmStamp>104444</CurTmStamp>
              <CurAgntSONID>5F86GWS</CurAgntSONID>
              <TravInsuranceInd/>
              <PNRBFTicketedInd>Y</PNRBFTicketedInd>
              <ZeppelinAgncyInd>N</ZeppelinAgncyInd>
              <AgncyAutoServiceInd>N</AgncyAutoServiceInd>
              <AgncyAutoNotifyInd>N</AgncyAutoNotifyInd>
              <ZeppelinPNRInd>N</ZeppelinPNRInd>
              <PNRAutoServiceInd>N</PNRAutoServiceInd>
              <PNRAutoNotifyInd>N</PNRAutoNotifyInd>
              <SuperPNRInd>N</SuperPNRInd>
              <PNRBFPurgeDt>20180320</PNRBFPurgeDt>
              <PNRBFChangeInd>N</PNRBFChangeInd>
              <MCODataExists>N</MCODataExists>
              <OrigRcvdField>PASSENGER</OrigRcvdField>
              <IntContExists>N</IntContExists>
            </GenPNRInfo>
            <Email>
              <ItemNum>1</ItemNum>
              <Type>T</Type>
              <Data>jain1994rakshit@gmail.com</Data>
            </Email>
            <VndRecLocs>
              <RecLocInfoAry>
                <RecLocInfo>
                  <TmStamp>1044</TmStamp>
                  <DtStamp>20180214</DtStamp>
                  <Vnd>AI</Vnd>
                  <RecLoc>HRQ9E</RecLoc>
                </RecLocInfo>
              </RecLocInfoAry>
            </VndRecLocs>
          </PNRBFRetrieve>
        </PNRBFManagement_13>
      </SubmitXmlOnSessionResult>
    </SubmitXmlOnSessionResponse>
  </soapenv:Body>
</soapenv:Envelope>";
            //Adult=4	Child=3	Infant=1

        PNBF2Res = @"<?xml version='1.0' encoding='UTF-8'?>
<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
 <soapenv:Body><SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'><SubmitXmlOnSessionResult><PNRBFManagement_13 xmlns=''><PNRBFRetrieve><Control><KLRCnt>51</KLRCnt><KlrAry><Klr><ID>BP10</ID><NumOccur>8</NumOccur></Klr><Klr><ID>BP12</ID><NumOccur>8</NumOccur></Klr><Klr><ID>BP13</ID><NumOccur>4</NumOccur></Klr><Klr><ID>IT01</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP21</ID><NumOccur>9</NumOccur></Klr><Klr><ID>BP22</ID><NumOccur>9</NumOccur></Klr><Klr><ID>BP20</ID><NumOccur>3</NumOccur></Klr><Klr><ID>BP19</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP16</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP52</ID><NumOccur>1</NumOccur></Klr><Klr><ID>DPP9</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP32</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP28</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP27</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP08</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP40</ID><NumOccur>1</NumOccur></Klr></KlrAry></Control><LNameInfo><LNameNum>1</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>JAIN</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum><FName>SHOBHADEVIMRS</FName></FNameInfo><LNameInfo><LNameNum>2</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>JAIN</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>2</AbsNameNum><FName>SHANTIDEVIMRS</FName></FNameInfo><LNameInfo><LNameNum>3</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>JAIN</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum><FName>SONALMRS</FName></FNameInfo><LNameInfo><LNameNum>4</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>JAIN</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum><FName>NIDHIMRS</FName></FNameInfo><LNameInfo><LNameNum>5</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>CHHABRA</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>5</AbsNameNum><FName>MISHIKAMISS</FName></FNameInfo><LNameInfo><LNameNum>6</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>CHHABRA</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>6</AbsNameNum><FName>AVISHKAMISS</FName></FNameInfo><LNameInfo><LNameNum>7</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>JAIN</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>7</AbsNameNum><FName>LAISHAMISS</FName></FNameInfo><LNameInfo><LNameNum>8</LNameNum><NumPsgrs>1</NumPsgrs><NameType>I</NameType><LName>JAIN</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>8</AbsNameNum><FName>MANYA</FName></FNameInfo><NameRmkInfo><LNameNum>5</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>5</AbsNameNum><NameRmk>P-C04</NameRmk></NameRmkInfo><NameRmkInfo><LNameNum>6</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>6</AbsNameNum><NameRmk>P-C01</NameRmk></NameRmkInfo><NameRmkInfo><LNameNum>7</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>7</AbsNameNum><NameRmk>P-C02</NameRmk></NameRmkInfo><NameRmkInfo><LNameNum>8</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>8</AbsNameNum><NameRmk>29NOV17</NameRmk></NameRmkInfo><AirSeg><SegNum>1</SegNum><Status>HK</Status><Dt>20180427</Dt><DayChg>00</DayChg><AirV>AI</AirV><NumPsgrs>7</NumPsgrs><StartAirp>DEL</StartAirp><EndAirp>IMF</EndAirp><StartTm>925</StartTm><EndTm>1330</EndTm><BIC>T</BIC><FltNum>889</FltNum><OpSuf/><COG>N</COG><TklessInd>Y</TklessInd><ConxInd>N</ConxInd><FltFlownInd>N</FltFlownInd><MarriageNum/><SellType>O</SellType><StopoverIgnoreInd/><TDSValidateInd>N</TDSValidateInd><NonBillingInd/><PrevStatusCode>NN</PrevStatusCode><ScheduleValidationInd/></AirSeg><ProgramaticSSR><GFAXNum>1</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071734C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>2</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>2</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>2</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071735C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>3</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>3</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071736C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>4</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>4</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071737C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>5</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>5</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>5</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071738C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>6</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>6</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>6</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071739C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>7</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>7</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>7</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071740C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>8</GFAXNum><SSRCode>INFT</SSRCode><Status>KK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>JAIN/MANYA 29NOV17</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>9</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>8</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>8</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316071741C1</Text></ProgramaticSSRText><NonProgramaticSSR><GFAXNum>10</GFAXNum><SSRCode>CHLD</SSRCode><Vnd>AI</Vnd><Status>HK</Status><NumRequired>1</NumRequired><SSRText>/-1CHHABRA/MISHIKAMISS</SSRText></NonProgramaticSSR><NonProgramaticSSR><GFAXNum>11</GFAXNum><SSRCode>CHLD</SSRCode><Vnd>AI</Vnd><Status>HK</Status><NumRequired>1</NumRequired><SSRText>/-1CHHABRA/AVISHKAMISS</SSRText></NonProgramaticSSR><NonProgramaticSSR><GFAXNum>12</GFAXNum><SSRCode>CHLD</SSRCode><Vnd>AI</Vnd><Status>HK</Status><NumRequired>1</NumRequired><SSRText>/-1JAIN/LAISHAMISS</SSRText></NonProgramaticSSR><OSI><GFAXNum>1</GFAXNum><OSIV>YY</OSIV><OSIMsg>MOBILE-9928718080</OSIMsg></OSI><PhoneInfo><PhoneFldNum>1</PhoneFldNum><Pt>DEL</Pt><Type>A</Type><Phone>011 47677777</Phone></PhoneInfo><OtherFOP><ID>1</ID><Type>2</Type><Currency/><Amt/><AdditionalInfoAry></AdditionalInfoAry></OtherFOP><TkArrangement><Text>BOM 12MAR1229Z WS AG</Text></TkArrangement><VndRmk><RmkNum>1</RmkNum><TmStamp>1229</TmStamp><DtStamp>20180312</DtStamp><RmkType>I</RmkType><VType>A</VType><Vnd>AI</Vnd><Rmk>AUTO XX IF SSR TKNA/E/M/C NOT RCVD BY AI BY 2159/12MAR/DE L LT</Rmk></VndRmk><GenPNRInfo><FileAddr>E5FEFF94</FileAddr><CodeCheck>EF</CodeCheck><RecLoc>98WG14</RecLoc><Ver>5</Ver><OwningCRS>1G</OwningCRS><OwningAgncyName>RWT COM</OwningAgncyName><OwningAgncyPCC>000V</OwningAgncyPCC><CreationDt>20180312</CreationDt><CreatingAgntSignOn>000VGWS</CreatingAgntSignOn><CreatingAgntDuty>AG</CreatingAgntDuty><CreatingAgncyIATANum>14319126</CreatingAgncyIATANum><OrigBkLocn>BOMOU</OrigBkLocn><SATONum/><PTAInd>N</PTAInd><InUseInd>N</InUseInd><SimultaneousUpdInd/><BorrowedInd>N</BorrowedInd><GlobInd>N</GlobInd><ReadOnlyInd>N</ReadOnlyInd><FareDataExistsInd>Y</FareDataExistsInd><PastDtQuickInd>N</PastDtQuickInd><CurAgncyPCC>0JO7</CurAgncyPCC><QInd>Y</QInd><TkNumExistInd>Y</TkNumExistInd><IMUdataexists>N</IMUdataexists><ETkDataExistInd>Y</ETkDataExistInd><CurDtStamp>20180312</CurDtStamp><CurTmStamp>122943</CurTmStamp><CurAgntSONID>JO7GWS</CurAgntSONID><TravInsuranceInd/><PNRBFTicketedInd>Y</PNRBFTicketedInd><ZeppelinAgncyInd>N</ZeppelinAgncyInd><AgncyAutoServiceInd>N</AgncyAutoServiceInd><AgncyAutoNotifyInd>N</AgncyAutoNotifyInd><ZeppelinPNRInd>N</ZeppelinPNRInd><PNRAutoServiceInd>N</PNRAutoServiceInd><PNRAutoNotifyInd>N</PNRAutoNotifyInd><SuperPNRInd>N</SuperPNRInd><PNRBFPurgeDt>20180429</PNRBFPurgeDt><PNRBFChangeInd>N</PNRBFChangeInd><MCODataExists>N</MCODataExists><OrigRcvdField>PASSENGER</OrigRcvdField><IntContExists>N</IntContExists></GenPNRInfo><Email><ItemNum>1</ItemNum><Type>T</Type><Data>vivek@gyantravels.in</Data></Email><VndRecLocs><RecLocInfoAry><RecLocInfo><TmStamp>1229</TmStamp><DtStamp>20180312</DtStamp><Vnd>AI</Vnd><RecLoc>HNK7E</RecLoc></RecLocInfo></RecLocInfoAry></VndRecLocs></PNRBFRetrieve></PNRBFManagement_13></SubmitXmlOnSessionResult></SubmitXmlOnSessionResponse></soapenv:Body></soapenv:Envelope>";
//Adult=2	Child=1	Infant=1

        PNBF2Res = @"<?xml version='1.0' encoding='UTF-8'?>
<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
 <soapenv:Body><SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'><SubmitXmlOnSessionResult><PNRBFManagement_13 xmlns=''><PNRBFRetrieve><Control><KLRCnt>33</KLRCnt><KlrAry><Klr><ID>BP10</ID><NumOccur>4</NumOccur></Klr><Klr><ID>BP12</ID><NumOccur>4</NumOccur></Klr><Klr><ID>BP13</ID><NumOccur>2</NumOccur></Klr><Klr><ID>IT01</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP21</ID><NumOccur>5</NumOccur></Klr><Klr><ID>BP22</ID><NumOccur>5</NumOccur></Klr><Klr><ID>BP20</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP19</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP16</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP52</ID><NumOccur>1</NumOccur></Klr><Klr><ID>DPP9</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP32</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP26</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP48</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP28</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP27</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP08</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP40</ID><NumOccur>1</NumOccur></Klr></KlrAry></Control><LNameInfo><LNameNum>1</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>SIMON</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum><FName>THRESYAMMAMRS</FName></FNameInfo><LNameInfo><LNameNum>2</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>JOSE</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>2</AbsNameNum><FName>JOSIMOLMRS</FName></FNameInfo><LNameInfo><LNameNum>3</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>SANJU</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum><FName>ANNTRESAMISS</FName></FNameInfo><LNameInfo><LNameNum>4</LNameNum><NumPsgrs>1</NumPsgrs><NameType>I</NameType><LName>JOSE</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum><FName>BABYOFJOSIMOL</FName></FNameInfo><NameRmkInfo><LNameNum>3</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum><NameRmk>P-C03</NameRmk></NameRmkInfo><NameRmkInfo><LNameNum>4</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum><NameRmk>23MAR18</NameRmk></NameRmkInfo><AirSeg><SegNum>1</SegNum><Status>HK</Status><Dt>20180410</Dt><DayChg>00</DayChg><AirV>AI</AirV><NumPsgrs>3</NumPsgrs><StartAirp>DEL</StartAirp><EndAirp>COK</EndAirp><StartTm>1405</StartTm><EndTm>1710</EndTm><BIC>U</BIC><FltNum>512</FltNum><OpSuf/><COG>N</COG><TklessInd>Y</TklessInd><ConxInd>N</ConxInd><FltFlownInd>N</FltFlownInd><MarriageNum/><SellType>O</SellType><StopoverIgnoreInd/><TDSValidateInd>N</TDSValidateInd><NonBillingInd/><PrevStatusCode>NN</PrevStatusCode><ScheduleValidationInd/></AirSeg><ProgramaticSSR><GFAXNum>1</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316875830C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>2</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>2</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>2</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316875831C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>3</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>3</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316875832C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>4</GFAXNum><SSRCode>INFT</SSRCode><Status>KK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>JOSE/BABYOFJOSIMOL 23MAR18</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>5</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>4</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985316875833C1</Text></ProgramaticSSRText><NonProgramaticSSR><GFAXNum>6</GFAXNum><SSRCode>CHLD</SSRCode><Vnd>AI</Vnd><Status>HK</Status><NumRequired>1</NumRequired><SSRText>/-1SANJU/ANNTRESAMISS</SSRText></NonProgramaticSSR><OSI><GFAXNum>1</GFAXNum><OSIV>YY</OSIV><OSIMsg>MOBILE-7836004677</OSIMsg></OSI><PhoneInfo><PhoneFldNum>1</PhoneFldNum><Pt>DEL</Pt><Type>A</Type><Phone>011 47677777</Phone></PhoneInfo><OtherFOP><ID>1</ID><Type>2</Type><Currency/><Amt/><AdditionalInfoAry></AdditionalInfoAry></OtherFOP><TkArrangement><Text>BOM 26MAR1419Z WS AG</Text></TkArrangement><GenRmkInfo><GenRmkNum>1</GenRmkNum><CreationDt>20180326</CreationDt><CreationTm>1418</CreationTm><GenlRmkQual/><GenRmk>ON-LINE BOOKING - 9870667718</GenRmk></GenRmkInfo><VndRmk><RmkNum>1</RmkNum><TmStamp>1418</TmStamp><DtStamp>20180326</DtStamp><RmkType>I</RmkType><VType>A</VType><Vnd>AI</Vnd><Rmk>AUTO XX IF SSR TKNA/E/M/C NOT RCVD BY AI BY 1948/30MAR/ DEL LT</Rmk></VndRmk><GenPNRInfo><FileAddr>E42F8F2A</FileAddr><CodeCheck>BB</CodeCheck><RecLoc>76DMNS</RecLoc><Ver>5</Ver><OwningCRS>1G</OwningCRS><OwningAgncyName>RWT COM</OwningAgncyName><OwningAgncyPCC>000V</OwningAgncyPCC><CreationDt>20180326</CreationDt><CreatingAgntSignOn>000VGWS</CreatingAgntSignOn><CreatingAgntDuty>AG</CreatingAgntDuty><CreatingAgncyIATANum>14319126</CreatingAgncyIATANum><OrigBkLocn>BOMOU</OrigBkLocn><SATONum/><PTAInd>N</PTAInd><InUseInd>N</InUseInd><SimultaneousUpdInd/><BorrowedInd>N</BorrowedInd><GlobInd>N</GlobInd><ReadOnlyInd>N</ReadOnlyInd><FareDataExistsInd>Y</FareDataExistsInd><PastDtQuickInd>N</PastDtQuickInd><CurAgncyPCC>0JO7</CurAgncyPCC><QInd>Y</QInd><TkNumExistInd>Y</TkNumExistInd><IMUdataexists>N</IMUdataexists><ETkDataExistInd>Y</ETkDataExistInd><CurDtStamp>20180326</CurDtStamp><CurTmStamp>141907</CurTmStamp><CurAgntSONID>JO7GWS</CurAgntSONID><TravInsuranceInd/><PNRBFTicketedInd>Y</PNRBFTicketedInd><ZeppelinAgncyInd>N</ZeppelinAgncyInd><AgncyAutoServiceInd>N</AgncyAutoServiceInd><AgncyAutoNotifyInd>N</AgncyAutoNotifyInd><ZeppelinPNRInd>N</ZeppelinPNRInd><PNRAutoServiceInd>N</PNRAutoServiceInd><PNRAutoNotifyInd>N</PNRAutoNotifyInd><SuperPNRInd>N</SuperPNRInd><PNRBFPurgeDt>20180412</PNRBFPurgeDt><PNRBFChangeInd>N</PNRBFChangeInd><MCODataExists>N</MCODataExists><OrigRcvdField>PASSENGER</OrigRcvdField><IntContExists>N</IntContExists></GenPNRInfo><Email><ItemNum>1</ItemNum><Type>T</Type><Data>9810667@gmail.com</Data></Email><VndRecLocs><RecLocInfoAry><RecLocInfo><TmStamp>1418</TmStamp><DtStamp>20180326</DtStamp><Vnd>AI</Vnd><RecLoc>J86V8</RecLoc></RecLocInfo></RecLocInfoAry></VndRecLocs></PNRBFRetrieve></PNRBFManagement_13></SubmitXmlOnSessionResult></SubmitXmlOnSessionResponse></soapenv:Body></soapenv:Envelope>";


            //Adult 1
        PNBF2Res = @"<?xml version='1.0' encoding='UTF-8'?>
<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
 <soapenv:Body><SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'><SubmitXmlOnSessionResult><PNRBFManagement_13 xmlns=''><PNRBFRetrieve><Control><KLRCnt>16</KLRCnt><KlrAry><Klr><ID>BP10</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP12</ID><NumOccur>1</NumOccur></Klr><Klr><ID>IT01</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP21</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP22</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP20</ID><NumOccur>0</NumOccur></Klr><Klr><ID>BP19</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP16</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP52</ID><NumOccur>1</NumOccur></Klr><Klr><ID>DPP9</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP32</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP26</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP48</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP28</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP27</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP08</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP40</ID><NumOccur>1</NumOccur></Klr></KlrAry></Control><LNameInfo><LNameNum>1</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>DEY</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum><FName>KANCHANKUMARMR</FName></FNameInfo><AirSeg><SegNum>1</SegNum><Status>HK</Status><Dt>20180305</Dt><DayChg>00</DayChg><AirV>AI</AirV><NumPsgrs>1</NumPsgrs><StartAirp>CCU</StartAirp><EndAirp>DEL</EndAirp><StartTm>700</StartTm><EndTm>910</EndTm><BIC>L</BIC><FltNum>763</FltNum><OpSuf/><COG>N</COG><TklessInd>Y</TklessInd><ConxInd>N</ConxInd><FltFlownInd>N</FltFlownInd><MarriageNum/><SellType>O</SellType><StopoverIgnoreInd/><TDSValidateInd>N</TDSValidateInd><NonBillingInd/><PrevStatusCode>NN</PrevStatusCode><ScheduleValidationInd/></AirSeg><ProgramaticSSR><GFAXNum>1</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0985315397018C1</Text></ProgramaticSSRText><OSI><GFAXNum>1</GFAXNum><OSIV>YY</OSIV><OSIMsg>MOBILE-9230510021</OSIMsg></OSI><PhoneInfo><PhoneFldNum>1</PhoneFldNum><Pt>DEL</Pt><Type>A</Type><Phone>011 47677777</Phone></PhoneInfo><OtherFOP><ID>1</ID><Type>2</Type><Currency/><Amt/><AdditionalInfoAry></AdditionalInfoAry></OtherFOP><TkArrangement><Text>BOM 23FEB1806Z WS AG</Text></TkArrangement><GenRmkInfo><GenRmkNum>1</GenRmkNum><CreationDt>20180223</CreationDt><CreationTm>1806</CreationTm><GenlRmkQual/><GenRmk>ON-LINE BOOKING - 8013441515</GenRmk></GenRmkInfo><VndRmk><RmkNum>1</RmkNum><TmStamp>1806</TmStamp><DtStamp>20180223</DtStamp><RmkType>I</RmkType><VType>A</VType><Vnd>AI</Vnd><Rmk>AUTO XX IF SSR TKNA/E/M/C NOT RCVD BY AI BY 2336/27FEB/DE L LT</Rmk></VndRmk><GenPNRInfo><FileAddr>C945794A</FileAddr><CodeCheck>D7</CodeCheck><RecLoc>XZ2ZMG</RecLoc><Ver>5</Ver><OwningCRS>1G</OwningCRS><OwningAgncyName>RWT COM</OwningAgncyName><OwningAgncyPCC>000V</OwningAgncyPCC><CreationDt>20180223</CreationDt><CreatingAgntSignOn>000VGWS</CreatingAgntSignOn><CreatingAgntDuty>AG</CreatingAgntDuty><CreatingAgncyIATANum>14319126</CreatingAgncyIATANum><OrigBkLocn>BOMOU</OrigBkLocn><SATONum/><PTAInd>N</PTAInd><InUseInd>N</InUseInd><SimultaneousUpdInd/><BorrowedInd>N</BorrowedInd><GlobInd>N</GlobInd><ReadOnlyInd>N</ReadOnlyInd><FareDataExistsInd>Y</FareDataExistsInd><PastDtQuickInd>N</PastDtQuickInd><CurAgncyPCC>5F86</CurAgncyPCC><QInd>Y</QInd><TkNumExistInd>Y</TkNumExistInd><IMUdataexists>N</IMUdataexists><ETkDataExistInd>Y</ETkDataExistInd><CurDtStamp>20180223</CurDtStamp><CurTmStamp>180653</CurTmStamp><CurAgntSONID>5F86GWS</CurAgntSONID><TravInsuranceInd/><PNRBFTicketedInd>Y</PNRBFTicketedInd><ZeppelinAgncyInd>N</ZeppelinAgncyInd><AgncyAutoServiceInd>N</AgncyAutoServiceInd><AgncyAutoNotifyInd>N</AgncyAutoNotifyInd><ZeppelinPNRInd>N</ZeppelinPNRInd><PNRAutoServiceInd>N</PNRAutoServiceInd><PNRAutoNotifyInd>N</PNRAutoNotifyInd><SuperPNRInd>N</SuperPNRInd><PNRBFPurgeDt>20180307</PNRBFPurgeDt><PNRBFChangeInd>N</PNRBFChangeInd><MCODataExists>N</MCODataExists><OrigRcvdField>PASSENGER</OrigRcvdField><IntContExists>N</IntContExists></GenPNRInfo><Email><ItemNum>1</ItemNum><Type>T</Type><Data>samarpaul249@gmail.com</Data></Email><VndRecLocs><RecLocInfoAry><RecLocInfo><TmStamp>1806</TmStamp><DtStamp>20180223</DtStamp><Vnd>AI</Vnd><RecLoc>JGZ28</RecLoc></RecLocInfo></RecLocInfoAry></VndRecLocs></PNRBFRetrieve></PNRBFManagement_13></SubmitXmlOnSessionResult></SubmitXmlOnSessionResponse></soapenv:Body></soapenv:Envelope>";

        //XmlTextReader xmlReader = new XmlTextReader("d:\\PNBF2Res.xml");

        ArrayList TktNoArray = new ArrayList();
        XmlDocument xd = new XmlDocument();
                            xd.LoadXml(PNBF2Res);
       // xd.LoadXml("d:\\PNBF2Res.xml");
                           // XmlNode xxd = xd.SelectSingleNode("//PNRBFRetrieve");
                           //// XmlNodeList xt = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSRText[starts-with(Text,'098')]");
                           // XmlNodeList xt = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSRText");
                           // // XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR");
                           // //XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR[starts-with(SSRCode,'TKNE')]");
                           // XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR");

                            XmlNode xxd = xd.SelectSingleNode("//PNRBFRetrieve");
                            XmlNodeList xt = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSRText");
                            XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR");
                            //XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR[starts-with(SSRCode,'TKNE')]");

                            int aa = xt.Count - 1;
                            for (int ii = 0; ii <= xt1.Count - 1; ii++)
                            {
                                XmlNode xt2 = xt1[ii];
                                xt1[ii].AppendChild(xt[ii]);
                                xxd.ReplaceChild(xt2, xt1[ii]);
                            }
                            int i = 0;
                            XDocument xdo = XDocument.Load(new XmlNodeReader(xxd));

                            var mmmm = from t in xdo.Descendants("PNRBFRetrieve").Descendants("ProgramaticSSR")
                                       select new
                                       {
                                           tk = t.Element("ProgramaticSSRText").Element("Text").Value
                                           //abs = t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value
                                       };


                            var tkt4 = from t1 in xdo.Descendants("PNRBFRetrieve").Descendants("ProgramaticSSR")
                                      //from p in xdo.Descendants("PNRBFRetrieve").Descendants("FNameInfo")
                                      //from l in xdo.Descendants("PNRBFRetrieve").Descendants("LNameInfo")
                                      where
                                      //p.Element("AbsNameNum").Value == t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value &&
                                      //l.Element("LNameNum").Value == t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value
                                      //&&
                                      t1.Element("Status").Value == "HK"

                                      select new
                                      {
                                          indx1 = i,
                                          //fn = p.Element("FName").Value,
                                          //ln = l.Element("LName").Value,
                                          tkl = t1.Element("ProgramaticSSRText").Element("Text").Value,
                                          //abs = t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value

                                      };


                            var tkt = from t in xdo.Descendants("PNRBFRetrieve").Descendants("ProgramaticSSR")
                                      from p in xdo.Descendants("PNRBFRetrieve").Descendants("FNameInfo")
                                      from l in xdo.Descendants("PNRBFRetrieve").Descendants("LNameInfo")
                                      where 
                                      p.Element("AbsNameNum").Value == t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value &&
                                      l.Element("LNameNum").Value == t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value 
                                      && t.Element("Status").Value == "HK" 
                                      
                                      select new
                                      {
                                          indx = i,
                                          fn = p.Element("FName").Value,
                                          ln = l.Element("LName").Value,
                                          tk = t.Element("ProgramaticSSRText").Element("Text").Value,
                                          abs = t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value

                                      };

                            foreach (var a in tkt)
                            {
                                i++;
                                string[] tkt1 = Utility.Split(a.fn, " ");
                                string nm = "", tkno = "";
                                for (int n = tkt1.Length - 1; n >= 0; n--)
                                {
                                    nm = nm + tkt1[n].ToString();
                                }
                                if (a.tk.Contains(PaxName) == false)
                                {
                                    tkno = Utility.Left(Utility.Left(a.tk, 13), 3) + "-" + Utility.Right(Utility.Left(a.tk, 13), 10); 
                                }

                                //tkno = Utility.Left(Utility.Left(a.tk, 13), 3) + "-" + Utility.Right(Utility.Left(a.tk, 13), 10);
                                TktNoArray.Add(nm + a.ln + "/" + tkno);
                            }
        }
        catch (Exception ex) { }
                        }
    

}
