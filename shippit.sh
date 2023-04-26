dotnet publish ./src/WontingOfSplorr/WontingOfSplorr.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./src/WontingOfSplorr/WontingOfSplorr.vbproj -o ./pub-windows -c Release --sc -r win-x64
dotnet publish ./src/WontingOfSplorr/WontingOfSplorr.vbproj -o ./pub-mac -c Release --sc -r osx-x64
butler push pub-windows thegrumpygamedev/wonting-of-splorr:windows
butler push pub-linux thegrumpygamedev/wonting-of-splorr:linux
butler push pub-mac thegrumpygamedev/wonting-of-splorr:mac
git add -A
git commit -m "shipped it!"