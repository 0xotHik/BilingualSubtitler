; 1. Битность
; 2. Поменять версию
; 3. Чекнуть набор файлов
#define Architecture = "x64"
#define SourceFolder = "D:\users\0xothik\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x64\BilingualSubtitler"
;
#define Architecture = "x86";
#define SourceFolder = "D:\users\0xothik\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\bin\Publish\Framework-dependent\x86\BilingualSubtitler"
;
#define MyAppVersion "11.3"
#define MajorVersion = "11"
#define MinorVersion = "3"

#define MyAppName "Bilingual Subtitler"
#define MyAppPublisher "0xotHik"
#define MyAppURL "https://0xothik.wordpress.com/bilingual-subtitler/"
#define MyAppExeName "BilingualSubtitler.exe"


[Files]
Source: "{#SourceFolder}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\BilingualSubtitler.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\BilingualSubtitler.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\BilingualSubtitler.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\BilingualSubtitler.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\BilingualSubtitler.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\BilingualSubtitler.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Gma.System.MouseKeyHook.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\libse.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.Bcl.HashCode.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.Extensions.ObjectPool.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\NeatInput.Windows.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\NonInvasiveKeyboardHookLibrary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Octokit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\RestSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\sni.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Syroot.KnownFolders.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ComponentModel.Composition.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ComponentModel.Composition.Registration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Data.Odbc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Data.OleDb.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Data.SqlClient.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "{#SourceFolder}\System.Diagnostics.EventLog.Messages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.DirectoryServices.AccountManagement.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.DirectoryServices.Protocols.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.IO.Ports.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Management.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Private.ServiceModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Reflection.Context.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Runtime.Caching.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.Duplex.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.NetTcp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceModel.Syndication.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.ServiceProcess.ServiceController.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Speech.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\System.Web.Services.Description.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\UtfUnknown.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\WindowsInput.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Xceed.Document.NET5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Xceed.Words.NET5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\YandexLinguistics.NET.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\zlib.net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.WindowsAPICodePack.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.WindowsAPICodePack.ExtendedLinguisticServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.WindowsAPICodePack.Sensors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Microsoft.WindowsAPICodePack.ShellExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceFolder}\Aspose.Zip.dll"; DestDir: "{app}"; Flags: ignoreversion
;
; Папки
Source: "{#SourceFolder}\cs\*"; DestDir: "{app}\cs\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\de\*"; DestDir: "{app}\de\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\es\*"; DestDir: "{app}\es\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\fr\*"; DestDir: "{app}\fr\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\it\*"; DestDir: "{app}\it\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\ja\*"; DestDir: "{app}\ja\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\ko\*"; DestDir: "{app}\ko\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\pl\*"; DestDir: "{app}\pl\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\pt-BR\*"; DestDir: "{app}\pt-BR\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\ru\*"; DestDir: "{app}\ru\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\tr\*"; DestDir: "{app}\tr\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\zh-Hans\*"; DestDir: "{app}\zh-Hans\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceFolder}\zh-Hant\*"; DestDir: "{app}\zh-Hant\"; Flags: ignoreversion recursesubdirs createallsubdirs
; Source: "C:\Users\0xothik\Downloads\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs


[Setup]
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
AllowNoIcons=yes
DefaultDirName={autopf}\BilingualSubtitler
DisableProgramGroupPage=yes
; The [Icons] "quicklaunchicon" entry uses {userappdata} but its [Tasks] entry has a proper IsAdminInstallMode Check.
UsedUserAreasWarning=no
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=bilingual-subtitler-installer-{#MajorVersion}-{#MinorVersion}-{#Architecture}
SetupIconFile=D:\source\repos\0xotHik\BilingualSubtitler\BilingualSubtitler\Resources\logo4_1.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
DefaultGroupName={#MyAppName}

#if Architecture == "x64"
ArchitecturesInstallIn64BitMode = x64
#endif

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode


[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Сайт программы с справкой по использованию"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
; Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent runascurrentuser
Filename: "https://0xothik.wordpress.com/bilingual-subtitler/"; Flags: shellexec runasoriginaluser postinstall; Description: "Открыть сайт программы с руководством по использованию"



