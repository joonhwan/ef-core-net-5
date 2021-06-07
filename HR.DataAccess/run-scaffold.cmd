@echo off
dotnet ef dbcontext scaffold "User Id=HR;Password=HR;Data Source=localhost:1521/XE" "Oracle.EntityFrameworkCore" -c HRContext --context-dir . -o ../HR.Domain --namespace "HR.Domain"