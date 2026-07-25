{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };
  outputs = { self, nixpkgs }:
    let
      system = "x86_64-linux";
      pkgs = import nixpkgs { inherit system; };
      dotnetCombined = pkgs.dotnetCorePackages.combinePackages [
        pkgs.dotnetCorePackages.sdk_9_0
        pkgs.dotnetCorePackages.sdk_10_0
      ];
    in
    {
      devShells.${system}.default = pkgs.mkShell {
        packages = [
          dotnetCombined
          # For legacy .NET Framework 4.8 projects:
          # pkgs.mono
          # pkgs.msbuild
        ];

        DOTNET_ROOT = "${dotnetCombined}";
      };
    };
}
