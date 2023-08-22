<%@ WebHandler Language="VB" Class="CAPTCHA" %>
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web
Imports System.Web.SessionState
Public Class CAPTCHA : Implements IHttpHandler, IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Using b As New Bitmap(101, 40, PixelFormat.Format32bppArgb)
            Using g As Graphics = Graphics.FromImage(b)
                Dim rect As New Rectangle(0, 0, 100, 39)
                g.FillRectangle(Brushes.White, rect)
                ' Create string to draw.
                Dim r As New Random()
                Dim startIndex As Integer = r.[Next](1, 5)
                Dim length As Integer = r.[Next](5, 5)
                Dim drawString As [String] = Guid.NewGuid().ToString().Replace("-", "0").Substring(startIndex, length)
                context.Session("Captcha") = drawString.ToString()
                ' Create font and brush.
                Dim drawFont As New Font("Arial", 16, FontStyle.Italic)
                Using drawBrush As New SolidBrush(Color.Black)
                    ' Create point for upper-left corner of drawing.
                    Dim drawPoint As New PointF(15, 10)

                    ' Draw string to screen.
                    g.DrawRectangle(New Pen(Color.DeepPink, 0), rect)
                    g.DrawString(drawString, drawFont, drawBrush, drawPoint)
                End Using
                b.Save(context.Response.OutputStream, ImageFormat.Jpeg)
                context.Response.ContentType = "image/jpeg"
                context.Response.[End]()
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class