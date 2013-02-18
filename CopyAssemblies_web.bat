call CopyAssemblies.bat .\Zkzx.Silverlight.Web\bin
call CopyAssemblies.bat .\Reference

call CopyAssembliesFeng.bat .\Zkzx.Silverlight.Web\bin
call CopyAssembliesFeng.bat .\Reference

call CopyAssemblies_Silverlight .\Zkzx.Silverlight\bin

copy ..\Feng\Feng.Security.WebService\WebLogin.* .\Zkzx.WS\
copy ..\Feng\Feng.Security.WebService\Login.* .\Zkzx.WS\
copy ..\Feng\Feng.Security.WebService\Membership\* .\Zkzx.WS\Membership\
copy ..\Feng\Feng.Security.WebService\App_Code\* .\Zkzx.WS\App_Code\
