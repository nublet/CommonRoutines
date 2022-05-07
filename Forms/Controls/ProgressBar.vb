Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    Public Class ProgressBar
        Inherits UserControl

        Private _BorderColor As Color = Color.MediumSlateBlue
        Private _BorderSize As Integer = 2
        Private _ChannelColor As Color = Color.LightSteelBlue
        Private _ChannelHeight As Integer = 6
        Private _ForeBackColor As Color = Color.RoyalBlue
        Private _Maximum As Integer = 100
        Private _Minimum As Integer = 0
        Private _ShowText As Boolean = False
        Private _SliderColor As Color = Color.RoyalBlue
        Private _SliderHeight As Integer = 6
        Private _SlideText As Boolean = False
        Private _TextAlignment As ContentAlignment = ContentAlignment.MiddleCenter
        Private _Value As Integer = 0

        Public Property HasPaintedBack As Boolean = False
        Public Property ShouldStopPainting As Boolean = False

        <Category("Common Routines")> Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(value As Color)
                _BorderColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderSize As Integer
            Get
                Return _BorderSize
            End Get
            Set(value As Integer)
                _BorderSize = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property ChannelColor As Color
            Get
                Return _ChannelColor
            End Get
            Set(value As Color)
                _ChannelColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property ChannelHeight As Integer
            Get
                Return _ChannelHeight
            End Get
            Set(value As Integer)
                _ChannelHeight = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property ForeBackColor As Color
            Get
                Return _ForeBackColor
            End Get
            Set(value As Color)
                _ForeBackColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property Maximum As Integer
            Get
                Return _Maximum
            End Get
            Set(value As Integer)
                _Maximum = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property Minimum As Integer
            Get
                Return _Minimum
            End Get
            Set(value As Integer)
                _Minimum = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property ShowText As Boolean
            Get
                Return _ShowText
            End Get
            Set(value As Boolean)
                _ShowText = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property SliderColor As Color
            Get
                Return _SliderColor
            End Get
            Set(value As Color)
                _SliderColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property SliderHeight As Integer
            Get
                Return _SliderHeight
            End Get
            Set(value As Integer)
                _SliderHeight = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property SlideText As Boolean
            Get
                Return _SlideText
            End Get
            Set(value As Boolean)
                _SlideText = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property TextAlignment As ContentAlignment
            Get
                Return _TextAlignment
            End Get
            Set(value As ContentAlignment)
                _TextAlignment = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property Value As Integer
            Get
                Return _Value
            End Get
            Set(value As Integer)
                _Value = value
                Invalidate()
            End Set
        End Property

        Public Sub New()

        End Sub

        Private Sub DrawValueText(sliderWidth As Integer, rectSlider As Rectangle, ByRef g As Graphics)
            Dim TextString As String = Replace(Text, "{Maximum}", _Maximum.ToString(),,, CompareMethod.Text)
            TextString = Replace(TextString, "{Minimum}", _Minimum.ToString(),,, CompareMethod.Text)
            TextString = Replace(TextString, "{Value}", _Value.ToString(),,, CompareMethod.Text)

            Dim TextSize = TextRenderer.MeasureText(Text, Font)
            Dim RectText = New Rectangle(0, 0, TextSize.Width, TextSize.Height + 2)

            Using textFormat = New StringFormat()
                If _SlideText Then
                    RectText.X = sliderWidth - TextSize.Width
                    textFormat.Alignment = StringAlignment.Center

                    Using brush = New SolidBrush(Parent.BackColor)
                        Dim Rect = rectSlider
                        Rect.Y = RectText.Y
                        Rect.Height = RectText.Height

                        g.FillRectangle(brush, Rect)
                    End Using
                Else
                    Select Case _TextAlignment
                        Case ContentAlignment.TopLeft
                            textFormat.Alignment = StringAlignment.Near
                            RectText.X = 0

                            textFormat.LineAlignment = StringAlignment.Near
                            RectText.Y = 0
                        Case ContentAlignment.TopCenter
                            textFormat.Alignment = StringAlignment.Center
                            RectText.X = CInt((Width - TextSize.Width) / 2)

                            textFormat.LineAlignment = StringAlignment.Near
                            RectText.Y = 0
                        Case ContentAlignment.TopRight
                            textFormat.Alignment = StringAlignment.Far
                            RectText.X = Width - TextSize.Width

                            textFormat.LineAlignment = StringAlignment.Near
                            RectText.Y = 0

                        Case ContentAlignment.MiddleLeft
                            textFormat.Alignment = StringAlignment.Near
                            RectText.X = 0

                            textFormat.LineAlignment = StringAlignment.Center
                            RectText.Y = CInt((Height - TextSize.Height) / 2)
                        Case ContentAlignment.MiddleCenter
                            textFormat.Alignment = StringAlignment.Center
                            RectText.X = CInt((Width - TextSize.Width) / 2)

                            textFormat.LineAlignment = StringAlignment.Center
                            RectText.Y = CInt((Height - TextSize.Height) / 2)
                        Case ContentAlignment.MiddleRight
                            textFormat.Alignment = StringAlignment.Far
                            RectText.X = Width - TextSize.Width

                            textFormat.LineAlignment = StringAlignment.Center
                            RectText.Y = CInt((Height - TextSize.Height) / 2)

                        Case ContentAlignment.BottomLeft
                            textFormat.Alignment = StringAlignment.Near
                            RectText.X = 0

                            textFormat.LineAlignment = StringAlignment.Far
                            RectText.Y = Height - TextSize.Height
                        Case ContentAlignment.BottomCenter
                            textFormat.Alignment = StringAlignment.Center
                            RectText.X = CInt((Width - TextSize.Width) / 2)

                            textFormat.LineAlignment = StringAlignment.Far
                            RectText.Y = Height - TextSize.Height
                        Case ContentAlignment.BottomRight
                            textFormat.Alignment = StringAlignment.Far
                            RectText.X = Width - TextSize.Width

                            textFormat.LineAlignment = StringAlignment.Far
                            RectText.Y = Height - TextSize.Height
                    End Select
                End If

                Using brush = New SolidBrush(_ForeBackColor)
                    g.FillRectangle(brush, RectText)
                End Using

                Using brush = New SolidBrush(ForeColor)
                    g.DrawString(TextString, Font, brush, RectText, textFormat)
                End Using
            End Using
        End Sub

