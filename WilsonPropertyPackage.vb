Public Class WilsonPropertyPackage

    Inherits DWSIM.Thermodynamics.PropertyPackages.ActivityCoefficientPropertyPackage

    Public Property WilsonM As WilsonModel
    Sub New()

        MyBase.New(False)

        WilsonM = New WilsonModel()

        m_act = WilsonM

        ComponentName = "Wilson"
        ComponentDescription = "Wilson Activity Coefficient Property Package"

        IsConfigurable = True
        _packagetype = PropertyPackages.PackageType.ActivityCoefficient

    End Sub

    Public Overrides Function ReturnInstance(typename As String) As Object

        Return New WilsonPropertyPackage()

    End Function

    Public Overrides Sub DisplayEditingForm()

        Dim f As New WilsonPPEditor With {.WilsonPP = Me}
        f.Show()

    End Sub

    Public Overrides Function GetModel() As Object

        Return WilsonM

    End Function

    Public Overrides Function CheckMissingInteractionParameters(Vx() As Double) As Boolean

        Return True

    End Function


    Public Overrides Function GetArguments() As Object

        Dim CASIDs = RET_VCAS()

        Dim MolarVolumes(Me.CurrentMaterialStream.Phases(0).Compounds.Count - 1) As Double

        Dim i As Integer = 0

        For Each subst In Me.CurrentMaterialStream.Phases(0).Compounds.Values
            MolarVolumes(i) = AUX_LIQDENSi(i, 298.15) / subst.ConstantProperties.Molar_Weight
            i += 1
        Next

        Return New Object() {CASIDs, MolarVolumes} 'kmol/m3

    End Function

End Class
