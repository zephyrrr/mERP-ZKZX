set ReferenceDir=%1
if "%ReferenceDir%"=="" set ReferenceDir=.\Reference

mkdir %ReferenceDir%

copy ..\Application\Hd\Hd.NetRead.dll %ReferenceDir%

