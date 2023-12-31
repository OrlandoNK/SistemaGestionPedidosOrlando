﻿Public Class DetallePedidoEntity
    Public Property ID As Integer
    Public Property IDPedido As Integer
    Public Property IDArticulo As Integer
    Public Property NombreArticulo As String
    Public Property Cantidad As Decimal
    Public Property Precio As Decimal
    Public Property Descuento As Decimal

    'propiedades calculadas

    Public ReadOnly Property SubTotal As Decimal
        Get
            Return Cantidad * Precio

        End Get
    End Property

    Public ReadOnly Property Impuesto As Decimal
        Get
            Return SubTotal * 0.18

        End Get
    End Property
    Public ReadOnly Property Total As Decimal
        Get
            Return SubTotal - Descuento + Impuesto

        End Get
    End Property
End Class
