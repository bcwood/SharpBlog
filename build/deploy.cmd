@echo off
echo Deploying content...
xcopy %DEPLOYMENT_SOURCE%\SharpBlog\Content %DEPLOYMENT_TARGET%\Content /Y
