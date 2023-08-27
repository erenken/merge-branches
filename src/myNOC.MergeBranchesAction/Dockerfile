#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/mynoc.MergeBranchesAction/myNOC.MergeBranchesAction.csproj", "src/mynoc.MergeBranchesAction/"]
RUN dotnet restore "src/mynoc.MergeBranchesAction/myNOC.MergeBranchesAction.csproj"
COPY . .
WORKDIR "/src/src/mynoc.MergeBranchesAction"
RUN dotnet build "myNOC.MergeBranchesAction.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "myNOC.MergeBranchesAction.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Label the container
LABEL maintainer="Eric Renken <eric@mynoc.com>"
LABEL repository="https://github.com/erenken/mergeBranches"
LABEL homepage="https://github.com/erenken/mergeBranches"

# Label as GitHub action
LABEL com.github.actions.name="Wildcard Branch Merge"
LABEL com.github.actions.description="A Github action that merges a source branch to many destination branches."
LABEL com.github.actions.icon="sliders"
LABEL com.github.actions.color="blue"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myNOC.MergeBranchesAction.dll"]
