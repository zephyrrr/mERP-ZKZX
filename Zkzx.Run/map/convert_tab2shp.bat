rmdir shp
set ConfigDir=.\tab

if exist mapfiles.txt del mapfiles.txt>nul
dir /s /b %ConfigDir%\*.tab > mapfiles.txt
for /f %%i in (mapfiles.txt ) do "D:\Program Files\FWTools2.4.7\bin\ogr2ogr.exe" -f "ESRI Shapefile" .\shp %%i
del mapfiles.txt>nul