FROM gitpod/workspace-dotnet
RUN dotnet tool install --global dotnet-script
ENV PATH="$PATH:$HOME/.dotnet/tools"
