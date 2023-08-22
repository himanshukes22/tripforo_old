using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
//using EncrytionDecryption;
using IRCTC_RDS;

public partial class Eticketing_Default1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string RDSRequest = string.Empty;
        string EncryptedReq = string.Empty;
        string DecryptedReq = string.Empty;
        string ReturnUrl = string.Empty;
        try
        {
            RDSRequest = Request.Form.ToString();
            EncryptedReq = Request.Form["encdata"];//Request.Form.ToString();
            ReturnUrl = Request.UrlReferrer.AbsoluteUri;

            string rtnString = "yourStringHere";
            Response.Write(rtnString);
            Response.End();
            //return EncryptedReq;
            // int flag = RDS.InsertExceptionLog("RDS-IRCTC", "Verification.aspx.cs", "Check Response", "RDSVERIFICATION1", Request.Form["encdata"], "IRCTC_ReturnUrl-" + ReturnUrl + "~~" + Request.Form.ToString());
        }
        catch (Exception ex)
        {
           // int flag = RDS.InsertExceptionLog("RDS-IRCTC", "Verification.aspx.cs", "Check Response", "RDSVERIFICATION1_EXC", ex.Message + "Request_Form" + Request.Form.ToString(), "IRCTC_ReturnUrl-" + ReturnUrl + ",Encdata: " + EncryptedReq + "~~" + ex.StackTrace.ToString());
        }

    }


    #region Socket
    public class StateObject
    {
        //EncryptDecryptString ENDE = new EncryptDecryptString();
        //IrctcRdsPayment RDS = new IrctcRdsPayment();

        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);// Local Working Enviroment         
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8081); // Test server

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            EncryptDecryptString ENDE = new EncryptDecryptString();
            IrctcRdsPayment RDS = new IrctcRdsPayment();
            string EncryptedReq = string.Empty;
            string DecryptedReq = string.Empty;
            string RDSEncryptedRes = string.Empty;
            string RDSDecryptedRes = string.Empty;
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            try
            {
                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read 
                    // more data.
                    content = Convert.ToString(state.sb); //state.sb.ToString();
                                                          //                content = @"content: GET /?encdata=D5620509499F1C240C7FBBDDA4D4C1C22EB36C2C5C3D74B0AC919C18AD3A13EEC9662620D667DFBE8AD2B03C3E596C8ACE9126280316014CBC62BDB9526DE58D03D91E3875616D63FAB9F29F8A00678C151F75C2134C87421B6971CD22BFA27A06601C113533E90D14248D6AFDAC9B849E7911543AF7D332E51335232D8FAF801A7CE72D71BDBF6EE3221F05B4C28E25EF1125CA01EA32FEB18E1AD26C38EBCF7B8E04A6AF7EF997E7348BACEE0F7734 HTTP/1.1
                                                          //Host: 172.18.80.75:8081
                                                          //Connection: keep-alive
                                                          //Upgrade-Insecure-Requests: 1
                                                          //User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36
                                                          //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
                                                          //Accept-Encoding: gzip, deflate
                                                          //Accept-Language: en-US,en;q=0.9

                    //";
                    bool Action = false;
                    #region Old Parse Data 
                    //   //EncryptedReq = content;//For Local Test
                    //if(content.Contains("encdata=")==true)
                    //{
                    //   if (content.Split('=').Length > 1 && (((content.Split('='))[1]).Split('/')).Length > 1)
                    //   {
                    //           EncryptedReq = (((((content.Split('='))[1]).Split('/')))[0]).Remove((((((content.Split('='))[1]).Split('/')))[0]).Length - 5);
                    //               //(((((content.Split('='))[1]).Split('/')))[0]).Remove((((((content.Split('='))[1]).Split('/')))[0]).Length - 9);//Request.Form["encdata"];
                    //       Action = true;
                    //   }
                    //}
                    #endregion


                    #region Encdata Value Get
                    try
                    {
                        if (content.Contains("encdata=") == true)
                        {
                            if (content.Split('=').Length > 1)//&& !string.IsNullOrEmpty(EncryptedReq))
                            {
                                EncryptedReq = content.Split('=')[1];
                                Action = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //int insert = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback", "ReadCallback2_bytesRead: " + Convert.ToString(bytesRead), ex.Message, ex.StackTrace);
                        int flag = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback-Exception", "ReadCallback-Exception", "ExMessage: " + ex.Message + ",EncryptedReq: " + EncryptedReq, "ExStackTrace: " + ex.StackTrace + ", content: " + content);
                    }
                    #endregion
                    try
                    {
                        int insert = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback", "ReadCallback", "EncryptedReq: " + EncryptedReq, "content: " + content);
                    }
                    catch (Exception ex)
                    {
                        int flag = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback-Exception", "ParseDoubleVerificationReq-Exception2", "ExMessage: " + ex.Message + ",EncryptedReq: " + EncryptedReq, "ExStackTrace: " + ex.StackTrace + ", content: " + content);
                        //int insert = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback", "ReadCallback", "EncryptedReq: " + EncryptedReq, "content: " + content);
                    }

                    if (!string.IsNullOrEmpty(EncryptedReq) && Action == true)
                    {
                        //EncryptedReq = RDSRequest.Split('=')[1];
                        //DecryptedReq = ENDE.DecryptString(EncryptedReq, "abfb7c8d48dfc4f1ce7ed92a44989f25");
                        //string EnDecryptKey = Convert.ToString(ConfigurationManager.AppSettings["RDSKEY"]);
                        //EncryptDecryptString obj = new EncryptDecryptString();
                        DecryptedReq = ENDE.DecryptString(EncryptedReq);

                        #region Parse Request
                        try
                        {
                            //EncryptDecryptString ENDE = new EncryptDecryptString();
                            IrctcRdsPayment IRCTC = new IrctcRdsPayment();
                            string PgMsg = IRCTC.ParseDoubleVerificationReq(EncryptedReq, DecryptedReq, "", "BookWidUs","");
                            if (PgMsg.Contains("~"))
                            {
                                if (PgMsg.Split('~').Length > 2 && PgMsg.Split('~')[0] == "yes")
                                {
                                    //if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                                    //{
                                    //    Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                                    //}
                                    RDSEncryptedRes = PgMsg.Split('~')[1];
                                    RDSDecryptedRes = PgMsg.Split('~')[2];
                                    // merchantCode = IR_CRIS | reservationId = 200000063011202 | txnAmount = 315.00 | bankTxnId = FR8113043473094 | checkSum = 6D7ED9E7CC1B316C1B56BAC9175C457EB77744C3C2745B52925D47E6C40FCD59
                                }
                            }
                            else
                            { }

                        }
                        catch (Exception ex)
                        {
                            int flag = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback-Exception", "ParseDoubleVerificationReq-Exception3", "ExMessage: " + ex.Message + ",EncryptedReq: " + EncryptedReq, "ExStackTrace: " + ex.StackTrace + ", content: " + content);
                        }
                        #endregion
                    }

                    content = RDSEncryptedRes;// +"~"+RDSDecryptedRes;
                    if (!string.IsNullOrEmpty(RDSEncryptedRes) && RDSDecryptedRes.Contains("reservationId"))
                    {
                        // All the data has been read from the 
                        // client. Display it on the console.
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        // Echo the data back to the client.
                        Send(handler, content);
                    }
                    else
                    {
                        Send(handler, content);
                    }
                    //else
                    //{
                    //    // Not all data received. Get more.
                    //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    //    new AsyncCallback(ReadCallback), state);
                    //}




                    //content = DecryptedReq+ "<EOF>";
                    //if (content.IndexOf("<EOF>") > -1) {
                    //    // All the data has been read from the 
                    //    // client. Display it on the console.
                    //    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    //        content.Length, content );
                    //    // Echo the data back to the client.
                    //    Send(handler, content);
                    //} else {
                    //    // Not all data received. Get more.
                    //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    //    new AsyncCallback(ReadCallback), state);
                    //}
                }
                //End
            }
            catch (Exception ex)
            {
                int insert = RDS.InsertExceptionLog("SERVER-VERIFICATION", "Server.cs", "public_static_void_ReadCallback", "ReadCallback2_bytesRead: " + Convert.ToString(bytesRead), ex.Message, ex.StackTrace);
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }
    #endregion

}
