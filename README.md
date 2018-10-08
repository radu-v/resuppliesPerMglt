# ResuppliesPerMglt

ResuppliesPerMglt is a tool that calculates the number of re-supply stops a Star Wars starship would need to travel a specific distance.

The starship data is fetched from the public http://swapi.co API.

## Usage

```
resuppliesPerMglt [options]

Options:
	-?, -h, --help		Show help
	-d, --distance		(optional, number) The distance the starship must travel.
	-i, --starship-id	(optional, number) The ID of the starship, in case calculation for a single starship is needed
```

## Output

The output is a list of starship names with their number of stops (or a message explaining the reason it hasn't been calculated), separated by a column.

## Examples

> resuppliesPerMglt -d 1000000

```
Executor: 0
Sentinel-class landing craft: 19
Death Star: 3
Millennium Falcon: 9
Y-wing: 74
X-wing: 59
...
Republic Cruiser: Unknown Megalights per Hour
...
```
> dotnet .\ResuppliesPerMglt.dll --distance=1000000 --starship-id=10

```
Millennium Falcon: 9
```

## Building

The app uses .NET Core 2.1, so it's cross-platform. Please refer to Microsoft's [web page](https://www.microsoft.com/net/download) for .NET Core installation instructions.

> In the solution folder:

```
dotnet build resuppliesPerMglt.sln -c "Release"
```

The following command will produce an .exe file under Windows:
```
dotnet build resuppliesPerMglt.sln -c "Release" -r "win81-x64"
```

The following command will produce an .exe file under Linux:
```
dotnet build resuppliesPerMglt.sln  -c "Release" -r "ubuntu.18.04-x64"
```

## Executing

Cross-platform:

> In the output folder, e.g. resuppliesPerMglt/bin/Release/netcoreapp2.1/:

```
dotnet ResuppliesPerMglt.dll --distance=12345678
```

When built as executable:

> In the output folder, e.g. resuppliesPerMglt/bin/Release/netcoreapp2.1/win81-x64/:

```
ResuppliesPerMglt --distance=12345678
```

## Running unit-tests

> In the solution folder:

```
dotnet test resuppliesPerMglt_Tests
```
