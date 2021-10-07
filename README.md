# SearchFight :crossed_swords:

Query search engines and compares how many results they return for a list of terms you specify.

## Prerequisites

You need to have .NET Core installed.

## How to run

1. Open your terminal and run this command passing the terms you want to search as arguments separated by spaces:

```bash
dotnet run --project SearchFight.Client -- [your-terms]
```

For example:

```bash
dotnet run -- java .net
```

Use quotation marks to search for terms with spaces.

```bash
dotnet run -- java .net "C Shell"
```

## How to test

Just execute:

```bash
dotnet test
```

## Supported engines

-   Google
-   Bing

## Technologies used

-   .NET Core 3.1
-   .NET Standard 2.1
-   xUnit
-   Visual Studio for Mac

### To-do list

-   [ ] Use a secure key storage (e.g. Azure Key Vault).
-   [ ] Implement a better logger
