mkdir .\Reference\LocalResource
del /Q .\Reference\LocalResource\* 

rem copy offsetdb
mkdir .\Reference\GMapCache
copy Zkzx.Run\OffsetDb.gmdb .\Reference\GMapCache\

rem Copy Script
cd Zkzx.Script
if exist pythonFiles.txt del pythonFiles.txt>nul
dir /s /b *.py > pythonFiles.txt
rem for /f %%i in (pythonFiles.txt ) do copy %%i ..\Reference\LocalResource
del pythonFiles.txt>nul
cd ..

copy .\Zkzx.Script\*.py .\Reference\LocalResource

rem Copy Assembly
cd Reference
copy hd.*.dll ..\Reference\LocalResource
cd..

rem Copy Report
rem cd Hd.Report
rem copy *.xsd ..\Reference\LocalResource
rem copy *.rpt ..\Reference\LocalResource
rem copy *.rdlc ..\Reference\LocalResource
rem cd..

rem Copy Config
cd Zkzx.Run
copy zkzx.model.config ..\Reference\LocalResource
copy sessionfactory.config ..\Reference\LocalResource
cd..

rem CopyConfigToRun
set ConfigDir=.\Zkzx.Run

copy %ConfigDir%\app.config .\Reference\Feng.Run.exe.config
rem copy %ConfigDir%\release.config .\Reference\Feng.Run.exe.config
copy %ConfigDir%\zkzx.model.config  .\Reference

copy %ConfigDir%\app.config .\Reference\ADInfosUtil.exe.config
copy %ConfigDir%\app.config .\Reference\ipy.exe.config
copy %ConfigDir%\app.config .\Reference\CredentialsManager.exe.config

copy %ConfigDir%\app.config .\Reference\neokernel.exe.config
copy %ConfigDir%\mime_types .\Reference\
copy %ConfigDir%\neokernel_props.xml .\Reference\
copy %ConfigDir%\clientaccesspolicy.xml .\Reference\

copy %ConfigDir%\app.config .\Reference\MapUtil.exe.config

copy %ConfigDir%\NingboImExport.gbfs .\Reference\NingboImExport.gbfs
copy %ConfigDir%\理论路线.* .\Reference\
copy %ConfigDir%\地点.* .\Reference\

.\Reference\ipy.exe .\encrypt_connectionstring.py

rem Upload Resource
.\Reference\ipy.exe .\upload_resource.py


IF ERRORLEVEL 1 pause