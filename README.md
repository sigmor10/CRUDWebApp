# CRUDWebApp

## Overview

The **CRUDWebApp** is composed of 2 projects:

- `CRUDService`: based on ASP.NET Core which serves as backend application
- `FrontEnd`: based on Blazor WebAssembly (WASM) which serves as SPA frontend application.

To view full documentation open ./html/index.html fiel in a browser.

## Caution

For the whole app to work properly as is, you need **Postgres Server** running on **localhost** on port **5432**.

If your configuration **differs**, edit connection specifics in **appsettings.json** in **root directory of CRUDService** to match yours.

## Used Libraries

### Backend

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Metadata.Builders
- Microsoft.AspNetCore.Mvc
- Microsoft.AspNetCore.Authorization
- Microsoft.EntityFrameworkCore
- System.ComponentModel.DataAnnotations
- System.Security.Cryptography
- Microsoft.EntityFrameworkCore.Migrations
- Npgsql.EntityFrameworkCore.PostgreSQL.Metadata
- Microsoft.EntityFrameworkCore.Storage.ValueConversion
- Microsoft.EntityFrameworkCore.Infrastructure
- Microsoft.IdentityModel.Tokens
- System.IdentityModel.Tokens.Jwt
- System.Security.Claims

### Frontend

- System.ComponentModel.DataAnnotations
- System.Text.Json
- System.Net.Http.Headers
- System.Net.Http.Json
- Microsoft.AspNetCore.Components
- System.IdentityModel.Tokens.Jwt
- System.Security.Claims
- Blazored.LocalStorage
- Microsoft.AspNetCore.Components.Forms
- Microsoft.AspNetCore.Components.Routing
- Microsoft.AspNetCore.Components.Web
- Microsoft.AspNetCore.Components.Web.Virtualization
- Microsoft.AspNetCore.Components.WebAssembly.Http
- Microsoft.JSInterop
- Microsoft.AspNetCore.Components.WebAssembly.Hosting

## How to compile

- Download code from github: https://github.com/sigmor10/CRUDWebApp
- Open Console / PowerShell in CRUDService project's root directory
- Run the "dotnet ef database update" command to run migrations, or you can load sql dump
- Open the main folder and run .sln file in Visual Studio 2022
- Run both projects CRUDService and FrontEnd in any order
    - Note 1: If FrontEnd is run before CRUDService, requests to backend will fail.
    - Note 2: On first run of CRUDService you will be prompted with whether to trust ASP.NET self signed Certificate. Agree to it so that the app will run with REST API communication over https