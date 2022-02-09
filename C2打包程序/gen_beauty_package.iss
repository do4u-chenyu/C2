; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!
;添加可以更改工作空间的目录
#define MyAppName "分析师单兵作战装备"
#define MyAppVersion "1.4.14"
#define MyAppPublisher "FiberHome"
#define MyAppURL ""
#define MyAppPkgDir "C:\Program Files\FiberHome\IAO解决方案"

#define MyAppExeName "C2.exe"
#define AppIconName "\Resources\C2\Icon\Icon.ico"

; 生成 目录setup.exe 所在文件夹
#define MySetupOutDir ".\output"
;生成安装包的名称
#define MySetupOutBaseFilename "分析师单兵作战(" +  GetDateTimeString('yyyy-mm-dd','-',':') + ")"

; 制作setup.exe所用的资源文件夹
#define MyResDir ".\res"
; 在注册表上记录上一次安装的位置
#define MyRegInstallPath_sk "Software\我的应用\linxinfa\install"
#define MyRegInstallPath_vn "installPath"
; ; 点击license 打开的网页连接
; #define MyAppLkLicenseURL 'http://fh2020.gitee.io/c1platform/'
; 安装目录至少需要的空间 100; 100 MB，TODO 要获取一些Minisize 之类的来计算，这里先写死
#define MyAppNeedSpaceByte 100
; 外部程序调用本setup.exe时，会向外部传 安装进度的window api Message ID 
#define WM_MY_INSTALL_PROGRESS 6364
; 安装时显示进度条，背景切换图的图片数
#define InsBgAniPicCount 9

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId=123456789
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}

