# flake.nix example
{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };
  outputs = { self, nixpkgs }:
    let
      system = "x86_64-linux";
      pkgs = import nixpkgs { inherit system; };
    in
    {
      devShells.${system}.default = pkgs.mkShell {
        packages = with pkgs; [
          dotnetCorePackages.sdk_9_0
          # For legacy .NET Framework 4.8 projects:
          # mono
          # msbuild
        ];
      };
    };
}
