FROM cimg/base:stable

# dotnet
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENV DOTNET_ROOT=/home/circleci/dotnet
RUN mkdir -p $DOTNET_ROOT; \
    curl -fsSL "https://dot.net/v1/dotnet-install.sh" | bash /dev/stdin --channel Current --install-dir $DOTNET_ROOT;
ENV PATH=$PATH:$DOTNET_ROOT:/home/circleci/.dotnet/tools
# dotnet tools
RUN dotnet tool install --global dotnet-script
# golang
ENV shell_profile="/home/circleci/.bashrc"
RUN wget -q -O - "https://raw.githubusercontent.com/canha/golang-tools-install-script/master/goinstall.sh" | bash
ENV GOROOT=/home/circleci/.go
ENV PATH=/home/circleci/.go/bin:/home/circleci/go/bin:$PATH
# golang tools
RUN go install github.com/jackyzha0/hugo-obsidian@latest
# hugo
RUN wget https://github.com/gohugoio/hugo/releases/download/v0.90.1/hugo_extended_0.90.1_Linux-64bit.deb \
    -O hugo.deb; \
    sudo dpkg -i hugo.deb; \
    rm hugo.deb