#Region " Overrides "

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                MyBase.BackColor = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            If Not ShouldStopPainting Then
                Dim ScaleFactor As Double = (CDbl(_Value) - _Minimum) / (CDbl(_Maximum) - _Minimum)
                Dim SliderWidth = CInt(Width * ScaleFactor)

                Dim RectSlider As New Rectangle(0, 0, SliderWidth, _SliderHeight)

                If _SliderHeight >= _ChannelHeight Then
                    RectSlider.Y = Height - _SliderHeight
                Else
                    RectSlider.Y = CInt(Height - (_SliderHeight + _ChannelHeight) / 2)
                End If

                If SliderWidth > 1 Then
                    Using brush = New SolidBrush(_SliderColor)
                        e.Graphics.FillRectangle(brush, RectSlider)
                    End Using
                End If

                If _ShowText Then
                    DrawValueText(SliderWidth, RectSlider, e.Graphics)
                End If

                Using pen As New Pen(_BorderColor, BorderSize)
                    pen.Alignment = PenAlignment.Inset

                    e.Graphics.DrawRectangle(pen, 0, 0, Width - 0.5!, Height - 0.5!)
                End Using
            End If

            If _Value = _Maximum Then
                ShouldStopPainting = True
            Else
                ShouldStopPainting = False
            End If
        End Sub

        Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
            If Not ShouldStopPainting Then
                If Not HasPaintedBack Then
                    Dim Rect As New Rectangle(0, 0, Width, _ChannelHeight)

                    If _ChannelHeight >= _SliderHeight Then
                        Rect.Y = Height - _ChannelHeight
                    Else
                        Rect.Y = CInt(Height - (_ChannelHeight + _SliderHeight) / 2)
                    End If

                    pevent.Graphics.Clear(Parent.BackColor)

                    Using brushChannel = New SolidBrush(_ChannelColor)
                        pevent.Graphics.FillRectangle(brushChannel, Rect)
                    End Using

                    If Not DesignMode Then
                        HasPaintedBack = True
                    End If
                End If

                If _Value = _Maximum OrElse _Value = _Minimum Then
                    HasPaintedBack = False
                End If
            End If
        End Sub

#End Region

    End Class

End Namespace