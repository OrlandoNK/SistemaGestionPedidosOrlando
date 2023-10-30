Imports SistemaGestionPedidos.Entities
Imports System.Data
Imports System.Data.SqlClient

Public Class ArticuloDAL
    Inherits BaseDAL
    Public Shared Sub Create(articulo As ArticuloEntity)
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            'agregar registro
            Dim sql As String = "Insert into Articulo (IDCategoria, Nombre, Descripcion, PrecioCompra, PrecioVenta, Stock) " &
             "values(@Idcategoria, @nombre, @descripcion, @preciocompra, @precioVenta, @stock) Select Scope_Identity()"
            Dim cmd As New SqlCommand(sql, conex)
            'agregamos el parametro
            cmd.Parameters.AddWithValue("@idcategoria", articulo.IDCategoria)
            cmd.Parameters.AddWithValue("@nombre", articulo.Nombre)
            cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion)
            cmd.Parameters.AddWithValue("@preciocompra", articulo.PrecioCompra)
            cmd.Parameters.AddWithValue("@precioventa", articulo.PrecioVenta)
            cmd.Parameters.AddWithValue("@stock", articulo.Stock)
            articulo.ID = Convert.ToInt32(cmd.ExecuteScalar())


        End Using



    End Sub
    Public Shared Sub Update(articulo As ArticuloEntity)
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            'agregar registro
            Dim sql As String = "update Articulo set IDCategoria = @Idcategoria, Nombre = @nombre, 
            Descripcion = @descripcion, PrecioCompra = @preciocompra, 
                 PrecioVenta =  @precioVenta, Stock =  @stock where ID =@idarticulo "

            Dim cmd As New SqlCommand(sql, conex)
            'agregamos el parametro
            cmd.Parameters.AddWithValue("@idarticulo", articulo.ID)
            cmd.Parameters.AddWithValue("@idcategoria", articulo.IDCategoria)
            cmd.Parameters.AddWithValue("@nombre", articulo.Nombre)
            cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion)
            cmd.Parameters.AddWithValue("@preciocompra", articulo.PrecioCompra)
            cmd.Parameters.AddWithValue("@precioventa", articulo.PrecioVenta)
            cmd.Parameters.AddWithValue("@stock", articulo.Stock)
            cmd.ExecuteNonQuery()


        End Using



    End Sub

    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            Dim sql As String = "Delete from articulo where ID= @idarticulo"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idarticulo", id)
            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using
        Return SeElimino
    End Function

    Public Shared Function GetByID(id As Integer) As ArticuloEntity

        Dim articulo As ArticuloEntity
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()

            Dim sql As String = "Select * from articulo where id = @idarticulo"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idarticulo", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                articulo = ConvertToObject(reader)
            End If


        End Using
    End Function

    Public Shared Function GetByValor(valor As String) As List(Of ArticuloEntity)
        Dim list As New List(Of ArticuloEntity)()
        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            Dim sql As String = "Select * from articulo " &
                "Where Nombre Like '%' + @valor + '%' or Descripcion like '%' + @valor + '%' ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                list.Add(ConvertToObject(reader))

            End While
            Return list

        End Using
    End Function


    Public Shared Function GetALL() As List(Of ArticuloEntity)
        Dim list As New List(Of ArticuloEntity)

        Using conex As New SqlConnection(CadenaConexion)
            conex.Open()
            Dim sql As String = "Select * from Articulo Order By Nombre "

            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                list.Add(ConvertToObject(reader))

            End While
            Return list

        End Using
    End Function
    Private Shared Function ConvertToObject(reader As IDataReader) As ArticuloEntity
        Dim articulo As New ArticuloEntity()
        articulo.ID = Convert.ToInt32(reader("ID"))
        articulo.IDCategoria = Convert.ToInt32(reader("idcategoria"))
        articulo.Nombre = Convert.ToString(reader("Nombre"))
        articulo.Descripcion = Convert.ToString(reader("Descripcion"))
        articulo.PrecioCompra = Convert.ToInt32(reader("PrecioCompra"))
        articulo.PrecioVenta = Convert.ToString(reader("PrecioVenta"))
        articulo.Stock = Convert.ToInt32(reader("Stock"))
        Return articulo

    End Function

End Class
