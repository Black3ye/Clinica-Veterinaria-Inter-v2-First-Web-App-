﻿Imports System.Data.SqlClient
Public Class MC
    Inherits System.Web.UI.MasterPage
    Dim Global_class As Declaraciones = New Declaraciones
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Global_class.UserID = Request.QueryString.Item("UID")
        Global_class.UserName = Request.QueryString.Item("UN")
        Global_class.ID = Request.QueryString.Item("ID")
        Nameuser.Text = Global_class.UserName
        If Session.IsNewSession = True Then
            Response.Redirect("Login.aspx")
        End If
        If Session.Contents.Count < "1" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub
    Private Sub Evaluate()
        Dim sql As String = "Select [role] as role from [dbo].[Users] where username='" & Trim(Global_class.UserID) & "'"
        Dim sda As SqlDataAdapter = New SqlDataAdapter(sql, Declaraciones.Coneccion)
        Dim dt As DataTable = New DataTable
        Dim commandBuilder As New SqlClient.SqlCommandBuilder(sda)
        dt.Locale = System.Globalization.CultureInfo.InvariantCulture
        dt.Clear()
        sda.Fill(dt)
        sda.Dispose()
        Global_class.role = Trim(dt.Rows(0).Item("role"))
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("Menu.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName)
    End Sub

    Private Sub btnlo_Click(sender As Object, e As EventArgs) Handles btnlo.Click
        Session.Contents.Clear()
        Session.RemoveAll()
        Session.Abandon()
        Response.Redirect("Search.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName)
        Response.Buffer = True
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
        Response.Expires = -1500
        Response.CacheControl = "no-cache"
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Response.Redirect("MCadd.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName & "&NI=" & Global_class.ID & " ")
    End Sub

    Private Sub btnSEdit_Click(sender As Object, e As EventArgs) Handles btnSEdit.Click
        Response.Redirect("MCSEdit.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName & "&NI=" & Global_class.ID & " ")
    End Sub

    Private Sub option_Click(sender As Object, e As EventArgs) Handles [option].Click
        If Report.Visible = False Then
            Report.Visible = True
        Else
            Report.Visible = False
        End If
        If Report1.Visible = False Then
            Report1.Visible = True
        Else
            Report1.Visible = False
        End If
    End Sub

    Private Sub Report_Click(sender As Object, e As EventArgs) Handles Report.Click
        Evaluate()
        If Global_class.role = "Administrator" Then
            Dim s As String = "window.open('Reportes/ReportC.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName & "&NI=" & Global_class.ID & " ', '_blank');"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Usted no es Administrador');", True)
        End If

    End Sub

    Private Sub btnDC_Click(sender As Object, e As EventArgs) Handles btnDC.Click
        Response.Redirect("MCDC.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName & "&NI=" & Global_class.ID & " ")
    End Sub

    Private Sub Report1_Click(sender As Object, e As EventArgs) Handles Report1.Click
        Evaluate()
        If Global_class.role = "Administrator" Then
            Dim s As String = "window.open('Reportes/ReporteDC.aspx?UID=" & Global_class.UserID & "&UN=" & Global_class.UserName & "&NI=" & Global_class.ID & " ', '_blank');"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Usted no es Administrador');", True)
        End If
    End Sub
End Class