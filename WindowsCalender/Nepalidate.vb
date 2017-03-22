Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace WindowsCalender

    '/// <summary>
    '/// NepaliDate - data object class
    '/// </summary>
    Public Class NepaliDate

        Private _nepaliDate As String

        '/// <summary>
        '/// String representation of Nepali Date. Format yyyy/m/d
        '/// </summary>
        Public Property npDate() As String
            Get
                Return _nepaliDate
            End Get
            Set(ByVal value As String)
                _nepaliDate = value
            End Set
        End Property

        Public _npDaysInMonth As Integer

        '/// <summary>
        '/// DaysInMonth of Nepali date
        '/// </summary>
        Public Property npDaysInMonth As Integer
            Get
                Return _npDaysInMonth
            End Get
            Set(ByVal value As Integer)
                _npDaysInMonth = value
            End Set

        End Property
        Private _npYear As Integer

        '/// <summary>
        '/// Numeric Year of Nepali date
        '/// </summary>
        Public Property npYear As Integer
            Get
                Return _npYear
            End Get
            Set(ByVal value As Integer)
                _npYear = value
            End Set
        End Property

        Private _npMonth As Integer

        '/// <summary>
        '/// Numeric Month of Nepali date
        '/// </summary>
        Public Property npMonth As Integer
            Get
                Return _npMonth
            End Get
            Set(ByVal value As Integer)
                _npMonth = value
            End Set
        End Property


        Private _npDay As Integer

        '/// <summary>
        '/// Numeric Day of Nepali date
        '/// </summary>
        Public Property npDay As Integer

            Get
                Return _npDay
            End Get

            Set(ByVal value As Integer)
                _npDay = value
            End Set
        End Property

        Private _DayOfWeek As Integer
        ''' <summary>
        ''' Day of the Week
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DayOfWeek() As Integer
            Get
                Return _DayOfWeek
            End Get
            Set(ByVal value As Integer)
                _DayOfWeek = value
            End Set
        End Property

    End Class
End Namespace
