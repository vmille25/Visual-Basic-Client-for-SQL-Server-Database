# Visual-Basic-Client-for-SQL-Server-Database

1. Add the JQuery Library to the project
2. Add a new class to the project called Pet.vb

The following code should be added to the Pet class
```visual basic
Public Class Pet
    Public Property ID As Integer
    Public Property Name As String
    Public Property Species As String
    Public Property AGe As Integer
End Class
```

3. Add the following namespaces to the WebForm1.aspx.vb file (Don't forget, this file may not appear in your solution explorer, if it is not in your solution explorer then look in your File Explorer in the project's working directory for this file): System.Data.SqlClient
4. Create and add the code for the GetPetById method in the WebForm1 class
The WebForm1 class should look as follows:
```visualbasic
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
```

5. Modify the WebForm1.aspx file to include a table to hold our Pet's information and the AJAX code to populate the table

The file should look like the following:
```html
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="PetDB_Client_VB.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MY PET DATABASE CLIENT</title>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGetPet').click(function () {
                var petId = $('#inputID').val();

                $.ajax({
                    url: 'WebForm1.aspx/GetPetById',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{petID:' + petId + '}',
                    dataType: 'json',
                    success: function (data) {
                        $('#outputName').val(data.d.Name);
                        $('#outputSpecies').val(data.d.Species);
                        $('#outputAge').val(data.d.AGe);
                    },
                    error: function (error) {
                        alert(error);
                    }
                })
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            ID :
            <input id="inputID" type="text" style="width: 100px"/>
            <input type="button" id="btnGetPet" value="Get Pet Information" />
            <br />
            <br />
            <table border="1" style="border-block: groove">
                <tr>
                    <td>Name</td>
                    <td><input id="outputName" type="text" /></td>

                </tr>
                <tr>
                    <td>Species</td>
                    <td><input id="outputSpecies" type="text" /></td>
                </tr>
                <tr>
                    <td>Age</td>
                    <td><input id="outputAge" type="text" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
```

6. Run the program and ensure proper functionality.
