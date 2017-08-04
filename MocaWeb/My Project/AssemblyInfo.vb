Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes
<Assembly: AssemblyDescription("Moca.NET Web")>
<Assembly: AssemblyCompany("MiYABiS")>
<Assembly: AssemblyProduct("Moca.NET Framework")>
<Assembly: AssemblyCopyright("© MiYABiS All Rights Reserved.")>
<Assembly: AssemblyTrademark("")>

<Assembly: ComVisible(True)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("334e9cee-0d47-4d70-924b-b5098a3432cb")>

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

' プログラム要素が CLS (Common Language Specification) に準拠しているかどうかを示します
<Assembly: System.CLSCompliant(True)>


<Assembly: AssemblyVersion("5.0.0")>
<Assembly: AssemblyFileVersion("3.0.0")>
<Assembly: AssemblyInformationalVersion("3.0.0")>


#If net20 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 2.0")>
#End If
#If net35 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 3.5")>
#End If
#If net40 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 4.0")>
#End If
#If net45 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 4.5")>
#End If
#If net452 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 4.5.2")>
#End If
#If net46 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 4.6")>
#End If
#If net462 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 4.6.2")>
#End If
#If net47 Then
<Assembly: AssemblyTitle("Moca.NET Web .NET 4.7")>
#End If
