dotnet "$(SolutionDir)tools\Epic.Solutions.BuildProcessor.dll" "$(TargetPath)"
mv "$(TargetDir)Epic.Framework.Standard.dll.mod" "$(TargetDir)Epic.Framework.Standard.dll"
copy /y  "$(TargetDir)" "$(SolutionDir)tools\"

