Imports SistemaGestionPedidos.Entities
Imports System.Data
Imports System.Data.SqlClient
Public Class CategoriaDAL
    Inherits BaseDAL

    'MetodosCrud
    Public Shared Sub Create(categoria As CategoriaEntity)
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            'agregar registro
            Dim sql As String = "Insert into Categoria(Nombre) values(@Nombre) Select Scope_Identity()"
            Dim cmd As New SqlCommand(sql, conex)
            'agregamos el parametro
            cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre)
            categoria.ID = cmd.ExecuteScalar()


        End Using



    End Sub
    Public Shared Sub Update(categoria As CategoriaEntity)
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            'agregar registro
            Dim sql As String = "Update Categoria set Nombre = @Nombre where ID = @idCategoria"
            Dim cmd As New SqlCommand(sql, conex)
            'agregamos el parametro
            cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre)
            cmd.Parameters.AddWithValue("@idCategoria", categoria.Nombre)
            cmd.ExecuteNonQuery() 'ejecutarsinretorno



        End Using



    End Sub

    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            Dim sql As String = "Delete from categoria where ID= @idcategoria"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idcategoria", id)
            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using
        Return SeElimino
    End Function

    Public Shared Function GetByID(id As Integer) As CategoriaEntity

        Dim categoria As CategoriaEntity
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()

            Dim sql As String = "Select * from categoria where id = @idcategoria"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idcategoria", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                categoria = ConvertToObject(reader)
            End If


        End Using
    End Function

    Public Shared Function GetByValor(valor As String) As List(Of CategoriaEntity)
        Dim list As New List(Of CategoriaEntity)
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            Dim sql As String = "Select * from categoria " &
                "Where Nombre Like '%' + @Valor + '%' ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                list.Add(ConvertToObject(reader))

            End While
            Return list

        End Using
    End Function


    Public Shared Function GetALL() As List(Of CategoriaEntity)
        Dim list As New List(Of CategoriaEntity)

        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            Dim sql As String = "Select * from categoria Order By Nombre "

            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                list.Add(ConvertToObject(reader))

            End While
            Return list

        End Using
    End Function
    Private Shared Function ConvertToObject(reader As IDataReader) As CategoriaEntity
        Dim categoria As New CategoriaEntity
        categoria.ID = reader("ID")
        categoria.Nombre = reader("Nombre")

        Return categoria

    End Function

End Class
