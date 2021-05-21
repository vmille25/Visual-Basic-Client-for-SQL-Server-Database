Imports System.Data.SqlClient


Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function GetPetById(ByVal petID As Integer) As Pet
        Dim pet As Pet = New Pet()
        Dim connStr As String = "server=.;database=PetsDB;integrated security=SSPI"

        Using conn As SqlConnection = New SqlConnection(connStr)
            Dim command As SqlCommand = New SqlCommand("spGetPetsById", conn)
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.Add(New SqlParameter() With {
                .ParameterName = "@Id",
                .Value = petID
            })
            conn.Open()
            Dim sqlReader As SqlDataReader = command.ExecuteReader()

            While sqlReader.Read()
                pet.ID = Convert.ToInt32(sqlReader("Id"))
                pet.Name = sqlReader("Name").ToString()
                pet.Species = sqlReader("Species").ToString()
                pet.AGe = Convert.ToInt32(sqlReader("Age"))
            End While
        End Using

        Return pet
    End Function

End Class