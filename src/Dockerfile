FROM osexp2000/ubuntu-with-utils
# dotnet
RUN mkdir -p /home/devuser/dotnet && curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current --install-dir /home/devuser/dotnet
RUN export PATH=$PATH:/home/devuser/dotnet
ENV DOTNET_ROOT=/home/devuser/dotnet
ENV PATH=$PATH:/home/devuser/dotnet
ENV PATH=$PATH:/home/devuser/.dotnet/tools
# golang
RUN wget -q -O - https://git.io/vQhTU | bash
ENV GOROOT=/home/devuser/.go
ENV PATH=$GOROOT/bin:$PATH
# hugo
RUN wget https://github.com/gohugoio/hugo/releases/download/v0.90.1/hugo_extended_0.90.1_Linux-64bit.deb \
    -O hugo.deb; \
    sudo dpkg -i hugo.deb; \
    rm hugo.deb
# extras
RUN dotnet tool install --global dotnet-script
RUN go install github.com/jackyzha0/hugo-obsidian@latest