VersionInfoDescription={#MyAppName}安装程序
VersionInfoCompany={#MyAppPublisher}
VersionInfoVersion={#MyAppVersion}
VersionInfoProductVersion={#MyAppVersion}

DefaultDirName={code:GetInstallPath}
DefaultGroupName={#MyAppName}
LicenseFile={#MyResDir}\License.txt
OutputDir={#MySetupOutDir}
OutputBaseFilename={#MySetupOutBaseFilename}
SetupIconFile={#MyResDir}\SetupIcon.ico
;这个不能用的话就用下面的
Compression=lzma2/ultra64
;Compression=lzma2 
SolidCompression=yes

UsePreviousAppDir=no
;管理员权限
PrivilegesRequired=admin

;先让一些默认的界面不要显示
DisableReadyPage=yes
;DisableProgramGroupPage=yes
;DirExistsWarning=no
DisableWelcomePage=no
DisableDirPage=no

[Languages]
Name: "chs"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone; 
;Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "quicklaunchicon"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#MyResDir}\tmp\*"; DestDir: "{tmp}"; Flags: dontcopy solidbreak ; Attribs: hidden system
Source: "{#MyAppPkgDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

;Source: "..\..\test_prj\MyProg.exe"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
;Source: compiler:InnoCallback.dll; DestDir: {tmp}; Flags: dontcopy

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; AfterInstall: ShoutcutRunAsAdmin('{group}\{#MyAppName}.lnk');IconFilename:"{app}\Resources\C2\Icon\Icon.ico"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon ; AfterInstall: ShoutcutRunAsAdmin('{commondesktop}\{#MyAppName}.lnk');IconFilename:"{app}\Resources\C2\Icon\Icon.ico"
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename:"{app}\{#MyAppExeName}"; Tasks: quicklaunchicon  ; AfterInstall: ShoutcutRunAsAdmin('{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}.lnk')
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\{#MyAppName}"; Filename:"{app}\{#MyAppExeName}"; Tasks: quicklaunchicon ; AfterInstall: ShoutcutRunAsAdmin('{userappdata}\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\{#MyAppName}.lnk')
;Name: "{userdesktop}\IAO解决方案";IconFilename:"{app}\Resources\C2\Icon\Icon.ico"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: runascurrentuser nowait postinstall skipifsilent

;[UninstallRun] 
;Filename: http://happyfish.lkgame.com/uninstallsurvey; Flags: shellexec runmaximized; Tasks: ; Languages:

[Registry]
Root:HKCU;Subkey: "{#MyRegInstallPath_sk}" ; ValueType:string; ValueName:"{#MyRegInstallPath_vn}"; ValueData:"{app}";Flags:uninsdeletekeyifempty
Root: HKCR; Subkey: ".c2"; Flags: uninsdeletekey
Root: HKCR; Subkey: ".c2"; ValueType: string; ValueName: ""; ValueData: "C2File"
Root: HKCR; Subkey: "C2File"; Flags: uninsdeletekey
Root: HKCR; Subkey: "C2File\DefaultIcon"; Flags: uninsdeletekey
Root: HKCR; Subkey: "C2File\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#AppIconName}"; Flags: 
Root: HKCR; Subkey: "C2File\shell"; Flags: uninsdeletekey
Root: HKCR; Subkey: "C2File\shell\open"; Flags: uninsdeletekey
Root: HKCR; Subkey: "C2File\shell\open\command"; Flags: uninsdeletekey
Root: HKCR; Subkey: "C2File\shell\open\command"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName} ""%1"""; Flags:

[Code]
#include "DllsImport.iss"  
#include "HoverEvent.iss"
#include "SetupMisc.iss"

var 
  g_notifyWnd : HWND;               // 发送通知的回调窗口
  g_notifyFinished : Boolean;       // 是否已经发送结束消息？

var 
  dNeedSpaceByte:Longint; //需要的安装的空间大小，（字节)
  PBOldProc:Longint;
  imgBg1, imgBg2 :Longint;
  btnClose, btnMin:HWND;

  DpiScalePctg:integer;
  CurrentDPI:integer;
  
  // page welcome 
  btnOneKey,btnCustomInstall:HWND;     //一键安装按钮，自定义按钮
  chkLicense:HWND;          //选中欢迎访问链接
  //lblLicense,lblWelcome,lblAgree:TLabel;
  lblWelcome:TLabel;
  imgBigIcon1, imgLogo1, imgOneKeySh:Longint;

  // page select dir 
  btnSelectDir1,btnNext1,btnBack1:HWND;//浏览按钮、上一步按钮、下一步按钮
  edtSelectDir1:TEdit;
  chkQuickLaunch:HWND;  // 快速启动栏 (check box )
  lblQuickLaunch:TLabel;// 快速启动栏 (上面的label)
  lblNeedSpace, lblDiskSpace,lblTipWDir,lblTipWDir2,lblTipWDir3 :TLabel;
  //是否选中快速启动栏
  isSelectedQuickLaunch:boolean;
  
  //新添加：   page select work dir
  btnSelectDir2,btnNext2,btnBack2:HWND;    //浏览按钮、上一步、下一步
  edtSelectDir2:TEdit;                     //目录栏中的文件目录地址
  WorkSpacePath:string;
  
  // page install 
  imgProgressBar, imgProgressBarBg:Longint; //进度条
  lblTipProgress :TLabel;
 
  // page finish;
  btnFinish:HWND;       //结束按钮

var 
  //PageStateLbl1,PageStateLbl2,PageStateLbl3, PageStateLblK1,PageStateLblK2: TLabel; 
  PageStateLblK1,PageStateLblK2: TLabel; 

//以管理员身份运行程序
procedure ShoutcutRunAsAdmin(Filename:string);
var 
  Buffer:String;
  Stream:TStream;
begin
  Filename := ExpandConstant(Filename);
  //Log(Format('ShoutcutRunAsAdmin File=%s )',[Filename]))
  Stream:=TFileStream.Create(FileName,fmOpenReadWrite);
  try
    Stream.Seek(21,soFromBeginning);
    SetLength(Buffer, 1)
    Stream.ReadBuffer(Buffer,1);
    Buffer[1] := Chr(Ord(Buffer[1]) or $20);
    Stream.Seek(-1, soFromCurrent);
    Stream.WriteBuffer(Buffer, 1);
  finally
    Stream.Free;
  end;
end;

//范围等级
function DpiScale(v:integer):integer;
begin
  Result:=v*DpiScalePctg/1000;
end;

//获取命令行参数
function GetCmdlineParam(PName:String):String;
var
  CmdLine : String;
  CmdLineLen : Integer;
  i : Integer;
begin
  CmdLineLen:=ParamCount();
  for i:=0 to CmdLineLen do
  begin
  CmdLine:=ParamStr(i);
  if CmdLine= PName then
    begin
      CmdLine:=ParamStr(i+1);
      Result := CmdLine;
      Exit;
    end;
  end;
  Result := '0';
end;

// 获取安装路径，默认在可用空间最大的盘
function GetInstallPath(Param: String):String;
var destDisk ,i: byte;
    maxSpace : Cardinal;
    tmpFree,tmpTotal : Cardinal;
    strPath:String;
begin
  destDisk := 97;
  maxSpace := 0;
  for i := 0 to 25 do
    begin
      if  GetSpaceOnDisk(Chr(97+i)+':\', True, tmpFree, tmpTotal) then
      begin
        if maxSpace < tmpFree then
        begin
          maxSpace := tmpFree;
          destDisk := 97+i;
        end
      end
    end;
    if GetCmdlineParam('-installPath') = '0' then
      begin 
        if RegQueryStringValue(HKEY_CURRENT_USER, '{#MyRegInstallPath_sk}', '{#MyRegInstallPath_vn}', strPath) then
        begin
          Result := 'C:\Program Files\FiberHome\分析师解决方案';
        end
        else 
        begin
           Result :='C:\Program Files\FiberHome\分析师解决方案';
        end
      end
    else 
      begin
      Result := GetCmdlineParam('-installPath');
      end;
end;

//窗口初始化
procedure Notify_Init();
begin
     g_notifyWnd := FindWindowByWindowName(GetCmdlineParam('-notify_wnd'));   //按照窗口名查找窗口
     g_notifyFinished := false;
end;

//通知安装进度情况
procedure Notify_DoNotifyProgress( pos, total: Longint);
begin 
   if g_notifyWnd <> 0 then
   begin 
      PostMessage(g_notifyWnd, {#WM_MY_INSTALL_PROGRESS}, pos, total );
   end;
end;

//通知安装结束
procedure Notify_DoNotifyFinish();
begin
   g_notifyFinished := true;
   if g_notifyWnd <> 0 then
   begin 
      PostMessage(g_notifyWnd, {#WM_MY_INSTALL_PROGRESS}, 10000, 9999);
   end;
end;

//页面状态标签
function PageState_CreateLabel(text:string; x, y:integer):TLabel;
var lbl:TLabel;
begin
  lbl := TLabel.Create(WizardForm);
  with lbl do
  begin
    Parent := WizardForm;
    Caption := text;
    Transparent := true;
    Font.Size:= 10;
    Font.Name:='微软雅黑';
    Font.Color:=$ffffff; //$555555
    Left := x;
    Top := y;
  end;
  result := lbl;
end;

//设置空间是否可见
procedure TconSetVisible(lbl:TControl; bVis:boolean);
begin
  if bVis then
    begin 
     lbl.Show;
   end 
  else 
    begin 
     lbl.Hide;
	end;
end;

procedure ReWriteAppConfig();
begin
  FileCopy('C:\Program Files\FiberHome\IAO解决方案\C2.exe.config', WizardForm.DirEdit.Text + '\C2.exe.config', False);
end;

//点击关闭按钮事件
procedure BtnClose_OnClick(hBtn:HWND);
begin
  if ExitSetupMsgBox then
  begin
    WizardForm.Release;
    WizardForm.Close;
    // stop and rollback actions you did from your after install   在安装后停止并回滚从中执行的操作
    // process and kill the setup process itself   进行与终止安装进程本身
    ExitProcess(0);
  end;  
end;

//点击自定义安装事件
procedure btnCustomInstallOnClick(hBtn:HWND);
begin
  WizardForm.NextButton.OnClick(WizardForm);
end;

//单击最小化按钮事件
procedure btnMin_OnClick(hBtn:HWND);
begin
  SendMessage(WizardForm.Handle,WM_SYSCOMMAND,61472,0);
end;

//点击一键安装按钮
procedure btnOneKey_OnClick(hBtn:HWND);
begin
  WizardForm.NextButton.OnClick(WizardForm);
  WizardForm.NextButton.OnClick(WizardForm);
  WizardForm.NextButton.OnClick(WizardForm);
end;

//欢迎访问门户是否选中
procedure InitGui_PageWelcome();
var
  BtnOneKeyFont:TFont;
  tmpFont:TFont;
begin
  tmpFont := TFont.Create;
  with tmpFont do begin 
    Size := 12;
    Name :='黑体';
    Color:=$986800;
  end;

//自定义选中按钮
  btnCustomInstall:=BtnCreate(WizardForm.Handle,DpiScale(540),DpiScale(417),DpiScale(89), DpiScale(30), ExpandConstant('{tmp}\btn.png'), 1, False)
  BtnSetEvent(btnCustomInstall,BtnClickEventID,WrapBtnCallback(@btnCustomInstallOnClick,1));
  BtnSetText(btnCustomInstall, '自定义');
  BtnSetFont(btnCustomInstall, tmpFont.Handle);
  BtnSetFontColor(btnCustomInstall,$986800,$986800,$986800,$AFAFAF);

//设置一键安装按钮的背景色
  btnOneKey:=BtnCreate(WizardForm.Handle,DpiScale(240),DpiScale(330),DpiScale(177), DpiScale(43), ExpandConstant('{tmp}\btnOneKeyInstall.png'),1,False)
  
  BtnSetText(btnOneKey, '一键安装');
  BtnOneKeyFont := TFont.Create;
  with BtnOneKeyFont do begin 
	Size := 20;
	Name:='黑体';
	Color:=$ffffff;
  end;
  BtnSetFont(btnOneKey, BtnOneKeyFont.Handle);
  BtnSetFontColor(btnOneKey,$FAFAFA,$FFFFFF,$FFFFFF,$FFFFFF);
  BtnSetEvent(btnOneKey,BtnClickEventID,WrapBtnCallback(@btnOneKey_OnClick,1)); 
  lblWelcome := TLabel.Create(WizardForm);
  with lblWelcome do
  begin
    Parent := WizardForm;
    Caption := '';
    Transparent := true;
    Font.Size:= 20
    Font.Name:='黑体'
    Font.Color:=$ffffff
    Left := DpiScale(200);
    Top := DpiScale(225);
  end;
end;

//获取所需磁盘空间文本
function GetNeedSpaceText():string;
var 
  iv:string;
begin 
  iv:= Misc_FomatByteText(dNeedSpaceByte*1024*1024);
  result:=format('所需要磁盘空间：%s',[iv]);
end;

//获取可用磁盘空间
function GetCurDirFreeSpace():Cardinal;
var
 curPath:string;
 FreeSpace,TotalSpace:Cardinal;
begin
  curPath := edtSelectDir1.Text;
  GetSpaceOnDisk(ExtractFileDrive(curPath),True,FreeSpace,TotalSpace);
  result := FreeSpace;
end;

//获取可用磁盘空间文本
function GetDiskSpaceText(FreeSpace:Cardinal):string;
var 
  iv:string;
begin 
  
  if FreeSpace > 1024 then 
  begin iv := Format('%.0f GB',[FreeSpace/1024.0 ]) ; 
  end
  else 
  begin iv := Format('%d MB',[FreeSpace]);
  end;
  result:=format('可用磁盘空间：%s        （建议安装在C盘）',[iv]);
end;

//安装时请检查磁盘空间
procedure WhenInstallDirChangeCheckDiskSpace();
var 
  FreeSpace:Cardinal; 
begin
  FreeSpace := GetCurDirFreeSpace();
  lblNeedSpace.Caption := GetNeedSpaceText();
  lblDiskSpace.Caption := GetDiskSpaceText(FreeSpace);
  TconSetVisible(lblTipWDir, (WizardForm.CurPageID = wpSelectDir) and (FreeSpace <= 0) );
end;

//安装目录更改
procedure EdtSelectDir1_EditChanged(Sender: TObject);
begin
  WizardForm.DirEdit.Text:=edtSelectDir1.Text;
  WhenInstallDirChangeCheckDiskSpace();
end;

//工作路径目录更改
procedure EdtSelectDir2_EditChanged(Sender: TObject);
begin
  WorkSpacePath:=edtSelectDir2.Text;
end;

//点击浏览按钮
procedure BtnSelectDir1_OnClick(hBtn:HWND);
begin
  WizardForm.DirBrowseButton.OnClick(WizardForm);
  edtSelectDir1.Text:=WizardForm.DirEdit.Text;
  WhenInstallDirChangeCheckDiskSpace();
end;

//点击下一步按钮
procedure BtnNext1_OnClick(hBtn:HWND);
var 
  FreeSpace:Cardinal; 
begin
  //检查一下有否有空间
  FreeSpace := GetCurDirFreeSpace();
  if FreeSpace <= 0 then 
  begin 
    msgbox('该目录为无效路径，请重新选择其他路径', mbInformation,MB_OK);
    exit;
  end;
  Log(Format('FreeSpace %d, Need:%d', [FreeSpace,  dNeedSpaceByte] ));
  if FreeSpace  < dNeedSpaceByte then 
  begin
    msgbox('该目录所在磁盘空间不足，请重新选择其他路径', mbInformation,MB_OK);
    exit;
  end;
  WizardForm.NextButton.OnClick(WizardForm);
end;

procedure BtnNext2_OnClick(hBtn:HWND);
var 
  FreeSpace:Cardinal; 
begin
  //检查一下有否有空间
  FreeSpace := GetCurDirFreeSpace();
  if FreeSpace <= 0 then 
  begin 
    msgbox('该目录为无效路径，请重新选择其他路径', mbInformation,MB_OK);
    exit;
  end;
  Log(Format('FreeSpace %d, Need:%d', [FreeSpace,  dNeedSpaceByte] ));
  if FreeSpace  < dNeedSpaceByte then 
  begin
    msgbox('该目录所在磁盘空间不足，请重新选择其他路径', mbInformation,MB_OK);
    exit;
  end;
  WizardForm.NextButton.OnClick(WizardForm);
end;
  


//点击上一步按钮
procedure BtnBack1_OnClick(hBtn:HWND);
begin
  WizardForm.BackButton.OnClick(WizardForm);
end;

//点击快速启动栏
procedure chkQuickLaunch_OnClick(bBtn :HWND);
begin
   isSelectedQuickLaunch := BtnGetChecked(chkQuickLaunch);
end;


//安装目录选择页面初始化
procedure InitGui_PageSelectDir();
var
  tmpFont:TFont;
begin
  lblTipWDir3 := TLabel.Create(WizardForm);
  with lblTipWDir3 do
  begin
    Parent := WizardForm;
    Caption := '安装程序将把  分析师单兵作战装备  安装到下面的文件夹中：';
    Transparent := true;
    Font.Size:= 10
    Font.Name:='微软雅黑'
    Font.Color:=$ffffff
    Left := DpiScale(130);
    Top := DpiScale(190);
  end;

  edtSelectDir1 := TEdit.Create(WizardForm);
  with edtSelectDir1 do
  begin
    Parent:= WizardForm;
    Text := WizardForm.DirEdit.Text;
    Font.Size:= 11
    Font.Color:=$555555
    Left:= DpiScale(132);
    Top := DpiScale(231);
    Width:= DpiScale(311);
    Height:= DpiScale(18);
    BorderStyle:=bsNone;
    TabStop := false;
    OnChange:=@EdtSelectDir1_EditChanged;
  end;
 
  btnSelectDir1:=BtnCreate(WizardForm.Handle,DpiScale(452),DpiScale(225),DpiScale(89), DpiScale(30), ExpandConstant('{tmp}\btn.png'), 1, False)
  BtnSetEvent(btnSelectDir1,BtnClickEventID,WrapBtnCallback(@BtnSelectDir1_OnClick,1));
  BtnSetText(btnSelectDir1, '浏览');
  tmpFont := TFont.Create;
  with tmpFont do begin 
    Size := 12;
    Name :='黑体';
    Color:=$986800;
  end;
  BtnSetFont(btnSelectDir1, tmpFont.Handle);
  BtnSetFontColor(btnSelectDir1,$986800,$986800,$986800,$AFAFAF);
  
  btnNext1:=BtnCreate(WizardForm.Handle,DpiScale(550),DpiScale(417),DpiScale(89), DpiScale(30), ExpandConstant('{tmp}\btn.png'), 1, False)
  BtnSetEvent(btnNext1,BtnClickEventID,WrapBtnCallback(@BtnNext1_OnClick,1));
  BtnSetText(btnNext1, '下一步');
  BtnSetFont(btnNext1, tmpFont.Handle);
  BtnSetFontColor(btnNext1,$986800,$986800,$986800,$AFAFAF);
  
  btnBack1:=BtnCreate(WizardForm.Handle,DpiScale(460),DpiScale(417),DpiScale(89), DpiScale(30), ExpandConstant('{tmp}\btn.png'), 1, False)
  BtnSetEvent(btnBack1,BtnClickEventID,WrapBtnCallback(@BtnBack1_OnClick,1));
  BtnSetText(btnBack1, '上一步');
  BtnSetFont(btnBack1, tmpFont.Handle);
  BtnSetFontColor(btnBack1,$986800,$986800,$986800,$AFAFAF);
  //默认选中快速启动栏
  isSelectedQuickLaunch := True
  chkQuickLaunch :=BtnCreate(WizardForm.Handle,DpiScale(133),DpiScale(300),DpiScale(15),DpiScale(15), ExpandConstant('{tmp}\check.png'),1, True);
  BtnSetChecked(chkQuickLaunch, isSelectedQuickLaunch); 
  BtnSetEvent(chkQuickLaunch,BtnClickEventID,WrapBtnCallback(@chkQuickLaunch_OnClick,1));

  lblQuickLaunch := TLabel.Create(WizardForm);
  with lblQuickLaunch do
  begin
    Parent := WizardForm;
    Caption := '快速启动栏';
    Transparent := true;
    Font.Size:= 10
    Font.Name:='微软雅黑'
    Font.Color:=$ffffff
    Left := DpiScale(150);
    Top := DpiScale(298);
  end;
  //所需空间
  lblNeedSpace := TLabel.Create(WizardForm);
  with lblNeedSpace do
  begin
    Parent := WizardForm;
    Caption := '';
    Transparent := true;
    Font.Size:= 10
    Font.Name:='微软雅黑'
    Font.Color:=$ffffff
    Left := DpiScale(130);
    Top := DpiScale(267);
  end;
  //磁盘空间
  lblDiskSpace := TLabel.Create(WizardForm);
  with lblDiskSpace do
  begin
    Parent := WizardForm;
    Caption := '';
    Transparent := true;
    Font.Size:= 10
    Font.Name:='微软雅黑'
    Font.Color:=$ffffff
    Left := DpiScale(320);
    Top := DpiScale(267);
  end;
  //提示信息
  lblTipWDir := TLabel.Create(WizardForm);
  with lblTipWDir do
  begin
    Parent := WizardForm;
    Caption := '该目录为无效路径，请重新选择其他路径';
    Transparent := true;
    Font.Size:= 10
    Font.Name:='微软雅黑'
    Font.Color:=$000099
    Left := DpiScale(129);
    Top := DpiScale(167);
  end;
  WhenInstallDirChangeCheckDiskSpace();
end;

procedure InitGui_PageSelectWorkSpace();
var
  tmpFont:TFont;
begin
  WorkSpacePath := 'C:\FiberHomeIAOModelDocument';
  
  lblTipWDir2 := TLabel.Create(WizardForm);
  with lblTipWDir2 do
  begin
    Parent := WizardForm;
    Caption := '正在设置您的工作空间：';
    Transparent := true;
    Font.Size:= 10
    Font.Name:='微软雅黑'
    Font.Color:=$ffffff
    Left := DpiScale(130);
    Top := DpiScale(190);
  end;

  edtSelectDir2 := TEdit.Create(WizardForm);
  with edtSelectDir2 do
    begin
    Parent:= WizardForm;
    Text := WorkSpacePath;
    Font.Size:= 11
    Font.Color:=$555555
    Left:= DpiScale(132);
    Top := DpiScale(231);
    Width:= DpiScale(311);
    Height:= DpiScale(18);
    BorderStyle:=bsNone;
    TabStop := false;
    OnChange:=@EdtSelectDir2_EditChanged;
  end;

  tmpFont := TFont.Create;
  with tmpFont do begin 
    Size := 12;
    Name :='黑体';
    Color:=$986800;
  end;

  btnNext2:=BtnCreate(WizardForm.Handle,DpiScale(550),DpiScale(417),DpiScale(89), DpiScale(30), ExpandConstant('{tmp}\btn.png'), 1, False)
  BtnSetEvent(btnNext2,BtnClickEventID,WrapBtnCallback(@BtnNext2_OnClick,1));
  BtnSetText(btnNext2, '下一步');
  BtnSetFont(btnNext2, tmpFont.Handle);
  BtnSetFontColor(btnNext2,$986800,$986800,$986800,$AFAFAF);
  
  btnBack2:=BtnCreate(WizardForm.Handle,DpiScale(460),DpiScale(417),DpiScale(89), DpiScale(30), ExpandConstant('{tmp}\btn.png'), 1, False)
  BtnSetEvent(btnBack2,BtnClickEventID,WrapBtnCallback(@BtnBack1_OnClick,1));
  BtnSetText(btnBack2, '上一步');
  BtnSetFont(btnBack2, tmpFont.Handle);
  BtnSetFontColor(btnBack2,$986800,$986800,$986800,$AFAFAF);
end;

Const 
  //保持时间
  InsBgAni_HoldTime = 2000;
  //切换时间
  InsBgAni_SwitchTime = 400;
  //图片数目
  InsBgAni_ImgCount = {#InsBgAniPicCount};
var 
  InsBgAni_ImgArr : array[0 .. InsBgAni_ImgCount ] of Longint;
  InsBgAni_Time : Longint;

//页面安装计时器程序
procedure PageInstall_TimerProc(H: LongWord; Msg: LongWord; IdEvent: LongWord; Time: LongWord);
var
  allAniTime, i, idx, nIdx, t0, t1, alpha:Longint;
  tImg :Longint;
begin
  if InsBgAni_Time >= 0 then
  begin
    allAniTime := (InsBgAni_HoldTime + InsBgAni_SwitchTime) * InsBgAni_ImgCount;
    //取余数
    InsBgAni_Time := (InsBgAni_Time + 50) mod allAniTime; 
	//取整数商
	idx := InsBgAni_Time div (InsBgAni_HoldTime + InsBgAni_SwitchTime);
     t0 := InsBgAni_Time mod (InsBgAni_HoldTime + InsBgAni_SwitchTime);
	if t0 < InsBgAni_HoldTime then 
	begin 
		for i:= 0 to InsBgAni_ImgCount do 
		begin 
		   tImg :=InsBgAni_ImgArr[i] 
		   if i = idx then 
		   begin 
		       ImgSetTransparent(tImg, 255);
		   end
		   else 
		       ImgSetTransparent(tImg, 0);
		end;
	end 
	else 
	begin
	  alpha := (t0 - InsBgAni_HoldTime) * 255 / InsBgAni_SwitchTime;
       nIdx := (Idx + 1) mod  InsBgAni_ImgCount;
	  for i:= 0 to InsBgAni_ImgCount do 
		begin 
		   tImg := InsBgAni_ImgArr[i] 
		   if i = idx then 
		   begin 
		      ImgSetTransparent(tImg, 255 - alpha);
		   end
		   else if i = nIdx then 
		   begin
		      ImgSetTransparent(tImg, alpha);
		   end
		   else
		   begin
		      ImgSetTransparent(tImg, 0);
		   end;
		end;
	end;
    ImgApplyChanges(WizardForm.Handle);
  end;
end;

//页面安装进度条情况
procedure PageInstall_SetProgress(pr:Extended);
var 
  w:longint;
begin
  w:=Round(577*pr/100);
  ImgSetPosition(imgProgressBar, DpiScale(38), DpiScale(429), DpiScale(w), DpiScale(29));
  ImgSetVisiblePart(imgProgressBar,0,0, w, 29);
  ImgApplyChanges(WizardForm.Handle);
  lblTipProgress.Caption := Format('正在为您安装 %d%%',[Round(pr)]);
end;

//正在安装中，进度条缓慢变化
procedure InitGui_PageInstall();
var
  tmpFont:TFont;
begin
  imgProgressBarBg:=ImgLoad(WizardForm.Handle,ExpandConstant('{tmp}\progressBg.png'), 0,0,0,0,True,True);
  ImgSetPosition(imgProgressBarBg, DpiScale(38), DpiScale(429), DpiScale(577),DpiScale(19));
  imgProgressBar:=ImgLoad(WizardForm.Handle,ExpandConstant('{tmp}\progress.png'),0,0,0,0,True,True);
  
  lblTipProgress := TLabel.Create(WizardForm);
  with lblTipProgress do
  begin
    Parent := WizardForm;
    Caption := '正在为您安装';
    Transparent := true;
    Font.Size:= 10;
    Font.Name:='微软雅黑';
    Font.Color:=$ffffff;
    Left := DpiScale(275);
    Top := DpiScale(400);
  end;

  InsBgAni_Time := -1;//Stop ani;  
  SetTimer(0, 0, 50, WrapTimerProc(@PageInstall_TimerProc, 4));
end;

//点击“完成安装”按钮
procedure btnFinish_OnClick(hBtn:HWND);
begin
  WizardForm.NextButton.OnClick(WizardForm);
end;

//初始化安装结束界面
procedure InitGui_PageFinish();
var
   tmpFont:TFont;
begin 
  btnFinish:=BtnCreate(WizardForm.Handle, DpiScale(240), DpiScale(330), DpiScale(177),DpiScale(43), ExpandConstant('{tmp}\btnOneKeyInstall.png'),1,False)
  BtnSetText(btnFinish, '完成安装');
  tmpFont := TFont.Create;
  with tmpFont do begin 
	Size := 20;
	Name:='黑体';
	Color:=$000000;
  end;
  BtnSetFont(btnFinish, tmpFont.Handle);
  BtnSetFontColor(btnFinish,$FAFAFA,$FFFFFF,$FFFFFF,$FFFFFF);
  BtnSetEvent(btnFinish,BtnClickEventID,WrapBtnCallback(@btnFinish_OnClick,1));
end;

//按描述设置任务值
procedure SetTaskValueByDesc(desc:string; value:boolean);
var tmpInx:integer;
begin 
    tmpInx := WizardForm.TasksList.Items.IndexOf(desc);
    if tmpInx <> -1 then 
    begin
       Log(Format('CurPageID = wpInstalling | set quicklaunchicon = %d ', [ value ] ));
       WizardForm.TasksList.Checked[tmpInx] := value; 
    end;
end;

//所有页面统筹发生变化的变化情况
procedure CurPageChanged(CurPageID: Integer);
var 
  isWpWelcome,isWpSelectDir,isWpSelectWorkSpace,isWpInstalling,isWpFinished : boolean;
  nErrCode:integer; 
  i:integer;
begin
  Log(format( 'CurPageID id = %d',[ CurPageID ]));
  isWpWelcome          := CurPageID = wpWelcome;
  isWpSelectDir        := CurPageID = wpSelectDir;
  isWpSelectWorkSpace  := CurPageID = wpSelectTasks;
  isWpInstalling       := CurPageID = wpInstalling;
  isWpFinished         := CurPageID = wpFinished;
  
  BtnSetEnabled(btnClose, isWpWelcome or isWpSelectDir or isWpSelectWorkSpace);

  BtnSetVisibility(btnOneKey,       isWpWelcome);
  BtnSetVisibility(btnCustomInstall, isWpWelcome);
  BtnSetVisibility(chkLicense,      isWpWelcome);
  ImgSetVisibility(imgBigIcon1,     isWpWelcome);
  ImgSetVisibility(imgOneKeySh,     isWpWelcome);
  //TconSetVisible(lblAgree,          isWpWelcome);
  TconSetVisible(lblWelcome,        isWpWelcome);
  //TconSetVisible(lblLicense,        isWpWelcome);
  
  ImgSetVisibility(imgBg1,   isWpWelcome or isWpSelectDir or isWpSelectWorkSpace);
  ImgSetVisibility(imgLogo1, isWpWelcome or isWpSelectDir or isWpSelectWorkSpace);
  
  TconSetVisible(lblTipWDir3,     isWpSelectDir);
  TconSetVisible(edtSelectDir1,   isWpSelectDir);
  TconSetVisible(lblQuickLaunch,  isWpSelectDir);
  TconSetVisible(lblNeedSpace,    isWpSelectDir);
  TconSetVisible(lblDiskSpace,    isWpSelectDir);
  TconSetVisible(lblTipWDir,      false);
  BtnSetVisibility(btnSelectDir1, isWpSelectDir);
  BtnSetVisibility(btnNext1,      isWpSelectDir);
  BtnSetVisibility(btnBack1,      isWpSelectDir);
  BtnSetVisibility(chkQuickLaunch,isWpSelectDir);

  TconSetVisible(lblTipWDir2,     isWpSelectWorkSpace);
  TconSetVisible(edtSelectDir2,   isWpSelectWorkSpace);
  BtnSetVisibility(btnNext2,      isWpSelectWorkSpace);
  BtnSetVisibility(btnBack2,      isWpSelectWorkSpace);

  for i := 1 to InsBgAni_ImgCount do
     ImgSetVisibility(InsBgAni_ImgArr[i-1], isWpInstalling);
  ImgSetVisibility(imgProgressBar,   isWpInstalling);
  ImgSetVisibility(imgProgressBarBg, isWpInstalling);
  TconSetVisible(lblTipProgress,     isWpInstalling);
  
  ImgSetVisibility(imgBg2,    isWpFinished);
  BtnSetVisibility(btnFinish, isWpFinished);

  //if CurPageID = wpWelcome then
  //begin
  //  Log('CurPageID = wpWelcome');
  //  PageState_Set(1);
  //end;
  
  if CurPageID = wpSelectDir then
  begin
	  edtSelectDir1.Text:=WizardForm.DirEdit.Text;
    WhenInstallDirChangeCheckDiskSpace();
    //PageState_Set(1);
  end;

  if isWpSelectWorkSpace then
  begin
	  edtSelectDir2.Text:=WorkSpacePath;
    //PageState_Set(1);
  end;

  if isWpInstalling then
  begin
    SetTaskValueByDesc('quicklaunchicon', isSelectedQuickLaunch);
 
    Notify_Init();
    //PageState_Set(2);
    InsBgAni_Time := 1;
  end
  else
  begin   
    InsBgAni_Time := 0;
  end;
  
  if isWpFinished then
  begin
    //Log('CurPageID = wpFinished');
    //PageState_Set(3);
    ReWriteAppConfig();
    ShellExec('taskbarpin', '{app}\{#MyAppExeName}', '', '', SW_SHOWNORMAL, ewNoWait, nErrCode);
    Notify_DoNotifyFinish();
  end;
 
  ImgApplyChanges(WizardForm.Handle);
end;

//调用窗口进程
function PBProc(h:hWnd;Msg,wParam,lParam:Longint):Longint;
var
  pr, pos,total: Longint;
  w : integer;
begin
  Result:=CallWindowProc(PBOldProc,h,Msg,wParam,lParam);
  if (Msg=$402) and (WizardForm.ProgressGauge.Position>WizardForm.ProgressGauge.Min) then
  begin
    pos:=WizardForm.ProgressGauge.Position-WizardForm.ProgressGauge.Min;
    total:=WizardForm.ProgressGauge.Max-WizardForm.ProgressGauge.Min;
    pr:=pos*100/total;
    PageInstall_SetProgress(pr);
    Notify_DoNotifyProgress(pos, total);
  end;
end;

//初始化向导窗体
procedure InitializeWizard();
var
  winW:integer;
  winH:integer;
  i:integer;
begin
  CurrentDPI  := WizardForm.Font.PixelsPerInch;
  DpiScalePctg  := 1000* CurrentDPI / 96;

  winW:=DpiScale(660)
  winH:=DpiScale(480)

  WizardForm.InnerNotebook.Hide;
  WizardForm.OuterNotebook.hide;
  WizardForm.BorderStyle:=bsNone;
  WizardForm.Position:=poDesktopCenter;
  
  WizardForm.Width:=winW;
  WizardForm.Height:=winH;
  WizardForm.Color:=clWhite ;

  WizardForm.Bevel.Hide;
  WizardForm.NextButton.Width:=0;
  WizardForm.BackButton.Width:=0;
  WizardForm.CancelButton.Width:=0;

  Misc_SetFormRoundRectRgn(WizardForm, 20);//圆角
  Misc_SetForm_Dragable(WizardForm);
  
  dNeedSpaceByte := {#MyAppNeedSpaceByte}

  //WizardForm.ComponentsDiskSpaceLabel.Left := 0;
  //WizardForm.ComponentsDiskSpaceLabel.Top := 100;
  //WizardForm.ComponentsList.Visible := True;
  //WizardForm.ComponentsDiskSpaceLabel.Visible := True;

  // 将tmp的资源， 解压到 安装运行时 搞出来的临时目录
  ExtractTemporaryFile('bg.png');
  ExtractTemporaryFile('bg2.png');
  ExtractTemporaryFile('btclose.png');
  ExtractTemporaryFile('btmin.png');
  ExtractTemporaryFile('btn.png');
  ExtractTemporaryFile('btnOneKeyInstall.png');
  ExtractTemporaryFile('check.png');
  ExtractTemporaryFile('progress.png');
  ExtractTemporaryFile('progressBg.png');

  for i := 1 to InsBgAni_ImgCount do
    ExtractTemporaryFile(Format('pic%d.png',[i]) );
	
  //加载安装背景图
  imgBg1 := ImgLoad(WizardForm.Handle,ExpandConstant('{tmp}\bg.png'),(0),(0),winW,winH,True,True);
  //加载完成背景图
  imgBg2 := ImgLoad(WizardForm.Handle,ExpandConstant('{tmp}\bg2.png'),(0),(0),winW,winH,True,True);
  ImgSetVisibility(imgBg2,false);

  for i := 1 to InsBgAni_ImgCount do
  begin
    InsBgAni_ImgArr[i-1] := ImgLoad( WizardForm.Handle, ExpandConstant(Format('{tmp}\pic%d.png',[i])),(0),(0), winW, winH,True,True);
    ImgSetVisibility(InsBgAni_ImgArr[i-1],false);
  end;
  
  //初始化关闭按钮并绑定相关事件
  btnClose:= BtnCreate(WizardForm.Handle, DpiScale(617), DpiScale(2), DpiScale(39),DpiScale(19), ExpandConstant('{tmp}\btclose.png'),1,False)
  BtnSetEvent(btnClose,BtnClickEventID,WrapBtnCallback(@BtnClose_OnClick,1));

  //初始化缩小按钮并绑定相关事件
  btnMin:=BtnCreate(WizardForm.Handle,DpiScale(585),DpiScale(2),DpiScale(39),DpiScale(19),ExpandConstant('{tmp}\btmin.png'),1,False)
  BtnSetEvent(btnMin,BtnClickEventID,WrapBtnCallback(@btnMin_OnClick,1));
  
  InitGui_PageWelcome();
  InitGui_PageSelectDir();
  InitGui_PageSelectWorkSpace();
  InitGui_PageInstall();
  InitGui_PageFinish();
  
  //PageState_Init();
  
  PBOldProc:=SetWindowLong(WizardForm.ProgressGauge.Handle,-4,PBCallBack(@PBProc,4));
  //应用图片更改
  ImgApplyChanges(WizardForm.Handle);
  //当前页面更改
  CurPageChanged(WizardForm.CurPageID);
end;

//是否应该跳过页面
function ShouldSkipPage(PageID: Integer): Boolean;
begin
 // wpWelcome, wpLicense, wpPassword, wpInfoBefore, wpUserInfo, wpSelectDir, wpSelectComponents, wpSelectProgramGroup, wpSelectTasks, wpReady, wpPreparing, wpInstalling, wpInfoAfter, wpFinished
  case PageID of
   wpWelcome:             result:=false; 
   wpLicense:             result:=true;  
   wpPassword:            result:=true;  
   wpInfoBefore:          result:=true;  
   wpUserInfo:            result:=true;  
   wpSelectDir:           result:=false; 
   wpSelectComponents:    result:=true;  
   wpSelectProgramGroup:  result:=true;  
   wpSelectTasks:         result:=false;  
   wpReady:               result:=true;  
   wpPreparing:           result:=true;  
   wpInstalling:          result:=false; 
   wpInfoAfter:           result:=true;  
   wpFinished:            result:=false; 
  else  result:=true;
  end;
end;

//取消初始化设置
procedure DeinitializeSetup();
begin
gdipShutdown;
if PBOldProc<>0 then SetWindowLong(WizardForm.ProgressGauge.Handle,-4,PBOldProc);
end;

