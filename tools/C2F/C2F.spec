# -*- mode: python ; coding: utf-8 -*-

import datetime
from os import path
block_cipher = None


a = Analysis(['C2F.py'],
             pathex=['work\\C2\\tools\\C2F'],
             binaries=[],
             datas=[(os.path.abspath(r'..\..\..\..\work\C2F\apk模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\ddos模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\侵公模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\第四方支付侦察报告模板.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\xss模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\四方模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\宝塔面板.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\机场模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\涉枪胶水系统.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\涉赌模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\涉黄胶水系统.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\盗洞模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\秒播vps.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\购置境外网络资产模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\黑吃黑模型.c2'),'c2f'),
                    (os.path.abspath(r'..\..\..\..\work\C2F\黑客模型.c2'),'c2f'),
					(os.path.abspath(r'..\..\..\..\work\C2F\大马模型.c2'),'c2f'),
					(os.path.abspath(r'..\..\..\..\work\C2F\网赌受害者模型.c2'),'c2f')],
             hiddenimports=[],
             hookspath=[],
             hooksconfig={},
             runtime_hooks=[],
             excludes=[],
             win_no_prefer_redirects=False,
             win_private_assemblies=False,
             cipher=block_cipher,
             noarchive=False)
pyz = PYZ(a.pure, a.zipped_data,
             cipher=block_cipher)

exe = EXE(pyz,
          a.scripts,
          a.binaries,
          a.zipfiles,
          a.datas,  
          [],
          name='战术手册' + '_' + datetime.datetime.now().strftime("%m%d"),
          icon='logo.ico',
          debug=False,
          bootloader_ignore_signals=False,
          strip=False,
          upx=True,
          upx_exclude=[],
          runtime_tmpdir=None,
          console=True,
          disable_windowed_traceback=False,
          target_arch=None,
          codesign_identity=None,
          entitlements_file=None )
