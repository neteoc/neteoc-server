#/usr/bin/env bash


set -e

rm -f public/*

cd ../client/

webpack

cd ../server

mocha

git add .
git commit -m "Deploy Script"
git push