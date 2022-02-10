# -*- mode: python ; coding: utf-8 -*-

import glob
import datetime
from os import path
block_cipher = None

C2F_LIST = [(c2f, 'c2f') for c2f in glob.glob(r'..\..\..\..\work\C2F\*.c2')]

a = Analysis(['C2F.py'],
             pathex=['work\\C2\\tools\\C2F'],
             binaries=[],
             datas= C2F_LIST,
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
          uac_admin=True,                  # 管理员权限
          disable_windowed_traceback=False,
          target_arch=None,
          codesign_identity=None,
          entitlements_file=None )
