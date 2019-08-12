To publish the to NuGet:

1. Review open issues and PRs to see if any can easily be fixed, closed, or
   merged.
2. Bump copyright year in `README.md` and the `AssemblyInfo.cs` files, if
   necessary.
3. Run `dotnet-outdated`, https://github.com/jerriep/dotnet-outdated, updating
   dependencies if appropriate.
4. Review `releasenotes.md` for completeness and correctness. Update its release
   date.
5. Run dev-bin/release.ps1. This will build the project, generate docs, upload to
   NuGet, and make a GitHub release.
6. Update GitHub Release page for the release.
7. Verify the release on [NuGet](https://www.nuget.org/packages/MaxMind.MaxMind/).
