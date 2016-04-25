Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Windows.Input
Imports Microsoft.VisualStudio.TestTools.UITest.Extension
Imports Microsoft.VisualStudio.TestTools.UITesting
Imports Microsoft.VisualStudio.TestTools.UITesting.Keyboard

<CodedUITest()>
Public Class CodedUITest1

        <TestMethod()>
        Public Sub CodedUITestMethod1()
            '            
            ' 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
            ' 有关生成的代码的详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=179463
            '
        End Sub

#Region "附加测试特性"
        '
        ' 编写测试时，可以使用以下附加特性:
        '
        '' 运行每项测试之前使用 TestInitialize 运行代码
        '<TestInitialize()> Public Sub MyTestInitialize()
        '    '
        '    ' 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
        '    ' 有关生成的代码的详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=179463
        '    '
        'End Sub

        '' 运行每项测试之后使用 TestCleanup 运行代码
        '<TestCleanup()> Public Sub MyTestCleanup()
        '    '
        '    ' 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
        '    ' 有关生成的代码的详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=179463
        '    '
        'End Sub

#End Region

        '''<summary>
        '''获取或设置测试上下文，该上下文提供
        '''有关当前测试运行及其功能的信息。
        '''</summary>
        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = Value
            End Set
        End Property

        Private testContextInstance As TestContext
    End Class
