Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace WindowsCalender

    '/// <summary>
    '/// EnglishDate - data object class
    '/// </summary>
    Public Class EnglishDate

        Private _EngDate As Date

        '/// <summary>
        '/// String representation of Nepali Date. Format yyyy/m/d
        '/// </summary>
        Public Property EngDate() As Date
            Get
                Return _EngDate
            End Get
            Set(ByVal value As Date)
                _EngDate = value
            End Set
        End Property

        Public _EngDaysInMonth As Integer

        '/// <summary>
        '/// DaysInMonth of English date
        '/// </summary>
        Public Property EngDaysInMonth As Integer
            Get
                Return _EngDaysInMonth
            End Get
            Set(ByVal value As Integer)
                _EngDaysInMonth = value
            End Set

        End Property
        Private _EngYear As Integer

        '/// <summary>
        '/// Numeric Year of English date
        '/// </summary>
        Public Property EngYear As Integer
            Get
                Return _EngYear
            End Get
            Set(ByVal value As Integer)
                _EngYear = value
            End Set
        End Property

        Private _EngMonth As Integer

        '/// <summary>
        '/// Numeric Month of English date
        '/// </summary>
        Public Property EngMonth As Integer
            Get
                Return _EngMonth
            End Get
            Set(ByVal value As Integer)
                _EngMonth = value
            End Set
        End Property


        Private _EngDay As Integer

        '/// <summary>
        '/// Numeric Day of English date
        '/// </summary>
        Public Property EngDay As Integer

            Get
                Return _EngDay
            End Get

            Set(ByVal value As Integer)
                _EngDay = value
            End Set
        End Property

    End Class
End Namespace
