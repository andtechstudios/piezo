FROM gitpod/workspace-dotnet
RUN wget https://github.com/gohugoio/hugo/releases/download/v0.90.1/hugo_extended_0.90.1_Linux-64bit.deb \
    -O hugo.deb; \
    sudo dpkg -i hugo.deb; \
    rm hugo.deb
RUN dotnet tool install --global dotnet-script
USER root
RUN go install github.com/jackyzha0/hugo-obsidian@latest
