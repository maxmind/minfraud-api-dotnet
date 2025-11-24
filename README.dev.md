To publish the to NuGet:

1. Review open issues and PRs to see if any can easily be fixed, closed, or
   merged. Pay special attention to Dependabot PRs for dependency updates.
2. Bump copyright year in `README.md` and the `AssemblyInfo.cs` files, if
   necessary.
3. Review `releasenotes.md` for completeness and correctness. Update its release
   date.
4. Run dev-bin/release.sh. This will build the project, create a version commit,
   and make a GitHub release.
5. Approve the workflow run in the GitHub Actions UI.
6. Verify the release on [NuGet](https://www.nuget.org/packages/MaxMind.MinFraud/).
