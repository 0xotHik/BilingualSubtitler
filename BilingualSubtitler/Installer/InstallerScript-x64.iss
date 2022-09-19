; 1. Поменять версию
; 2. Поменять по новой версии имя сетапника
; 3. Чекнуть набор файлов
#define MyAppVersion "10.0"

#define MyAppName "Bilingual Subtitler"
#define MyAppPublisher "0xotHik"
#define MyAppURL "https://0xothik.wordpress.com/bilingual-subtitler/"
#define MyAppExeName "BilingualSubtitler.exe"

[Setup]
; !!!
OutputBaseFilename=bilingual-subtitler-installer-10-0-x64
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{7CC8CBB0-5496-40FD-96DC-C283B2EFA75B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\BilingualSubtitler
DisableProgramGroupPage=yes
; The [Icons] "quicklaunchicon" entry uses {userappdata} but its [Tasks] entry has a proper IsAdminInstallMode Check.
UsedUserAreasWarning=no
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
SetupIconFile=D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\Resources\logo4_1.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

ArchitecturesInstallIn64BitMode = x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode


[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
Filename: "https://0xothik.wordpress.com/bilingual-subtitler/"; Flags: shellexec runasoriginaluser postinstall; Description: "Открыть сайт программы с руководством по использованию"

[Files]
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\BilingualSubtitler.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\BilingualSubtitler.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\BilingualSubtitler.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\BilingualSubtitler.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\BilingualSubtitler.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\BilingualSubtitler.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Gma.System.MouseKeyHook.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\libse.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.Bcl.HashCode.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.Extensions.ObjectPool.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\NeatInput.Windows.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\NonInvasiveKeyboardHookLibrary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Octokit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\RestSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\sni.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Syroot.KnownFolders.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ComponentModel.Composition.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ComponentModel.Composition.Registration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Data.Odbc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Data.OleDb.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Data.SqlClient.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Diagnostics.EventLog.Messages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.DirectoryServices.AccountManagement.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.DirectoryServices.Protocols.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.IO.Ports.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Management.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Private.ServiceModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Reflection.Context.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Runtime.Caching.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.Duplex.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.NetTcp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceModel.Syndication.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.ServiceProcess.ServiceController.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Speech.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\System.Web.Services.Description.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\UtfUnknown.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\WindowsInput.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Xceed.Document.NET.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Xceed.Words.NET.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\YandexLinguistics.NET.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\zlib.net.dll"; DestDir: "{app}"; Flags: ignoreversion
; 10.0
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.WindowsAPICodePack.ExtendedLinguisticServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.WindowsAPICodePack.Sensors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\Microsoft.WindowsAPICodePack.ShellExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
; Папки
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\cs\*"; DestDir: "{app}\cs\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\de\*"; DestDir: "{app}\de\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\es\*"; DestDir: "{app}\es\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\fr\*"; DestDir: "{app}\fr\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\it\*"; DestDir: "{app}\it\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\ja\*"; DestDir: "{app}\ja\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\ko\*"; DestDir: "{app}\ko\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\pl\*"; DestDir: "{app}\pl\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\pt-BR\*"; DestDir: "{app}\pt-BR\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\ru\*"; DestDir: "{app}\ru\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\tr\*"; DestDir: "{app}\tr\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\zh-Hans\*"; DestDir: "{app}\zh-Hans\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler\zh-Hant\*"; DestDir: "{app}\zh-Hant\"; Flags: ignoreversion recursesubdirs createallsubdirs
; Source: "C:\Users\0xothik\Downloads\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs


