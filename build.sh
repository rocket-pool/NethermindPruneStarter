#!/bin/bash

export CGO_ENABLED=0

# Build x64 version
GOARCH=amd64 GOOS=linux go build -o nm-prune-starter-linux-amd64 nm-prune-starter.go

# Build the arm64 version
GOARCH=arm64 GOOS=linux go build -o nm-prune-starter-linux-arm64 nm-prune-starter.go