# SWA Code Rename - Workaround

A C# application showing a workaround for Azure SWA being unable to pass a 'code' parameter to Azure functions running on V4
See issue here: https://github.com/Azure/static-web-apps/issues/165

# Running the application
(Install SWA tools - https://azure.github.io/static-web-apps-cli/)

swa start --app-location src --api-location api --func-args --csharp
