set ReferenceDir=%1
if "%ReferenceDir%"=="" set ReferenceDir=.\Reference

mkdir %ReferenceDir%

copy ..\Support\Feng\*.dll %ReferenceDir%
copy ..\Support\Feng\*.pdb %ReferenceDir%
copy ..\Support\Feng\*.exe %ReferenceDir%

copy Hd.NetRead.dll %ReferenceDir%

