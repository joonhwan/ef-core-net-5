@echo off 
rem dotnet-aspnet-codegenerator controller -name "CountriesController" --model "HR.Domain.Country" --dataContext "HR.DataAccess.HRContext" --useDefaultLayout --referenceScriptLibraries
dotnet-aspnet-codegenerator controller -f -api -m HR.Domain.Country -dc HR.DataAccess.HRContext -name "CountriesController"
dotnet-aspnet-codegenerator controller -f -api -m HR.Domain.Employee -dc HR.DataAccess.HRContext -name "EmployeesController"
dotnet-aspnet-codegenerator controller -f -api -m HR.Domain.EmpDetailsView -dc HR.DataAccess.HRContext -name "EmpDetailsViewController"
dotnet-aspnet-codegenerator controller -f -api -m HR.Domain.Job -dc HR.DataAccess.HRContext -name "JobsController"
dotnet-aspnet-codegenerator controller -f -api -m HR.Domain.Region -dc HR.DataAccess.HRContext -name "RegionsController"
dotnet-aspnet-codegenerator controller -f -api -m HR.Domain.Location -dc HR.DataAccess.HRContext -name "LocationsController"